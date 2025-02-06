using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Templates.DeleteTemplateWorkout;
using sports_service.Core.Domain.Templates.Blocks;

namespace sport_service.tests.Commands.Templates
{
    public class DeleteTemplateWorkoutCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task DeleteTemplateWorkoutCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteTemplateWorkoutCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var templateId = SportContextFactory.TemplateWorkoutToDeleteId;
            var entityBeforeDelite = await _context.TemplateWorkouts
                .FindAsync(templateId);

            // Act
            await handler.Handle(
                new DeleteTemplateWorkoutCommand
                {
                    Id = templateId,
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            var WorkoutTemplateFromDb = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == templateId);

            Assert.Null(WorkoutTemplateFromDb);

            var TemplatesBlockCardio = new List<TemplateBlockCardio>();

            foreach(var item in entityBeforeDelite!.TemplatesBlockCardio)
            {
                var block = _context.TemplatesBlockCardio
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    TemplatesBlockCardio.Add(block);
                }
            }

            Assert.Empty(TemplatesBlockCardio);

            var TemplatesBlockStrenght = new List<TemplateBlockStrenght>();

            foreach (var item in entityBeforeDelite.TemplatesBlockStrenght)
            {
                var block = _context.TemplatesBlockStrenght
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    TemplatesBlockStrenght.Add(block);
                }

                var set = _context.SetsInTemplateBlockStrength
                    .Where(s => s.TemplateBlockStrenghtId == item.Id).ToList();
                Assert.Empty(set);
            }

            Assert.Empty(TemplatesBlockStrenght);

            var TemplatesBlockSplit = new List<TemplateBlockSplit>();

            foreach (var item in entityBeforeDelite.TemplatesBlockSplit)
            {
                var block = _context.TemplatesBlockSplit
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    TemplatesBlockSplit.Add(block);
                }

                var exercises = _context.ExercisesInTemplateBlockSplit
                    .Where(s => s.TemplateBlockSplitId == item.Id).ToList();
                Assert.Empty(exercises);
            }

            Assert.Empty(TemplatesBlockSplit);

            var TemplatesBlockWarmUp = new List<TemplateBlockWarmUp>();

            foreach (var item in entityBeforeDelite.TemplatesBlockWarmUp)
            {
                var block = _context.TemplatesBlockWarmUp
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    TemplatesBlockWarmUp.Add(block);
                }

                var exercises = _context.ExercisesInTemplateBlockWarmUp
                    .Where(s => s.TemplateBlockWarmUpId == item.Id).ToList();
                Assert.Empty(exercises);
            }

            Assert.Empty(TemplatesBlockWarmUp);

            // ToDo: проверить не удаляются ли тренировки, добавить Failed Tests
        }
    }
}
