using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace SeleniumFramework.Utilities
{
    internal class Base
    {
        // before IWebDriver driver;
        // now adding thread to run test in parallel
        // we need to add 'driver.Value' in order to get the thread
        public ThreadLocal <IWebDriver> driver = new ThreadLocal<IWebDriver>();
        // reporting 
        public ExtentReports extent;
        public ExtentTest test; // screenshots

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Luis Rojas");
        }

        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            TestContext.Progress.WriteLine("Initializing SetUp");

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value = new ChromeDriver();
            // Implicit wait can be declared globaly

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();

            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [TearDown]
        public void CloseBrowser()
        {            
            // adding implementation for reporting test case failed
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            
            if(status == TestStatus.Failed)
            {
                test.Fail("Test has failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logthace" + stackTrace);
            }

            TestContext.Progress.WriteLine("Finishing");
            
            extent.Flush(); // release all objects 
            driver.Value.Quit();
        }
        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName) {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
