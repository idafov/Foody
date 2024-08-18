using Foody_UI_Testing.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody_UI_Testing.Tests
{
    public class FoodyTests : BaseTest
    {
        public static string lastCreatedFood;
        public static string lastCreatedDescription;

        [Test, Order(1)]
        public void AddFoodWithInvalidData()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();
            addFoodPage.AddFoodInvalidData();

            Assert.AreEqual(addFoodPage.Url, driver.Url);
            addFoodPage.AssertErrorMsgs();
        }
        [Test, Order(2), Repeat(4)]
        public void AddFoodWithRandomData()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();
            addFoodPage.OpenPage();

            Random random = new Random();
            int randomNum = random.Next(100,1000);

            lastCreatedFood = "Food" + randomNum;
            lastCreatedDescription = "Description " + randomNum;

            addFoodPage.AddFoodWithValidData(lastCreatedFood, lastCreatedDescription);

            Assert.AreEqual("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/", driver.Url);
            var lastFood = driver.FindElements(By.XPath("//div[@class ='p-5']//h2[@class='display-4']"));

            Assert.AreEqual(lastCreatedFood, lastFood.Last().Text);
        }

        [Test, Order(3)]
        public void EditTheLastAddedFood()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();


            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var foods = wait.Until(driver => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")));

            Assert.IsTrue(foods.Count > 0);

            var lastFoodField = foods.Last();
            var editButton = lastFoodField.FindElement(By.CssSelector("a[href*='/Food/Edit']"));


            Actions actions = new Actions(driver);
            actions.MoveToElement(editButton).Click().Perform();

            var foodNameFieldToEdit = driver.FindElement(By.XPath("//div[@class='form-outline mb-4']//input[@id='name']"));
            foodNameFieldToEdit.Click();
            foodNameFieldToEdit.SendKeys("UpdatedNAME");


            var descriptionFieldToEdit = driver.FindElement(By.XPath("//div[@class='form-outline mb-4']//input[@id='description']"));
            descriptionFieldToEdit.Click();
            descriptionFieldToEdit.SendKeys("UpdatedDESCR");

            var addButton = driver.FindElement(By.XPath("//div[@class='text-center pt-1 mb-5 pb-1']//button"));
            addButton.Click();

            var lastFood = driver.FindElements(By.XPath("//div[@class ='p-5']//h2[@class='display-4']"));

            Assert.AreEqual(lastCreatedFood, lastFood.Last().Text);

        }

        [Test, Order(4)]
        public void SearchForTheLastFood()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();

            var search = driver.FindElement(By.XPath("//input[@placeholder='Search']"));
            search.Click();
            search.SendKeys(lastCreatedFood);

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var foods = wait.Until(driver => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")));

            Assert.IsTrue(foods.Count == 1);
        }

        [Test, Order(5)]
        public void DeleteTheLastFood()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var foodsBefore = wait.Until(driver => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")));

            int foodsCountBefore = 0;
            foreach (var food in foodsBefore)
            {
                foodsCountBefore++;
            }

            var lastFoodField = foodsBefore.Last();
            var deleteBtn = lastFoodField.FindElement(By.CssSelector("a[href*='/Food/Delete']"));

            Actions actions = new Actions(driver);
            actions.MoveToElement(deleteBtn).Click().Perform();

            var waitAfter = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var foodsAfter = waitAfter.Until(driver => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")));

            int foodsCountAfter = 0;
            foreach (var food in foodsAfter)
            {
                foodsCountAfter++;
            }

            Assert.True(foodsCountBefore > foodsCountAfter);
            bool isFoodDeleted = foodsAfter.All(foods => !foods.Text.Contains(lastCreatedFood));
            Assert.IsTrue(isFoodDeleted);
        }

        [Test, Order(6)]
        public void SearchForDeletedFood()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();

            var search = driver.FindElement(By.XPath("//input[@placeholder='Search']"));
            search.Click();
            search.SendKeys(lastCreatedFood);

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var msg = driver.FindElement(By.XPath("//h2[normalize-space()='There are no foods :(']"));
            var addFoodBtn = driver.FindElement(By.CssSelector("a[href*='/Food/Add']"));

            Assert.AreEqual("There are no foods :(", msg.Text);
            Assert.IsTrue(addFoodBtn.Displayed);
        }

        [Test, Order(7)]
        public void DeleteEachFood()
        {
            loginPage.OpenPage();
            loginPage.LoginValidCredentials();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var foods = wait.Until(driver => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")));

            foreach (var food in foods)
            {
                var deleteEach = driver.FindElement(By.CssSelector("a[href*='/Food/Delete']"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(deleteEach).Click().Perform();
            }
            var totalFoods = wait.Until(driver => driver.FindElement(By.XPath("//div[@class='row gx-5 align-items-center']//h2")));

            Assert.AreEqual("There are no foods :(", totalFoods.Text);
        }
    }
}
