using sports_service.Core.Application.Commands.Exercises.CreateExercisesGroup;
using sports_service.Core.Application.Commands.Exercises.CreateExerciseType;
using sports_service.Core.Application.Commands.Exercises.DeleteExercisesGroup;
using sports_service.Core.Application.Commands.Exercises.DeleteExerciseType;
using sports_service.Core.Application.Commands.Exercises.UpdateInfoExerciseType;
using sports_service.Core.Application.Commands.Exercises.UpdateNameExercisesGroup;
using sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesGroup;
using sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesType;
using sports_service.Core.Application.Commands.SetWorkoutAlertOptionForUser;
using sports_service.Core.Application.Commands.Templates.CreateTemplateWorkout;
using sports_service.Core.Application.Commands.Templates.DeleteTemplateWorkout;
using sports_service.Core.Application.Commands.Templates.UpdateTemplateWorkout;
using sports_service.Core.Application.Commands.Workouts.CreateWorkoutByTemplate;
using sports_service.Core.Application.Commands.Workouts.CreateWorkoutsByTemplateList;
using sports_service.Core.Application.Commands.Workouts.DeleteWorkout;
using sports_service.Core.Application.Commands.Workouts.DeleteWorkoutsList;
using sports_service.Core.Application.Commands.Workouts.SaveWorkoutResult;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutByTemplate;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutDate;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutNote;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutsByTemplateList;
using sports_service.Core.Application.DTOs.Templates.Blocks;
using sports_service.Presentation.Contract.ExersisesControllerRequest;
using sports_service.Presentation.Contract.Items;
using sports_service.Presentation.Contract.TemplatesControllerRequest;
using sports_service.Presentation.Contract.WorkoutNotificationSettingsControllerRequest;
using sports_service.Presentation.Contract.WorkoutsControllerRequest;
using static sports_service.Presentation.Contract.Items.TemplateBlockSplitRequestItem;
using static sports_service.Presentation.Contract.Items.TemplateBlockStrenghtRequestItem;
using static sports_service.Presentation.Contract.Items.TemplateBlockWarmUpRequestItem;

namespace sports_service.Presentation.Common.Extentions
{
    public static class ContractMappers
    {
        // Common TemplateWorkoutRequest
        public static TemplateBlockCardioDTO ToCommand(
            this TemplateBlockCardioRequestItem request)
        {
            return new TemplateBlockCardioDTO
            {
                NumberInTemplate = request.NumberInTemplate,
                ExerciseTypeId = request.ExerciseTypeId,
                ParametrValue = request.ParametrValue,
                ParametrName = request.ParametrName,
                SecondsOfDuration = request.SecondsOfDuration,
                SecondsToRest = request.SecondsToRest
            };
        }

        public static List<TemplateBlockCardioDTO> ToCommand(
            this IEnumerable<TemplateBlockCardioRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        public static SetInTemplateBlockStrengthDTO ToCommand(
            this SetInTemplateBlockStrengthRequestItem request)
        {
            return new SetInTemplateBlockStrengthDTO
            {
                SetNumber = request.SetNumber,
                Weight = request.Weight,
                Reps = request.Reps
            };
        }

        public static List<SetInTemplateBlockStrengthDTO> ToCommand(
            this IEnumerable<SetInTemplateBlockStrengthRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        public static TemplateBlockStrenghtDTO ToCommand(
            this TemplateBlockStrenghtRequestItem request)
        {
            return new TemplateBlockStrenghtDTO
            {
                NumberInTemplate = request.NumberInTemplate,
                ExerciseTypeId = request.ExerciseTypeId,
                NumberOfSets = request.NumberOfSets,
                SetsListDTO = request.SetsList.ToCommand(),
                SecondsToRest = request.SecondsToRest
            };
        }

        public static List<TemplateBlockStrenghtDTO> ToCommand(
            this IEnumerable<TemplateBlockStrenghtRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        public static ExerciseInTemplateBlockSplitDTO ToCommand(
            this ExerciseInTemplateBlockSplitRequestItem request)
        {
            return new ExerciseInTemplateBlockSplitDTO
            {
                NumberInSplit = request.NumberInSplit,
                ExerciseTypeId = request.ExerciseTypeId,
                Weight = request.Weight,
                Reps = request.Reps
            };
        }

        public static List<ExerciseInTemplateBlockSplitDTO> ToCommand(
            this IEnumerable<ExerciseInTemplateBlockSplitRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        public static TemplateBlockSplitDTO ToCommand(
            this TemplateBlockSplitRequestItem request)
        {
            return new TemplateBlockSplitDTO
            {
                NumberInTemplate = request.NumberInTemplate,
                NumberOfCircles = request.NumberOfCircles,
                ExercisesInSplitDTO = request.ExercisesInSplitList.ToCommand(),
                SecondsToRest = request.SecondsToRest
            };
        }

        public static List<TemplateBlockSplitDTO> ToCommand(
            this IEnumerable<TemplateBlockSplitRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }
        
        public static ExerciseInTemplateBlockWarmUpDTO ToCommand(
            this ExerciseInTemplateBlockWarmUpRequestItem request)
        {
            return new ExerciseInTemplateBlockWarmUpDTO
            {
                NumberInWarmUp = request.NumberInWarmUp,
                ExerciseTypeId = request.ExerciseTypeId
            };
        }

        public static List<ExerciseInTemplateBlockWarmUpDTO> ToCommand(
            this IEnumerable<ExerciseInTemplateBlockWarmUpRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        public static TemplateBlockWarmUpDTO ToCommand(
            this TemplateBlockWarmUpRequestItem request)
        {
            return new TemplateBlockWarmUpDTO
            {
                NumberInTemplate = request.NumberInTemplate,
                ExercisesInWarmUpDTO = request.ExercisesInWarmUpList.ToCommand()
            };
        }

        public static List<TemplateBlockWarmUpDTO> ToCommand(
            this IEnumerable<TemplateBlockWarmUpRequestItem> request)
        {
            return request.Select(e => e.ToCommand()).ToList();
        }

        // CreateTemplateWorkoutRequest
        public static CreateTemplateWorkoutCommand ToCommand(
            this CreateTemplateWorkoutRequest request,
            Guid userId)
        {
            return new CreateTemplateWorkoutCommand
            {
                UserId = userId,
                Name = request.Name,
                Description = request.Description,
                TemplatesBlockCardioDTO = request.TemplatesBlockCardioList.ToCommand(),
                TemplatesBlockStrenghtDTO = request.TemplatesBlockStrenghtList.ToCommand(),
                TemplatesBlockSplitDTO = request.TemplatesBlockSplitList.ToCommand(),
                TemplatesBlockWarmUpDTO = request.TemplatesBlockWarmUpList.ToCommand()
            };
        }

        // UpdateTemplateWorkoutRequest
        public static UpdateTemplateWorkoutCommand ToCommand(
            this UpdateTemplateWorkoutRequest request,
            Guid userId)
        {
            return new UpdateTemplateWorkoutCommand
            {
                Id = request.Id,
                UserId = userId,
                Name = request.Name,
                Description = request.Description,
                TemplatesBlockCardioDTO = request.TemplatesBlockCardioList.ToCommand(),
                TemplatesBlockStrenghtDTO = request.TemplatesBlockStrenghtList.ToCommand(),
                TemplatesBlockSplitDTO = request.TemplatesBlockSplitList.ToCommand(),
                TemplatesBlockWarmUpDTO = request.TemplatesBlockWarmUpList.ToCommand()
            };
        }

        // DeleteTemplateWorkoutRequest
        public static DeleteTemplateWorkoutCommand ToCommand(
            this DeleteTemplateWorkoutRequest request,
            Guid userId)
        {
            return new DeleteTemplateWorkoutCommand
            {
                Id = request.Id,
                UserId = userId
            };
        }

        // Exersises
        public static CreateExercisesGroupCommand ToCommand(
            this CreateExercisesGroupRequest request,
            Guid userId)
        {
            return new CreateExercisesGroupCommand
            {
                UserId = userId,
                ParentGroupId = request.ParentGroupId,
                Name = request.Name
            };
        }

        public static CreateExerciseTypeCommand ToCommand(
            this CreateExerciseTypeRequest request,
            Guid userId)
        {
            return new CreateExerciseTypeCommand
            {
                UserId = userId,
                ExerciseGroupId = request.ExerciseGroupId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static UpdateInfoExercisesTypeCommand ToCommand(
            this UpdateInfoExerciseTypeRequest request,
            Guid userId)
        {
            return new UpdateInfoExercisesTypeCommand
            {
                Id = request.Id,
                UserId = userId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static UpdateNameExercisesGroupCommand ToCommand(
            this UpdateNameExercisesGroupRequest request,
            Guid userId)
        {
            return new UpdateNameExercisesGroupCommand
            {
                Id = request.Id,
                UserId = userId,
                Name = request.Name
            };
        }

        public static UpdateParentExercisesGroupCommand ToCommand(
            this UpdateParentExercisesGroupRequest request,
            Guid userId)
        {
            return new UpdateParentExercisesGroupCommand
            {
                Id = request.Id,
                UserId = userId,
                ParentGroupId = request.ParentGroupId
            };
        }

        public static UpdateParentExercisesTypeCommand ToCommand(
            this UpdateParentExercisesTypeRequest request,
            Guid userId)
        {
            return new UpdateParentExercisesTypeCommand
            {
                Id = request.Id,
                UserId = userId,
                ExerciseGroupId = request.ExerciseGroupId
            };
        }

        public static DeleteExercisesGroupCommand ToCommand(
            this DeleteExercisesGroupRequest request,
            Guid userId)
        {
            return new DeleteExercisesGroupCommand
            {
                Id = request.Id,
                UserId = userId
            };
        }

        public static DeleteExerciseTypeCommand ToCommand(
            this DeleteExerciseTypeRequest request,
            Guid userId)
        {
            return new DeleteExerciseTypeCommand
            {
                Id = request.Id,
                UserId = userId
            };
        }

        // Workouts
        public static CreateWorkoutByTemplateCommand ToCommand(
            this CreateWorkoutByTemplateRequest request,
            Guid userId)
        {
            return new CreateWorkoutByTemplateCommand
            {
                UserId = userId,
                TemplateWorkoutId = request.TemplateWorkoutId,
                DateOfWorkout = request.DateOfWorkout,
                Note = request.Note
            };
        }

        public static CreateWorkoutsByTemplateListCommand ToCommand(
            this CreateWorkoutsByTemplateListRequest request,
            Guid userId)
        {
            return new CreateWorkoutsByTemplateListCommand
            {
                UserId = userId,
                TemplateWorkoutId = request.TemplateWorkoutId,
                WorkoutDTOs = request.WorkoutDTOs
            };
        }

        public static DeleteWorkoutCommand ToCommand(
            this DeleteWorkoutRequest request,
            Guid userId)
        {
            return new DeleteWorkoutCommand
            {
                Id = request.Id,
                UserId = userId
            };
        }

        public static DeleteWorkoutsListCommand ToCommand(
            this DeleteWorkoutsListRequest request,
            Guid userId)
        {
            return new DeleteWorkoutsListCommand
            {
                ListId = request.ListId,
                UserId = userId
            };
        }

        public static SaveWorkoutResultCommand ToCommand(
            this SaveWorkoutResultRequest request,
            Guid userId)
        {
            return new SaveWorkoutResultCommand
            {
                UserId = userId,
                WorkoutResultsDTO = request.WorkoutResultsDTO
            };
        }

        public static UpdateWorkoutByTemplateCommand ToCommand(
            this UpdateWorkoutByTemplateRequest request,
            Guid userId)
        {
            return new UpdateWorkoutByTemplateCommand
            {
                Id = request.Id,
                UserId = userId,
                TemplateWorkoutId = request.TemplateWorkoutId
            };
        }

        public static UpdateWorkoutDateCommand ToCommand(
            this UpdateWorkoutDateRequest request,
            Guid userId)
        {
            return new UpdateWorkoutDateCommand
            {
                Id = request.Id,
                UserId = userId,
                DateOfWorkout = request.DateOfWorkout
            };
        }

        public static UpdateWorkoutNoteCommand ToCommand(
            this UpdateWorkoutNoteRequest request,
            Guid userId)
        {
            return new UpdateWorkoutNoteCommand
            {
                Id = request.Id,
                UserId = userId,
                NewNote = request.NewNote
            };
        }

        public static UpdateWorkoutsByTemplateListCommand ToCommand(
            this UpdateWorkoutsByTemplateListRequest request,
            Guid userId)
        {
            return new UpdateWorkoutsByTemplateListCommand
            {
                TemplateWorkoutId = request.TemplateWorkoutId,
                UserId = userId,
                WorkoutsId = request.WorkoutsId
            };
        }

        // WorkoutNotificationSettings
        public static SetWorkoutAlertOptionForUserCommand ToCommand(
            this SetWorkoutNotificationAlertOptionRequest request, 
            Guid userId)
        {
            return new SetWorkoutAlertOptionForUserCommand
            {
                UserId = userId,
                AforehandHourBeforeWorkout = request.AforehandHourBeforeWorkout
            };
        }
    }
}
