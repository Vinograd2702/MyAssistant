using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutDate
{
    public class UpdateWorkoutDateCommandHandler : IRequestHandler<UpdateWorkoutDateCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateWorkoutDateCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateWorkoutDateCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityWorkout = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (entityWorkout == null)
            {
                throw new NotFoundEntityException(nameof(Workout), request.Id);
            }

            if (entityWorkout.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (entityWorkout.IsCompleted)
            {
                throw new CompletedWorkoutException(entityWorkout.Id, nameof(UpdateWorkoutDateCommand));
            }

            entityWorkout.DateOfWorkout = request.DateOfWorkout;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
