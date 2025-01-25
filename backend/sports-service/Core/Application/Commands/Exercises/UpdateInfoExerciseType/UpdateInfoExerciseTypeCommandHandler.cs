using MediatR;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.UpdateInfoExerciseType
{
    public class UpdateInfoExerciseTypeCommandHandler 
        : IRequestHandler<UpdateInfoExerciseTypeCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateInfoExerciseTypeCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateInfoExerciseTypeCommand request, CancellationToken cancellationToken)
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

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameExerciseType = _sportServiseDbContext.ExerciseTypes.FirstOrDefault(t => t.Name == request.Name);

            if (understudyNameExerciseType != null && understudyNameExerciseType.Id != request.Id && understudyNameExerciseType.IsDeleted != true)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(ExerciseType), request.UserId);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
