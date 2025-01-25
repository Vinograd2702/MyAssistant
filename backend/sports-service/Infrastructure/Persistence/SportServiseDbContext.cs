using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates.Blocks;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts.Blocks;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Exercises;
using sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Exercises;
using sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates;
using sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence
{
    public class SportServiseDbContext : DbContext, ISportServiseDbContext
    {
        // Exercises
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        // Templates
        public DbSet<TemplateWorkout> TemplateWorkouts { get; set; }
        //Templates/Blocks
        public DbSet<TemplateBlockCardio> TemplatesBlockCardio { get; set; }
        public DbSet<TemplateBlockSplit> TemplatesBlockSplit { get; set; }
        public DbSet<TemplateBlockStrenght> TemplatesBlockStrenght { get; set; }
        public DbSet<TemplateBlockWarmUp> TemplatesBlockWarmUp { get; set; }
        public DbSet<SetInTemplateBlockStrength> SetsInTemplateBlockStrength { get; set; }
        public DbSet<ExerciseInTemplateBlockSplit> ExercisesInTemplateBlockSplit { get; set; }
        public DbSet<ExerciseInTemplateBlockWarmUp> ExercisesInTemplateBlockWarmUp { get; set; }

        // Workouts
        public DbSet<Workout> Workouts { get; set; }
        // Workouts/Blocks
        public DbSet<BlockCardio> BlocksCardio { get; set; }
        public DbSet<BlockSplit> BlocksSplit { get; set; }
        public DbSet<BlockStrenght> BlocksStrenght { get; set; }
        public DbSet<BlockWarmUp> BlocksWarmUp { get; set; }
        public DbSet<SetInBlockStrength> SetsInBlockStrength { get; set; }
        public DbSet<ExerciseInBlockSplit> ExercisesInBlockSplit { get; set; }
        public DbSet<ExerciseInBlockWarmUp> ExercisesInBlockWarmUp { get; set; }
        public SportServiseDbContext(DbContextOptions<SportServiseDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exercises
            modelBuilder.ApplyConfiguration(new ExerciseGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ExerciseTypeConfiguration());

            // Templates
            modelBuilder.ApplyConfiguration(new TemplateWorkoutConfiguration());
            //Templates/Blocks
            modelBuilder.ApplyConfiguration(new TemplateBlockCardioConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateBlockSplitConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateBlockStrenghtConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateBlockWarmUpConfiguration());
            modelBuilder.ApplyConfiguration(new SetInTemplateBlockStrengthConfiguration());
            modelBuilder.ApplyConfiguration(new ExerciseInTemplateBlockSplitConfiguration());
            modelBuilder.ApplyConfiguration(new ExerciseInTemplateBlockWarmUpConfiguration());

            // Workouts
            // Workouts/Blocks

        }
    }
}