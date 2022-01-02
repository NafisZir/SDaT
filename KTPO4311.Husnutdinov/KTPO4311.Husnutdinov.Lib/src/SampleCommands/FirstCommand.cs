using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Husnutdinov.Lib.src.LogAn;

namespace KTPO4311.Husnutdinov.Lib.src.SampleCommands
{
    public class FirstCommand : ISampleCommand
    {
        public FirstCommand(IView view)
        {
            this.view = view;
        }

        private readonly IView view;

        private int iExecute = 0;

        public void Execute()
        {
            iExecute += 1;
            view.Render(this.GetType().ToString() + "\n iExecute = " + iExecute);
        }
    }
}
