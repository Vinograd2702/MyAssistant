using auth_servise.Core.Application.Commands.QueueTaskStatuses.SetQueueTaskStatus;
using auth_servise.Core.Application.Interfaces.RabbitMq;
using auth_servise.Infrastructure.Options;
using auth_servise.Infrastructure.UsedServices.Messages.ToAuthService;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace auth_servise.Infrastructure.RabbitMq
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

        public async Task SendMessage(object obj, string service = "all")
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var message = JsonSerializer.Serialize(obj, options);
            await SendMessage(message, service);
        }

        public async Task SendMessage(string message, string recipientService)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _queueForExternalInteractionServices.TryGetValue(recipientService, out var queueName);


            await _channelToSending!.BasicPublishAsync("", queueName ?? string.Empty, body, CancellationToken.None);
        }

        private async Task ResiveMessage(object? ch, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();

            JsonMessageTaskReport? message;

            var isCorrectMessage = DeserializeMessage(body, out message);

            
            if(isCorrectMessage == false || message == null)
            {
                _logger.LogWarning("Queue has unknown message type.");
            }

            else
            {
                _logger.LogInformation("Process Json Message Task Report: {message}", message.Description);

                var command = new SetQueueTaskStatusCommand
                {
                    TaskId = message.TaskId,
                    СonsumerService = _serviceEnvironmentOptions.CurrentService,
                    Decription = message.Description
                };

                switch (message.Status)
                {
                    case "Success":
                        command.Status = Core.Domain.StatusOfTask.Success; 
                        break;
                    case "Failure":
                        command.Status = Core.Domain.StatusOfTask.Failure;
                        break;
                    default:
                        _logger.LogWarning("Queue has unknown message type.");
                        command.Status = Core.Domain.StatusOfTask.Error;
                        break;
                }

                using (var scope = _appServiceProvider.CreateScope())
                {
                    var serviceProvaider = scope.ServiceProvider;

                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator!.Send(command);
                }

                await _channelToReceiving!.BasicAckAsync(ea.DeliveryTag, false);
            }
        }

        private bool DeserializeMessage(byte[] body, out JsonMessageTaskReport? message)
        {
            var  jsonElement = new JsonElement();
            message = null;
            var isKnownMessageType = false;
            try
            {
                jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return isKnownMessageType;
            }

            if (jsonElement.TryGetProperty("Header", out var messageHeder))
            {
                switch (messageHeder.GetString())
                {
                    case "CheckEmailNotificationReport":
                        message = JsonSerializer.Deserialize<JsonMessageTaskReport>(body);
                        isKnownMessageType = true;
                        break;

                    case "EmailInfoToNotificationServiceReport":
                        message = JsonSerializer.Deserialize<JsonMessageTaskReport>(body);
                        isKnownMessageType = true;
                        break;

                    case "NotificationSettingsForUserReport":
                        message = JsonSerializer.Deserialize<JsonMessageTaskReport>(body);
                        isKnownMessageType = true;
                        break;

                    default:
                        break;
                }
            }

            return isKnownMessageType;
        }
    }
}