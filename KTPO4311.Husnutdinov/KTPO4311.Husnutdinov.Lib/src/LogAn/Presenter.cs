using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    public class Presenter
    {
        ILogAnalyze logAnalyzer;
        IView view;
        public Presenter(ILogAnalyze logAnalyzer, IView view)
        {
            this.logAnalyzer = logAnalyzer;
            this.view = view;
            logAnalyzer.Analyzed += OnLogAnalyzed;
        }

        private void OnLogAnalyzed()
        {
            view.Render("Обработка завершена");
        }
    }
}
