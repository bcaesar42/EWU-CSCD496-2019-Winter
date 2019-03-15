using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UserPageTests
    {
        private const string RootUrl = "https://localhost:44302/";

        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }

        [TestMethod]
        public void CanGetToUsersPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            var homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            //Act
            page.AddUser.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));
        }

        [TestMethod]
        public void CanAddUsers()
        {
            //Arrange /Act
            string userFirstName = "User First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "User Last Name" + Guid.NewGuid().ToString("N");
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains($"{userFirstName} {userLastName}"));

            // Cleanup
            DeleteUser(userFirstName, userLastName);
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            string userFirstName = "User First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "User Last Name" + Guid.NewGuid().ToString("N");
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(userFirstName, userLastName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Accept();

            //Assert
            List<string> userNames = page.UserNames;
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanNavigateToEditUsersPage()
        {
            //Arrange
            string userFirstName = "User First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "User Last Name" + Guid.NewGuid().ToString("N");
            UsersPage page = CreateUser(userFirstName, userLastName);
            string createdUserId = page.GetUserId(userFirstName, userLastName);

            //Act
            page.GetEditLink(userFirstName, userLastName).Click();

            //Assert
            //Assert.AreEqual<string>($"https://localhost:44302/{EditUsersPage.Slug}/{createdUserId}", Driver.Url);
            Assert.IsTrue(Driver.Url.EndsWith($"{EditUsersPage.Slug}/{createdUserId}"));

            // Cleanup
            DeleteUser(userFirstName, userLastName);
        }

        [TestMethod]
        public void CanEditUsers()
        {
            //Arrange
            string userOriginalFirstName = "User First Name" + Guid.NewGuid().ToString("N");
            string userOriginalLastName = "User Last Name" + Guid.NewGuid().ToString("N");
            UsersPage page = CreateUser(userOriginalFirstName, userOriginalLastName);
            string createdUserId = page.GetUserId(userOriginalFirstName, userOriginalLastName);
            string userNewFirstName = "User First Name" + Guid.NewGuid().ToString("N");
            string userNewLastName = "User Last Name" + Guid.NewGuid().ToString("N");

            page.GetEditLink(userOriginalFirstName, userOriginalLastName).Click();

            //Act
            EditUsersPage editUsersPage = new EditUsersPage(Driver);

            editUsersPage.UserFirstNameTextBox.Clear();
            editUsersPage.UserFirstNameTextBox.SendKeys(userNewFirstName);

            editUsersPage.UserLastNameTextBox.Clear();
            editUsersPage.UserLastNameTextBox.SendKeys(userNewLastName);

            editUsersPage.SubmitButton.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains($"{userNewFirstName} {userNewLastName}"));
            Assert.IsFalse(userNames.Contains($"{userOriginalFirstName} {userOriginalLastName}"));

            // Cleanup
            DeleteUser(userNewFirstName, userNewLastName);
        }

        private UsersPage CreateUser(string userFirstName, string userLastName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addUserPage = new AddUsersPage(Driver);

            addUserPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUserPage.UserLastNameTextBox.SendKeys(userLastName);
            addUserPage.SubmitButton.Click();
            return page;
        }

        private void DeleteUser(string firstName, string lastName)
        {
            var page = new UsersPage(Driver);
            try
            {
                IWebElement deleteLink = page.GetDeleteLink(firstName, lastName);
                deleteLink.Click();

                Driver.SwitchTo().Alert().Accept();
            }
            catch
            {
                // Left blank because this method is only for test cleanup,
                // and cleanup should not interfer with the tests.
            }
        }
    }
}
