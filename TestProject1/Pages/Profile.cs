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
    public class ProfilePage : BasePage
    {
        protected new IWebDriver Driver { get; }


        public ProfilePage(IWebDriver driver, IWait<IWebDriver> wait) : base(driver, wait)
        {
            Driver = driver;
        }

        private readonly By PopularMake = By.XPath("//img[contains(@title,'Alfa Romeo')]");

        private readonly By PopularModel = By.XPath("//img[contains(@title,'Guilia Quadrifoglio')]");

        private readonly By allregisteredmodelsimg = By.XPath("(//img[contains(@class,'img-fluid center-block')])[3]");

        private readonly By commenttextarea = By.XPath("//textarea[@id='comment']");

        private readonly By voteButton = By.XPath("//button[contains(.,'Vote!')]");

        private readonly By ThankYouforVote = By.XPath("//p[contains(.,'Thank you for your vote!')]");

        private By selectedmodel(string carmake) 
        { 
           return  By.XPath("//img[@class='img-thumbnail' and contains(@title,'" + carmake + "')]"); 
        }


        public void selectamodel(string carmake)
        {
            ClickOnElement(selectedmodel(carmake));
        }

        public void Typecomments(string comments)
        {
            TypeOnElement(commenttextarea, comments);
        }



        public void clickPopularMake()
        {
            ClickOnElement(PopularMake);

        }

        public void clickVoteButton()
        {
            ClickOnElement(voteButton);

        }
        public void clickPopularModel()
        {
            ClickOnElement(PopularModel);

        }

        public void clickallregisteredmodels()
        {
            ClickOnElement(allregisteredmodelsimg);

        }


        public bool IsThankYouVoteDisplayed()
        {
            return IsElementDisplayed(ThankYouforVote);

        }
    }
}