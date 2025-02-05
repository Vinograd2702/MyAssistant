using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.UpdateNameExercisesGroup
{
    public class UpdateNameExercisesGroupCommandHandler 
        : IRequestHandler<UpdateNameExercisesGroupCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateNameExercisesGroupCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateNameExercisesGroupCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.ExerciseGroups
                .FirstOrDefaultAsync(g => g.Id == request.Id 
                && g.IsDeleted == false);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseGroup), request.Id);
            }

            if (entity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameGroup = await _sportServiseDbContext.ExerciseGroups
                .FirstOrDefaultAsync(g => g.UserId == request.UserId && g.Name == request.Name
                && g.IsDeleted == false && g.Id != request.Id);

            if (understudyNameGroup != null)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(ExerciseGroup), request.UserId);
            }

            entity.Name = request.Name;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
