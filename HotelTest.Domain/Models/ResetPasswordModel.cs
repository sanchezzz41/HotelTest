using System;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Класс для востановления пароля
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Id пользователя, чей пароль будет востановлен
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Код который отправляется пользователю для воставновления пароля
        /// </summary>
        public string Code { get; set; }
    }
}
