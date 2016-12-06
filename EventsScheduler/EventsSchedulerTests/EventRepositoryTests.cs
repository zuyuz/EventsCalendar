using EventsScheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using EventsScheduler.Entities;
using System;
using System.Linq;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class EventRepositoryTests
    {
        Mock<AppDbContext> dbContext;
        Mock<DbSet<Event>> dbSet;
        EventRepository repository;
        List<Event> events;
        User creator;

        public EventRepositoryTests()
        {
            dbContext = new Mock<AppDbContext>();
            dbSet = new Mock<DbSet<Event>>();
            dbContext.SetupGet(obj => obj.Events)
                .Returns(dbSet.Object);
            repository = new EventRepository(dbContext.Object);

            creator = new User()
            {
                Id = 1,
                Name = "Creator",
                Login = "login",
                Password = "pass",
                UserRole = User.Role.User
            };

            events = new List<Event>()
            {
                new Event()
                {
                    Id = 1, Name = "One",
                    StartTime = new DateTime(2016, 12, 6),
                    EndTime = new DateTime(2016, 12, 7),
                    Creator = creator
                },
                new Event()
                {
                    Id = 2, Name = "Two",
                    StartTime = new DateTime(2016, 12, 1),
                    EndTime = new DateTime(2016, 12, 3),
                    Creator = creator
                }
            };

            creator.CreatedEvents = events;

            dbSet.As<IQueryable<Event>>()
                .Setup(obj => obj.GetEnumerator())
                .Returns(events.GetEnumerator());
            dbSet.As<IQueryable<Event>>()
                .SetupGet(obj => obj.ElementType)
                .Returns(events.AsQueryable().ElementType);
            dbSet.As<IQueryable<Event>>()
                .SetupGet(obj => obj.Expression)
                .Returns(events.AsQueryable().Expression);
            dbSet.As<IQueryable<Event>>()
                .SetupGet(obj => obj.Provider)
                .Returns(events.AsQueryable().Provider);
        }

        [TestMethod()]
        public void GetEventsInSpecificPeriodAllInTest()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 11, 1),
                new DateTime(2016, 12, 17)).ToList();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual(2, actual[1].Id);
        }

        [TestMethod()]
        public void GetEventsInSpecificPeriodSomeIn1Test()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 12, 5),
                new DateTime(2016, 12, 17)).ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
        }

        [TestMethod()]
        public void GetEventsInSpecificPeriodSomeIn2Test()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 12, 1),
                new DateTime(2016, 12, 4)).ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(2, actual[0].Id);
        }

        [TestMethod()]
        public void GetEventsInSpecificPeriodZeroIn1Test()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 12, 16),
                new DateTime(2016, 12, 17)).ToList();

            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod()]
        public void GetEventsInSpecificPeriodZeroIn2Test()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 11, 16),
                new DateTime(2016, 11, 17)).ToList();

            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEventsInSpecificPeriodBadIntervalTest()
        {
            var actual = repository.GetEventsInSpecificPeriod(
                new DateTime(2016, 12, 17),
                new DateTime(2016, 12, 10));
        }

        [TestMethod()]
        public void GetCurrentUserEventsTest()
        {
            var actual = repository
                .GetCurrentUserEvents(creator).ToList();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual(2, actual[1].Id);
        }

        [TestMethod()]
        public void GetCurrentUserEventsEmptyResultTest()
        {
            var currUser = new User() { Id = -1, Login = "txt"};
            var actual = repository
                .GetCurrentUserEvents(currUser).ToList();

            Assert.IsTrue(actual.Count == 0);
        }
    }
}
