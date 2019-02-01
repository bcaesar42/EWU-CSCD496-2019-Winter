using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            new GiftController(null);
        }

        [TestMethod]
        public void GetGiftForUser_ReturnsUsersFromService()
        {
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };
            var returnList = new List<Gift>();
            returnList.Add(gift);

            var testService = new TestableGiftService();
            testService.GetGiftsForUser_Return = returnList;
            
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(4);

            Assert.AreEqual<int>(4, testService.GetGiftsForUser_UserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual<int>(gift.Id, resultGift.Id);
            Assert.AreEqual<string>(gift.Title, resultGift.Title);
            Assert.AreEqual<string>(gift.Description, resultGift.Description);
            Assert.AreEqual<string>(gift.Url, resultGift.Url);
            Assert.AreEqual<int>(gift.OrderOfImportance, resultGift.OrderOfImportance);
        }

        [TestMethod]
        public void GetGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(-1);
            
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual<int>(0, testService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.AddGiftToUser(null, 4);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the controller does not AddGitToUser on the service
            Assert.AreEqual<int>(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_InvokesService()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);
            var giftDto = new DTO.Gift { Id = 42 };

            ActionResult result = controller.AddGiftToUser(giftDto, 4);

            OkResult okResult = result as OkResult;

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual<int>(4, testService.AddGiftToUser_UserId);
        }
    }
}
