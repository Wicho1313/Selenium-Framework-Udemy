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
    internal class windowHandle
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

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void newWindowHandle()
        {
            // store the parent window 
            String parentWindowId = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();// clicking separate window

            Assert.AreEqual(2, driver.WindowHandles.Count);

            driver.SwitchTo().Window(driver.WindowHandles[1]);// window is stored on windowHandless property

            // getting email from the text of child window
            String emailText = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = emailText.Split("at");
            String[]trimmedText = splittedText[1].Trim().Split(" ");

            String emailExpected = "mentor@rahulshettyacademy.com";

            Assert.AreEqual(emailExpected, trimmedText[0]);

            // switching back to parent window

            driver.SwitchTo().Window(parentWindowId);
            // using string stored text from child window into parent window
            driver.FindElement(By.Id("username")).SendKeys(trimmedText[0]);
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Quit(); // quit close all parent and windows oppened 
        }
    }
}