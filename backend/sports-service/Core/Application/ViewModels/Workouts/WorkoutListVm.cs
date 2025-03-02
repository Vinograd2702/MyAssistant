namespace sports_service.Core.Application.ViewModels.Workouts
{
    public class WorkoutListVm
    {
        public IList<WorkoutLookupDto> Workouts { get; set; }
            = new List<WorkoutLookupDto>();
    }
}
