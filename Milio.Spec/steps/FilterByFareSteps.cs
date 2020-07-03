using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Milio.Spec.steps
{
    [Binding]
    public class FilterByFareSteps
    {
        IWebDriver driver = new ChromeDriver();

        [Given(@"The client press the button Order")]
        public void GivenTheClientPressTheButtonOrder()
        {
           
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:4200/login");

            IWebElement inputUserName = driver.FindElement(By.Name("UserName"));
            IWebElement inputPassword = driver.FindElement(By.Name("Password"));

            inputUserName.SendKeys("javier");
            inputPassword.SendKeys("password");

            IWebElement buttonLogin = driver.FindElement(By.Name("LoginButton"));

            buttonLogin.Click();
            
                 
        }
        
        [When(@"Select fare")]
        public void WhenSelectFare()
        {
            //IWebElement buttonSearchCarers = driver.FindElement(By.Name("BuscarCuidadores"));
            //buttonSearchCarers.Click();
            //driver.Navigate().GoToUrl("http://localhost:4200/carers");

          
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("minFareForHour")));
           


            IWebElement inputMinFare = driver.FindElement(By.Id("minFareForHour"));
            IWebElement inputMaxFare = driver.FindElement(By.Id("maxFareForHour"));

            inputMinFare.Clear();
            inputMaxFare.Clear();
            inputMinFare.SendKeys("20");
            inputMaxFare.SendKeys("30");
            
        }
        
        [Then(@"the result should be the carers order by fare")]
        public void ThenTheResultShouldBeTheCarersOrderByFare()
        {

            IWebElement buttonFilter = driver.FindElement(By.Name("AplicarFiltros"));

            buttonFilter.Click();
            driver.Quit();
        }
    }
}