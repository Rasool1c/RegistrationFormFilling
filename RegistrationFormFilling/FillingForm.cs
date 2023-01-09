using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.Extensions;

namespace RegistrationFormFilling
{
    public class Tests
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void FormFillingTest()
        {
            //Navigating to the web url
            driver.Navigate().GoToUrl("http://demo.automationtesting.in/Register.html");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@ng-model='FirstName']")));
            //Filling the form by interacting with the corresponding webelements
            driver.FindElement(By.XPath("//input[@ng-model='FirstName']")).SendKeys("Rasool");
            driver.FindElement(By.XPath("//input[@ng-model='LastName']")).SendKeys("A");
            var Address = "Banglore, Karbataka, India";
            driver.FindElement(By.XPath("//*[@rows='3']")).SendKeys(Address);
            driver.FindElement(By.XPath("//*[@ng-model='EmailAdress']")).SendKeys("abcdefg@gmail.com");
            driver.FindElement(By.XPath("//input[@type='tel']")).SendKeys("1234567890");
            driver.FindElement(By.XPath("//*[@value = 'Male']")).Click();
            var Hobby1 = driver.FindElement(By.Id("checkbox1"));
            CheckBox(Hobby1);
            var Hobby2 = driver.FindElement(By.Id("checkbox2"));
            CheckBox(Hobby2);
            driver.FindElement(By.Id("msdd")).Click();
            driver.FindElement(By.XPath("//a[normalize-space()='English']")).Click();
            driver.FindElement(By.XPath("//a[normalize-space()='Hindi']")).Click();
            new SelectElement(driver.FindElement(By.Id("Skills"))).SelectByValue("C");
            new SelectElement(driver.FindElement(By.ClassName("select2-hidden-accessible"))).SelectByText("India");
            new SelectElement(driver.FindElement(By.Id("yearbox"))).SelectByValue("2000");
            new SelectElement(driver.FindElement(By.XPath("//select[@placeholder='Month']"))).SelectByValue("April");
            new SelectElement(driver.FindElement(By.XPath("//select[@placeholder='Day']"))).SelectByIndex(5);
            driver.FindElement(By.Id("firstpassword")).SendKeys("Rasool@123");
            driver.FindElement(By.Id("secondpassword")).SendKeys("Rasool@123");
            string FPath = @"C:\Users\mindtree2301\Downloads\naruto.jpg";
            //File uploading 
            driver.FindElement(By.Id("imagesrc")).SendKeys(FPath);
            //Highlighting the webelement submit
            var element = driver.FindElement(By.Name("signup"));
            Highlight(element);
            //taking the screenshot
            Screenshot("FormFillingWebScreenshot");
            driver.FindElement(By.Name("signup")).Click();
        }
        public void CheckBox(IWebElement ItemToCheck)
        {
            bool IsSelected = ItemToCheck.Selected;
            if (IsSelected == false)
            {
                ItemToCheck.Click();
            }
            Assert.IsTrue(ItemToCheck.Selected);
        }
        private void Highlight(IWebElement ElementToHighlight)
        {          
            var OriginalStyle = ElementToHighlight.GetAttribute("style");
            IJavaScriptExecutor JavaScriptExecutor = driver as IJavaScriptExecutor;
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1],arguments[2])", ElementToHighlight,
                "style",
                "border: 7px solid yellow; border-style: dashed;");
        }
        public void Screenshot(string name)
        {

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(@"C:\Users\mindtree2301\source\repos\RegistrationFormFilling\RegistrationFormFilling\Screenshots\" + name + ".png");
        }
        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}