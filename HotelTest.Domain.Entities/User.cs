using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTest.Domain.Entities
{

    /// <summary>
    /// Предоставляет класс для пользоваетля
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }


        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Fio { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Соль для хэша
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Id роли
        /// </summary>
        [ForeignKey(nameof(Role))]
        public RolesOptions RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Записи
        /// </summary>
        public virtual List<Visitor> Visitors { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        /// <param name="fio">Имя пользователя</param>
        /// <param name="email">Еmail пользователя</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="passwordSalt">Cоль для пароля</param>
        /// <param name="passworhHash">Хэш пароля</param>
        /// <param name="role">Роль</param>
        public User(string fio, string email, string phoneNumber, string passwordSalt, string passworhHash,
            RolesOptions role)
        {
            Id = Guid.NewGuid();
            Fio = fio;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordSalt = passwordSalt;
            PasswordHash = passworhHash;
            RoleId = role;
        }
    }
}
