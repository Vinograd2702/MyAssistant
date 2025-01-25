//using sport_service.tests.Common;
//using sports_service.Core.Application.Commands.Templates.CreateTemplateWorkout;
//using sports_service.Core.Application.DTOs.Templates.Blocks;

//namespace sport_service.tests.Commands.Templates
//{
//    public class CreateWorkoutTemplateCommandHandlerTests : TestCommandBase
//    {
//        [Fact]
//        public async Task CreateWorkoutTemplate_ByCorrectData_Success()
//        {
//            // Arrange
//            var handler = new CreateTemplateWorkoutCommandHandler(_context);
//            // Act
//            var NewWorkoutTemplateId = await handler.Handle(
//                new CreateTemplateWorkoutCommand
//                {
//                    UserId = Guid.Parse("588509B9-D51B-4780-A120-39AB384E18DE"),
//                    WorkoutTemplatesName = "Тренировка на грудь",
//                    Description = "Тренировка на грудь по понедельникам",
//                    BlockTemplateStrengthExerciseDTOs = new List<zBlockTemplateStrengthExerciseDTO>()
//                    {
//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 0,
//                            ExerciseName = "Жим штанги лежа",
//                            Description = "Классический жим штанги лежа",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 100,
//                                    Reps = 6
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 110,
//                                    Reps = 4
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 120,
//                                    Reps = 2
//                                },
//                            },
//                            SecondsToRest = 150
//                        },

//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 1,
//                            ExerciseName = "Жим гантели сидя",
//                            Description = "Классический жим гантели сидя",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 30,
//                                    Reps = 12
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 35,
//                                    Reps = 10
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 40,
//                                    Reps = 8
//                                },
//                            },
//                            SecondsToRest = 120
//                        },


//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 2,
//                            ExerciseName = "Отжимания",
//                            Description = "Отжимания от пола",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                            },
//                            SecondsToRest = 90
//                        }
//                    }
//                },
//                CancellationToken.None);
//            // Assert
//            var WorkoutTemplates = _context.WorkoutTemplates.ToList();
//            var BlockType = _context.BlockTypes.ToList();
//            var BlockTemplate = _context.BlockTemplates.ToList();
//            var BlockTemplateStrength = _context.BlockTemplatesStrengthExercise.ToList();
//            var SetInBlockTemplateStrength = _context.SetsInBlockTemplateStrengthExercise.ToList();

//            Assert.NotNull(NewWorkoutTemplateId);
//        }

//        [Fact]
//        public async Task CreateWorkoutTemplates_ByCorrectData_Success1()
//        {
//            // Arrange
//            var handler = new CreateWorkoutTemplatesCommandHandler(_context);
//            // Act

//            await Assert.ThrowsAsync<Exception>(async () =>
//            await handler.Handle(
//                new CreateWorkoutTemplatesCommand
//                {
//                    UserId = Guid.Parse("588509B9-D51B-4780-A120-39AB384E18DE"),
//                    WorkoutTemplatesName = "Тренировка на грудь",
//                    Description = "Тренировка на грудь по понедельникам",
//                    BlockTemplateStrengthExerciseDTOs = new List<zBlockTemplateStrengthExerciseDTO>()
//                    {
//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 0,
//                            ExerciseName = "Жим штанги лежа",
//                            Description = "Классический жим штанги лежа",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 100,
//                                    Reps = 6
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 110,
//                                    Reps = 4
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 120,
//                                    Reps = 2
//                                },
//                            },
//                            SecondsToRest = 150
//                        },

//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 1,
//                            ExerciseName = "Жим гантели сидя",
//                            Description = "Классический жим гантели сидя",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 30,
//                                    Reps = 12
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 35,
//                                    Reps = 10
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 40,
//                                    Reps = 8
//                                },
//                            },
//                            SecondsToRest = 120
//                        },


//                        new zBlockTemplateStrengthExerciseDTO()
//                        {
//                            NumberOfBlokInTemplate = 2,
//                            ExerciseName = "Отжимания",
//                            Description = "Отжимания от пола",
//                            SetDTOsList = new List<zSetInBlockTemplateStrengthExerciseDTO>()
//                            {
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 0,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 1,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                                new zSetInBlockTemplateStrengthExerciseDTO()
//                                {
//                                    NumberOfSetInBlock = 2,
//                                    Weight = 0,
//                                    Reps = 30
//                                },
//                            },
//                            SecondsToRest = 90
//                        }
//                    }
//                },
//                CancellationToken.None));


//            // Assert
//            var WorkoutTemplates = _context.WorkoutTemplates.ToList();
//            var BlockType = _context.BlockTypes.ToList();
//            var BlockTemplate = _context.BlockTemplates.ToList();
//            var BlockTemplateStrength = _context.BlockTemplatesStrengthExercise.ToList();
//            var SetInBlockTemplateStrength = _context.SetsInBlockTemplateStrengthExercise.ToList();

//            //Assert.NotNull(NewWorkoutTemplateId);
//        }
//    }
//}
