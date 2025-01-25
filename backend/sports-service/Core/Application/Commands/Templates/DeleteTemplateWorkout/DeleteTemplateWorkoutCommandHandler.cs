using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;

namespace sports_service.Core.Application.Commands.Templates.DeleteTemplateWorkout
{
    public class DeleteTemplateWorkoutCommandHandler
        : IRequestHandler<DeleteTemplateWorkoutCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public DeleteTemplateWorkoutCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(DeleteTemplateWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityTemplateWorkout = await _sportServiseDbContext.TemplateWorkouts
                .FirstOrDefaultAsync(tw => tw.Id == request.Id, cancellationToken);

            if (entityTemplateWorkout != null)
            {
                throw new NotFoundEntityException(nameof(TemplateWorkout), request.Id);
            }

            if (entityTemplateWorkout!.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            // ! проверить каскадное удаление, не удалаять уже созданные тренировки
            _sportServiseDbContext.TemplateWorkouts.Remove(entityTemplateWorkout);
            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
