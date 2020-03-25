﻿using OpenQA.Selenium;
using BDD_Automation.Context;

namespace BDD_Automation.Pages
{
    public class LoginPage : BasePage
    {
        private WebDriver driver;
        Logging.Logger log = new Logging.Logger();
        private By txtUserName => By.Name("uid");
        private By txtPassword => By.Name("password");
        private By btnLogin => By.Name("btnLogin");

        //public LoginPage(Driver driver, Assert assert) : base(driver, assert) { }

        public LoginPage(WebDriver _driver)
        {
            this.driver =_driver;
        }

        public void Login(string userName, string password)
        {
            log.Info("Login with username: " + userName + " and password: " + password);
            driver.SendKeys(txtUserName, userName, "Enter text in UserName");
            driver.SendKeys(txtPassword, password, "Enter text in password");                    
        }

        public HomePage ClickLogin()
        {
            log.Info("Click Login button");
            driver.FindElement(btnLogin).Click();
            return new HomePage(driver);
        }
    }
}
