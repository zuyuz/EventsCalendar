using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EventsScheduler.Entities;
using System.Data.Entity;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        Mock<AppDbContext> dbContext;
        Mock<DbSet<User>> dbSet;
        UserRepository repository;
        List<User> users;

        public UserRepositoryTests()
        {
            dbContext = new Mock<AppDbContext>();
            dbSet = new Mock<DbSet<User>>();
            dbContext.SetupGet(obj => obj.Users)
                .Returns(dbSet.Object);
            repository = new UserRepository(dbContext.Object);

            users = new List<User>()
            {
                new User()
                {
                    Id = 1, Login = "admin",
                    UserRole = User.Role.Admin
                },
                new User()
                {
                    Id = 2, Login = "guest",
                    UserRole = User.Role.Guest
                },
                new User()
                {
                    Id = 3, Login = "user",
                    UserRole = User.Role.User
                }
            };

            dbSet.As<IQueryable<User>>()
                .Setup(obj => obj.GetEnumerator())
                .Returns(users.GetEnumerator());
            dbSet.As<IQueryable<User>>()
                .SetupGet(obj => obj.ElementType)
                .Returns(users.AsQueryable().ElementType);
            dbSet.As<IQueryable<User>>()
                .SetupGet(obj => obj.Expression)
                .Returns(users.AsQueryable().Expression);
            dbSet.As<IQueryable<User>>()
                .SetupGet(obj => obj.Provider)
                .Returns(users.AsQueryable().Provider);
        }

        [TestMethod()]
        public void GetUserAdminTest()
        {
            var actual = repository.GetUserAdmin();

            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("admin", actual.Login);
            Assert.AreEqual(User.Role.Admin, actual.UserRole);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUserAdminEmptyResultTest()
        {
            users.RemoveAt(0);
            var actual = repository.GetUserAdmin();
        }

        [TestMethod()]
        public void GetUserGuestTest()
        {
            var actual = repository.GetUserGuest();

            Assert.AreEqual(2, actual.Id);
            Assert.AreEqual("guest", actual.Login);
            Assert.AreEqual(User.Role.Guest, actual.UserRole);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUserGuestEmptyResultTest()
        {
            users.RemoveAt(1);
            var actual = repository.GetUserGuest();
        }

        [TestMethod()]
        public void GetUserByLoginTest()
        {
            var actual = repository.GetUserByLogin("user");

            Assert.AreEqual(3, actual.Id);
            Assert.AreEqual("user", actual.Login);
            Assert.AreEqual(User.Role.User, actual.UserRole);
        }

        [TestMethod()]
        public void GetUserByLoginEmptyResultTest()
        {
            var actual = repository.GetUserByLogin("nope");

            Assert.IsNull(actual);
        }
    }
}
