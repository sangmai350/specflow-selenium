using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BDD_Automation.Context;
using BDD_Automation.Pages;
using BDD_Automation.Steps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using TechTalk.SpecFlow;


namespace BDD_Automation.Hooks
{
    [Binding]
    public sealed class TestScenariosHooks
    {
        
        private readonly WebDriver webDriver;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static string path;
        public TestScenariosHooks(WebDriver driver)
        {
           
            webDriver = driver;
            
        }
        [BeforeTestRun]
        public static void InitializeReport()
        {
            path = Path.Combine(Path.GetDirectoryName(new Uri(typeof(TestScenariosHooks).Assembly.CodeBase).LocalPath), "ExtentReports");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
                SetFolderPermission(path);
            }

            var htmlreport = new ExtentHtmlReporter(path);
            htmlreport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlreport);
        }
        public static void SetFolderPermission(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            var directorySecurity = directoryInfo.GetAccessControl();
            var currentUserIdentity = WindowsIdentity.GetCurrent();
            var fileSystemRule = new FileSystemAccessRule(currentUserIdentity.Name,
                                                          FileSystemRights.FullControl,
                                                          InheritanceFlags.ObjectInherit |
                                                          InheritanceFlags.ContainerInherit,
                                                          PropagationFlags.None,
                                                          AccessControlType.Allow);

            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }


        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
      

        }
        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo piInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = piInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " : " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                
            }
            if (TestResult.ToString()=="StepDefinitionPending")
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if(stepType == "Then")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
            }

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);

            // ExtentTestManager.CreateParentTest(GetType().Name);

            // ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.Properties.Get("Description").ToString());

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
