using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTest.Domain.Entities
{
    /// <summary>
    /// Класс представляющий запись посетителя
    /// </summary>
    public class Visitor
    {
        /// <summary>
        /// Id Записи
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Id комнаты
        /// </summary>
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public Room Room { get; set; }


        /// <summary>
        /// Id пользователя
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Дата въезда
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTime DateOfDeparture { get; set; }

        /// <summary>
        /// Итоговая цена за проживание
        /// </summary>
        public int Price { get; set; }

        public Visitor()
        {

        }

        public Visitor(int roomId, Guid userId, DateTime arrivalDate)
        {
            Id = Guid.NewGuid();
            RoomId = roomId;
            UserId = userId;
            ArrivalDate = arrivalDate;
        }
    }
}
