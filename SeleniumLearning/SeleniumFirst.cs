using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class SeleniumFirst
    {
        IWebDriver driver;
        [SetUp]
        public void StartChromeBrowser()
        {
            TestContext.Progress.WriteLine("Initializing SetUp");
            //Methods - geturl, click, etc
            //chromebrowser.exe on chrome browser
            //94 ,exe (94)
            //WebDriverManager -()

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver();
            //driver.Manage().Window.Maximize();

            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new EdgeDriver();
            //driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            //driver.Url = "https://rahulshettyacademy.com/#/index";
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //driver.Close(); // 1 window is goinf to cles
            //driver.Quit(); //all tabs and windows is closed
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
        }
    }
}
