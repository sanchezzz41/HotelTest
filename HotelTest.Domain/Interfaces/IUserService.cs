using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Models;

namespace HotelTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        List<User> Users { get; }

        /// <summary>
        /// Добавляет пользователя в хранилище
        /// </summary>
        /// <param name="userModel">Модель пользователя для добавления</param>
        /// <returns>Id нового пользователя</returns>
        Task<Guid> AddAsync(UserRegisterModel userModel);

        /// <summary>
        /// Изменяет пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task EditAsync(Guid id, UserEditModel userModel);

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id">Id удаляемого пользователя</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();

        /// <summary>
        /// Возвращает пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> FindByIdAsync(Guid id);

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idUser">ID пользователя, которому надо сменить пароль</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        Task ResetPassword(Guid idUser, string newPassword);
    }
}
