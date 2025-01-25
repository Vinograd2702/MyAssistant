using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sports_service.Core.Application.Commands.Workouts.DeleteWorkout;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.DeleteWorkoutsList
{
    public class DeleteWorkoutsListCommandHandler
        : IRequestHandler<DeleteWorkoutsListCommand, int>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public DeleteWorkoutsListCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<int> Handle(DeleteWorkoutsListCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var deletedEntitysList = new List<Workout>();

            foreach (var entityWorkoutId in request.ListId)
            {
                var entityWorkout = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(w => w.UserId == request.UserId
                && w.Id == entityWorkoutId, cancellationToken);

                if (entityWorkout != null)
                {
                    deletedEntitysList.Add(entityWorkout);
                }
            }

            _sportServiseDbContext.Workouts.RemoveRange(deletedEntitysList);

            return deletedEntitysList.Count;
        }
    }
}
