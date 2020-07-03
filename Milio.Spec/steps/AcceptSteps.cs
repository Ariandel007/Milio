using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Milio.Spec.steps
{
    [Binding]
    public class AcceptSteps
    {
        
        IWebDriver driver = new ChromeDriver();

        [Given(@"The carer is logged")]
        public void GivenTheCarerIsLogged()
        {
           
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:4200/login");

            IWebElement inputUserName = driver.FindElement(By.Name("UserName"));
            IWebElement inputPassword = driver.FindElement(By.Name("Password"));

            inputUserName.SendKeys("leila");
            System.Threading.Thread.Sleep(1000);
            inputPassword.SendKeys("password");
            System.Threading.Thread.Sleep(1000);

            IWebElement buttonLogin = driver.FindElement(By.Name("LoginButton"));

            buttonLogin.Click();
     
        }

        [When(@"The carer press the button contract")]
        public void WhenTheCarerPressTheButtonContract()
        {
           
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("3")));
           
            IWebElement buttonContract = driver.FindElement(By.Name("3"));

            System.Threading.Thread.Sleep(1000);
            buttonContract.Click();
            
        }
                
        [Then(@"The carer init a conversation")]
        public void ThenTheCarerInitAConversation()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

    }
}