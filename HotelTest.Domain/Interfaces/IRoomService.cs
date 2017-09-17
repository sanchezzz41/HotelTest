using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Models;

namespace HotelTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с комнатами
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Предоставляет список комнат
        /// </summary>
        List<Room> Rooms { get; }
        
        /// <summary>
        /// Добавляет комнату в отель
        /// </summary>
        /// <param name="model">Параметры комнаты</param>
        /// <returns>Возвращает номер комнаты</returns>
        Task<int> AddAsync(RoomModel model);

        /// <summary>
        /// Изменяет комнату
        /// </summary>
        /// <param name="idRoom">Номер комнаты</param>
        /// <param name="model">Параметры комнаты</param>
        /// <returns></returns>
        Task EditAsync(int idRoom, RoomModel model);

        /// <summary>
        /// Возвращает список комнат
        /// </summary>
        /// <returns></returns>
        Task<List<Room>> GetAsync();


    }
}
