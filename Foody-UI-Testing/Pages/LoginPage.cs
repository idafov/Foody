using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody_UI_Testing.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
                
        }

        public string Url = BaseUrl + "/User/Login";
        public string username = "softuni18thaugust";
        public string password = "123456";


        public IWebElement UserNameField => driver.FindElement(By.Id("username"));
        public IWebElement PasswordField => driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//div[@class='text-center pt-1 mb-5 pb-1']//button"));


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void LoginValidCredentials()
        {
            UserNameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
