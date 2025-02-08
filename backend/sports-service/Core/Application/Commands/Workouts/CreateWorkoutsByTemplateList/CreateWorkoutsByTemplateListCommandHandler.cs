using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Application.Commands.Workouts.CreateWorkoutsByTemplateList
{
    public class CreateWorkoutsByTemplateListCommandHandler
        : IRequestHandler<CreateWorkoutsByTemplateListCommand, List<Guid>>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public CreateWorkoutsByTemplateListCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<List<Guid>> Handle(CreateWorkoutsByTemplateListCommand request,
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

            var entityWorkoutList = new List<Workout>();

            var entityBlockCardioList = new List<BlockCardio>();

            var entityBlockStrenghtList = new List<BlockStrenght>();

            var entityBlockSplitList = new List<BlockSplit>();

            var entityBlockWarmUpList = new List<BlockWarmUp>();

            //
            foreach (var entityWorkoutDTO in request.WorkoutDTOs)
            {
                var entityWorkout = new Workout
                {
                    UserId = request.UserId,
                    TemplateWorkout = templateWorkout,
                    TemplateWorkoutName = templateWorkout.Name,
                    DateOfWorkout = entityWorkoutDTO.DateOfWorkout,
                    Note = entityWorkoutDTO.Note
                };

                entityWorkoutList.Add(entityWorkout);

                entityBlockCardioList.AddRange(templateWorkout
                    .TemplatesBlockCardio.ToWorkoutBlock(entityWorkout));

                entityBlockStrenghtList.AddRange(templateWorkout
                    .TemplatesBlockStrenght.ToWorkoutBlock(entityWorkout));

                entityBlockSplitList.AddRange(templateWorkout
                    .TemplatesBlockSplit.ToWorkoutBlock(entityWorkout));

                entityBlockWarmUpList.AddRange(templateWorkout
                    .TemplatesBlockWarmUp.ToWorkoutBlock(entityWorkout));
            }

            _sportServiseDbContext.Workouts.AddRange(entityWorkoutList);

            _sportServiseDbContext.BlocksCardio.AddRange(entityBlockCardioList);

            _sportServiseDbContext.BlocksStrenght.AddRange(entityBlockStrenghtList);

            _sportServiseDbContext.BlocksSplit.AddRange(entityBlockSplitList);

            _sportServiseDbContext.BlocksWarmUp.AddRange(entityBlockWarmUpList);

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);

            var addedWorkoutIdList = new List<Guid>();

            foreach (var workout in entityWorkoutList)
            {
                addedWorkoutIdList.Add(workout.Id);
            }

            return addedWorkoutIdList;
        }
    }
}
