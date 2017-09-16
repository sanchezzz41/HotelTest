using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTest.Domain.Entities
{
    public class Room
    {
        /// <summary>
        /// Id комнаты, и так же её номер
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public uint Id { get; set; }

        /// <summary>
        /// Максимальное количество людей в комнате
        /// </summary>
        public uint MaxCount { get; set; }

        /// <summary>
        /// Свободна ли комната(true = свободна)
        /// </summary>
        public bool IsFree { get; set; }

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
    }
}
