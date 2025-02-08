using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.DeleteWorkoutsList;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sport_service.tests.Commands.Workouts
{
    public class DeleteWorkoutsListCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task DeleteWorkoutsListCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteWorkoutsListCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var listId = new List<Guid>
            {
                SportContextFactory.Workout1ToDeleteId,
                SportContextFactory.Workout2ToDeleteId
            };

            var workoutEntityListBeforeDelite = new List<Workout>
            {
                _context.Workouts.Find(listId[0])!,
                _context.Workouts.Find(listId[1])!
            };

            // Act
            await handler.Handle(
                new DeleteWorkoutsListCommand
                {
                    ListId = listId,
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            foreach (var guid in listId)
            {
                var entity = _context.Workouts.SingleOrDefault(w => w.Id == guid);
                Assert.Null(entity);
            }

            foreach (var removedWorkout in workoutEntityListBeforeDelite)
            {
                var blocksCardio = new List<BlockCardio>();

                foreach (var item in removedWorkout.BlocksCardio)
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

                foreach (var item in removedWorkout.BlocksStrenght)
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

                foreach (var item in removedWorkout.BlocksSplit)
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

                foreach (var item in removedWorkout.BlocksWarmUp)
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
}
