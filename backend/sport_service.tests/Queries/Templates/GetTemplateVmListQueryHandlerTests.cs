using Shouldly;
using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Templates.GetTemplateVmList;
using sports_service.Core.Application.ViewModels.Templates;

namespace sport_service.tests.Queries.Templates
{
    public class GetTemplateVmListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetTemplateVmListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetTemplateVmListQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;

            // Act
            var result = await handler.Handle(
                new GetTemplateVmListQuery
                {
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TemplateListVm>(result); 

            var templateEntityList = Context.TemplateWorkouts
                .Where(t => t.UserId == userId).ToList();

            Assert.Equal(templateEntityList.Count, result.Templates.Count);

            var i = 0;

            foreach(var templateLookup in result.Templates)
            {
                var templateEntity = templateEntityList[i];
                Assert.IsType<TemplateLookupDto>(templateLookup);
                Assert.Equal(templateEntity.Id, templateLookup.Id);
                Assert.Equal(templateEntity.Name, templateLookup.Name);
                Assert.Equal(templateEntity.Description, templateLookup.Description);

                i++;
            }
        }
    }
}
