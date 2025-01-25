using MediatR;
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

            var entity = _sportServiseDbContext.ExerciseTypes.FirstOrDefault(t => t.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseType), request.Id);
            }

            if (entity.UserId == request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            entity.ExerciseGroupId = request.ExerciseGroupId;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
