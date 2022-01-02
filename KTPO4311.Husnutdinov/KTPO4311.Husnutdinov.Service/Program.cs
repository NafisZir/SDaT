using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Husnutdinov.Lib.src.Common;
using KTPO4311.Husnutdinov.Lib.src.SampleCommands;
using KTPO4311.Husnutdinov.Service.src.WindsorInstallers;

namespace KTPO4311.Husnutdinov.Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            CastleFactory.container.Install(
                new SampleCommandInstaller(),
                new ViewInstaller()
                );

            for(int i = 0; i < 3; i++)
            {
                ISampleCommand someCommand = CastleFactory.container.Resolve<ISampleCommand>();
                someCommand.Execute();
            }
        }
    }
}
