using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.NotificationService;
using auth_servise.Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.Users.UpdateNotificationUserSettings
{
    public class UpdateNotificationUserSettingsCommandHandler
        : IRequestHandler<UpdateNotificationUserSettingsCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly IManageNotificationUserSettings _manageNotificationUserSettings;

        public UpdateNotificationUserSettingsCommandHandler(IAuthServiseDbContext authServiseDbContext, IManageNotificationUserSettings manageNotificationUserSettings)
        {
            _authServiseDbContext = authServiseDbContext;
            _manageNotificationUserSettings = manageNotificationUserSettings;
        }

        public async Task Handle(UpdateNotificationUserSettingsCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != request.ClientUserId)
            {
                throw new UnauthorizedAccessException();
            }

            var taskId = await _manageNotificationUserSettings
                .UpdateNotificationSettingsForUser(request.Id, 
                request.IsUseEmail, request.IsUsePush);

            var task = await _authServiseDbContext
                .QueueTasksStatus.AsNoTracking()
                .FirstAsync(t => t.Id == taskId);

            var attempt = 0;

            while ((task.Status == "NotSent" || task.Status == "WaitingResponse")
                && attempt < 10)
            {
                await Task.Delay(3000);
                attempt++;
                task = _authServiseDbContext.QueueTasksStatus.AsNoTracking()
                    .First(t => t.Id == taskId);
            }

            if (task.Status == "NotSent")
            {
                var exceptionMessage = "The task -> " + task.Id
                    + " was not sent to queue.";
                throw new Exception(exceptionMessage);
            }

            if (task.Status == "Error" || task.Status == "Failure")
            {
                var exceptionMessage = "The task -> " + task.Id
                    + " was completed with an error."
                    + " Error of task -> " + task.Decription + ".";
                throw new NotificationServiseErrorException(exceptionMessage);
            }

            if (task.Status == "WaitingResponse")
            {
                var exceptionMessage = "The task -> " + task.Id
                    + " was sent to the queue, but the NotificationServise has not yet had time to complete its processing."
                    + " Last Status of task -> " + task.Status + ".";
                throw new TimeoutException(exceptionMessage);
            }
        }
    }
}
