using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Quit();
        }
    }
}
