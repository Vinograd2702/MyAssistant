using auth_servise.Core.Application.Commands.QueueTaskStatuses.SetQueueTaskStatus;
using auth_servise.Core.Application.Interfaces.RabbitMq;
using auth_servise.Core.Domain;
using auth_servise.Infrastructure.UsedServices.Messages;
using MediatR;

namespace auth_servise.Infrastructure.UsedServices.Connectors
{
    public class SportServiceConnector
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IServiceProvider _appServiceProvider;
        private readonly string _serviseNameCurrent = "Auth";
        private readonly string _serviseNameToConnect = "Sports";

        public SportServiceConnector(IRabbitMqService rabbitMqService, 
            IServiceProvider appServiceProvider)
        {
            _rabbitMqService = rabbitMqService;
            _appServiceProvider = appServiceProvider;
        }

        private async Task SendTask(Guid taskId, BaseJsonMessage jsonMessage)
        {
            using (var scope = _appServiceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();

                var commandStatysTask = new SetQueueTaskStatusCommand
                {
                    TaskId = taskId,
                    //TaskName
                    Status = StatusOfTask.NotSent,
                    ProducerService = _serviseNameCurrent,
                    СonsumerService = _serviseNameToConnect
                };

                await mediator.Send(commandStatysTask);

                await _rabbitMqService.SendMessage(jsonMessage, _serviseNameToConnect);

                commandStatysTask = new SetQueueTaskStatusCommand
                {
                    TaskId = taskId,
                    Status = StatusOfTask.WaitingResponse,
                    ProducerService = _serviseNameCurrent,
                    СonsumerService = _serviseNameToConnect
                };

                await mediator.Send(commandStatysTask);
            }
        }
    }
}
