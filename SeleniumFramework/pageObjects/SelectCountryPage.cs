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
    internal class SelectCountryPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryDropdown;
        public IWebElement countryDropdownSelect() { return countryDropdown; }
        
        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement indiaCountry;
        public void selectIndiaCountry() { indiaCountry.Click(); }                      

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement checkBoxPurchase;
        public void selectCheckboxPurchase() { checkBoxPurchase.Click(); }

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement purchaseButton;
        public void purchaseProducts() { purchaseButton.Click(); }

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement textConfirmPurchaseWebElement;
        public String confirmedPurchaseText() 
        { 
            String textConfirmedPurchase = textConfirmPurchaseWebElement.Text;
            return textConfirmedPurchase;
        }


        public SelectCountryPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public void waitForElementIsVisible()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }
    }
}
