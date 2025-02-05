using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesType
{
    public class UpdateParentExercisesTypeCommandHandler
        : IRequestHandler<UpdateParentExercisesTypeCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateParentExercisesTypeCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateParentExercisesTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.ExerciseTypes
                .FirstOrDefaultAsync(t => t.Id == request.Id 
                && t.IsDeleted == false);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseType), request.Id);
            }

            if (entity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            var newGroupEntity = await _sportServiseDbContext.ExerciseGroups
                .FirstOrDefaultAsync(g => g.Id == request.ExerciseGroupId && g.IsDeleted == false);

            if (newGroupEntity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseGroup), request.ExerciseGroupId);
            }

            if (newGroupEntity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            entity.ExerciseGroup = newGroupEntity;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
