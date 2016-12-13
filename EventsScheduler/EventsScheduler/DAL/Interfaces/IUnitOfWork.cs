using System;

namespace EventsScheduler.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        ILocationRepository Locations { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
