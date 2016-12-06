namespace EventsScheduler
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        ILocationRepository Locations { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
