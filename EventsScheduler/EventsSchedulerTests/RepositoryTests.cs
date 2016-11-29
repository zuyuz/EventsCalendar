using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.EntityClient;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        Mock<DbContext> dbContext;
        Mock<DbSet<Entity>> dbSet;
        Repository<Entity> repository;
        Entity entity;

        public RepositoryTests()
        {
            dbContext = new Mock<DbContext>();
            dbSet = new Mock<DbSet<Entity>>();
            dbContext.Setup(obj => obj.Set<Entity>())
                .Returns(dbSet.Object);
            repository = new Repository<Entity>(dbContext.Object);
            entity = new Entity()
            {
                Id = 1,
                Name = "Entity"
            };
        }

        [TestMethod()]
        public void GetTest()
        {
            dbSet.Setup(obj => obj.Find("1"))
                .Returns(entity);
        }

        internal class Entity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}