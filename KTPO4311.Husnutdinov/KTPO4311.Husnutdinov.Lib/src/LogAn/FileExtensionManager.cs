using System.Configuration;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    /// <summary>Менеджер расширений файлов</summary>
    public class FileExtensionManager : IExtensionManager
    {
        private string trueFileExt;
        /// <summary>Проверка правильности расширения</summary>
        public bool IsValid(string fileName)
        {
            //читать конфигурационный файл
            //вернуть true
            //если конфигурация поддерживается
            trueFileExt = ConfigurationManager.AppSettings.Get(0);


            if (fileName.Contains(trueFileExt))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}