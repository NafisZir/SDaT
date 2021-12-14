using System;
using KTPO4311.Husnutdinov.Lib.src.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4311.Husnutdinov.UnitTest.src.LogAn
{
    class PresenterTest
    {
        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender()
        {
            FakeLogAnalyzer fakeLogAnalyzer = new FakeLogAnalyzer();
            IView mockView = Substitute.For<IView>();
            Presenter presenter = new Presenter(fakeLogAnalyzer, mockView);

            fakeLogAnalyzer.CallRaiseAnalyzedEvent();

            mockView.Received().Render("Обработка завершена");
        }
        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender_NSubstitute()
        {
            ILogAnalyze stubLogAnalyzer = Substitute.For<ILogAnalyze>();
            IView mockView = Substitute.For<IView>();
            Presenter presenter = new Presenter(stubLogAnalyzer, mockView);

            stubLogAnalyzer.Analyzed += Raise.Event<LogAnalyzerAction>();

            mockView.Received().Render("Обработка завершена");
        }
    }

    /// <summary>
    /// Заглушка для имитации вызова события
    /// </summary>
    class FakeLogAnalyzer : LogAnalyzer
    {
        public void CallRaiseAnalyzedEvent()
        {
            base.RaiseAnalyzedEvent();
        }
    }
}
