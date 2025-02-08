using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Domain.Exercises;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Templates.Blocks;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;
using sports_service.Infrastructure.Persistence;

namespace sport_service.tests.Common
{
    public class SportContextFactory
    {
        public static Guid OriginalTestUserId = Guid.Parse("A5602D24-7A06-4621-908B-563AFA422381");

        // For Templates
        public static Guid CommonExerciseTypeId = Guid.Parse("43054C54-3E26-4DB3-8EC3-74825F0597E6");

        public static Guid TemplateWorkoutToDeleteId = Guid.Parse("BB628967-2CB6-4709-A01D-785A7838CE6C");
        public static Guid TemplateWorkoutToUpdateId = Guid.Parse("EE66D26C-086A-4948-B843-DCC533F76CDE");

        public static Guid WorkoutDependetOnTemplateToDelete = Guid.Parse("1FD6A321-9C83-45CA-B334-72330714F0F3");
        public static Guid WorkoutDependetOnTemplateToUpdate = Guid.Parse("DD0DFCCB-D899-45E4-997E-EF274141F132");

        // For Workouts
        public static Guid CommonTemplateWorkoutId = Guid.Parse("A55CABF9-05F2-4B37-AF54-614BE57B0058");

        public static Guid Workout1ToDeleteId = Guid.Parse("7FCD4174-7615-403E-BB52-2529434EA2DC");
        public static Guid Workout2ToDeleteId = Guid.Parse("8CDDA162-EB78-4BA4-B657-B58499E1BA5F");
        public static Guid Workout1ToUpdateId = Guid.Parse("DA439ACB-BA81-491B-A662-9FF6E653B284");
        public static Guid Workout2ToUpdateId = Guid.Parse("61FEBDDA-F9F7-4D79-AB59-1FD7DABA0399");

        // For Exercise Type and Command test
        public static Guid ParentGroupId = Guid.Parse("6E6B69F5-3CD6-4E07-B690-653F939BB473");
        public static Guid DeletedParentGroupId = Guid.Parse("4121D54B-C78D-4D0D-BDAF-3E4CDA67030C");
        public static Guid GroupToDeleteId = Guid.Parse("679BABC6-BA04-4DA7-B1AE-5F39AD9F45D9");
        public static Guid GroupWithChildGroupToDeleteId = Guid.Parse("099AF386-BEEF-494F-B0F7-ADB275590EBA");
        public static Guid GroupWithChildTypeToDeleteId = Guid.Parse("64C50812-7A2F-46DA-8639-28C45F0EE8D7");
        public static Guid TypeToDeleteId = Guid.Parse("36A7D727-FCB2-437C-8B20-4A27292C40B6");
        public static Guid TypeIdNameWithDescToUpdate = Guid.Parse("3DE75AEC-06C8-4F48-B2C1-52F53DDD01B0");
        public static Guid TypeIdNameWithoutDescToUpdate = Guid.Parse("C7B2AD20-D7F5-41C9-A043-FA1084793098");
        public static Guid GroupIdToUpdate = Guid.Parse("8E448DA2-4CEF-419E-AA76-F785356CEBA1");
        public static Guid TypeIdDeleted = Guid.Parse("7544E276-4FAB-43F1-A3D1-219E412F6CE0");
        public static Guid GroupIdDeleted = Guid.Parse("0DED22B9-7EAC-406F-8228-182FB6B7E716");
        public static Guid GroupIdOldParent = Guid.Parse("5A66541A-6ABD-4D49-83F7-B915BF060673");
        public static Guid GroupIdNewParent = Guid.Parse("7AE31F13-414A-447F-9568-02BD209DF59B");
        public static Guid GroupIdChildToUpdate = Guid.Parse("96457952-2E1E-4934-8DF8-DEF6F4B0E2D3");
        public static Guid TypeIdChildToUpdate = Guid.Parse("7E1D82ED-756F-4E6C-A670-0EC693302E52");
        public static Guid GroupIdNewParentByAnotherUser = Guid.Parse("D47079B1-91D1-46A9-A8AA-AF88883875EA");
        public static string UnderstudyByOriginalUserNameGroup = "UnderstudyByOriginalUserNameGroup";
        public static string UnderstudyByAnotherUserNameGroup = "UnderstudyByAnotherUserNameGroup";
        public static string UnderstudyByOriginalUserNameType = "UnderstudyByOriginalUserNameType";
        public static string UnderstudyByAnotherUserNameType = "UnderstudyByAnotherUserNameType";
        
        public static SportServiseDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SportServiseDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new SportServiseDbContext(options);
            context.Database.EnsureCreated();

            // ExersiseGroup
            var testArrayOfExerciseGroup = new ExerciseGroup[]
            {
                // Родительская группа для создания наследника группы
                new ExerciseGroup
                {
                    Id = ParentGroupId,
                    UserId = OriginalTestUserId,
                    Name = "ParentGroup"
                },
                // Удаленная родительская группа для создания наследника группы
                new ExerciseGroup
                {
                    Id = DeletedParentGroupId,
                    UserId = OriginalTestUserId,
                    Name = "DeletedParentGroup",
                    IsDeleted = true
                },
                // Группа с занятым именем для этого пользователя
                new ExerciseGroup
                {
                    Id = Guid.NewGuid(),
                    UserId = OriginalTestUserId,
                    Name = UnderstudyByOriginalUserNameGroup
                },
                // Группа с занятым именем для другого пользовтаеля пользователя
                new ExerciseGroup
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = UnderstudyByAnotherUserNameGroup
                },
                // Группа для проверки удаления
                new ExerciseGroup
                {
                    Id = GroupToDeleteId,
                    UserId = OriginalTestUserId,
                    Name = "GroupToDelete"
                },
                // Группа для проверки удаления, имеющая дочерние группы
                new ExerciseGroup
                {
                    Id = GroupWithChildGroupToDeleteId,
                    UserId = OriginalTestUserId,
                    Name = "GroupToDeleteWithChildGroup"
                },
                // Группа для проверки удаления, имеющая дочерние типы
                new ExerciseGroup
                {
                    Id = GroupWithChildTypeToDeleteId,
                    UserId = OriginalTestUserId,
                    Name = "GroupToDeleteWithChildType"
                },
                // Дочерняя группа
                new ExerciseGroup
                {
                    Id = Guid.NewGuid(),
                    UserId = OriginalTestUserId,
                    Name = "SomethingGroup1",
                    ParentGroupId = GroupWithChildGroupToDeleteId
                },
                // Группа для проверки обновления
                new ExerciseGroup
                {
                    Id = GroupIdToUpdate,
                    UserId = OriginalTestUserId,
                    Name = "GroupToUpdate"
                },
                // Удаленная ранее группа
                new ExerciseGroup
                {
                    Id = GroupIdDeleted,
                    UserId = OriginalTestUserId,
                    Name = "DeletedGroup",
                    IsDeleted = true
                },
                // Группа старый родитель для проверки обновления родителя
                new ExerciseGroup
                {
                    Id = GroupIdOldParent,
                    UserId = OriginalTestUserId,
                    Name = "OldParentGroup",
                },
                // Группа новый родитель для проверки обновления родителя
                new ExerciseGroup
                {
                    Id = GroupIdNewParent,
                    UserId = OriginalTestUserId,
                    Name = "NewParentGroup",
                },
                // Группа наследник для проверки обновления родителя
                new ExerciseGroup
                {
                    Id = GroupIdChildToUpdate,
                    UserId = OriginalTestUserId,
                    Name = "ChildGroupToUpdate",
                    ParentGroupId = GroupIdOldParent
                },
                // Группа новый родитель для проверки обновления родителя
                new ExerciseGroup
                {
                    Id = GroupIdNewParentByAnotherUser,
                    UserId = Guid.NewGuid(),
                    Name = "GroupAnotherUser",
                }

            };

            var testArrayOfExerciseType = new ExerciseType[]
            {
                // Дочернее упражнение
                new ExerciseType
                {
                    Id = Guid.NewGuid(),
                    UserId = OriginalTestUserId,
                    Name = "SomethingType1",
                    ExerciseGroupId = GroupWithChildTypeToDeleteId
                },
                // Упражнение для проверки удаления
                new ExerciseType
                {
                    Id = TypeToDeleteId,
                    UserId = OriginalTestUserId,
                    Name = "TypeToDelete"
                },
                // Упражнение для проверки обновления с изначальным описанием
                new ExerciseType
                {
                    Id = TypeIdNameWithDescToUpdate,
                    UserId = OriginalTestUserId,
                    Name = "TypeExerciseWithDesc",
                    Description = "TypeExerciseWithDesc"
                },
                // Упражнение для проверки обновления без изначального описания
                new ExerciseType
                {
                    Id = TypeIdNameWithoutDescToUpdate,
                    UserId = OriginalTestUserId,
                    Name = "TypeExerciseWithoutDesc"
                },
                // Упражнение с занятым именем для этого пользователя
                new ExerciseType
                {
                    Id = Guid.NewGuid(),
                    UserId = OriginalTestUserId,
                    Name = UnderstudyByOriginalUserNameType
                },
                // Упражнение с занятым именем для другого пользовтаеля пользователя
                new ExerciseType
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = UnderstudyByAnotherUserNameType
                },
                // Удаленное ранее упражнение
                new ExerciseType
                {
                    Id = TypeIdDeleted,
                    UserId = OriginalTestUserId,
                    Name = "DeletedType",
                    IsDeleted = true
                },
                // Упражнение наследник для проверки обновления родителя
                new ExerciseType
                {
                    Id = TypeIdChildToUpdate,
                    UserId = OriginalTestUserId,
                    Name = "ChildTypeToUpdate",
                    ExerciseGroupId = GroupIdOldParent
                },
                // Тип упражнения для шаблона
                new ExerciseType
                {
                    Id = CommonExerciseTypeId,
                    UserId = OriginalTestUserId,
                    Name = "TestExerciseTypeForTemplate",
                    Description = "DescForTestExerciseTypeForTemplate"
                }
            };

            var TBStToDeleteId = Guid.NewGuid();
            var TBStToUpdateId = Guid.NewGuid();
            var TBSpToDeleteId = Guid.NewGuid();
            var TBSpToUpdateId = Guid.NewGuid();
            var TBWToDeleteId = Guid.NewGuid();
            var TBWToUpdateId = Guid.NewGuid();
            
            var TBStToCommonId = Guid.NewGuid();
            var TBSpToCommonId = Guid.NewGuid();
            var TBWToCommonId = Guid.NewGuid();

            var testArrayOfTemplateWorkout = new TemplateWorkout[]
            {
                // Для шаблон проверки удаления
                new TemplateWorkout
                {
                    Id = TemplateWorkoutToDeleteId,
                    UserId = OriginalTestUserId,
                    Name = "Name Template To Delete",
                    Description = "Name Template To Delete",
                    TemplatesBlockCardio = 
                    {
                        new TemplateBlockCardio
                        {
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 0,
                            ExerciseTypeId = CommonExerciseTypeId,
                            ParametrValue = 11,
                            ParametrName = "Name",
                            SecondsOfDuration = 12,
                            SecondsToRest = 13,
                            TemplateWorkoutId = TemplateWorkoutToDeleteId
                        }
                    },
                    TemplatesBlockStrenght =
                    {
                        new TemplateBlockStrenght
                        {
                            Id = TBStToDeleteId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 1,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInTemplateBlockStrength
                                {
                                    TemplateBlockStrenghtId = TBStToDeleteId,
                                    SetNumber = 0,
                                    Weight = 31,
                                    Reps = 31
                                }
                            },
                            SecondsToRest = 22,
                            TemplateWorkoutId = TemplateWorkoutToDeleteId
                        }
                    },
                    TemplatesBlockSplit =
                    {
                        new TemplateBlockSplit
                        {
                            Id = TBSpToDeleteId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 2,
                            NumberOfCircles = 2,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockSplit
                                {
                                    TemplateBlockSplitId = TBSpToDeleteId,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    Weight = 51,
                                    Reps = 52
                                }
                            },
                            SecondsToRest = 41,
                            TemplateWorkoutId = TemplateWorkoutToDeleteId
                        }
                    },
                    TemplatesBlockWarmUp =
                    {
                        new TemplateBlockWarmUp
                        {
                            Id = TBWToDeleteId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 3,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockWarmUp
                                {
                                    TemplateBlockWarmUpId = TBWToDeleteId,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            TemplateWorkoutId = TemplateWorkoutToDeleteId
                        }
                    }
                },

                // Для шаблон проверки обновления
                new TemplateWorkout
                {
                    Id= TemplateWorkoutToUpdateId,
                    UserId = OriginalTestUserId,
                    Name = "Name Template Brfore Update",
                    Description = "Name Template Brfore Update",
                    TemplatesBlockCardio =
                    {
                        new TemplateBlockCardio
                        {
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 0,
                            ExerciseTypeId = CommonExerciseTypeId,
                            ParametrValue = 11,
                            ParametrName = "Name",
                            SecondsOfDuration = 12,
                            SecondsToRest = 13,
                            TemplateWorkoutId = TemplateWorkoutToUpdateId
                        }
                    },
                    TemplatesBlockStrenght =
                    {
                        new TemplateBlockStrenght
                        {
                            Id = TBStToUpdateId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 1,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInTemplateBlockStrength
                                {
                                    TemplateBlockStrenghtId = TBStToUpdateId,
                                    SetNumber = 0,
                                    Weight = 31,
                                    Reps = 31
                                }
                            },
                            SecondsToRest = 22,
                            TemplateWorkoutId = TemplateWorkoutToUpdateId
                        }
                    },
                    TemplatesBlockSplit =
                    {
                        new TemplateBlockSplit
                        {
                            Id = TBSpToUpdateId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 2,
                            NumberOfCircles = 2,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockSplit
                                {
                                    TemplateBlockSplitId = TBSpToUpdateId,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    Weight = 51,
                                    Reps = 52
                                }
                            },
                            SecondsToRest = 41,
                            TemplateWorkoutId = TemplateWorkoutToUpdateId
                        }
                    },
                    TemplatesBlockWarmUp =
                    {
                        new TemplateBlockWarmUp
                        {
                            Id = TBWToUpdateId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 3,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockWarmUp
                                {
                                    TemplateBlockWarmUpId = TBWToUpdateId,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            TemplateWorkoutId = TemplateWorkoutToUpdateId
                        }
                    }
                },
            
                // Для создания и обновления тренировок
                new TemplateWorkout
                {
                    Id= CommonTemplateWorkoutId,
                    UserId = OriginalTestUserId,
                    Name = "Common Workout Template",
                    Description = "Common Workout Template",
                    TemplatesBlockCardio =
                    {
                        new TemplateBlockCardio
                        {
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 0,
                            ExerciseTypeId = CommonExerciseTypeId,
                            ParametrValue = 11,
                            ParametrName = "Name",
                            SecondsOfDuration = 12,
                            SecondsToRest = 13,
                            TemplateWorkoutId = CommonTemplateWorkoutId
                        }
                    },
                    TemplatesBlockStrenght =
                    {
                        new TemplateBlockStrenght
                        {
                            Id = TBStToCommonId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 1,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInTemplateBlockStrength
                                {
                                    TemplateBlockStrenghtId = TBStToCommonId,
                                    SetNumber = 0,
                                    Weight = 31,
                                    Reps = 31
                                }
                            },
                            SecondsToRest = 22,
                            TemplateWorkoutId = CommonTemplateWorkoutId
                        }
                    },
                    TemplatesBlockSplit =
                    {
                        new TemplateBlockSplit
                        {
                            Id = TBSpToCommonId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 2,
                            NumberOfCircles = 2,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockSplit
                                {
                                    TemplateBlockSplitId = TBSpToCommonId,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    Weight = 51,
                                    Reps = 52
                                }
                            },
                            SecondsToRest = 41,
                            TemplateWorkoutId = CommonTemplateWorkoutId
                        }
                    },
                    TemplatesBlockWarmUp =
                    {
                        new TemplateBlockWarmUp
                        {
                            Id = TBWToCommonId,
                            UserId = OriginalTestUserId,
                            NumberInTemplate = 3,
                            Exercises =
                            {
                                new ExerciseInTemplateBlockWarmUp
                                {
                                    TemplateBlockWarmUpId = TBWToCommonId,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            TemplateWorkoutId = CommonTemplateWorkoutId
                        }
                    }
                }

            };

            var BStToDelete1Id = Guid.NewGuid();
            var BSpToDelete1Id = Guid.NewGuid();
            var BWToDelete1Id = Guid.NewGuid();

            var BStToDelete2Id = Guid.NewGuid();
            var BSpToDelete2Id = Guid.NewGuid();
            var BWToDelete2Id = Guid.NewGuid();

            var BStToUpdate1Id = Guid.NewGuid();
            var BSpToUpdate1Id = Guid.NewGuid();
            var BWToUpdate1Id = Guid.NewGuid();

            var BStToUpdate2Id = Guid.NewGuid();
            var BSpToUpdate2Id = Guid.NewGuid();
            var BWToUpdate2Id = Guid.NewGuid();

            var testArrayOfWorkout = new Workout[]
            {
                // Для проверки неудаления вместе с шаблоном
                new Workout
                {
                    Id = WorkoutDependetOnTemplateToDelete,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToDeleteId,
                    Note = "WorkoutDependetOnTemplateToDelete",
                    DateOfWorkout = DateTime.UtcNow
                },
                new Workout
                {
                    Id = WorkoutDependetOnTemplateToUpdate,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToUpdateId,
                    Note = "WorkoutDependetOnTemplateToUpdate",
                    DateOfWorkout = DateTime.UtcNow
                },
                // Для проверки удаления 
                new Workout
                {
                    Id = Workout1ToDeleteId,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToDeleteId,
                    Note = "Workout1ToDelete",
                    DateOfWorkout = DateTime.UtcNow,
                    BlocksCardio =
                    {
                        new BlockCardio
                        {
                            Id = Guid.NewGuid(),
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            WorkoutId = Workout1ToDeleteId,
                            NumberInWorkout = 0
                        }
                    },
                    BlocksStrenght =
                    {
                        new BlockStrenght
                        {
                            Id = BStToDelete1Id,
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInBlockStrength
                                {
                                    Id = Guid.NewGuid(),
                                    BlockStrenghtId = BStToDelete1Id
                                }
                            },
                            WorkoutId = Workout1ToDeleteId,
                            NumberInWorkout = 1
                        }
                    },
                    BlocksSplit =
                    {
                        new BlockSplit
                        {
                            Id = BSpToDelete1Id,
                            UserId = OriginalTestUserId,
                            NumberOfCircles = 1,
                            ExercisesInSplit =
                            {
                                new ExerciseInBlockSplit
                                {
                                    Id = Guid.NewGuid(),
                                    BlockSplitId = BSpToDelete1Id,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    PlannedWeight = 10,
                                    PlannedReps = 10,
                                }
                            },
                            WorkoutId = Workout1ToDeleteId,
                            NumberInWorkout = 2
                        }
                    },
                    BlocksWarmUp =
                    {
                        new BlockWarmUp
                        {
                            Id = BWToDelete1Id,
                            UserId = OriginalTestUserId,
                            ExercisesInWarmUp =
                            {
                                new ExerciseInBlockWarmUp
                                {
                                    Id = Guid.NewGuid(),
                                    BlockWarmUpId = BWToDelete1Id,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            WorkoutId = Workout1ToDeleteId,
                            NumberInWorkout = 3
                        }
                    }
                },
                new Workout
                {
                    Id = Workout2ToDeleteId,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToUpdateId,
                    Note = "Workout2ToDelete",
                    DateOfWorkout = DateTime.UtcNow,
                    BlocksCardio =
                    {
                        new BlockCardio
                        {
                            Id = Guid.NewGuid(),
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            WorkoutId = Workout2ToDeleteId,
                            NumberInWorkout = 0
                        }
                    },
                    BlocksStrenght =
                    {
                        new BlockStrenght
                        {
                            Id = BStToDelete2Id,
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInBlockStrength
                                {
                                    Id = Guid.NewGuid(),
                                    BlockStrenghtId = BStToDelete2Id
                                }
                            },
                            WorkoutId = Workout2ToDeleteId,
                            NumberInWorkout = 1
                        }
                    },
                    BlocksSplit =
                    {
                        new BlockSplit
                        {
                            Id = BSpToDelete2Id,
                            UserId = OriginalTestUserId,
                            NumberOfCircles = 1,
                            ExercisesInSplit =
                            {
                                new ExerciseInBlockSplit
                                {
                                    Id = Guid.NewGuid(),
                                    BlockSplitId = BSpToDelete2Id,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    PlannedWeight = 10,
                                    PlannedReps = 10,
                                }
                            },
                            WorkoutId = Workout2ToDeleteId,
                            NumberInWorkout = 2
                        }
                    },
                    BlocksWarmUp =
                    {
                        new BlockWarmUp
                        {
                            Id = BWToDelete2Id,
                            UserId = OriginalTestUserId,
                            ExercisesInWarmUp =
                            {
                                new ExerciseInBlockWarmUp
                                {
                                    Id = Guid.NewGuid(),
                                    BlockWarmUpId = BWToDelete2Id,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            WorkoutId = Workout2ToDeleteId,
                            NumberInWorkout = 3
                        }
                    }
                },
                // Для проверки обновления
                new Workout
                {
                    Id = Workout1ToUpdateId,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToDeleteId,
                    Note = "Workout1ToUpdate",
                    DateOfWorkout = DateTime.UtcNow,
                     BlocksCardio =
                    {
                        new BlockCardio
                        {
                            Id = Guid.NewGuid(),
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            WorkoutId = Workout1ToUpdateId,
                            NumberInWorkout = 0
                        }
                    },
                    BlocksStrenght =
                    {
                        new BlockStrenght
                        {
                            Id = BStToUpdate1Id,
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInBlockStrength
                                {
                                    Id = Guid.NewGuid(),
                                    BlockStrenghtId = BStToUpdate1Id
                                }
                            },
                            WorkoutId = Workout1ToUpdateId,
                            NumberInWorkout = 1
                        }
                    },
                    BlocksSplit =
                    {
                        new BlockSplit
                        {
                            Id = BSpToUpdate1Id,
                            UserId = OriginalTestUserId,
                            NumberOfCircles = 1,
                            ExercisesInSplit =
                            {
                                new ExerciseInBlockSplit
                                {
                                    Id = Guid.NewGuid(),
                                    BlockSplitId = BSpToUpdate1Id,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    PlannedWeight = 10,
                                    PlannedReps = 10,
                                }
                            },
                            WorkoutId = Workout1ToUpdateId,
                            NumberInWorkout = 2
                        }
                    },
                    BlocksWarmUp =
                    {
                        new BlockWarmUp
                        {
                            Id = BWToUpdate1Id,
                            UserId = OriginalTestUserId,
                            ExercisesInWarmUp =
                            {
                                new ExerciseInBlockWarmUp
                                {
                                    Id = Guid.NewGuid(),
                                    BlockWarmUpId = BWToUpdate1Id,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            WorkoutId = Workout1ToUpdateId,
                            NumberInWorkout = 3
                        }
                    }
                },
                new Workout
                {
                    Id = Workout2ToUpdateId,
                    UserId = OriginalTestUserId,
                    TemplateWorkoutId = TemplateWorkoutToUpdateId,
                    Note = "Workout2ToUpdate",
                    DateOfWorkout = DateTime.UtcNow,
                     BlocksCardio =
                    {
                        new BlockCardio
                        {
                            Id = Guid.NewGuid(),
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            WorkoutId = Workout2ToUpdateId,
                            NumberInWorkout = 0
                        }
                    },
                    BlocksStrenght =
                    {
                        new BlockStrenght
                        {
                            Id = BStToUpdate2Id, 
                            UserId = OriginalTestUserId,
                            ExerciseTypeId = CommonExerciseTypeId,
                            NumberOfSets = 1,
                            Sets =
                            {
                                new SetInBlockStrength
                                {
                                    Id = Guid.NewGuid(),
                                    BlockStrenghtId = BStToUpdate2Id
                                }
                            },
                            WorkoutId = Workout2ToUpdateId,
                            NumberInWorkout = 1
                        }
                    },
                    BlocksSplit =
                    {
                        new BlockSplit
                        {
                            Id = BSpToUpdate2Id,
                            UserId = OriginalTestUserId,
                            NumberOfCircles = 1,
                            ExercisesInSplit =
                            {
                                new ExerciseInBlockSplit
                                {
                                    Id = Guid.NewGuid(),
                                    BlockSplitId = BSpToUpdate2Id,
                                    NumberInSplit = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                    PlannedWeight = 10,
                                    PlannedReps = 10,
                                }
                            },
                            WorkoutId = Workout2ToUpdateId,
                            NumberInWorkout = 2
                        }
                    },
                    BlocksWarmUp =
                    {
                        new BlockWarmUp
                        {
                            Id = BWToUpdate2Id,
                            UserId = OriginalTestUserId,
                            ExercisesInWarmUp =
                            {
                                new ExerciseInBlockWarmUp
                                {
                                    Id = Guid.NewGuid(),
                                    BlockWarmUpId = BWToUpdate2Id,
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = CommonExerciseTypeId,
                                }
                            },
                            WorkoutId = Workout2ToUpdateId,
                            NumberInWorkout = 3
                        }
                    }
                }
            };

            context.ExerciseGroups.AddRange(testArrayOfExerciseGroup);
            context.ExerciseTypes.AddRange(testArrayOfExerciseType);
            context.TemplateWorkouts.AddRange(testArrayOfTemplateWorkout);
            context.Workouts.AddRange(testArrayOfWorkout);

            context.SaveChangesAsync();
            return context;
        }

        public static void Destroy(SportServiseDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}