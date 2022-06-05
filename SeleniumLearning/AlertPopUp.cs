using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    internal class AlertPopUp
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Initializing SetUp");

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            // Implicit wait can be declared globaly

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            driver.Manage().Window.Maximize();

            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }
        [Test]
        public void TestAlert()
        {
            String name = "Luis";
            driver.FindElement(By.CssSelector("#name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();

            String alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();// .Dismiss(); - .SendKeys("hello");


            StringAssert.Contains(name, alertText);


        }
        [Test]
        public void SuggestiveDropdowns()
        {
            String selectorCssSuggestions = ".ui-menu-item div";
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");

            IWebElement suggestions = (IWebElement)driver.FindElement(By.CssSelector(selectorCssSuggestions));
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(suggestions));

            // Thread.Sleep(3000);
            IList <IWebElement> options = driver.FindElements(By.CssSelector(selectorCssSuggestions));

            foreach(IWebElement option in options)
            {
                if (option.Text.Equals("India"))
                {
                    option.Click();
                }
                
            }
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Quit();
        }
    }
}
