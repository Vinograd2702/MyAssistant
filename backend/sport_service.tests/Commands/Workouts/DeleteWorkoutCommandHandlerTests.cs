using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.DeleteWorkout;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sport_service.tests.Commands.Workouts
{
    public class DeleteWorkoutCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task DeleteWorkoutCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteWorkoutCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var workoutId = SportContextFactory.Workout1ToDeleteId;
            var workoutEntityBeforeDelite = _context.Workouts
                .Find(workoutId);

            // Act
            await handler.Handle(
                new DeleteWorkoutCommand
                {
                    Id = workoutId,
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            var workoutEntityAfterDelite = _context.Workouts
                .SingleOrDefault(w => w.Id == workoutId);

            Assert.Null(workoutEntityAfterDelite);

            var blocksCardio = new List<BlockCardio>();

            foreach(var item in workoutEntityBeforeDelite!.BlocksCardio)
            {
                var block = _context.BlocksCardio
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    blocksCardio.Add(block);
                }
            }

            Assert.Empty(blocksCardio);

            var blocksStrenght = new List<BlockStrenght>();

            foreach (var item in workoutEntityBeforeDelite!.BlocksStrenght)
            {
                var block = _context.BlocksStrenght
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    blocksStrenght.Add(block);
                }

                var sets = _context.SetsInBlockStrength
                    .Where(s => s.BlockStrenghtId == item.Id).ToList();
                Assert.Empty(sets);
            }

            Assert.Empty(blocksStrenght);

            var blocksSplit = new List<BlockSplit>();

            foreach (var item in workoutEntityBeforeDelite!.BlocksSplit)
            {
                var block = _context.BlocksSplit
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    blocksSplit.Add(block);
                }
                
                var exercises = _context.ExercisesInBlockSplit
                    .Where(s => s.BlockSplitId == item.Id).ToList();
                Assert.Empty(exercises);
            }

            Assert.Empty(blocksSplit);

            var blocksWarmUp = new List<BlockWarmUp>();

            foreach (var item in workoutEntityBeforeDelite!.BlocksWarmUp)
            {
                var block = _context.BlocksWarmUp
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    blocksWarmUp.Add(block);
                }
                
                var exercises = _context.ExercisesInBlockWarmUp
                    .Where(s => s.BlockWarmUpId == item.Id).ToList();
                Assert.Empty(exercises);
            }

            Assert.Empty(blocksWarmUp);
        }
    }
}
