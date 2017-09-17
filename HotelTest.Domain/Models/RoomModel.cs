using System.ComponentModel.DataAnnotations;
using HotelTest.Domain.Entities;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Модель для работы с комнатой
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Максимальное количество людей в комнате
        /// </summary>
        [Required]
        public int MaxCount { get; set; }

        /// <summary>
        /// Свободна ли комната(true = свободна)
        /// </summary>
        [Required]
        public bool IsFree { get; set; }



        /// <summary>
        /// Тип комнаты
        /// </summary>
        [Required]
        public RoomOptions RoomOptionId { get; set; }
    }
}
