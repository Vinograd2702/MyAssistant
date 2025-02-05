using MediatR;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.DeleteExercisesGroup
{
    public class DeleteExercisesGroupCommandHandler
        : IRequestHandler<DeleteExercisesGroupCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public DeleteExercisesGroupCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(DeleteExercisesGroupCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = _sportServiseDbContext.ExerciseGroups.FirstOrDefault(g => g.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseGroup), request.Id);
            }

            if (entity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            var childGroupExersiseType = _sportServiseDbContext.ExerciseTypes
                .Where(t => t.ExerciseGroupId == request.Id
                && t.IsDeleted != true).ToList();

            if (childGroupExersiseType.Any())
            {
                throw new EntityHasChildEntityException(entity.Name, nameof(ExerciseGroup), nameof(ExerciseType));
            }

            var childGroupExersiseGroup = _sportServiseDbContext.ExerciseGroups
                .Where(t => t.ParentGroupId == request.Id
                && t.IsDeleted != true).ToList();

            if (childGroupExersiseGroup.Any())
            {
                throw new EntityHasChildEntityException(entity.Name, nameof(ExerciseGroup), nameof(ExerciseGroup));
            }

            entity.IsDeleted = true;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
