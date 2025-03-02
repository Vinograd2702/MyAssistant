using Microsoft.EntityFrameworkCore;
using sports_service.Core.Domain.Exercises;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Templates.Blocks;
using sports_service.Core.Domain.WorkoutNotificationSettings;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Application.Interfaces.Repositories
{
    public interface ISportServiseDbContext
    {
        // Exercises
        DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        DbSet<ExerciseType> ExerciseTypes { get; set; }

        // Templates
        DbSet<TemplateWorkout> TemplateWorkouts { get; set; }
        //Templates/Blocks
        DbSet<TemplateBlockCardio> TemplatesBlockCardio { get; set; }
        DbSet<TemplateBlockSplit> TemplatesBlockSplit { get; set; }
        DbSet<TemplateBlockStrenght> TemplatesBlockStrenght { get; set; }
        DbSet<TemplateBlockWarmUp> TemplatesBlockWarmUp { get; set; }
        DbSet<SetInTemplateBlockStrength> SetsInTemplateBlockStrength { get; set; }
        DbSet<ExerciseInTemplateBlockSplit> ExercisesInTemplateBlockSplit { get; set; }
        DbSet<ExerciseInTemplateBlockWarmUp> ExercisesInTemplateBlockWarmUp { get; set; }

        // Workouts
        DbSet<Workout> Workouts { get; set; }
        // Workouts/Blocks
        DbSet<BlockCardio> BlocksCardio { get; set; }
        DbSet<BlockSplit> BlocksSplit { get; set; }
        DbSet<BlockStrenght> BlocksStrenght { get; set; }
        DbSet<BlockWarmUp> BlocksWarmUp { get; set; }
        DbSet<SetInBlockStrength> SetsInBlockStrength { get; set; }
        DbSet<ExerciseInBlockSplit> ExercisesInBlockSplit { get; set; }
        DbSet<ExerciseInBlockWarmUp> ExercisesInBlockWarmUp { get; set; }

        //WorkoutNotificationSettings
        DbSet<WorkoutNotificationSetting> WorkoutNotificationSettings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
