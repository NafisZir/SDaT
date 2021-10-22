namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    public class WebServiceFactory
    {
        private static IWebService service = null;
        /// <summary>Создание объектов</summary>
        public static IWebService Create()
        {
            if (service != null)
            {
                return service;
            }

            return new WebService();
        }

        /// <summary>Метод позволит тестам контролировать,
        /// что возвращает фабрика</summary>
        /// <param name="wser"></param>
        public static void SetService(IWebService wser)
        {
            service = wser;
        }
    }
}
