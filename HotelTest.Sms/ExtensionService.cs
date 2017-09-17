using HotelTest.Sms.Stub;
using Microsoft.Extensions.DependencyInjection;

namespace HotelTest.Sms
{
    /// <summary>
    /// Класс для добалвения смс сервиса
    /// </summary>
    public static class ExtensionSmsService
    {
        /// <summary>
        /// Метод расширения для добавления смс сервиса
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddSmsService(this IServiceCollection service)
        {
            service.AddScoped<ISmsService, SmsService>();
            return service;
        }

    }
}
