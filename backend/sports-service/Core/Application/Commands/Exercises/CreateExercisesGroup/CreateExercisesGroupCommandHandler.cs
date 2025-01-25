using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.CreateExercisesGroup
{
    public class CreateExercisesGroupCommandHandler 
        : IRequestHandler<CreateExercisesGroupCommand, Guid>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public CreateExercisesGroupCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<Guid> Handle(CreateExercisesGroupCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameGroup = await _sportServiseDbContext.ExerciseGroups
                .FirstOrDefaultAsync(group => group.UserId == request.UserId
                && group.Name == request.Name);

            if (understudyNameGroup != null && understudyNameGroup.IsDeleted != true)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(ExerciseGroup), request.UserId);
            }

            var entity = new ExerciseGroup
            {
                UserId = request.UserId,
                ParentGroupId = request.ParentGroupId,
                Name = request.Name
            };

            _sportServiseDbContext.ExerciseGroups.Add(entity);
            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
