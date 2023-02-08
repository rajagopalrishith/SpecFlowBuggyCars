using BuggyProject;
using BuggyProject.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using SpecFlowBuggy.Context;
using SpecFlowBuggy.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BuggyProject.Steps
{
    [Binding]
    public class StepDefenitions 
    {
        private LoginPage _Loginpage { get; }
        private Registration _RegistrationPage { get; }

        private DataContext DataContext;
        private ProfilePage  _ProfilePage { get; }
        public StepDefenitions(LoginPage loginPage, Registration registration, ProfilePage profilePage, DataContext dataContext)
        {
            _Loginpage = loginPage;
            _RegistrationPage = registration;
            _ProfilePage = profilePage;
            DataContext = dataContext;
        }


        [Then(@"I close the application")]
        public void ThenICloseTheApplication()
        {
            _Loginpage.CloseApplication();
        }

        [When(@"I open the buggy application home page")]
        [Then(@"I open the buggy application home page")]
        [Given(@"I open the buggy application home page")]
        public void GivenIOpenTheBuggyApplication()
        {
            _Loginpage.NavigatetoBuggyHome();
            _Loginpage.Waitfor5seconds();
        }


        [Then(@"I validate successfull login")]
        public void ThenIValidateSuccessfullLogin()
        {
            _Loginpage.Waitfor2seconds();
            Assert.IsTrue(_Loginpage.ProfileLinkButtonVisible(), "profile button not visible");
        }


        [When(@"I fill in login Information")]
        public void WhenIFillInLoginInformation(Table table)
        {
            foreach (var item in table.Rows)

            {
                string login = item.GetString("login");
                string password = item.GetString("password");
                if (login != null && login != string.Empty)
                {
                    _Loginpage.SetUsername(login);
                    _Loginpage.Waitfor2seconds();
                }
                if (password != null && password != string.Empty)
                {
                    _Loginpage.SetPassword(password);
                    _Loginpage.Waitfor2seconds();
                }
            }
        }

        [Then(@"I Select category ([^']*)")]
        public void ThenISelectCategory(string category)
        {
            if(category == "Popular Make") 
            {
                _ProfilePage.clickPopularMake();
                _Loginpage.Waitfor5seconds();

            }

            if (category == "Popular Model")
            {
                _ProfilePage.clickPopularModel();
                _Loginpage.Waitfor5seconds();

            }
        }




        [Then(@"I vote for car model ([^']*)")]
        public void ThenIVoteForCarModel(string mito)
        {
            _ProfilePage.selectamodel(mito);
            _Loginpage.Waitfor5seconds();
            _ProfilePage.Typecomments("Test Comment");
            _ProfilePage.clickVoteButton();
            _Loginpage.Waitfor5seconds();
        }

        [Then(@"I validate successful voting message is shown")]
        public void ThenIValidateSuccessfulVotingMessageIsShown()
        {
            Assert.IsTrue(_ProfilePage.IsThankYouVoteDisplayed(), "Voting successful message is not shown");
        }


        [Then(@"I login to the application with credentials from context")]
        public void ThenILoginToTheApplicationWithCredentialsFromContext()
        {
         
             _Loginpage.SetUsername(DataContext.LoginName);
            _Loginpage.Waitfor2seconds();
            _Loginpage.SetPassword(DataContext.LoginPassword);
        
        }


        [Then(@"I fill registration fields with random values")]
        public void ThenIFillRegistrationFieldsWithRandomValues()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            DataContext.LoginName = finalString;                 // Context Injection 
            var randomhardcodedpassword = "Special_Password1";
            DataContext.LoginPassword= randomhardcodedpassword;  // Context Injection 
            var login = finalString;
            var firstname = finalString;
            var lastname = finalString;
            var password = randomhardcodedpassword;
            var confirmpassword = randomhardcodedpassword;
            if (login != null && login != string.Empty)
            {
                _RegistrationPage.EnterLogin(login);
                _Loginpage.Waitfor2seconds();
            }
            if (firstname != null && firstname != string.Empty)
            {
                _RegistrationPage.EnterFirstname(firstname);
                _Loginpage.Waitfor2seconds();
            }
            if (lastname != null && lastname != string.Empty)
            {
                _RegistrationPage.EnterLastname(lastname);
                _Loginpage.Waitfor2seconds();
            }
            if (password != null && password != string.Empty)
            {
                _RegistrationPage.EnterPassword(password);
                _Loginpage.Waitfor2seconds();
            }
            if (confirmpassword != null && confirmpassword != string.Empty)
            {
                _RegistrationPage.EnterConfirmPassword(confirmpassword);
                _Loginpage.Waitfor2seconds();
            }
        }


        [Then(@"I Validate registration is successfull")]
        public void ThenIValidateRegistrationIsSuccessfull()
        {
            var expectedmessage = "Registration is successful";
            var actualsuccessmessage = _RegistrationPage.ValidationMessageforSuccessfulRegistation();
            Assert.AreEqual(expectedmessage, actualsuccessmessage, "messages doesnt match");
        }


        [Then(@"I Validate error message for invalid login")]
        public void ThenIValidateErrorMessageForInvalidLogin()
        {
            string expectedvalidationmessage = "Invalid username/password";
            string actualvalidationmessage = _Loginpage.ValidationMessageforLogin();
            Assert.AreEqual(expectedvalidationmessage, actualvalidationmessage, "validation message not found or doesnt match");
        }

        [Then(@"I click login button")]
        public void ThenIClickLoginButton()
        {
            _Loginpage.clickLoginButton();
        }


        [Given(@"I navigate to application registration")]
        public void GivenINavigateToApplicationRegistration()
        {
            _RegistrationPage.ClickRegisterButton();
            _Loginpage.Waitfor5seconds();
        }

        [Then(@"I click register button")]
        public void ThenIClickRegisterButton()
        {
            _RegistrationPage.clickRegisterButtonforSubmission();
            _Loginpage.Waitfor2seconds();
        }

        [Then(@"I validate the following error message as below")]
        public void ThenIValidateTheFollowingErrorMessageAsBelow(Table table)
        {
            foreach (var item in table.Rows)

            {
                var expectederrormessage = item.GetString("errormessage");
                var actualerrormessage = _RegistrationPage.ValidationMessage();
                Assert.AreEqual(expectederrormessage,actualerrormessage, "errormessage not found");
            }
        }
        [Then(@"I validate password policy breach message is shown")]
        public void ThenIValidatePasswordPolicyBreachMessageIsShown()
        {
            Assert.IsTrue(_RegistrationPage.isGenericValidationMessageFound(), "Validation message is not found");
        }


        [Then(@"I validate error message for the condition ([^']*)")]
        public void ThenIValidateErrorMessageForTheCondition(string text)
        {
            Assert.IsTrue(_RegistrationPage.isValidationMessageFound(text), "validation message not found");
        }
        [Then(@"I fill registration fields with random username")]
        public void ThenIFillRegistrationFieldsWithRandomUsername(Table table)
        {
            foreach (var item in table.Rows)

            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                var login = finalString;
               
                string firstname = item.GetString("FirstName");
                string lastname = item.GetString("LastName");
                string password = item.GetString("Password");
                string confirmpassword = item.GetString("ConfirmPassword");
                if (login != null && login != string.Empty)
                {
                    _RegistrationPage.EnterLogin(login);
                    System.Threading.Thread.Sleep(2000);
                }
                if (firstname != null && firstname != string.Empty)
                {
                    _RegistrationPage.EnterFirstname(firstname);
                    System.Threading.Thread.Sleep(2000);
                }
                if (lastname != null && lastname != string.Empty)
                {
                    _RegistrationPage.EnterLastname(lastname);
                    System.Threading.Thread.Sleep(2000);
                }
                if (password != null && password != string.Empty)
                {
                    _RegistrationPage.EnterPassword(password);
                    System.Threading.Thread.Sleep(2000);
                }
                if (confirmpassword != null && confirmpassword != string.Empty)
                {
                    _RegistrationPage.EnterConfirmPassword(confirmpassword);
                    System.Threading.Thread.Sleep(2000);
                }

            }
        }


        [Then(@"I fill registration fields as below")]
        public void ThenIFillRegistrationFieldsAsBelow(Table table)
        {
            foreach (var item in table.Rows) 
            
            {
                string login = item.GetString("login");
                string firstname = item.GetString("FirstName");
                string lastname  = item.GetString("LastName");
                string password  = item.GetString("Password");
                string confirmpassword = item.GetString("ConfirmPassword");
                if (login != null && login != string.Empty)
                {
                    _RegistrationPage.EnterLogin(login);
                    System.Threading.Thread.Sleep(2000);
                }
                if (firstname != null && firstname != string.Empty)
                {
                    _RegistrationPage.EnterFirstname(firstname);
                    System.Threading.Thread.Sleep(2000);
                }
                if (lastname != null && lastname != string.Empty)
                {
                    _RegistrationPage.EnterLastname(lastname);
                    System.Threading.Thread.Sleep(2000);
                }
                if (password != null && password != string.Empty)
                {
                    _RegistrationPage.EnterPassword(password);
                    System.Threading.Thread.Sleep(2000);
                }
                if (confirmpassword != null && confirmpassword != string.Empty)
                {
                    _RegistrationPage.EnterConfirmPassword(confirmpassword);
                    System.Threading.Thread.Sleep(2000);
                }
                
            }
        }


        [When(@"I provide the login information as below")]
        public void WhenIProvideTheLoginInformationAsBelow()
        {
            throw new PendingStepException();
        }

        [Then(@"I verify user registration is successful")]
        public void ThenIVerifyUserRegistrationIsSuccessful()
        {
            throw new PendingStepException();
        }


    }
}
