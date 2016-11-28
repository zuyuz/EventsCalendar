using EventsScheduler.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsScheduler
{
    class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context)
            : base(context)
        {
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }

        public IEnumerable<Event> GetEventsInSpecificPeriod(DateTime fromTime, DateTime toTime)
        {
            if (fromTime > toTime)
                throw new ArgumentException("Bad arguments");

            return from ev in AppDbContext.Events
                   where ev.StartTime >= fromTime 
                      && ev.EndTime <= toTime
                   select ev;
        }
    }
}
