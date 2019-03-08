using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class GroupPageTests
    {
        private const string RootUrl = "https://localhost:44323/";
        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void CanAddGroup()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            var homePage = new HomePage(Driver);
            homePage.GroupsLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));
        }
    }

    public class HomePage
    {
        public IWebDriver Driver { get; }

        //public IWebElement GroupsLink => Driver.FindElement(By.CssSelector("a[href=\"/Groups\""));
        public IWebElement GroupsLink => Driver.FindElement(By.LinkText("Groups"));

        public HomePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentException(nameof(driver));
        }
    }

    public class GroupsPage
    {
        public static string Slug { get; } = "blah";

        public IWebDriver Driver { get; }

        //public IWebElement GroupsLink => Driver.FindElement(By.CssSelector("a[href=\"/Groups\""));
        public IWebElement GroupsLink => Driver.FindElement(By.LinkText("Groups"));

        public GroupsPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentException(nameof(driver));
        }
    }
}
