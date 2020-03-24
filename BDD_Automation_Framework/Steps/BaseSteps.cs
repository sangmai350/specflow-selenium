using BDD_Automation.Context;
using BDD_Automation.Pages;

namespace BDD_Automation.Steps
{
    public class BaseSteps 
    {
        public HomePage homePage { get; set; }
        public LoginPage loginPage { get; set; }
        private readonly WebDriver webDriver;
        public BaseSteps(WebDriver driver)
        {
            webDriver = driver;
            homePage = new HomePage(webDriver);
            loginPage = new LoginPage(webDriver);
        }

    }
}
