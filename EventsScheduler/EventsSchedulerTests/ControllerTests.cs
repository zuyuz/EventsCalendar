using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EventsScheduler.DAL.Interfaces;
using EventsScheduler.DAL.Entities;

namespace EventsScheduler.Tests
{
    [TestClass()]
    public class ControllerTests
    {
        private Controller controller;
        private Mock<IUnitOfWork> dataAccess;
        private Mock<IEventRepository> events;
        private Mock<IUserRepository> users;
        private Mock<ILocationRepository> locations;

        public ControllerTests()
        {
            controller = Controller.Instance;
            dataAccess = new Mock<IUnitOfWork>();
            events = new Mock<IEventRepository>();
            users = new Mock<IUserRepository>();
            locations = new Mock<ILocationRepository>();
            controller.NewUnitOfWork = () => dataAccess.Object;
            dataAccess.SetupGet(_ => _.Events).Returns(events.Object);
            dataAccess.SetupGet(_ => _.Users).Returns(users.Object);
            dataAccess.SetupGet(_ => _.Locations).Returns(locations.Object);
        }

        [TestMethod()]
        public void SignInAsyncTest()
        {
            users.Setup(_ => _.GetUserByLogin("admin"))
                .Returns(new User()
                {
                    Login = "admin",
                    Password = "pass"
                });
            var task = controller.SignInAsync("admin", "pass");
            task.Wait();

            Assert.IsTrue(task.Result);
            Assert.AreEqual("admin", controller.CurrentUser.Login);
            Assert.AreEqual("pass", controller.CurrentUser.Password);
        }

        [TestMethod()]
        public void SignInAsyncIdentificationFailTest()
        {
            users.Setup(_ => _.GetUserByLogin("none"))
                .Returns<User>(null);
            var task = controller.SignInAsync("none", "_");
            task.Wait();

            Assert.IsFalse(task.Result);
        }

        [TestMethod()]
        public void SignInAsyncAuthentificationFailTest()
        {
            users.Setup(_ => _.GetUserByLogin("admin"))
                .Returns(new User()
                {
                    Login = "admin",
                    Password = "admin"
                });
            var task = controller.SignInAsync("admin", "bad_pass");
            task.Wait();

            Assert.IsFalse(task.Result);
        }

        [TestMethod()]
        public void SignOutTest()
        {
            users.Setup(_ => _.GetUserByLogin("admin"))
               .Returns(new User()
               {
                   Login = "admin",
                   Password = "pass"
               });
            var task = controller.SignInAsync("admin", "pass");
            task.Wait();
            controller.SignOut();

            Assert.IsTrue(task.Result);
            Assert.IsNull(controller.CurrentUser);
        }
    }
}