using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Password = "admin"
                });
            var task = controller.SignInAsync("admin", "admin");
            task.Wait();

            Assert.IsTrue(task.Result);
        }
    }
}