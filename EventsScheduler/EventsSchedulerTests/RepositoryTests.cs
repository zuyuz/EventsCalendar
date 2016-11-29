using EventsScheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        public class Entity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        Mock<DbContext> dbContext;
        Mock<DbSet<Entity>> dbSet;
        Repository<Entity> repository;
        Entity entity;
        List<Entity> entities;

        public RepositoryTests()
        {
            dbContext = new Mock<DbContext>();
            dbSet = new Mock<DbSet<Entity>>();
            dbContext.Setup(obj => obj.Set<Entity>())
                .Returns(dbSet.Object);
            repository = new Repository<Entity>(dbContext.Object);
            entity = new Entity()
            {
                Id = 0,
                Name = "Entity"
            };
            entities = new List<Entity>()
            {
                new Entity() { Id = 1, Name = "One" },
                new Entity() { Id = 2, Name = "Two" }
            };
        }

        [TestMethod()]
        public void GetTest()
        {
            dbSet.Setup(obj => obj.Find(0))
                .Returns(entity);
            var actual = repository.Get(0);

            Assert.AreEqual(0, actual.Id);
            Assert.AreEqual("Entity", actual.Name);
        }

        [TestMethod()]
        public void GetNonExistentIdTest()
        {
            dbSet.Setup(obj => obj.Find(-1))
                .Returns<Entity>(null);
            var actual = repository.Get(-1);

            Assert.IsNull(actual);

        }

        [TestMethod()]
        public void GetAllTest()
        {
            bool check = false;
            dbContext.Setup(obj => obj.Set<Entity>())
                .Callback(() => { check = true; });
            try
            {
                repository.GetAll();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(check);
                return;
            }

            Assert.Fail();
        }
    }
}