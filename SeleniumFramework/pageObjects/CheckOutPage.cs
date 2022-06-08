using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.pageObjects
{
    internal class CheckOutPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList <IWebElement> checkOutCards;
        public IList<IWebElement> getCheckOutCards() { return checkOutCards; }

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement checkoutButton;
        public SelectCountryPage checkoutButtonBilling() { checkoutButton.Click(); return new SelectCountryPage(driver); }
        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
