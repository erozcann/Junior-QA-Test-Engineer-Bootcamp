using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AutomationTests.Pages;
using System.Threading;
using AventStack.ExtentReports;
using AutomationTests.Utils;
using AventStack.ExtentReports.Reporter;

namespace AutomationTests.Tests
{
    [TestFixture]
    public class AutomationExerciseTests : IDisposable
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private LoginPage _loginPage;
        private SignupPage _signupPage;
        private string _registeredEmail;
        private string _registeredPassword;
        private ExtentReports _extent;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _extent = ExtentManager.GetExtent();
        }

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://automationexercise.com/");
            Thread.Sleep(2000);
            _homePage = new HomePage(_driver);
            _loginPage = new LoginPage(_driver);
            _signupPage = new SignupPage(_driver);
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FullUserJourneyTest()
        {
            try
            {
                _test.Info("Yeni kullanıcı kaydı başlatılıyor");
                _homePage.NavigateToSignup();
                Thread.Sleep(2000);
                string uniqueEmail = $"ali{System.DateTime.Now.Ticks}@test.com";
                string password = "Test1234";
                _signupPage.FillSignupForm("Ali", uniqueEmail, password);
                Thread.Sleep(2000);
                _registeredEmail = uniqueEmail;
                _registeredPassword = password;
                _test.Pass("Kayıt başarılı");

                _test.Info("Ürün arama başlatılıyor");
                _driver.Navigate().GoToUrl("https://automationexercise.com/products");
                Thread.Sleep(2000);
                _homePage.SearchProduct("Blue Top");
                Thread.Sleep(2000);
                Assert.That(_driver.Url, Does.Contain("search"));
                _test.Pass("Ürün arama başarılı");

                _test.Info("Logout işlemi başlatılıyor");
                var logoutButton = _driver.FindElement(By.CssSelector("a[href='/logout']"));
                logoutButton.Click();
                Thread.Sleep(2000);
                Assert.That(_driver.Url, Does.Contain("login"));
                _test.Pass("Logout başarılı");

                _test.Info("Doğru bilgilerle tekrar giriş yapılıyor");
                _loginPage.Login(_registeredEmail, _registeredPassword);
                Thread.Sleep(2000);
                Assert.That(_driver.Url, Is.EqualTo("https://automationexercise.com/"));

                _test.Pass("Başarılı giriş yapıldı");

                _test.Info("Tekrar logout yapılıyor");
                logoutButton = _driver.FindElement(By.CssSelector("a[href='/logout']"));
                logoutButton.Click();
                Thread.Sleep(2000);
                Assert.That(_driver.Url, Does.Contain("login"));
                _test.Pass("Logout başarılı (2)");

                _test.Info("Login formu kontrol ediliyor");
                var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
                Assert.That(emailInput.Displayed);

                _test.Info("Yanlış şifreyle giriş deneniyor");
                _loginPage.Login(_registeredEmail, "YanlisSifre123!");
                Thread.Sleep(2000);
                Assert.That(_loginPage.GetErrorMessage(), Does.Contain("Your email or password is incorrect!"));
                _test.Pass("Yanlış şifreyle girişte hata mesajı görüldü");
            }
            catch (Exception ex)
            {
                _test.Fail("Test başarısız: " + ex.Message);
                try
                {
                    var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    var path = $"TestResults/screenshot_{DateTime.Now.Ticks}.png";
                    screenshot.SaveAsFile(path);
                    _test.AddScreenCaptureFromPath(path);
                }
                catch { }
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(2000);
            _driver?.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _extent.Flush();
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
} 