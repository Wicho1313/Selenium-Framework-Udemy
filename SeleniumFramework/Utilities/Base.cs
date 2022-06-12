using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;

namespace SeleniumFramework.Utilities
{
    internal class Base
    {
        // before IWebDriver driver;
        // now adding thread to run test in parallel
        // we need to add 'driver.Value' in order to get the thread
        public ThreadLocal <IWebDriver> driver = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Initializing SetUp");

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value = new ChromeDriver();
            // Implicit wait can be declared globaly

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            driver.Value.Manage().Window.Maximize();

            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Value.Quit();
        }
    }
}
