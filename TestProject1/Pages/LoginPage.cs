using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoDi;
using System.Threading.Tasks;

namespace BuggyProject.Pages
{
    public class LoginPage : BasePage
    {
        protected new IWebDriver Driver { get; }


        public LoginPage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            Driver = driver;
        }

        //Login Page Fields

        private readonly By loginusernamefield = By.XPath("//input[contains(@name,'login')]");

        private readonly By loginpasswordfield = By.XPath("//input[contains(@name,'password')]");

        private readonly By LoginButton = By.XPath("//button[contains(.,'Login')]");

        private readonly By ProfileLink  = By.XPath("//a[contains(.,'Profile')]");


        //Login Page Methods
        public void CloseApplication()

        {

            Driver.Dispose();
        }
        public void SetUsername(string logintext)
        {
            TypeOnElement(loginusernamefield, logintext);
        }

        public void SetPassword(string password)
        {
            TypeOnElement(loginpasswordfield, password);
        }

        public void clickLoginButton()
        {
            ClickOnElement(LoginButton);
        }

        public void Waitfor5seconds()
        {
            System.Threading.Thread.Sleep(5000);
        }


        public void Waitfor2seconds()
        {
            System.Threading.Thread.Sleep(2000);
        }


        public bool ProfileLinkButtonVisible()
        {
            return IsElementDisplayed(ProfileLink);

        }



        private readonly By validationerrormessageforLogin = By.XPath("//span[@class='label label-warning']");

        public string ValidationMessageforLogin()
        {

            return GetElementText(validationerrormessageforLogin);
        }

     
        public void NavigatetoBuggyHome()
        {
            Driver.Navigate().GoToUrl("https://buggy.justtestit.org/");
        }

     

   

    }
}
