using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HotelTest.Domain.Entities;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Модель пользователя для изменения данных
    /// </summary>
    public class UserEditModel
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
        /// Номер телефона пользователя
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
    
    }
}
