using System;

namespace KTPO4311.Husnutdinov.Lib.src.LogAn
{
    public class EmailServiceFactory
    {
        private static IEmailService emailService = null;
        /// <summary>Создание объектов</summary>
        public static IEmailService Create()
        {
            if (emailService != null)
            {
                return emailService;
            }

            //Настоящая почтовая служба еще не реализована
            throw new NotImplementedException();
        }

        /// <summary>Метод позволит тестам контролировать,
        /// что возвращает фабрика</summary>
        /// <param name="eser"></param>
        public static void SetService(IEmailService eser)
        {
            emailService = eser;
        }
    }
}
