using auth_servise.Core.Application.Interfaces.RabbitMq;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace auth_servise.Infrastructure.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqOptions _options;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        public RabbitMqService(IOptions<RabbitMqOptions> options) 
        {
            _options = options.Value;

            _connectionFactory = new ConnectionFactory() 
            { 
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.UserPassword 
            };
        }

        public async Task CreateConnection()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(
                queue: _options.NotificatorQueue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public async Task SendMessage(object obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var message = JsonSerializer.Serialize(obj, options);
            await SendMessage(message);
        }

        public async Task SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync("", _options.NotificatorQueue, body, CancellationToken.None);
        }

        public async Task WaitMessage(Guid TaskId /*из какой очререди ждать параметры*/)
        {

        }
    }
}
