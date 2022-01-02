using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Husnutdinov.Lib.src.LogAn;
using KTPO4311.Husnutdinov.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4311.Husnutdinov.UnitTest.src.SampleCommands
{
    class SampleCommandTests
    {
        [Test]
        public void FirstCommand_Execute_IsValidText()
        {
            IView mockView = Substitute.For<IView>();
            FirstCommand firstCommand = new FirstCommand(mockView);

            firstCommand.Execute();
            int iExecute = 1;

            mockView.Received().Render(firstCommand.GetType().ToString() + "\n iExecute = " + iExecute);
        }

        [Test]
        public void SampleCommandDecorator_Execute_CallsExecute()
        {
            IView stubView = Substitute.For<IView>();
            ISampleCommand mockSampleCommand = Substitute.For<ISampleCommand>();

            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(mockSampleCommand, stubView);

            sampleCommandDecorator.Execute();
            mockSampleCommand.Received().Execute();
        }

        [Test]
        public void SampleCommandDecorator_Execute_IsValidText()
        {
            ISampleCommand stubSampleCommand = Substitute.For<ISampleCommand>();
            IView mockView = Substitute.For<IView>();

            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(stubSampleCommand, mockView);
            sampleCommandDecorator.Execute();

            mockView.Received().Render("Начало: " + sampleCommandDecorator.GetType().ToString());
            mockView.Received().Render("Конец: " + sampleCommandDecorator.GetType().ToString());
        }

        [Test]
        public void ExceptionCommandDecorator_Execute_CallsExecute()
        {
            ISampleCommand mockSampleCommand = Substitute.For<ISampleCommand>();
            IView stubView = Substitute.For<IView>();

            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(mockSampleCommand, stubView);

            exceptionCommandDecorator.Execute();
            mockSampleCommand.Received().Execute();
        }

        [Test]
        public void ExceptionCommandDecorator_Execute_CatchException()
        {
            ISampleCommand fakeSampleCommand = Substitute.For<ISampleCommand>();
            IView mockView = Substitute.For<IView>();

            fakeSampleCommand.When(o => o.Execute()).Do(context => { throw new System.Exception(); });

            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(fakeSampleCommand, mockView);

            exceptionCommandDecorator.Execute();
            mockView.Received().Render("Exception: " + exceptionCommandDecorator.GetType().ToString());
        }
    }
}
