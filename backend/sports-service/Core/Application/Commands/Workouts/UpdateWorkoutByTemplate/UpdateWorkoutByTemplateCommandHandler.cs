using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutByTemplate
{
    public class UpdateWorkoutByTemplateCommandHandler
        : IRequestHandler<UpdateWorkoutByTemplateCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateWorkoutByTemplateCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateWorkoutByTemplateCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.TemplateWorkoutId == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.TemplateWorkoutId));
            }

            var entityWorkout = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (entityWorkout == null)
            {
                throw new NotFoundEntityException(nameof(Workout), request.Id);
            }

            var templateWorkout = await _sportServiseDbContext.TemplateWorkouts
                .FirstOrDefaultAsync(t => t.Id == request.TemplateWorkoutId, cancellationToken);

            if (templateWorkout == null)
            {
                throw new NotFoundEntityException(nameof(TemplateWorkout),
                    request.TemplateWorkoutId);
            }

            if (templateWorkout.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            entityWorkout.TemplateWorkout = templateWorkout;
            entityWorkout.TemplateWorkoutName = templateWorkout.Name;

            _sportServiseDbContext.BlocksCardio.RemoveRange(entityWorkout.BlocksCardio);
            _sportServiseDbContext.BlocksStrenght.RemoveRange(entityWorkout.BlocksStrenght);
            _sportServiseDbContext.BlocksSplit.RemoveRange(entityWorkout.BlocksSplit);
            _sportServiseDbContext.BlocksWarmUp.RemoveRange(entityWorkout.BlocksWarmUp);

            var entityTemplateBlockCardioList = templateWorkout.TemplatesBlockCardio
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksCardio.AddRange(entityTemplateBlockCardioList);

            var entityBlockStrenghtList = templateWorkout.TemplatesBlockStrenght
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksStrenght.AddRange(entityBlockStrenghtList);

            var entityBlockSplitList = templateWorkout.TemplatesBlockSplit
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksSplit.AddRange(entityBlockSplitList);

            var entityBlockWarmUpList = templateWorkout.TemplatesBlockWarmUp
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksWarmUp.AddRange(entityBlockWarmUpList);

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
