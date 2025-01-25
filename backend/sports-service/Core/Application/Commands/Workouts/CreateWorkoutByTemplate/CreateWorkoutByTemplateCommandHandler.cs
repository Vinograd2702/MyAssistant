using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Application.Commands.Workouts.CreateWorkoutByTemplate
{
    public class CreateWorkoutByTemplateCommandHandler
        : IRequestHandler<CreateWorkoutByTemplateCommand, Guid>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public CreateWorkoutByTemplateCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<Guid> Handle(CreateWorkoutByTemplateCommand request,
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

            var templateWorkout = await _sportServiseDbContext.TemplateWorkouts
                .FirstOrDefaultAsync(t => t.Id == request.TemplateWorkoutId, cancellationToken);

            if (templateWorkout == null)
            {
                throw new NotFoundEntityException(nameof(TemplateWorkout), request.TemplateWorkoutId);
            }

            if (templateWorkout.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            var entityWorkout = new Workout
            {
                UserId = request.UserId,
                TemplateWorkout = templateWorkout,
                TemplateWorkoutName = templateWorkout.Name,
                DateOfWorkout = request.DateOfWorkout,
                Note = request.Note,
                IsCompleted = false
            };

            _sportServiseDbContext.Workouts.Add(entityWorkout);

            var entityBlockCardioList = templateWorkout.TemplatesBlockCardio
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksCardio.AddRange(entityBlockCardioList);

            var entityBlockStrenghtList = templateWorkout.TemplatesBlockStrenght
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksStrenght.AddRange(entityBlockStrenghtList);

            _sportServiseDbContext.SetsInBlockStrength.AddRange(entityBlockStrenghtList
                .GetSetsList());

            var entityBlockSplitList = templateWorkout.TemplatesBlockSplit
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksSplit.AddRange(entityBlockSplitList);

            _sportServiseDbContext.ExercisesInBlockSplit.AddRange(entityBlockSplitList
                .GetExercisesList());

            var entityBlockWarmUpList = templateWorkout.TemplatesBlockWarmUp
                .ToWorkoutBlock(entityWorkout);

            _sportServiseDbContext.BlocksWarmUp.AddRange(entityBlockWarmUpList);

            _sportServiseDbContext.ExercisesInBlockWarmUp.AddRange(entityBlockWarmUpList
                .GetExercisesList());

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);

            return entityWorkout.Id;
        }
    }
}