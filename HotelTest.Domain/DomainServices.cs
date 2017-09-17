using System;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Options;
using HotelTest.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelTest.Domain
{
    /// <summary>
    /// Статический класс для добавления сервисов в колекцию
    /// </summary>
    public static class DomainServices
    {
        /// <summary>
        /// Метод расширения, добавляющий сервисы из domain для DI 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection service, Action<HotelOptions> opt)
        {

            var options = new HotelOptions();
            opt(options);

            service.AddSingleton(options);

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthorizationService, AuthorizationService>();
            service.AddScoped<IRoomService, RoomService>();
            service.AddScoped<IVisitorService, VisitorService>();
            return service;
        }
    }
}
