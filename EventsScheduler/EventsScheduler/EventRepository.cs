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
            return from ev in AppDbContext.Events
                   where fromTime == ev.StartTime
                      && fromTime == ev.EndTime
                   select ev;
        }
    }
}
