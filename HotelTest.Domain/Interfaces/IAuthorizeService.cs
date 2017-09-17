using System;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;

namespace HotelTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для авторизации
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Возвращает пользователя, если он авторизовался, иначе null
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Возвращает пользователя или null если пользователь не подтверждён</returns>
        Task<User> AuthorizationAsync(string email, string password);

        #region Востановление пароля

        /// <summary>
        /// Отправляет и добавляет в memoryCashe код для смены пароля
        /// </summary>
        /// <param name="userName">Имя пользователя, который меняет пароль</param>
        /// <returns>Возвращает Id операции, которая занимается востановлением пароля</returns>
        Task<Guid> SendSmsToResetPassword(string userName);

        /// <summary>
        /// Проверяет код который отправляется по смс
        /// </summary>
        /// <param name="idAction">Id операции, которая отправляла код</param>
        /// <param name="code">Код для подтверждения</param>
        /// <returns>Возвращает id для подтверждение смены пароля</returns>
        Task<Guid> ConfirmCodeFromSms(Guid idAction, string code);

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idAction">Id операции, которая подтверждает смену пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        Task ResetPassword(Guid idAction, string newPassword);

        #endregion
    }
}
