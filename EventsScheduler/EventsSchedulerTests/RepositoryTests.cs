using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

            dbSet.Setup(obj => obj.Find(0))
                .Returns(entity);

            dbSet.Setup(obj => obj.Find(-1))
                .Returns<Entity>(null);

            dbSet.As<IEnumerable<Entity>>()
                .Setup(obj => obj.GetEnumerator())
                .Returns(entities.GetEnumerator());
            dbSet.As<IQueryable<Entity>>()
                .Setup(obj => obj.GetEnumerator())
                .Returns(entities.GetEnumerator());
            dbSet.As<IQueryable<Entity>>()
                .SetupGet(obj => obj.ElementType)
                .Returns(entities.AsQueryable().ElementType);
            dbSet.As<IQueryable<Entity>>()
                .SetupGet(obj => obj.Expression)
                .Returns(entities.AsQueryable().Expression);
            dbSet.As<IQueryable<Entity>>()
                .SetupGet(obj => obj.Provider)
                .Returns(entities.AsQueryable().Provider);
        }

        [TestMethod()]
        public void GetTest()
        {
            var actual = repository.Get(0);

            Assert.AreEqual(0, actual.Id);
            Assert.AreEqual("Entity", actual.Name);
        }

        [TestMethod()]
        public void GetNonExistentIdTest()
        {
            var actual = repository.Get(-1);

            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var actual = repository.GetAll().ToList();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual("One", actual[0].Name);
            Assert.AreEqual("Two", actual[1].Name);
        }

        [TestMethod()]
        public void FindTest()
        {
            var actual = repository
                .Find(e => e.Id.Equals(1))
                .ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual("One", actual[0].Name);
        }

        [TestMethod()]
        public void FindEmptyResultTest()
        {
            var actual = repository
                .Find(e => e.Id.Equals(-1))
                .ToList();

            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod()]
        public void AddTest()
        {
            bool check = false;
            dbSet.Setup(obj => obj.Add(It.IsAny<Entity>()))
                .Callback(() => check = true);
            repository.Add(entity);

            Assert.IsTrue(check);
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            bool check = false;
            dbSet.Setup(obj => obj.AddRange(
                It.IsAny<IEnumerable<Entity>>()))
                .Callback(() => check = true);
            repository.AddRange(entities);

            Assert.IsTrue(check);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            bool check = false;
            dbSet.Setup(obj => obj.Remove(It.IsAny<Entity>()))
                .Callback(() => check = true);
            repository.Remove(entity);
            var e = entities;
            System.Console.WriteLine(e.Count);
            Assert.IsTrue(check);
        }

        [TestMethod()]
        public void RemoveRangeTest()
        {
            bool check = false;
            dbSet.Setup(obj => obj.RemoveRange(
                It.IsAny<IEnumerable<Entity>>()))
                .Callback(() => check = true);
            repository.RemoveRange(entities);

            Assert.IsTrue(check);
        }
    }
}