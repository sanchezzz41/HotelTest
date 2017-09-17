using HotelTest.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Модель для регистрации пользователя
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Fio { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }


        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Роль, к которой принадлежит пользователь
        /// </summary>
        public RolesOptions RoleId { get; set; }

 
    }
}
