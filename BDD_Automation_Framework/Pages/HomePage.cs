using OpenQA.Selenium;
using BDD_Automation.Context;

namespace BDD_Automation.Pages
{
    public class HomePage : BasePage
    {
        private By hmbrgrMenu => By.CssSelector("marquee.heading3");
        private By txtSrUsName => By.Id("searchByParamInput");

        private Context.WebDriver driver;

        public HomePage(Context.WebDriver _driver)
        {
            this.driver = _driver;
        }

        public void VerifyHomePage()
        {
            try
            {
                driver.WaitForCondition(d => d.FindElement(hmbrgrMenu).Displayed, "Wait for Hamberger menu to appear");
            }
            catch
            {
                Assert.Fail("Home Page was not loaded");
            }
        }
    }
}
