namespace sports_service.Core.Application.Common.Exceptions
{
    public class CompletedWorkoutException : Exception
    {
        public CompletedWorkoutException(object workoutId, object command)
        :base($"Workout ({workoutId}) is completed and can't be {command}.") { }
    }
}
