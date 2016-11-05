using EventsScheduler.Entities;
using System.Data.Entity;

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
    }
}
