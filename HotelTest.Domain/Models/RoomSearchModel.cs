using System;
using System.Collections.Generic;
using System.Text;

namespace HotelTest.Domain.Models
{
    /// <summary>
    /// Модель для поиска комнаты
    /// </summary>
    public class RoomSearchModel
    {
        public int MinPeoplecount { get; set; }

        public int MaxPeopleCount{ get; set; }

        public bool IsStandart { get; set; }

        public bool IsHalfLux { get; set; }

        public bool IsLux { get; set; }
    }
}
