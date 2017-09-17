using System;
using System.Linq;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Domain.Utilits;
using HotelTest.Sms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace HotelTest.Domain.Services
{
    /// <summary>
    /// Класс для авторизации пользователя
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ISmsService _smsService;
        private readonly IMemoryCache _memoryCache;

        public AuthorizationService(IUserService userService, IPasswordHasher<User> passwordHasher, ISmsService smsService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _smsService = smsService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Возвращает пользователя, если он успешно прошёл авторизацию, в противном случае null
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public async Task<User> AuthorizationAsync(string email, string password)
        {
            if (email == null)
            {
                return null;
            }
            var users = await _userService.GetAsync();
            var resultUser = users.SingleOrDefault(x => x.Email == email);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким мылом: {email} нету.");
            }
            var resultHash = _passwordHasher.HashPassword(resultUser, password);

            if (resultHash != resultUser.PasswordHash)
            {
                throw new ArgumentException($"Пароль введён неверно.");
            }
            return resultUser;
        }

        #region Востановление пароля через смс(сначала надо добавить смс сервис и memoryCashe)

        /// <summary>
        /// Отправляет и добавляет в memoryCashe код для смены пароля
        /// </summary>
        /// <param name="email">email пользователя, который меняет пароль</param>
        /// <returns></returns>
        public async Task<Guid> SendSmsToResetPassword(string email)
        {
            var userList = await _userService.GetAsync();
            var resultUser = userList.SingleOrDefault(x => x.Email == email);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким именем {email} не существует.");
            }

            var codeToSend = Randomizer.GetNumbers(5);
            var resultModel = new ResetPasswordModel { IdUser = resultUser.Id, Code = codeToSend };
            //Этот индификатор будет применяться как ключ в memorycashe
            var guidAction = Guid.NewGuid();
            _memoryCache.Set(guidAction, resultModel,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));

            await _smsService.SendSmsAsync(resultUser.PhoneNumber, codeToSend);

            return guidAction;
        }

        /// <summary>
        /// Проверяет код который отправляется по смс
        /// </summary>
        /// <param name="idAction">Id операции, которая отправляла код</param>
        /// <param name="code">Код для подтверждения</param>
        /// <returns>Возвращает id для подтверждение смены пароля</returns>
        public async Task<Guid> ConfirmCodeFromSms(Guid idAction, string code)
        {
            var resultModel = _memoryCache.Get<ResetPasswordModel>(idAction);
            if (resultModel == null)
            {
                throw new NullReferenceException($"Операции с данным Id:{idAction} нету.");
            }
            if (String.Compare(resultModel.Code, code, StringComparison.Ordinal) != 0)
            {
                throw new InvalidOperationException($"Код для подтверждения не совпадает.");
            }

            var resultActionGuid = Guid.NewGuid();

            _memoryCache.Set(resultActionGuid, resultModel.IdUser,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return await Task.FromResult(resultActionGuid);
        }

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idAction">Id операции, которая подтверждает смену пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public async Task ResetPassword(Guid idAction, string newPassword)
        {
            var resultUserId = _memoryCache.Get<Guid>(idAction);
            if (resultUserId == Guid.Empty)
            {
                throw new NullReferenceException($"Операции с данным Id:{idAction} нету.");
            }
            await _userService.ResetPassword(resultUserId, newPassword);

        }
        #endregion
    }
}
