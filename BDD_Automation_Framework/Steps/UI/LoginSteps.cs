using BDD_Automation.Context;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BDD_Automation.Steps
{
    [Binding]
   public class LoginSteps : BaseSteps
    {


        private readonly WebDriver webDriver;

        public LoginSteps(WebDriver driver) : base(driver)
        {
            webDriver = driver;
        }
     
        [Given(@"I have navigated to the application")]
        public void GivenIHaveNavigatedToTheApplication()
        {
            webDriver._driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);

        }

        [Given(@"I have typed username and password")]
        public void GivenIHaveTypedUsernameAndPassword()
        {
            loginPage.Login(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
        }

        [When(@"I click login button")]
        public void WhenIClickLoginButton()
        {
            
            loginPage.ClickLogin();
        }
     

        [Then(@"I should see the HomePage page")]
        public void ThenIShouldSeeTheHomePagePage()
        {
           homePage.VerifyHomePage();
        }

        [Then(@"I should see the Login page")]
        public void ThenIShouldSeeTheLoginPage()
        {
            loginPage.VerifyLoginPage();
        }


    }
}
