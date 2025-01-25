using MediatR;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.DeleteExerciseType
{
    public class DeleteExerciseTypeCommandHandler
        : IRequestHandler<DeleteExerciseTypeCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public DeleteExerciseTypeCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(DeleteExerciseTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = _sportServiseDbContext.ExerciseTypes.FirstOrDefault(t => t.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseGroup), request.Id);
            }

            if (entity.UserId == request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            //! Удалить связанные шаблоны!

            _sportServiseDbContext.ExerciseTypes.Remove(entity);

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
