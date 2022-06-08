using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumFramework.pageObjects
{
    internal class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //oage object factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;
        public IWebElement getUsername() { return username; }

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;
        public IWebElement getPassword() { return password; }

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;
        public IWebElement checkBoxLogin() { return checkBox; }

        [FindsBy(How = How.CssSelector, Using = "input[id='signInBtn']")]
        private IWebElement btnLogin;
        public IWebElement btnLogIn() { return btnLogin; }

        public ProductsPage validLogin(String user, String pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkBox.Click();
            btnLogin.Click();
            return new ProductsPage(driver);
        }
    }
}
