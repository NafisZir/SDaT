using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Husnutdinov.Lib.src.LogAn;

namespace KTPO4311.Husnutdinov.Service.src
{
    public class App
    {
        public static void Main()
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            Console.WriteLine("Для правильного " + logAnalyzer.IsValidLogFileName("trueName.txt"));
            Console.WriteLine("Для неправильного " + logAnalyzer.IsValidLogFileName("wrong.ext"));
        }
    }
}
