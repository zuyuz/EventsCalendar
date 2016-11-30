using EventsScheduler.Entities;
using System;
using System.Collections.Generic;

namespace EventsScheduler
{
    interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetEventsInSpecificPeriod(DateTime fromTime, DateTime toTime);
        IEnumerable<Event> GetCurrentUserEvents(Entities.User currentUser);
    }
}
