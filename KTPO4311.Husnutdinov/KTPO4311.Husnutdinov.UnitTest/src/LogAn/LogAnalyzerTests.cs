using NUnit.Framework;
using KTPO4311.Husnutdinov.Lib.src.LogAn;
using System;

namespace KTPO4311.Husnutdinov.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtens_ReturnsTrue()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = true;

            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("fileName.txt");

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_NoneSupportedExtens_ReturnsFalse()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;

            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("short.ext");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnFalse()
        {
           
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillThrow = new Exception();

            ExtensionManagerFactory.SetManager(fakeManager);

            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("short.ext");

            Assert.False(result);
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            //Подготовка теста
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проврека ожидаемого результата
            StringAssert.Contains("слишком короткое имя файла: abc.ext", mockWebService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            //Подготовка теста
            FakeWebService stubWebService = new FakeWebService();
            WebServiceFactory.SetService(stubWebService);
            stubWebService.WillThrow = new Exception("это подделка");

            FakeEmailService mockEmail = new FakeEmailService();
            EmailServiceFactory.SetService(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            //Воздействие на тестируемый объект
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            //...здесь тест будет ложным, если неверно хотя бы одно утверждение
            //...поэтому здесь допустимо несколько утверждений
            StringAssert.Contains("someone@somewhere.com", mockEmail.lastTo);
            StringAssert.Contains("это подделка", mockEmail.lastBody);
            StringAssert.Contains("Невозможно вызвать веб-сервис", mockEmail.lastSubject);
        }

        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetService(null);
            EmailServiceFactory.SetService(null);
        }
    }

    /// <summary>Поддельный менеджер расширений</summary>
    internal class FakeExtensionManager : IExtensionManager
    {
        /// <summary>Это поле позволяет настроить
        /// поддельный результат для метода IsValid</summary>
        public bool WillBeValid = false;

        public Exception WillThrow = null;
        public bool IsValid(string fileName)
        {
            if(WillThrow != null)
            {
                throw WillThrow;
            }

            return WillBeValid;
        }
    }

    /// <summary>Поддельная веб-служба</summary>
    internal class FakeWebService : IWebService
    {
        /// <summary>Это поле запоминает состояние после вызова метода LogError
        /// при тестировании взаимодействия утверждения высказываются относительно</summary>
        public string LastError;
        public Exception WillThrow = null;

        public void LogError(string message)
        {
            if(WillThrow != null)
            {
                throw WillThrow;
            }
            else
            {
                LastError = message;
            }
        }
    }
    internal class FakeEmailService : IEmailService
    {
        public string lastTo;
        public string lastSubject;
        public string lastBody;
        public void SendEmail(string to, string subject, string body)
        {
            lastTo = to;
            lastSubject = subject;
            lastBody = body;
        }
    }
}
