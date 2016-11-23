using EventsScheduler.Entities;

namespace EventsScheduler
{
    interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationByAddress(string address);
    }
}
