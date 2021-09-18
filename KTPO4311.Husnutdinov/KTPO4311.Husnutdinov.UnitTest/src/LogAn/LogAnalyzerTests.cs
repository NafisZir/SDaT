using NUnit.Framework;
using KTPO4311.Husnutdinov.Lib.src.LogAn;
using System;

namespace KTPO4311.Husnutdinov.UnitTest.src.LogAn
{
    [TestFixture]
    class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");
            Assert.False(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithgoodextension.HUSNUTDINOV");
            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithgoodextension.husnutdinov");
            Assert.True(result);
        }

        [TestCase("filewithgoodextension.HUSNUTDINOV")]
        [TestCase("filewithgoodextension.husnutdinov")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string file)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName(file);
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            var ex = Assert.Catch<Exception>(() => analyzer.IsValidLogFileName(""));
            StringAssert.Contains("имя файла должно быть задано", ex.Message);
        }

        [TestCase("badfile.foo", true)]
        [TestCase("goodfile.husnutdinov", false)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.IsValidLogFileName(file);
            Assert.AreEqual(expected, analyzer.WasLastedFileNameValed);
        }
    }
}
