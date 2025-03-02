
using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.WorkoutNotificationSettings;
using sports_service.Infrastructure.Persistence;

namespace sports_service.Core.Application.Commands.SetWorkoutAlertOptionForUser
{
    public class SetWorkoutAlertOptionForUserCommandHandler
        : IRequestHandler<SetWorkoutAlertOptionForUserCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;
        public SetWorkoutAlertOptionForUserCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(SetWorkoutAlertOptionForUserCommand request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var workoutEntity = await _sportServiseDbContext.WorkoutNotificationSettings
                .FirstOrDefaultAsync(w => w.UserId == request.UserId);

            if (workoutEntity == null)
            {
                workoutEntity = new WorkoutNotificationSetting
                {
                    UserId = request.UserId,
                };
            }

            if (request.AforehandHourBeforeWorkout == null)
            {
                workoutEntity.AforehandTimeBeforeWorkout = null;
            }
            else
            {
                workoutEntity.AforehandTimeBeforeWorkout = TimeSpan.FromHours(Convert.ToDouble(request.AforehandHourBeforeWorkout));
            }

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);    
        }
    }
}
