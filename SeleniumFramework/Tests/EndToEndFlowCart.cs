using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumFramework.pageObjects;
using SeleniumFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    internal class EndToEndFlowCart : Base
    {
        
        [Test]
        [Parallelizable(ParallelScope.All)]
        [TestCase("rahulshettyacademy", "learning", "iphone X", "Blackberry")]// adding TDD - Test Data Driven
        [TestCase("hulshecademy", "learn", "iphone X", "Blackberry")]
        public void EndToEndFlow_AddingProductsToCart(String username, string pass, String expectedProd1, String expecctedProd2)
        {
            String[] expectedProducts = {expectedProd1, expecctedProd2};
            String[] actualProducts = new String[2];
            LoginPage loginPage = new LoginPage(getDriver());

            ProductsPage productsPage = loginPage.validLogin(username, pass);
            productsPage.waitForPageDisplay();

            IList<IWebElement> products = productsPage.getCards();
            foreach (IWebElement product in products)
            {
                // doing matching with the expected on first list                
                if (expectedProducts.Contains(product.FindElement(productsPage.getCardTitle()).Text))
                {
                    product.FindElement(productsPage.addToCartBtn()).Click();
                }
            }
            
            CheckOutPage checkOutPage = productsPage.checkOut();

            IList<IWebElement> checkoutCards = checkOutPage.getCheckOutCards();

            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            SelectCountryPage selectCountryPage = checkOutPage.checkoutButtonBilling();
            selectCountryPage.countryDropdownSelect().SendKeys("Ind");
            // finishing the purchasing 
            selectCountryPage.waitForElementIsVisible();
            selectCountryPage.selectIndiaCountry();
            selectCountryPage.selectCheckboxPurchase();

            selectCountryPage.purchaseProducts();
            String confirmedPurchaseText = selectCountryPage.confirmedPurchaseText();
            
            StringAssert.Contains("Success", confirmedPurchaseText); // See if 'Success' is present on hole text confirmTextPurchase 
        }
    }
}
