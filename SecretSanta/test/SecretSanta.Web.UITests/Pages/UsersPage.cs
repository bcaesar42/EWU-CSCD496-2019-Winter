using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public AddUsersPage AddUsersPage => new AddUsersPage(Driver);

        public List<string> UserNames
        {
            get
            {
                ReadOnlyCollection<IWebElement> elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        string text = x.Text;
                        if (text.EndsWith(" Edit Delete"))
                        {
                            text = text.Substring(0, text.Length - " Edit Delete".Length);
                        }
                        return text;
                    })
                    .ToList();
            }
        }

        public IWebElement GetDeleteLink(string userFirstName, string userLastName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick")
                              .EndsWith($"{userFirstName} {userLastName}?')"));
        }

        public IWebElement GetEditLink(string userFirstName, string userLastName)
        {
            ReadOnlyCollection<IWebElement> editLinks =
                Driver.FindElements(By.CssSelector("li a[href^=\"/Users/Edit/\"].button"));

            IWebElement deleteLink = GetDeleteLink(userFirstName, userLastName);
            string userId = deleteLink.GetCssValue("href").Split('/').Last<string>();

            return editLinks.Single(x => x.GetAttribute("href")
                              .EndsWith($"/Users/Edit/{userId}"));
        }

        public string GetUserId(string userFirstName, string userLastName)
        {
            IWebElement deleteLink = GetDeleteLink(userFirstName, userLastName);
            return deleteLink.GetCssValue("href").Split('/').Last<string>();
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
