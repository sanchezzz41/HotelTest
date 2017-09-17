using System;
using HotelTest.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HotelTest.Web.Extensions
{
    /// <summary>
    /// Статический класс предоставляющий методы расширения для отображения записей
    /// </summary>
    public static class JsonView
    {
        /// <summary>
        /// Метод для отображения инфы о пользователе(для админа)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object UserViewForAdmin(this User obj,HttpContext context)
        {
            if(context.User.IsInRole(nameof(RolesOptions.Admin)))
            if (obj != null)
            {
                return new
                {
                   obj.Id,
                   obj.Email,
                   obj.Fio,
                   obj.PhoneNumber,
                   Role = Enum.GetName(typeof(RolesOptions),obj.RoleId)
                };
            }
            return null;
        }

        /// <summary>
        /// Метод для отображения инфы о пользователе
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object UserView(this User obj)
        {
            if (obj != null)
            {
                return new
                {
                    obj.Id,
                    obj.Fio,
                    obj.PhoneNumber,
                };
            }
            return null;
        }

        /// <summary>
        /// Метод для отображения инфы о комнате
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object RoomView(this Room obj)
        {
            if (obj != null)
            {
                return new
                {
                    obj.Id,
                    obj.IsFree,
                    obj.MaxCount,
                    Type = Enum.GetName(typeof(RoomOptions), obj.RoomOptionId)
                };
            }
            return null;
        }

        /// <summary>
        /// Метод для отображения инфы о поситителе
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object VisitorView(this Visitor obj)
        {
            if (obj != null)
            {
                return new
                {
                    obj.Id,
                    obj.Price,
                    obj.ArrivalDate,
                    obj.DateOfDeparture,
                    obj.RoomId,
                    obj.UserId,
                };
            }
            return null;
        }
    }
}
