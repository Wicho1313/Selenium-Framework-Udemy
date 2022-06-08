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
    internal class EndToEndFlowCart : Base
    {
        

        [Test]
        public void EndToEndFlow_AddingProductsToCart()
        {
            String[] expectedProducts = {"iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            LoginPage loginPage = new LoginPage(getDriver());

            ProductsPage productsPage = loginPage.validLogin("rahulshettyacademy", "learning");
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
