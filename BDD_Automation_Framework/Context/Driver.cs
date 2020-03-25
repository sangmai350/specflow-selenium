using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Protractor;

namespace BDD_Automation.Context
{
    public class Driver
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private ExtentTest _reporter;
        private string _path;
        private IJavaScriptExecutor _executor;
        private RandomHelper _random;
        private NgWebDriver ngDriver;
        private IWebDriver driver;
        //private ExtentTest extentTest;
        private object path;

        public By FindElementByText(By by)
        {
            throw new NotImplementedException();
        }

        public Driver(IWebDriver driver, ExtentTest reporter, string path)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["TIMEOUT"])));
            _reporter = reporter;
            _path = path;
            _executor = _driver as IJavaScriptExecutor;
            _random = new RandomHelper();
            ngDriver = new NgWebDriver(_driver);
        }


        public static object Navigate()
        {
            throw new NotImplementedException();
        }

        public void WaitForCondition(Action<IWebDriver> condition, string comment)

        {
            try
            {
                _wait.Until(d =>
                {
                    try
                    {
                        condition.Invoke(d);

                        return true;
                    }

                    catch
                    {
                        return false;
                    }
                });
            }
            catch
            {
               // TakeScreenshot(Status.Error, comment);
                condition.Invoke(_driver);
            }

        }
        public void WaitForCondition(Action<IWebDriver> condition, TimeSpan @override, string comment)
        {
            _wait.Timeout = @override;
            WaitForCondition(condition, comment);
            _wait.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["TIMEOUT"]));
        }

        public void WaitForCondition(Func<IWebDriver, bool> condition, string comment)
        {
            try
            {
                _wait.Until(d =>
                {
                    try
                    {
                        return condition.Invoke(d);
                    }
                    catch
                    {
                        return false;
                    }
                });

            }

            catch
            {
                //TakeScreenshot(Status.Error, comment);
                condition.Invoke(_driver);
            }

        }

        public void WaitForCondition(Func<IWebDriver, bool> condition, TimeSpan @override, string comment)
        {
            _wait.Timeout = @override;
            WaitForCondition(condition, comment);
            _wait.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["TIMEOUT"]));
        }
        public void SendKeys(By locator, string text, string comment)
        {
            WaitForCondition(d =>
            {
                var element = d.FindElement(locator);
                try
                {
                    element.Clear();
                }
                catch
                {

                }
                element.SendKeys(text);
            }, comment);
        }

		public void SendKeysDelayed(By locator, string text, string comment)
        {
            WaitForCondition(d =>
            {
                var element = d.FindElement(locator);
                try
                {
                    element.Clear();
                }
                catch
                {

                }
                foreach (char character in text)
				{
					element.SendKeys(character.ToString());
					Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["SENDKEYS_DELAY"]));
				}
            }, comment);
        }

        public IEnumerable<IWebElement> FindElements(By locator)
        {
            var elements = _driver.FindElements(locator);
            return elements;
        }
        public IWebElement FindElement(By locator)
        {
            var element = _driver.FindElement(locator);
            return element;
        }
        public void SendKeysToAll(By locator, string text, string comment)
        {
            var elements = _driver.FindElements(locator);
            foreach (var element in elements)
            {
                element.SendKeys("1000");
                //TakeScreenshot(Status.Info, comment);
            }
        }
        public void SelectFromDropdown(By locator, string text, string comment)
        {
            WaitForCondition(d =>
            {
                new SelectElement(d.FindElement(locator)).SelectByText(text);
            }, comment);
        }
        public void SelectFromDropdown(By locator, int index, string comment)
        {
            WaitForCondition(d =>
            {
                new SelectElement(d.FindElement(locator)).SelectByIndex(index);
            }, comment);
        }

        public void SelectFromOptions(By locator, int index) 
        {
            WaitForCondition(d =>
            {
                var optn = d.FindElements(locator);
                optn[index].Click();
            }, "");
        }

        public void SelectFromVisibleDropdown(By locator, int index, string comment)
        {
            WaitForCondition(d =>
            {
                var element = d.FindElements(locator).First(e => e.Displayed);
                new SelectElement(element).SelectByIndex(index);
            }, comment);
        }

        public void Click(By locator, string comment)
        {
            WaitForCondition(d =>
            {
                d.FindElement(locator).Click();
            }, comment);
        }
        public void Sleep()
        {
            Thread.Sleep(1000);
        }
        public bool IsElementVisible(IWebElement element)
        {
            var location = element.Location;
            var windowSize = _driver.Manage().Window.Size;
            return location.Y < windowSize.Height - 100;
        }
        public void ClickAll(By locator, string comment)
        {
            var elements = _driver.FindElements(locator);
            foreach (var element in elements)
            {
                element.Click();
                //TakeScreenshot(Status.Info, comment);
            }
        }
        public void ClickVisible(By locator, string comment)
        {
            _driver.FindElements(locator).First(e => e.Displayed).Click();
            //TakeScreenshot(Status.Info, comment);
        }
        public void AssertDisplayed(By locator, Assert assert, string comment)
        {
            WaitForCondition(d =>
            {
                assert.IsTrue(d.FindElement(locator).Displayed, comment);
            }, comment);
        }
        public bool IsDisplayed(By locator, string comment)
        {
            try
            {
                var element = _driver.FindElement(locator);

                return element.Displayed;
            }
            catch
            {
                return false;
            }
        }
        public bool IsEnabled(By locator, string comment)
        {
            try
            {
                var elements = _driver.FindElements(locator);
                return elements.Any(e => e.Enabled);
            }
            catch
            {
                return false;
            }
        }
        public string GetAttributeValue(By locator, string attribute)
        {
            return _driver.FindElement(locator).GetAttribute(attribute);
        }
        public string GetElementText(By locator)
        {
            return _driver.FindElement(locator).Text;
        }
        public void ClickByJavascript(By locator, string comment)
        {
            WaitForCondition(d =>
            {
                var element = _driver.FindElement(locator);
                ClickByJavascript(element);
            }, comment);
        }
        public void ClickByJavascript(IWebElement element)
        {
            _executor.ExecuteScript("arguments[0].click()", element);

        }

        public void ScrollUp(int n=1)
        {
          for (int i=0; i <=n; i++)
            {
                _executor.ExecuteScript("window.scrollBy(0,-window.innerHeight)");
            }          
        }
        public void ScrollDown()
        {
            _executor.ExecuteScript("window.scrollBy(0,window.innerHeight)");
        }

        public object EnterByJavascript(string id, string text)
        {
            return _executor.ExecuteScript("document.getElementById('" + id + "').value = '" + text + "'");
            //_executor.ExecuteScript("document.getElementById('city-source').removeAttribute('ng-readonly')");
            //_executor.ExecuteScript("document.getElementById('city-source').removeAttribute('ng-disabled')");       
            //return _executor.ExecuteScript("document.getElementById('city-source').value = '15143'");
            ////WebElement wb = driver.findElement(By.name("phone"));
            ////JavascriptExecutor jse = (JavascriptExecutor)driver;
            ////jse.executeScript("arguments[0].value='(222)222-2222';", wb);
            ////jse.executeScript("document.getElementById('ssn').value='555-55-5555';");
        }
        public void GetRepeaterButton(string name, int index, By locator)
        {
            //ngDriver.FindElements(NgBy.Repeater(name)).ElementAt(index).FindElement(locator).Click();
            _wait.Until(d =>
            {
                try
                {
                    ngDriver.FindElements(NgBy.Repeater(name)).ElementAt(index).FindElement(locator).Click();
                    return true;
                }

                catch
                {
                    return false;
                }
            });
        }

        public void GetNgDropDown(string ngModelname,By drpdwlist, By txtBox)
        {
            NgWebElement element =  ngDriver.FindElement(NgByModel.Name(ngModelname));
            string text = element.Text.Replace("\r\n", ",");
            string[] items = text.Split(',');
            _driver.FindElement(drpdwlist).Click();
            _driver.FindElement(txtBox).SendKeys(items[1] + Keys.Enter);
        }

        public void GetNgSelectBox(string ngModelname,By drpdwlist,int index) 
        {
            NgWebElement element = ngDriver.FindElement(NgByModel.Name(ngModelname));
            string text = element.Text.Replace("\r\n", ",");
            string[] items = text.Split(',');
            _driver.FindElement(drpdwlist).Click();
            _driver.FindElement(drpdwlist).SendKeys(items[index] + Keys.Enter);
            _driver.FindElement(drpdwlist).SendKeys(Keys.Enter);
        }

        public void CheckForDrpdwnList(By input, By drpList,string text)
        {
            _driver.FindElement(input).SendKeys(text);
            for (var i = 0; i < text.Length; i++)
            {
                if (!IsEnabled(drpList, ""))
                {
                    _driver.FindElement(input).SendKeys(Keys.Backspace);
                }
                else if (IsEnabled(drpList,""))
                {
                    break;
                }
                else if (i == text.Length-1) 
                {
                    _driver.FindElement(input).SendKeys(text);
                    continue;
                }
            }
        }

        public IWebElement GetRepeaterInfo(string name, int index, By locator)
        {
            return ngDriver.FindElements(NgBy.Repeater(name)).ElementAt(index).FindElement(locator);
        }
        public int GetRepeaterCount(string name)
        {
            return ngDriver.FindElements(NgBy.Repeater(name)).Count;
        }
        public void SwitchToFrame(string id)
        {
            _driver.SwitchTo().Frame(id);
        }
        public void GetNgModelControl(string ngmodel, string eventtype)
        {
            ngDriver.FindElement(NgBy.Model(ngmodel)).Click();
        }
        public int GetNumRowsOfGrid(string id)
        {
            return Convert.ToInt32(_executor.ExecuteScript("return jQuery('#" + id + "').data('kendoGrid').dataSource.data().length"));
        }
        public string GetGridValue(string id, int index, string value)
        {
            return Convert.ToString(_executor.ExecuteScript("return jQuery('#" + id + "').data('kendoGrid').dataSource.data()[" + index + "]." + value));
        }
        public int GetGridRowWithCellText(string id, string text)
        {
            int row;
            int col;
            var result = -1;
            Thread.Sleep(1000);
            var rowCount = Convert.ToInt32(_executor.ExecuteScript("return jQuery('#" + id + "').data('kendoGrid').items().length"));
            var columnCount = Convert.ToInt32(_executor.ExecuteScript("return jQuery('#" + id + "').data('kendoGrid').columns.length"));
            for (row = 1; row <= rowCount; row++)
            {
                for (col = 1; col <= columnCount; col++)
                {
                    var cell = _executor.ExecuteScript("return jQuery('#" + id + " tr:nth-child(" + row + "):visible td:nth-child(" + col + "):visible')");
                    var textActual = _executor.ExecuteScript("return jQuery('#" + id + " tr:nth-child(" + row + "):visible td:nth-child(" + col + "):visible').text()");
                    if ((string)textActual == text)
                    {
                        result = row;
                        return Convert.ToInt32(result);
                    }
                }
            }
            return result;
        }
        public T GetGridRowById<T>(string gridId, string rowId, string id, int index = 1)
        {
            var numRows = GetNumRowsOfGrid(gridId);
            var numTimesFound = 0;
            for (int i = 1; i <= numRows; i++)
            {
                if (GetGridValue(gridId, i - 1, rowId) == id)
                {
                    numTimesFound++;
                    if (numTimesFound >= index)
                    {
                        var json = _executor.ExecuteScript("return JSON.stringify(jQuery('#" + gridId + "').data('kendoGrid').dataSource.data()[" + (i - 1) + "])") as string;
                        //return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            return default(T);
        }
        public void SwitchWindow()
        {
            foreach (var handle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(handle);
            }
        }
        public void CloseWindow()
        {
            _driver.Close();
            SwitchWindow();
        }
        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }
        public void AssertTitle(string title, Assert assert)
        {
            WaitForCondition(d => assert.AreEqual(title, d.Title), "Assert title equals '" + title + "'.");
        }
        public void AssertText(By locator, string text, Assert assert)
        {
            assert.AreEqual(text, _driver.FindElement(locator).Text);
        }
        public void AssertTextContains(By locator, string text, Assert assert, string message)
        {
            assert.IsTrue(_driver.FindElement(locator).Text.Contains(text), message);
        }
        public string GetText(By locator)
        {
            return _driver.FindElement(locator).Text;
        }
        //public void TakeScreenshot(Status status, string comment)
        //{
        //    if (ExtentTestManager.GetTest() != null)
        //    {
        //        var screenshot = (_driver as ITakesScreenshot).GetScreenshot();
        //        var filename = _random.RandomString() + ".png";
        //        var path = Path.Combine(_path, filename);
        //        screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
        //        //_reporter.Log(status, comment, MediaEntityBuilder.CreateScreenCaptureFromPath(filename).Build());
        //        ExtentTestManager.GetTest().Log(status, comment, MediaEntityBuilder.CreateScreenCaptureFromPath(filename).Build());

        //    }
        //}
        public static By FindElementByText(string text)
        {
            return By.XPath("//*[text() = '" + text + "']");
        }
        public static By FindElementByTextContains(string text)
        {
            return By.XPath("//*[contains(text(), '" + text + "')]");
        }
        public static By FindElementByPropertyContains(string property, string text)
        {
            return FindElementByTagAndPropertyContains("*", property, text);
        }
        public static By FindElementByPropertyEquals(string property, string text)
        {
            return By.XPath("//*[@" + property + " = '" + text + "']");
        }
        public static By FindElementByTagAndPropertyContains(string tag, string property, string text)
        {
            return By.XPath("//" + tag + "[contains(@" + property + ", '" + text + "')]");
        }
        public static By FindElementByTagAndText(string tag, string text)
        {
            return By.XPath("//" + tag + "[text() = '" + text + "']");
        }
    }
}