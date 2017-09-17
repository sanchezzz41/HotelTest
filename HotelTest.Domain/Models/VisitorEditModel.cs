using System;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Модель для изменения записи
    /// </summary>
    public class VisitorEditModel
    {
        /// <summary>
        /// Id комнаты
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата въезда
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTime DateOfDeparture { get; set; }
    }
}
