using EventsScheduler.Entities;
using System.Linq;

namespace EventsScheduler
{
    class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext context)
            : base(context)
        {
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }

        public Location GetLocationByAddress(string address)
        {
            var result = AppDbContext.Locations.Where(l => l.Address == address).ToArray();
            return result.Length > 0 ? result[0] : null;
        }
    }
}
