using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class BlockCardio // Блок Кардио упражнения
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int? ParametrValue { get; set; }  // Значение целевого параметра
        public string? ParametrName { get; set; } // Единицы измерения целевого параметра
        public int? PlannedSecondsOfDuration { get; set; } // Заданное время выполнения в секундах
        public int? AchievedSecondsOfDuration { get; set; } // Достигнутое время выполнения в секундах
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public int NumberInWorkout { get; set; }
}
}
