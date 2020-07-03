using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Milio.Spec.steps
{
    [Binding]
    public class LoginSteps
    {
        IWebDriver driver = new ChromeDriver();

        [Given(@"Open the Chrome and launch the application")]
        public void GivenOpenTheChromeAndLaunchTheApplication()
        {
           
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:5001/login");
     
        }
        
        [When(@"Enter the Email and Password")]
        public void WhenEnterTheEmailAndPassword()
        {
        
            IWebElement inputUserName = driver.FindElement(By.Id("Input_UserName"));
            IWebElement inputPassword = driver.FindElement(By.Id("Input_Password"));

            inputUserName.SendKeys("javier");
            inputPassword.SendKeys("password");
        }
        
        [Then(@"the result should be the user logged")]
        public void ThenTheResultShouldBeTheUserLogged()
        {

            IWebElement buttonLogin = driver.FindElement(By.Id("login_button"));

            buttonLogin.Click();
        }
    }
}