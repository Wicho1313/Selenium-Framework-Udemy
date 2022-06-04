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
    internal class Selectors
    {
        //Xpath, Css, id, classname, name, tagname, linktext
        IWebDriver driver;
        [SetUp]
        public void StartChromeBrowser()
        {
            TestContext.Progress.WriteLine("Initializing SetUp");

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            // Implicit wait can be declared globaly
            //3 seconds 
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void LocatorsIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.FindElement(By.Name("password")).SendKeys("123456");

            // css selector 
            // tagname[attribute='value']
            // driver.FindElement(By.CssSelector("input[value='Sing In']")).Click();
            
            // clicking on checkbox inside more HTML tags
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            // xPath
            // //tagname[@attribute = 'value']
            var buttonSignIn = driver.FindElement(By.XPath("//input[@id='signInBtn']"));
            buttonSignIn.Click();

            //classname
            //Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(buttonSignIn, "Sign In"));
            String errMsg = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errMsg);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
            // validate url of the link text
            Assert.AreEqual(expectedUrl, hrefAttr);

            



        }

        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Close();
        }
    }
}
