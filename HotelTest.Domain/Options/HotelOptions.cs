using System;
using System.Collections.Generic;
using System.Text;

namespace HotelTest.Domain.Options
{
    /// <summary>
    /// Класс содержащий настройки для отеля
    /// </summary>
    public class HotelOptions
    {
        /// <summary>
        /// Макс. количество комнат
        /// </summary>
        public uint RoomCount { get; set; }
    }
}
