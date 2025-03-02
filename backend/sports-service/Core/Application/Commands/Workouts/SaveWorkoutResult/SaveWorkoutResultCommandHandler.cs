using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.SaveWorkoutResult
{
    public class SaveWorkoutResultCommandHandler
        : IRequestHandler<SaveWorkoutResultCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public SaveWorkoutResultCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(SaveWorkoutResultCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.WorkoutResultsDTO.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.WorkoutResultsDTO.Id));
            }

            var workoutEntity = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(w => w.Id == request.WorkoutResultsDTO.Id);

            if (workoutEntity == null)
            {
                throw new NotFoundEntityException(nameof(Workout),
                    request.WorkoutResultsDTO.Id);
            }

            if (workoutEntity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (workoutEntity.IsCompleted == true)
            {
                throw new CompletedWorkoutException(workoutEntity.Id, nameof(SaveWorkoutResultCommand));
            }

            workoutEntity.SaveWorkoutResult(request.WorkoutResultsDTO);

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
