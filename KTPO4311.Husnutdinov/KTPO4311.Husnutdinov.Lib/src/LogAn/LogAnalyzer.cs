using System;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    public class LogAnalyzer
    {
        public bool WasLastedFileNameValed { get; set; }
        public bool IsValidLogFileName(string filename)
        {
            WasLastedFileNameValed = false;

            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("имя файла должно быть задано");
            }

            if (filename.EndsWith(".husnutdinov", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            WasLastedFileNameValed = true;

            return false;
        }
    }
}
