using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelTest.Database;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Domain.Utilits;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelTest.Domain.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Реализация интерфейса IUserService
    /// </summary>
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<User> Users => _context.Users
            .Include(x => x.Role)
            .Include(x => x.Visitors)
            .ToList();

        public UserService(DatabaseContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Добавляет пользователя в хранилище
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id нового пользователя</returns>
        public async Task<Guid> AddAsync(UserRegisterModel userModel)
        {
            if (userModel == null)
            {
                throw new NullReferenceException("Ссылка указывает на null.");
            }

            var checkOnEmail = await _context.Users.AnyAsync(x => x.Email == userModel.Email);
            if (checkOnEmail)
            {
                throw new ArgumentException($"Пользователь с таким мылом: {userModel.Email} уже существует.");
            }

            var passwordSalt = Randomizer.GetString(10);
            //Сначала пароль потом соль
            var passwordHash = _passwordHasher.HashPassword(null, userModel.Password + passwordSalt);

            var resultUser = new User(userModel.Fio, userModel.Email, userModel.PhoneNumber, passwordSalt, passwordHash,
                userModel.RoleId);

            await _context.Users.AddAsync(resultUser);
            await _context.SaveChangesAsync();

            return resultUser.Id;
        }

        /// <summary>
        /// Изменяет пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid id, UserEditModel userModel)
        {
            if (userModel == null)
            {
                throw new NullReferenceException("Ссылка указывает на null.");
            }

            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким id: {id} не существует!");
            }
            var checkOnEmail = await _context.Users.AnyAsync(x => x.Email == userModel.Email);
            if (checkOnEmail)
            {
                throw new ArgumentException($"Пользователь с таким мылом: {userModel.Email} уже существует.");
            }

            resultUser.Email = userModel.Email;
            resultUser.Fio = userModel.Fio;
            resultUser.PhoneNumber = userModel.PhoneNumber;

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id">Id удаляемого пользователя</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким id: {id} не существует!");
            }
            _context.Users.Remove(resultUser);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Visitors)
                .ToListAsync();
        }

        /// <summary>
        /// Возвращает пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(Guid id)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким id: {id} не существует!");
            }
            return resultUser;
        }
    }
}
