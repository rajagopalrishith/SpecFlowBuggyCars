using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace BuggyProject
{
    public class BasePage : TechTalk.SpecFlow.Steps
    {
        private readonly IWait<IWebDriver> _wait;
        private readonly IWebDriver _driver;
        private ReadOnlyCollection<string> _winHandle;

        protected IWebDriver Driver { get; set; }
     
        public BasePage(IWebDriver driver, IWait<IWebDriver> wait)
        {
            _wait = wait;
            Driver = driver;

        }


        public void NavigateToURL(string URL)
        {
            Driver.Navigate().GoToUrl(URL);
        }

        public IWebElement FindElementToClick(By elementLocator)
        {
            IWebElement? element = null;
            try
            {
                element = _wait.Until(d =>
                {
                    element = d.FindElement(elementLocator);
                    if (element.Enabled && element.Displayed)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Element not found");
            }
            return element;
        }


    

        public void ClickOnElement(By elementLocator) 
        {
            IWebElement element;
            int attempt = 0;
            while(attempt < 3)
            {
                try 
                
                {
                    element = FindElementToClick(elementLocator);
                    element.Click();
                    break;
                
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }

        }


        public void ClickUsingJavascript(By elementLocator)
        {
            IWebElement element = null;
           try
            {
                element = _wait.Until(d => d.FindElement(elementLocator));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The element identified by the locator " + elementLocator + " is not found in this page");
            }
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)_driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", element);

        }



        public void TypeOnElement(By bylocator, string texttoType)
        {
            IWebElement element;
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    element = FindElementToClick(bylocator);
                    //element.Clear();
                    System.Threading.Thread.Sleep(1000);
                    element.SendKeys(texttoType);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }

        }


        public void ClearElement(By elementLocator)
        {
            IWebElement element;
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    element = FindElementToClick(elementLocator);
                    element.Click();
                    element.Clear();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }

        }

        public void WaitTillJavascriptLoad()

        {
            try
            {
                _wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch(WebDriverTimeoutException)
            {
                Assert.Fail("The js in the page did not load within the default time out");
            }


        }

        public void waitTillPopupIsOpened()
        {
            try
            {
                _wait.Until(d =>
                    {
                        if (d.WindowHandles.Count > 1)
                        {
                            return true;
                        }
                        else { return false; }
                    });
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Popup window is not displayed");
            }
        }


        public List<IWebElement> FindElements(By elementLocator) 
        {
            WaitTillJavascriptLoad();
            return _driver.FindElements(elementLocator).ToList();
        
        }

        public void WaitTillElementIsClickable(By elementLocator)
        {
            IWebElement element = null;
            try
            {
                element = _wait.Until(d =>
                {
                    element = d.FindElement(elementLocator);
                    if (element.Enabled && element.Displayed)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The element identified by the locator " + element.ToString() + " is not found in the page");
            }

        }


        public bool IsElementPresent(By bylocator)
        {
            return _driver.FindElements(bylocator).Count > 0;
        }

        public void WaitTillValueMatches(By byLocator, string valueToMatch)
        {
            WaitTillElementIsClickable(byLocator);
            string currentText = "";
            try
            {
                _wait.Until(d =>
                {
                    if (d.FindElement(byLocator).GetAttribute("value").Equals(valueToMatch, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    currentText = d.FindElement(byLocator).GetAttribute("Value");
                    return false;

                });
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail("The element located by " + byLocator + " has value populated to " + currentText + " instead of ");

            }

        }

        public void WaitforElementtoMatch(By byLocator, string valueToMatch)
        {
            WaitTillElementIsClickable(byLocator);
            string currentText = "";
            try
            {
                _wait.Until(d =>
                {
                    if (d.FindElement(byLocator).Text.Equals(valueToMatch, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    currentText = d.FindElement(byLocator).Text;
                    return false;

                });
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail("The element located by " + byLocator + " has value populated to " + currentText + " instead of ");

            }

        }


        public void WaitforElementToContainText(By byLocator, string Expectedvalue)
        {
            WaitTillElementIsClickable(byLocator);
            string currentText = "";
            try
            {
                _wait.Until(d =>
                {
                    if (d.FindElement(byLocator).Text.ToLower().Contains(Expectedvalue.ToLower()))
                    {
                        return true;
                    }
                    currentText = d.FindElement(byLocator).Text;
                    return false;

                });
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail("The element located by " + byLocator + " has value populated to " + currentText + " instead of ");

            }

        }

        public void RefreshCurrentPage()
        {

            _driver.Navigate().Refresh();
        }

        public void KeyBoardTabOnElement(By bylocator, string texttoType)
        {
            IWebElement element;
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    element = FindElementToClick(bylocator);
                    element.SendKeys(Keys.Tab);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }

        }


        public void KeyBoardEnterOnElement(By bylocator, string texttoType)
        {
            IWebElement element;
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    element = FindElementToClick(bylocator);
                    element.SendKeys(Keys.Enter);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }

        }


        public string GetElementText(By bylocator)
        {
            string elementtext = "";
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    var element = FindElementToClick(bylocator);
                    elementtext = element.Text;
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }
               
            }
            return elementtext;
        }


        public string GetElementAttribute(By bylocator, string attributetoFetch)
        {
            string elementAttributeValue = "";
            IWebElement element;
            int attempt = 0;
            while (attempt < 3)
            {
                try

                {
                    element = FindElementToClick(bylocator);
                    elementAttributeValue = element.GetAttribute(attributetoFetch);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }

            }
            return elementAttributeValue;
        }

        public string GetElementValue(By bylocator) 
        {
         return GetElementAttribute(bylocator, "value");
        }

        public void SwitchToPopWindow()
        {
            waitTillPopupIsOpened();
            var currentwindowHandle = _driver.CurrentWindowHandle;
            foreach (string handle in _driver.WindowHandles)
            {
                if (!currentwindowHandle.Equals(handle))
                {
                    _driver.SwitchTo().Window(handle);
                }


            }
        }

        public void ScrollToAnElement(By bylocator)
        {
            IWebElement element = null;
            int attempt = 0;
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)_driver;
            while (attempt < 3)
            {
                try
                {
                    element = _wait.Until(d => d.FindElement(bylocator));
                    javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", element);
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    Assert.Fail("The element identified by the locator " + bylocator.ToString() + " is not found in the page");
                }
                catch (StaleElementReferenceException)
                { attempt++; }
            }
        }
        
        public void HoverOnElement(By bylocator)
        {
            IWebElement element = null;
            try
            {
                element = _wait.Until(d => d.FindElement(bylocator));
            }
            catch(WebDriverTimeoutException)
                {
                Assert.Fail("The element identified by the locator " + bylocator.ToString() + " is not found in the page");

            }
            Actions action = new Actions(_driver);
            action.MoveToElement(element).Click().Build().Perform();
        }

        public void AcceptAlert()
        {
            try
            {
                _wait.Until(d => isAlertShown(d));
                _driver.SwitchTo().Alert().Accept();
            }
            catch(NoAlertPresentException) 
            {
                Assert.Fail("No alert is displayed");
            }

        }

        bool isAlertShown(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
            }
            catch(NoAlertPresentException)
            {
                return false;

            }

            return true;

        }


        public void CollectCurrentWindowHandles()
        {
            _winHandle = _driver.WindowHandles;
        }
        

        public void SwitchBrowserWindow(int index)
        {

            _driver.SwitchTo().Window(_winHandle[index]);
        }


        public Boolean IsElementNotDisplayed(By bylocator)
        {
            Console.WriteLine("The element is present " + IsElementPresent(bylocator));
            if(IsElementPresent(bylocator))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isElementDisabled(By elementLocator)
        {
            IWebElement element = null;
            Boolean elementStatus = false;
            try
            {
                element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
                if (!element.Enabled)
                {
                    elementStatus = true;
                }
                else
                {
                    elementStatus = false;
                }
            }
            catch(WebDriverTimeoutException)
            {
                Assert.Fail("The element identified by the locator " + element.ToString() + " is not found in the page");
            }
            return elementStatus;
            }



        public bool isElementEnabled(By elementLocator)
        {
            IWebElement element = null;
            Boolean elementStatus = false;
            try
            {
                element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
                if (element.Enabled)
                {
                    elementStatus = true;
                }
                else
                {
                    elementStatus = false;
                }
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The element identified by the locator " + element.ToString() + " is not found in the page");
            }
            return elementStatus;
        }



        public bool IsElementDisplayed(By bylocator, int duration = 5) 
        {
            WebDriverWait webDriverWait = new WebDriverWait(Driver,TimeSpan.FromSeconds(duration));
            try
            {
                webDriverWait.Until(d =>
                {
                    var element = d.FindElement(bylocator);
                    if (element.Displayed && element.Enabled)
                    {

                        return true;
                    }
                    else { return false; }
                });
                return true;
            }
            catch(WebDriverTimeoutException)
            {

                return false;
            }
        
        }

        public bool IsSelected(By bylocator)
        {
            WebDriverWait webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            int attempt = 0;
            while (attempt < 3)
            {
                try
                {
                    webDriverWait.Until(d =>
                    {
                        var element = d.FindElement(bylocator);

                        if (element.Selected)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                    return true;
                }
                catch(WebDriverTimeoutException)
                {
                    return false;
                }
                catch(StaleElementReferenceException)
                {
                    attempt++;
                }
                }
            return false;
            }


        public bool SelectDropDownByText(By bylocator, string elementText)
        {
            int attempt = 0;
            while (attempt < 3)
            {
                try
                {
                    SelectElement select = new SelectElement(FindElementToClick(bylocator));
                    _wait.Until(d =>
                    {
                        select = new SelectElement(FindElementToClick(bylocator));

                        if (select.Options.Select(x => x.Text).ToList().Contains(elementText))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                   select.SelectByText(elementText);
                   Assert.AreEqual(elementText, select.SelectedOption.Text, "Select option is not set correctly");
                   break;
                
                }
                catch (WebDriverTimeoutException)
                {
                    Assert.Fail("The option " + elementText + " is found in the list of options");
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }
            }
            return false;
        }

        public void SelectDropDownByIndex(By bylocator, int index)
        {
            int attempt = 0;
            while (attempt < 3)
            {
                try
                {
                    SelectElement select = new SelectElement(FindElementToClick(bylocator));
                    _wait.Until(d =>
                        select = new SelectElement(FindElementToClick(bylocator))
                        );
                    select.SelectByIndex(index);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempt++;
                }
                catch (WebDriverTimeoutException)
                {
                    Assert.Fail("The option could not be selected");
                    break;

                }
            }
        }

        public void SelectDropDownByValue(By bylocator, string value)
        {
            IWebElement element = FindElementToClick(bylocator);
            SelectElement select = new SelectElement(element);
            select.SelectByText(value);
        }

        public IList<string> FetchAllDropDownValues(By bylocator)
        {
            List<string> dropdownoptions = new List<string>();
            IWebElement element = FindElementToClick(bylocator);
            SelectElement select;
            int selectSize;
            try
            {
                select = new SelectElement(element);
                selectSize = select.Options.Count;
            }
            catch (StaleElementReferenceException)
            {
                element = FindElementToClick(bylocator);
                select = new SelectElement(element);
                selectSize = select.Options.Count;
            }
            for(int i = 0;i <= selectSize;i++)
            {
                string optionLocator = bylocator.ToString() + "/Option["+ i + "]";
                int found = optionLocator.IndexOf(": ");
                string OptionText;
                try
                {
                    var option = _driver.FindElement(By.XPath(optionLocator.Substring(found + 1)));
                    OptionText = option.Text;
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    i--;
                    continue;
                }
                dropdownoptions.Add(OptionText);
            }

            return dropdownoptions;
        }


        public string GetSelectedValuefromDropDown(By bylocator)
        {
            IWebElement element = FindElementToClick(bylocator);
            string selectedOption;
            SelectElement select;
            try
            {
                select = new SelectElement(element);
                selectedOption = select.SelectedOption.Text;
            }
            catch (StaleElementReferenceException)
            {
                WaitTillJavascriptLoad();
                select = new SelectElement(FindElementToClick(bylocator));
                selectedOption = select.SelectedOption.Text;

            }
            return selectedOption;
        }

        public void SelectByPartialText(string textToSelect, By bylocator)
        {
            SelectElement select = new SelectElement(FindElementToClick(bylocator));
            var options = select.Options.ToList();
            bool itemFound = false;
            foreach (var option in options)
            {
                if (option.Text.ToUpper().Contains(textToSelect.ToUpper()))
                {
                    string fullText = option.Text;
                    SelectDropDownByText(bylocator, fullText);
                    itemFound = true;
                    break;

                }
            }
            if (!itemFound)
            {
                Assert.Fail("item with the Text " + textToSelect + " is not found in the list of options");
            }

        }
    }
}
