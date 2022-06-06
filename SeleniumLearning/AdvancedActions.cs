using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    internal class AdvancedActions
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

            
        }
        [Test]
        public void hover_Test_Actions()
        {
            driver.Url = "https://rahulshettyacademy.com/#/index";

            Actions a = new Actions(driver);

            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();// Hover
            a.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();
        }
        [Test]
        public void drag_drop_Test_Actions()
        {
            driver.Url = "https://demoqa.com/droppable/";
            Actions a = new Actions(driver);

            a.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();
        }
        [Test]
        public void frames_Test_Actions()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            // scroll
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));    
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            // id, name, index
            driver.SwitchTo().Frame("courses-iframe");// switching to the frame 
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            driver.SwitchTo().DefaultContent();// switching back out of frame
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Quit();
        }
    }
}
