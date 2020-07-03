using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Milio.Spec.steps
{
    [Binding]
    public class ContractSteps
    {
        IWebDriver driver = new ChromeDriver();

        [Given(@"The user is logged")]
        public void GivenTheUserIsLogged()
        {
           
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:4200/login");

            IWebElement inputUserName = driver.FindElement(By.Name("UserName"));
            IWebElement inputPassword = driver.FindElement(By.Name("Password"));

            inputUserName.SendKeys("javier");
            System.Threading.Thread.Sleep(1000);
            inputPassword.SendKeys("password");
            System.Threading.Thread.Sleep(1000);

            IWebElement buttonLogin = driver.FindElement(By.Name("LoginButton"));

            buttonLogin.Click();
     
        }

        [When(@"The user press the button contract")]
        public void WhenTheUserPressTheButtonContract()
        {
           
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Leila")));
           
            IWebElement buttonContractLeila = driver.FindElement(By.Name("Leila"));

            System.Threading.Thread.Sleep(1000);
            buttonContractLeila.Click();
            
        }
        
        [When(@"The user complete the form")]
        public void WhenTheUserCompleteTheForm()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("FechaContrato")));

            IWebElement inputFecha = driver.FindElement(By.Name("FechaContrato"));
            IWebElement inputHoraInicio= driver.FindElement(By.Name("HoraInicio"));
            IWebElement inputHoraFin = driver.FindElement(By.Name("HoraFinal"));
            IWebElement buttonEstablecer = driver.FindElement(By.Name("EstablecerFecha"));

            inputFecha.Clear();
            inputHoraInicio.Clear();
            inputHoraFin.Clear();

            inputFecha.SendKeys("07/08/2020");
            System.Threading.Thread.Sleep(1000);
            inputHoraInicio.SendKeys("13");
            System.Threading.Thread.Sleep(1000);
            inputHoraFin.SendKeys("17");

            buttonEstablecer.Click();
            System.Threading.Thread.Sleep(1000);

        }
        
        [Then(@"The user contracts a carer")]
        public void ThenTheUserContractsACarer()
        {

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Realizar-Contrato")));

            System.Threading.Thread.Sleep(1000);

            IWebElement buttonContract = driver.FindElement(By.Name("Realizar-Contrato"));

            buttonContract.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }
    }
}