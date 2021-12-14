using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    public interface ILogAnalyze
    {
        public event LogAnalyzerAction Analyzed;
        public bool IsValidLogFileName(string fileName);
        public void Analyze(string filename);
    }
}
