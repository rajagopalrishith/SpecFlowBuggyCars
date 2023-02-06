using BoDi;
using BuggyProject.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlowBuggy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BuggyProject
{
    public class BasePage : TechTalk.SpecFlow.Steps
    {
        private readonly IWait<IWebDriver> _wait;

    
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

    }
}
