using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий тип комнаты
    /// </summary>
    public class TypeRoom
    {
        /// <summary>
        /// Id, и так же цена за тип команты
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RoomOptions Id { get; set; }

        /// <summary>
        /// Название типа команты
        /// </summary>
        public string NameOfTypeRoom { get; set; }

        /// <summary>
        /// Комнтаы
        /// </summary>
        public virtual List<Room> Rooms { get; set; }

        public TypeRoom()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomOpt">Id комнаты</param>
        /// <param name="nameOfTypeRoom">Тип комнтаы(название)</param>
        public TypeRoom(RoomOptions roomOpt, string nameOfTypeRoom)
        {
            Id = roomOpt;
            NameOfTypeRoom = nameOfTypeRoom;
        }
    }
}
