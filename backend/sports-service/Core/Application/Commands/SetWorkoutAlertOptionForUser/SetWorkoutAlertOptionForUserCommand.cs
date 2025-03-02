using MediatR;

namespace sports_service.Core.Application.Commands.SetWorkoutAlertOptionForUser
{
    public class SetWorkoutAlertOptionForUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int? AforehandHourBeforeWorkout { get; set; }
    }
}
