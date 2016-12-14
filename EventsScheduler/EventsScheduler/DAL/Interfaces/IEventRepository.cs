using EventsScheduler.DAL.Entities;
using System;
using System.Collections.Generic;

namespace EventsScheduler.DAL.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetEventsWithSpecificName(string eventName);
        IEnumerable<Event> GetEventsInSpecificPeriod(DateTime fromTime, DateTime toTime);
        IEnumerable<Event> GetCurrentUserEvents(Entities.User currentUser);
    }
}
