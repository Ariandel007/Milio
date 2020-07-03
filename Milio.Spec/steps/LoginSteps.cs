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
            driver.Navigate().GoToUrl("https://milio.azurewebsites.net/login");
     
        }
        
        [When(@"Enter the Email and Password")]
        public void WhenEnterTheEmailAndPassword()
        {
        
            IWebElement inputUserName = driver.FindElement(By.Name("UserName"));
            IWebElement inputPassword = driver.FindElement(By.Name("Password"));

            inputUserName.SendKeys("javier");
            inputPassword.SendKeys("password");
        }
        
        [Then(@"the result should be the user logged")]
        public void ThenTheResultShouldBeTheUserLogged()
        {

            IWebElement buttonLogin = driver.FindElement(By.Name("LoginButton"));

            buttonLogin.Click();
            driver.Quit();
        }
    }
}