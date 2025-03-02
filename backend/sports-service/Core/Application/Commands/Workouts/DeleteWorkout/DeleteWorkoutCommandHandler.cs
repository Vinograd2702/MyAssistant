using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.DeleteWorkout
{
    public class DeleteWorkoutCommandHandler
        : IRequestHandler<DeleteWorkoutCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public DeleteWorkoutCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
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

            if (entityWorkout!.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            _sportServiseDbContext.Workouts.Remove(entityWorkout);
            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
