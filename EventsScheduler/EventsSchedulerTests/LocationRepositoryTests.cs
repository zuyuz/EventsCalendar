using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Data.Entity;
using EventsScheduler.DAL;
using EventsScheduler.DAL.Entities;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class LocationRepositoryTests
    {
        Mock<AppDbContext> dbContext;
        Mock<DbSet<Location>> dbSet;
        LocationRepository repository;
        List<Location> locations;

        public LocationRepositoryTests()
        {
            dbContext = new Mock<AppDbContext>();
            dbSet = new Mock<DbSet<Location>>();
            dbContext.SetupGet(obj => obj.Locations)
                .Returns(dbSet.Object);
            repository = new LocationRepository(dbContext.Object);

            locations = new List<Location>()
            {
                new Location()
                {
                    Id = 1,
                    Address = "Street1"
                },
                new Location()
                {
                    Id = 2,
                    Address = "Street2"
                }
            };

            dbSet.As<IQueryable<Location>>()
                .Setup(obj => obj.GetEnumerator())
                .Returns(locations.GetEnumerator());
            dbSet.As<IQueryable<Location>>()
                .SetupGet(obj => obj.ElementType)
                .Returns(locations.AsQueryable().ElementType);
            dbSet.As<IQueryable<Location>>()
                .SetupGet(obj => obj.Expression)
                .Returns(locations.AsQueryable().Expression);
            dbSet.As<IQueryable<Location>>()
                .SetupGet(obj => obj.Provider)
                .Returns(locations.AsQueryable().Provider);
        }

        [TestMethod()]
        public void GetLocationByAddressTest()
        {
            var actual = repository.GetLocationByAddress("Street1");

            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("Street1", actual.Address);
        }

        [TestMethod()]
        public void GetLocationByAddressEmptyResultTest()
        {
            var actual = repository.GetLocationByAddress("Street");

            Assert.IsNull(null);
        }
    }
}
