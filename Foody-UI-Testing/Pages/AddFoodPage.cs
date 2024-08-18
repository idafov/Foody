using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody_UI_Testing.Pages
{
    public class AddFoodPage : BasePage
    {

        public AddFoodPage(IWebDriver driver) : base(driver) 
        { 
        }

        public string Url = BaseUrl + "/Food/Add";

        public IWebElement AddFood => driver.FindElements(By.XPath("//li[@class='nav-item']//a[@class='nav-link']"))[0];
        public IWebElement FoodNameField => driver.FindElement(By.Id("name"));
        public IWebElement FoodDescriptionField => driver.FindElement(By.Id("description"));
        public IWebElement FoodUrlField => driver.FindElement(By.Id("url"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

        public IWebElement GeneralErrorMsg => driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li"));
        public IWebElement FoodNameErrorMsg => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[0];
        public IWebElement FoodDescriptionErrorMsg => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[1];
                
        
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void AddFoodInvalidData()
        {
            AddFood.Click();
            FoodNameField.SendKeys("");
            FoodDescriptionField.SendKeys("");
            FoodUrlField.SendKeys("");
            AddButton.Click();
        }

        public void AddFoodWithValidData(string foodName, string foodDescription)
        {
            FoodNameField.SendKeys(foodName);

            FoodDescriptionField.SendKeys(foodDescription);

            AddButton.Click();
        }

        public void AssertErrorMsgs()
        {
            Assert.True(GeneralErrorMsg.Text.Equals("Unable to add this food revue!"));
            Assert.True(FoodNameErrorMsg.Text.Equals("The Name field is required."));
            Assert.True(FoodDescriptionErrorMsg.Text.Equals("The Description field is required."));
        }
    }
}
