using MediatR;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesGroup
{
    public class UpdateParentExercisesGroupCommandHandler
        : IRequestHandler<UpdateParentExercisesGroupCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateParentExercisesGroupCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateParentExercisesGroupCommand request,
            CancellationToken cancellationToken)
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

            if (entity.UserId == request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            entity.ParentGroupId = request.ParentGroupId;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
