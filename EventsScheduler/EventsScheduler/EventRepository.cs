using EventsScheduler.Entities;
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

            return (from ev in AppDbContext.Events
                   where ev.StartTime >= fromTime 
                      && ev.EndTime <= toTime
                   select ev).ToList();
        }

        public IEnumerable<Event> GetCurrentUserEvents(Entities.User currentUser)
        {
            foreach (var ev in AppDbContext.Events.ToList())
            {
                if (ev.Creator.Login == currentUser.Login)
                {
                    yield return 
                        new Entities.Event()
                    {
                        Id = ev.Id,
                        Creator = ev.Creator,
                        EndTime = ev.EndTime,
                        StartTime = ev.StartTime,
                        EventLocation = ev.EventLocation,
                        FreePlaces = ev.FreePlaces,
                        Name = ev.Name,
                        Participants = ev.Participants
                    };
                }
            }
        }
    }
}
