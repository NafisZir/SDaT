using System;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    /// <summary>Анализатор лог. файлов</summary>
    public class LogAnalyzer : ILogAnalyze
    /// <summary>Проверка правильности имени файла</summary>
    {
        /// <summary>Объявление события</summary>
        public event LogAnalyzerAction Analyzed = null;
        IExtensionManager extensionManager;

        public LogAnalyzer()
        {
            extensionManager = ExtensionManagerFactory.Create();
        }
        public bool IsValidLogFileName(string fileName)
        {
            try
            {
                return extensionManager.IsValid(fileName);
            } catch (Exception)
            {
                return false;
            }
        }

        /// <summary>Анализировать лог. файл</summary>
        /// <param name="filename"></param>
        public void Analyze(string filename)
        {
            if(filename.Length < 8)
            {
                try
                {
                    //передать внешней службе сообщение об ошибке
                    IWebService webService = WebServiceFactory.Create();
                    webService.LogError("слишком короткое имя файла: " + filename);
                }
                catch(Exception e)
                {
                    //отправить сообщение по электронной почте
                    IEmailService emailService = EmailServiceFactory.Create();
                    emailService.SendEmail("someone@somewhere.com", "Невозможно вызвать веб-сервис", e.Message);
                }
            }

            //Обработка лога
            //..

            //Вызов события
            if(Analyzed != null)
            {
                // Analyzed();
                RaiseAnalyzedEvent();
            }
        }

        protected void RaiseAnalyzedEvent()
        {
            //Вызов события
            if (Analyzed != null)
            {
                Analyzed();
            }
        }
    }
}
