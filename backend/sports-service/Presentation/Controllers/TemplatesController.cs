using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sports_service.Core.Application.Commands.Templates.CreateTemplateWorkout;
using sports_service.Core.Application.DTOs.Templates.Blocks;
using sports_service.Presentation.Contract;

namespace sports_service.Presentation.Controllers
{
    public class TemplatesController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTemplateWorkout([FromBody] CreateTemplateWorkoutRequest request)
        {
            var templatesBlockCardioDTO = new List<TemplateBlockCardioDTO>();

            if (!request.TemplatesBlockCardioList.IsNullOrEmpty())
            {
                foreach (var templateBlockCardioRequestItem in request.TemplatesBlockCardioList!)
                {
                    var templateBlockCardioDTO = new TemplateBlockCardioDTO
                    {
                        NumberInTemplate = templateBlockCardioRequestItem.NumberInTemplate,
                        ExerciseTypeId = templateBlockCardioRequestItem.ExerciseTypeId,
                        ParametrValue = templateBlockCardioRequestItem.ParametrValue,
                        ParametrName = templateBlockCardioRequestItem.ParametrName,
                        SecondsOfDuration = templateBlockCardioRequestItem.SecondsOfDuration,
                        SecondsToRest = templateBlockCardioRequestItem.SecondsToRest
                    };

                    templatesBlockCardioDTO.Add(templateBlockCardioDTO);
                }
            }

            var templatesBlockStrenghtDTO = new List<TemplateBlockStrenghtDTO>();

            if (!request.TemplatesBlockStrenghtList.IsNullOrEmpty())
            {
                foreach (var templateBlockStrenghtRequestItem in request.TemplatesBlockStrenghtList!)
                {
                    if (!templateBlockStrenghtRequestItem.SetsList.IsNullOrEmpty())
                    {
                        foreach (var setsListRequestItem in templateBlockStrenghtRequestItem.SetsList!)
                        {
                            var setListDTO = new SetInTemplateBlockStrengthDTO
                            {
                                SetNumber = setsListRequestItem.SetNumber,
                                Weight = setsListRequestItem.Weight,
                                Reps = setsListRequestItem.Reps
                            };

                        }
                    }

                    var templateBlockStrenghtDTO = new TemplateBlockStrenghtDTO
                    {

                    };



                    templatesBlockStrenghtDTO.Add(templateBlockStrenghtDTO);
                }
            }




            var command = new CreateTemplateWorkoutCommand
            {
                UserId = UserId,
                Name = request.Name,
                Description = request.Description
            };

            return Ok();
        }

            
    }
}
