using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGiftService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void AddGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.AddGroup(null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.IsNull(testService.AddGroup_Group);
        }

        [TestMethod]
        public void AddGroup_InvokesService()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            Group group = new Group {Id = 42};

            ActionResult result = controller.AddGroup(group);

            Assert.IsTrue(result is OkResult);
            //This check ensures that the service was called
            Assert.AreEqual<int>(42, testService.AddGroup_Group.Id);
        }
    }
}
