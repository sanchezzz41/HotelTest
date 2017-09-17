
namespace HotelTest.Domain.Entities
{
    /// <summary>
    /// Перечеслитель предоставляющий типы комнат, и цену за 12 часов
    /// </summary>
    public enum RoomOptions
    {
        /// <summary>
        /// Стандартная комната(цена 1000)
        /// </summary>
        Standard = 1000,
        /// <summary>
        /// Полу-люкс комната(цена 2000)
        /// </summary>
        HalfLux = 2000,
        /// <summary>
        /// Люкс комната(цена 3000)
        /// </summary>
        Lux = 3000
    }
}
