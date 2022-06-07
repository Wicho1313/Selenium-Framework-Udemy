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
    internal class AddProductsToCart
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
        public void AddingProductsToCart()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");           
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); 
            driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach(IWebElement product in products)
            {
                // doing matching with the expected on first list
                if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }
            
            driver.FindElement(By.PartialLinkText("Checkout")).Click();// checking cart
            IList<IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));

            for(int i=0; i<checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.AreEqual(expectedProducts, actualProducts);

            driver.FindElement(By.CssSelector(".btn-success")).Click();
            driver.FindElement(By.Id("country")).SendKeys("ind");
            // finishing the purchasing 
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();

            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
            driver.FindElement(By.CssSelector("[value='Purchase']")).Click();
            String confirmTextPurchase = driver.FindElement(By.CssSelector(".alert-success")).Text;

            StringAssert.Contains("Success", confirmTextPurchase); // See if 'Success' is present on hole text confirmTextPurchase 
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
            driver.Quit();
        }
    }
}
