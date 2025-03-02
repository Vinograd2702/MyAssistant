namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record DeleteWorkoutsListRequest
    {
        public List<Guid> ListId { get; init; }
            = new List<Guid>();
    }
}
