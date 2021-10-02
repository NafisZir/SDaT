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

            LogAnalyzer log = new LogAnalyzer(fakeManager);

            bool result = log.IsValidLogFileName("short.ext");

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_NoneSupportedExtens_ReturnsFalse()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;

            LogAnalyzer log = new LogAnalyzer(fakeManager);

            bool result = log.IsValidLogFileName("short.ext");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnFalse()
        {
            try
            {
                FakeExtensionManager fakeManager = new FakeExtensionManager();
                fakeManager.WillThrow = new Exception();

                LogAnalyzer log = new LogAnalyzer(fakeManager);

                bool result = log.IsValidLogFileName("short.ext");
            } catch(Exception e)
            {
                Assert.False(false);
            }
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
}
