using NUnit.Framework;
using TRAX.WebAutomation;
using System;

namespace Examples
{
    [TestFixture(AppKind.ChromeBrowser)]
    //[TestFixture(AppKind.FirefoxBrowser)]
    public class HospitalManagementTests
    {
        private AppKind myAppKind;
        private AppAndDriverOptions myAppAndDriverOptions;
        private WebAutomatorOptions myCommonWebAutomatorOptions;
        private IWebAutomator myWebAutomator;

        // ctor
        public HospitalManagementTests(AppKind appKind)
        {
            myAppKind = appKind;
            myAppAndDriverOptions = new AppAndDriverOptions(myAppKind);

            // Define a common options base that can (but does not have to) be used later.
            myCommonWebAutomatorOptions = new WebAutomatorOptions(WebAutomatorOptionsPreset.Default);
            myCommonWebAutomatorOptions.DefaultWaitTimeout = TimeSpan.FromSeconds(10);
        }

        [SetUp]
        public void SetUp()
        {
            // create a new automator for each test with the pre-defined options
            myWebAutomator = WebAutomatorFactory.Create(myAppAndDriverOptions, myCommonWebAutomatorOptions);
        }

        [TearDown]
        public void TearDown()
        {
            // important: dispose the web automator after each test
            myWebAutomator.Dispose();
        }

        [Test]
        public void VerifyHomePageElements()
        {
            myWebAutomator.NavigateToUrl("http://localhost:8080/Hospital-PHP/");

            myWebAutomator.SetWindowState(WindowState.Maximized);

            string pageTitle = myWebAutomator.GetDocumentTitle();
            Assert.That(pageTitle, Is.EqualTo("Hospital Management System"), "Incorrect page title");

            var homeLink = myWebAutomator.FindElement(FindBy.CssSelector(".menu-active a"));
            Assert.IsNotNull(homeLink, "Home link not found");

            var doctorsLoginLink = myWebAutomator.FindElement(FindBy.CssSelector("a[href='backend/doc/index.php']"));
            Assert.IsNotNull(doctorsLoginLink, "Doctor's Login link not found");

            var adminLoginLink = myWebAutomator.FindElement(FindBy.CssSelector("a[href='backend/admin/index.php']"));
            Assert.IsNotNull(adminLoginLink, "Admin Login link not found");
        }

        [Test]
        public void VerifyHomePageLink()
        {
            myWebAutomator.NavigateToUrl("http://localhost:8080/Hospital-PHP/");

            myWebAutomator.SetWindowState(WindowState.Maximized);

            var homeLink = myWebAutomator.FindElement(FindBy.CssSelector(".menu-active a"));
            homeLink.Click();

            myWebAutomator.Wait(WaitFor.Ready(IsReady.Document));

            string currentUrl = myWebAutomator.GetUrl();
            Assert.That(currentUrl, Is.EqualTo("http://localhost:8080/Hospital-PHP/index.php"), "Home link does not navigate to the correct page");
        }

        [Test]
        public void AdminLoginTest()
        {
            myWebAutomator.NavigateToUrl("http://localhost:8080/Hospital-PHP/");

            myWebAutomator.SetWindowState(WindowState.Maximized);

            var adminLoginLink = myWebAutomator.FindElement(FindBy.CssSelector("a[href='backend/admin/index.php']"));
            adminLoginLink.Click();

            myWebAutomator.Wait(WaitFor.Ready(IsReady.Document));

            IElement emailInput = myWebAutomator.FindElement(FindBy.Id("emailaddress"));
            emailInput.EnterNewText("admin@mail.com");

            IElement passwordInput = myWebAutomator.FindElement(FindBy.Id("password"));
            passwordInput.EnterNewText("Password@123");

            IElement loginButton = myWebAutomator.FindElement(FindBy.Name("admin_login"));
            loginButton.Click();

            myWebAutomator.Wait(WaitFor.Ready(IsReady.Document));

            string currentUrl = myWebAutomator.GetUrl();
            Assert.That(currentUrl, Does.Contain("http://localhost:8080/Hospital-PHP/backend/admin/his_admin_dashboard.php"), "Admin login failed");
        }
    }
}
