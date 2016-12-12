using EventsScheduler.DAL.Entities;

namespace EventsScheduler.DAL.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationByAddress(string address);
    }
}
