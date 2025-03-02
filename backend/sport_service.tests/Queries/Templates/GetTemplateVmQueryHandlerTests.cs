using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Templates.GetTemplateVm;
using sports_service.Core.Application.ViewModels.Templates;

namespace sport_service.tests.Queries.Templates
{
    public class GetTemplateVmQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetTemplateVmQueryHandler_Success()
        {
            // Arrange
            var handler = new GetTemplateVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var templateId = SportContextFactory.TemplateWorkoutToQueryId;

            // Act
            var result = await handler.Handle(
                new GetTemplateVmQuery
                {
                    Id = templateId,
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TemplateDetailsVm>(result);


            var entity = Context.TemplateWorkouts
                .FirstOrDefault(x => x.Id == templateId);

            Assert.NotNull(entity);

            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);

            Assert.Equal(entity.TemplatesBlockCardio.Count, entity.TemplatesBlockCardio.Count);

            var i = 0;
            var j = 0;

            foreach (var blockVm in result.TemplatesBlockCardio)
            {
                var blockEntity = entity.TemplatesBlockCardio[i];
                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInTemplate, blockVm.NumberInTemplate);
                Assert.Equal(blockEntity.ExerciseTypeId, blockVm.ExerciseTypeId);
                Assert.Equal(blockEntity.ExerciseType!.Name, blockVm.ExerciseType);
                Assert.Equal(blockEntity.ParametrValue, blockVm.ParametrValue);
                Assert.Equal(blockEntity.ParametrName, blockVm.ParametrName);
                Assert.Equal(blockEntity.SecondsOfDuration, blockVm.SecondsOfDuration);
                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }

            i = 0;
            foreach (var blockVm in result.TemplatesBlockStrenght)
            {
                var blockEntity = entity.TemplatesBlockStrenght[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInTemplate, blockVm.NumberInTemplate);
                Assert.Equal(blockEntity.ExerciseTypeId, blockVm.ExerciseTypeId);
                Assert.Equal(blockEntity.ExerciseType!.Name, blockVm.ExerciseType);
                Assert.Equal(blockEntity.NumberOfSets, blockVm.NumberOfSets);
                
                j = 0;
                foreach (var setVm in blockVm.Sets)
                {
                    var entityBlockSet = blockEntity.Sets[j];
                    Assert.Equal(entityBlockSet.Id, setVm.Id);
                    Assert.Equal(entityBlockSet.SetNumber, setVm.SetNumber);
                    Assert.Equal(entityBlockSet.Weight, setVm.Weight);
                    Assert.Equal(entityBlockSet.Reps, setVm.Reps);
                    j++;
                }

                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }

            i = 0;
            foreach (var blockVm in result.TemplatesBlockSplit)
            {
                var blockEntity = entity.TemplatesBlockSplit[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInTemplate, blockVm.NumberInTemplate);
                Assert.Equal(blockEntity.NumberOfCircles, blockVm.NumberOfCircles);

                j = 0;
                foreach (var exerciseVm in blockVm.Exercises)
                {
                    var entityBlockExersise = blockEntity.Exercises[j];
                    Assert.Equal(entityBlockExersise.Id, exerciseVm.Id);
                    Assert.Equal(entityBlockExersise.NumberInSplit, exerciseVm.NumberInSplit);
                    Assert.Equal(entityBlockExersise.ExerciseTypeId, exerciseVm.ExerciseTypeId);
                    Assert.Equal(entityBlockExersise.ExerciseType!.Name, exerciseVm.ExerciseType);
                    Assert.Equal(entityBlockExersise.Weight, exerciseVm.Weight);
                    Assert.Equal(entityBlockExersise.Reps, exerciseVm.Reps);
                    j++;
                }

                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }
            
            i = 0;
            foreach (var blockVm in result.TemplatesBlockWarmUp)
            {
                var blockEntity = entity.TemplatesBlockWarmUp[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInTemplate, blockVm.NumberInTemplate);

                j = 0;
                foreach (var exerciseVm in blockVm.Exercises)
                {
                    var entityBlockExersise = blockEntity.Exercises[j];
                    Assert.Equal(entityBlockExersise.Id, exerciseVm.Id);
                    Assert.Equal(entityBlockExersise.NumberInWarmUp, exerciseVm.NumberInWarmUp);
                    Assert.Equal(entityBlockExersise.ExerciseTypeId, exerciseVm.ExerciseTypeId);
                    Assert.Equal(entityBlockExersise.ExerciseType!.Name, exerciseVm.ExerciseType);
                    j++;
                }
                i++;
            }
        }
    }
}
