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
        public int RoomCount { get; set; }

        /// <summary>
        /// Цена за стандартную комнату(по умолчанию 1)
        /// </summary>
        public int PriceForStandard { get; set; } = 1;

        /// <summary>
        /// Цена за полу-люкс комнату(по умолчанию 2)
        /// </summary>
        public int PriceForHalfLux { get; set; } = 2;

        /// <summary>
        /// Цена за люкс комнату(по умолчанию 3)
        /// </summary>
        public int PriceForLux { get; set; } = 3;
    }
}
