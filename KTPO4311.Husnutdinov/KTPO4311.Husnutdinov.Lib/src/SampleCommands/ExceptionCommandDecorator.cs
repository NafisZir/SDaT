using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Husnutdinov.Lib.src.LogAn;

namespace KTPO4311.Husnutdinov.Lib.src.SampleCommands
{
    public class ExceptionCommandDecorator : ISampleCommand
    {
        private readonly ISampleCommand sampleCommand;
        private readonly IView view;

        public ExceptionCommandDecorator(ISampleCommand sampleCommand, IView view)
        {
            this.sampleCommand = sampleCommand;
            this.view = view;
        }

        public void Execute()
        {
            try
            {
                sampleCommand.Execute();
            }
            catch
            {
                view.Render("Exception: " + this.GetType().ToString());
            }
        }
    }
}
