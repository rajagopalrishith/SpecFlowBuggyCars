using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System.Security.Policy;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowBuggy
{
    [Binding]
    public class WebDriverSupport : IDisposable
    {
        private readonly IObjectContainer objectContainer;
        private IWait<IWebDriver> _defaultWait = null;
        private IWebDriver Driver = null;

        public WebDriverSupport(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            this.Driver = new ChromeDriver();
            { 
            };
            objectContainer.RegisterInstanceAs(Driver,typeof (IWebDriver));
            int timeout=5000;
            int pollInterval=100;
            _defaultWait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(timeout))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollInterval)
            };
           objectContainer.RegisterInstanceAs(_defaultWait,typeof(IWait<IWebDriver>));
        }

        [AfterScenario]
        public void Dispose()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
