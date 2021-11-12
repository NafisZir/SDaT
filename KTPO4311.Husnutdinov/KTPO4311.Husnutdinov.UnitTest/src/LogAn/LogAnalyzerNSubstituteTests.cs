using System;
using KTPO4311.Husnutdinov.Lib.src.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4311.Husnutdinov.UnitTest.src.LogAn
{
    class LogAnalyzerNSubstituteTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            fakeExtensionManager.IsValid("fileName.txt").Returns(true);

            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("fileName.txt");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidFileName_NoneSupportedExtension_ReturnsFalse()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            fakeExtensionManager.IsValid("fileName.txt").Returns(true);

            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("short.ext");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            fakeExtensionManager.When(x => x.IsValid(Arg.Any<string>()))
                .Do(context => { throw new Exception("fake exception"); });

            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer log = new LogAnalyzer();

            Assert.IsFalse(log.IsValidLogFileName("anything"));
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            IWebService mockWebService = Substitute.For<IWebService>();

            WebServiceFactory.SetService(mockWebService);

            LogAnalyzer log = new LogAnalyzer();

            string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            mockWebService.Received().LogError("слишком короткое имя файла: abc.ext");
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            IWebService mockWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetService(mockWebService);
            mockWebService.When(x => x.LogError(Arg.Any<string>()))
                .Do(context => { throw new Exception("это подделка"); });

            IEmailService mockEmail = Substitute.For<IEmailService>();
            EmailServiceFactory.SetService(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            mockEmail.Received().SendEmail("someone@somewhere.com", "Невозможно вызвать веб-сервис", "это подделка");
        }

        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);

            WebServiceFactory.SetService(null);

            EmailServiceFactory.SetService(null);
        }
    }
}
