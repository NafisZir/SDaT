namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    /// <summary>Анализатор лог. файлов</summary>
    public class LogAnalyzer
    /// <summary>Проверка правильности имени файла</summary>
    {
        IExtensionManager extensionManager;

        public LogAnalyzer()
        {
            extensionManager = ExtensionManagerFactory.Create();
        }
        public bool IsValidLogFileName(string fileName)
        {
            return extensionManager.IsValid(fileName);
        }
    }
}
