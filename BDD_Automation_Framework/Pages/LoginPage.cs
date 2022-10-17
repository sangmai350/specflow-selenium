using OpenQA.Selenium;
using BDD_Automation.Context;

namespace BDD_Automation.Pages
{
    public class LoginPage : BasePage
    {
        private Context.WebDriver driver;
        private By txtUserName => By.Name("uid");
        private By txtPassword => By.Name("password");
        private By btnLogin => By.Name("btnLogin");
        private By loginHeader => By.CssSelector(".user-login__tab");

        //public LoginPage(Driver driver, Assert assert) : base(driver, assert) { }

        public LoginPage(Context.WebDriver _driver)
        {
            this.driver =_driver;
        }

        public void Login(string userName, string password)
        {           
            driver.SendKeys(txtUserName, userName, "Enter text in UserName");
            driver.SendKeys(txtPassword, password, "Enter text in password");                    
        }

        public HomePage ClickLogin()
        {         
           driver.FindElement(btnLogin).Click();
            return new HomePage(driver);
        }

        public void VerifyLoginPage()
        {
            try
            {
                driver.WaitForCondition(d => d.FindElement(loginHeader).Displayed, "Wait for Login tab to appear");
            }
            catch
            {
                Assert.Fail("Login Page was not loaded");
            }
        }
    }
}
