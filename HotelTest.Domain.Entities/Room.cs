using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace HotelTest.Domain.Entities
{
    public class Room
    {
        /// <summary>
        /// Id комнаты, и так же её номер
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(0, Int32.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Максимальное количество людей,которые могут проживать в комнате
        /// </summary>
        [Range(0, Int32.MaxValue)]
        [Required]
        public int MaxCount { get; set; }

        /// <summary>
        /// Свободна ли комната(true = свободна)
        /// </summary>
        [Required]
        public bool IsFree { get; set; }

        /// <summary>
        /// Цена за комнату
        /// </summary>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// Тип комнаты
        /// </summary>
        public TypeRoom TypeRoom { get; set; }

        /// <summary>
        /// Id типа комнат
        /// </summary>
        [ForeignKey(nameof(TypeRoom))]
        public RoomOptions RoomOptionId { get; set; }

        /// <summary>
        /// Записи
        /// </summary>
        public List<Visitor> Visitors { get; set; }

        public Room()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Номер комнаты</param>
        /// <param name="maxCount">Максимальное число людей, которые могут проживать в комнате</param>
        /// <param name="price">Цена за комнату</param>
        /// <param name="roomOption">Тип комнаты</param>
        /// <param name="isFree">Свободна ли комната</param>
        public Room(int id, int maxCount, bool isFree, int price, RoomOptions roomOption)
        {
            Id = id;
            MaxCount = maxCount;
            IsFree = isFree;
            Price = price;
            RoomOptionId = roomOption;
        }
    }
}
