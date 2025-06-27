using notification_service.Infrastructure.Options;
using Microsoft.Extensions.Options;
using notification_service.Core.Application.Interfaces.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_service.Infrastructure.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ServiceEnvironmentOptions _serviceEnvironmentOptions;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection? _connection;

        private IChannel? _channelToReceiving;
        private string _queueForCurrentService;

        private AsyncEventingBasicConsumer? consumer;

        private IChannel? _channelToSending;
        private Dictionary<string, string> _queueForExternalInteractionServices;

        private readonly IServiceProvider _appServiceProvider;

        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(IOptions<ServiceEnvironmentOptions> serviceEnvironmentOptions,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IServiceProvider appServiceProvider,
            ILogger<RabbitMqService> logger)
        {
            _appServiceProvider = appServiceProvider;

            _serviceEnvironmentOptions = serviceEnvironmentOptions.Value;
            _rabbitMqOptions = rabbitMqOptions.Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMqOptions.HostName,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.UserPassword
            };

            _queueForCurrentService = _serviceEnvironmentOptions.CurrentService + "Queue";

            _queueForExternalInteractionServices = new Dictionary<string, string>();

            foreach (var serviceQueue in _serviceEnvironmentOptions.ExternalInteractionServices)
            {
                _queueForExternalInteractionServices.Add(serviceQueue, serviceQueue + "Queue");
            }

            _logger = logger;
        }

        public async Task CreateConnection()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();

            _channelToReceiving = await _connection.CreateChannelAsync();

            await _channelToReceiving.QueueDeclareAsync(
               queue: _queueForCurrentService,
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null);

            consumer = new AsyncEventingBasicConsumer(_channelToReceiving);

            consumer.ReceivedAsync += ResiveMessage;

            await _channelToReceiving.BasicConsumeAsync(_queueForCurrentService, false, consumer: consumer);

            _channelToSending = await _connection.CreateChannelAsync();

            foreach (var serviceQueue in _queueForExternalInteractionServices)
            {

                await _channelToSending.QueueDeclareAsync(
                    queue: serviceQueue.Value,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
        }

        public Task SendMessage(object obj, string service = "all")
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(string message, string recipientService)
        {
            throw new NotImplementedException();
        }
        // обработчик получения сообщения из очереди
        private async Task ResiveMessage(object? ch, BasicDeliverEventArgs ea)
        {

            var body  = ea.Body.ToArray();
            using (var scope = _appServiceProvider.CreateScope())
            {
                var messageResiver = scope.ServiceProvider.GetService<IResiveMessageService>();

                var isResivedMessage = await messageResiver.ResiveMessage(body);

                await _channelToReceiving.BasicAckAsync(ea.DeliveryTag, false);
            }
        }
    }
}
