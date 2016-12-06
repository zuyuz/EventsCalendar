using EventsScheduler.Entities;

namespace EventsScheduler
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationByAddress(string address);
    }
}
