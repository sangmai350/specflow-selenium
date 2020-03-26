using BDD_Automation.Context;
using System;
using TechTalk.SpecFlow;


namespace BDD_Automation.Hooks
{
    [Binding]
    public sealed class TestScenariosHooks
    {
        
        private readonly WebDriver webDriver;
        Logging.Logger log = new Logging.Logger();
        String FeatureTitle = FeatureContext.Current.FeatureInfo.Title;
        String ScenarioTitle = ScenarioContext.Current.ScenarioInfo.Title;
        public TestScenariosHooks(WebDriver driver)
        {
            webDriver = driver;
        }
        [BeforeTestRun]
        public static void InitializeReport()
        {
        }
        public static void SetFolderPermission(string folderPath)
        {
        }


        [AfterTestRun]
        public static void TearDownReport()
        {
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
        }
        [AfterStep]
        public void InsertReportingSteps()
        {
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            log.Info("[SCENARIO] ---------------" + FeatureTitle + " - " + ScenarioTitle + "---------------");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            
            webDriver._driver.Quit();
            webDriver._driver.Dispose();
            //try
            //{
            //    var status = TestContext.CurrentContext.Result.Outcome.Status;
            //    var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            //    Status logstatus;

            //    switch (status)
            //    {
            //        case TestStatus.Failed:
            //            logstatus = Status.Fail;
            //            break;
            //        case TestStatus.Inconclusive:
            //            logstatus = Status.Warning;
            //            break;
            //        case TestStatus.Skipped:
            //            logstatus = Status.Skip;
            //            break;
            //        default:
            //            logstatus = Status.Pass;
            //            break;
            //    }

            //    ExtentTestManager.GetTest().Log(logstatus, "Test ended with status: " + logstatus + stacktrace);
            //}
            //catch { }
            //ExtentManager.Instance.Flush();
        }
    }
}
