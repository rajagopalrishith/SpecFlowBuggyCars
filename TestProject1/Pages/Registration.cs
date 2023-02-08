using BuggyProject;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBuggy.Pages
{
    public  class Registration : BasePage
    {
        protected new IWebDriver Driver { get; }


        public Registration(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            Driver = driver;
        }
        private readonly By RegisterButton = By.XPath("//a[contains(.,'Register')]");

        private readonly By logintextfield = By.XPath("//input[contains(@id,'username')]");

        private readonly By firstnamefield = By.XPath("//input[contains(@id,'firstName')]");

        private readonly By lastnamefield = By.XPath("//input[contains(@id,'lastName')]");

        private readonly By passwordfield = By.XPath("//input[contains(@id,'password')]");

        private readonly By confirmpasswordfield = By.XPath("//input[contains(@name,'confirmPassword')]");

        private readonly By registrationbutton = By.XPath("(//button[@type='submit'])[2]");

        private readonly By validationerrormessage = By.XPath("//div[contains(@class,'result alert alert-danger')]");
        private By validationprompt(string message)
        {
            return By.XPath("//div[contains(@class,'alert-danger') and contains(.,'" + message + "')]");
        }


        private readonly By RegistrationSuccess = By.XPath("//div[contains(@class,'alert-success')]");


        public string ValidationMessageforSuccessfulRegistation()
        {
            return GetElementText(RegistrationSuccess);
        }

        public void ClickRegisterButton()
        {
            ClickOnElement(RegisterButton);
        }

        public void EnterLogin(string logintext)
        {
            ClickOnElement(logintextfield);
            TypeOnElement(logintextfield, logintext);
        }

        public void EnterFirstname(string logintext)

        {
            ClickOnElement(firstnamefield);
            TypeOnElement(firstnamefield, logintext);

        }

        public void EnterLastname(string logintext)

        {
            ClickOnElement(lastnamefield);
            TypeOnElement(lastnamefield, logintext);

        }

        public void EnterPassword(string logintext)

        {
            ClickOnElement(passwordfield);
            TypeOnElement(passwordfield, logintext);
        }

        public void EnterConfirmPassword(string logintext)

        {
            ClickOnElement(confirmpasswordfield);
            TypeOnElement(confirmpasswordfield, logintext);
        }

        public void clickRegisterButtonforSubmission()

        {
            ClickOnElement(registrationbutton);
        }

        public string ValidationMessage()
        {
            return GetElementText(validationerrormessage);
        }

        public bool isValidationMessageFound(string message)
        {
            return IsElementDisplayed(validationprompt(message));
        }

        public bool isGenericValidationMessageFound()
        { 
            return IsElementDisplayed(validationerrormessage);
        }
    }
}
