using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsScheduler
{
    class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext context;
        private IEventRepository events;
        private ILocationRepository locations;
        private IUserRepository users;
        
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            events = new EventRepository(context);
            locations = new LocationRepository(context);
            users = new UserRepository(context);
        } 
        
        public IEventRepository Events
        {
            get { return events; }
        }

        public ILocationRepository Locations
        {
            get { return locations; }
        }

        public IUserRepository Users
        {
            get { return users; }
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        
    }
}
