using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.pageObjects
{
    internal class ProductsPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;
        public IList<IWebElement> getCards() { return cards; }
        
        By cardTitleLocator = By.CssSelector(".card-title a");
        public By getCardTitle() { return cardTitleLocator; }

        By addToCartButton = By.CssSelector(".card-footer button");
        public By addToCartBtn() { return addToCartButton; }

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkOutButton;
        public CheckOutPage checkOut() { checkOutButton.Click(); return new CheckOutPage(driver); }

        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void waitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.PartialLinkText("Checkout"))));
        }
    }
}
