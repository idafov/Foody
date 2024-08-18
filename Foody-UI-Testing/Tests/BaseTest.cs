using Foody_UI_Testing.Pages;
using Foody_UI_Testing.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Foody_UI_Testing
{
    public class BaseTest
    {
        public IWebDriver driver;

        public static string lastCreatedFood;
        public static string lastCreatedDescription;

        public LoginPage loginPage;
        public AddFoodPage addFoodPage;
        public BasePage basePage;
        
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
            addFoodPage = new AddFoodPage(driver);
            basePage = new BasePage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}