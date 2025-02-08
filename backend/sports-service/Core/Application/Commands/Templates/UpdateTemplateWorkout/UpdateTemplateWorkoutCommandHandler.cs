using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;

namespace sports_service.Core.Application.Commands.Templates.UpdateTemplateWorkout
{
    public class UpdateTemplateWorkoutCommandHandler
        : IRequestHandler<UpdateTemplateWorkoutCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateTemplateWorkoutCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateTemplateWorkoutCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityTemplateWorkout = await _sportServiseDbContext.TemplateWorkouts.
                FirstOrDefaultAsync(TemplateWorkout => TemplateWorkout.Id == request.Id, cancellationToken);

            if (entityTemplateWorkout == null)
            {
                throw new NotFoundEntityException(nameof(TemplateWorkout), request.Id);
            }

            if (entityTemplateWorkout.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameTemplate = await _sportServiseDbContext.TemplateWorkouts
                .FirstOrDefaultAsync(template => template.Name == request.Name, cancellationToken);

            if (understudyNameTemplate != null && understudyNameTemplate.Id != request.Id)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name,
                    nameof(TemplateWorkout), request.UserId);
            }

            // ! Протестировать каскадное удаление
            entityTemplateWorkout.Name = request.Name;
            entityTemplateWorkout.Description = request.Description;

            _sportServiseDbContext.TemplatesBlockCardio.RemoveRange(entityTemplateWorkout.TemplatesBlockCardio);
            _sportServiseDbContext.TemplatesBlockStrenght.RemoveRange(entityTemplateWorkout.TemplatesBlockStrenght);
            _sportServiseDbContext.TemplatesBlockSplit.RemoveRange(entityTemplateWorkout.TemplatesBlockSplit);
            _sportServiseDbContext.TemplatesBlockWarmUp.RemoveRange(entityTemplateWorkout.TemplatesBlockWarmUp);

            var entityTemplateBlockCardioList = request.TemplatesBlockCardioDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockCardio.AddRange(entityTemplateBlockCardioList);

            var entityTemplateBlockStrenghtList = request.TemplatesBlockStrenghtDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockStrenght.AddRange(entityTemplateBlockStrenghtList);

            var entityTemplateBlockSplitList = request.TemplatesBlockSplitDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockSplit.AddRange(entityTemplateBlockSplitList);

            var entityTemplateBlockWarmUpList = request.TemplatesBlockWarmUpDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockWarmUp.AddRange(entityTemplateBlockWarmUpList);

            var dependedWorkouts = await _sportServiseDbContext.Workouts.Where(w => w.TemplateWorkoutId == request.Id).ToListAsync();

            foreach (var workout in dependedWorkouts)
            {
                workout.TemplateWorkoutId = null;
            }

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
