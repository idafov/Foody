using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody_UI_Testing.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected static readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }

        public IWebElement FoodyLogo => driver.FindElement(By.ClassName("navbar-brand"));
        public IWebElement SignUp => driver.FindElement(By.LinkText("Sign Up"));
        public IWebElement LogIn => driver.FindElement(By.LinkText("Log In"));

        public IWebElement FoodList => driver.FindElement(By.XPath("//div[@class ='p-5']//h2[@class='display-4']"));
    }
}
