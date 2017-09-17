using System;  
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelTest.Database;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Domain.Options;
using Microsoft.EntityFrameworkCore;

namespace HotelTest.Domain.Services
{
    /// <summary>
    /// Класс реализующий IRoomService
    /// </summary>
    public class RoomService : IRoomService
    {
        /// <summary>
        /// Предоставляет список комнат
        /// </summary>
        public List<Room> Rooms => _context.Rooms
            .Include(x => x.Visitors)
            .Include(x => x.TypeRoom)
            .ToList();

        private readonly DatabaseContext _context;
        private readonly HotelOptions _options;

        public RoomService(DatabaseContext context, HotelOptions options)
        {
            _context = context;
            _options = options;
        }


        /// <summary>
        /// Добавляет комнату в отель
        /// </summary>
        /// <param name="model">Параметры комнаты</param>
        /// <returns>Возвращает номер комнаты</returns>
        public async Task<int> AddAsync(RoomModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }
            var number = 0;
            if (!_context.Rooms.Any())
            {
                number = 1;
            }
            else
            {
                number = await _context.Rooms.MaxAsync(x => x.Id);
                number++;
            }

            var resultRoom = new Room(number, model.MaxCount, model.IsFree,model.Price, model.RoomOptionId);

            await _context.Rooms.AddAsync(resultRoom);
            await _context.SaveChangesAsync();
            
            return resultRoom.Id;
        }

        /// <summary>
        /// Изменяет комнату
        /// </summary>
        /// <param name="idRoom">Номер комнаты</param>
        /// <param name="model">Параметры комнаты</param>
        /// <returns></returns>
        public async Task EditAsync(int idRoom, RoomModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultRoom = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == idRoom);
            if (resultRoom == null)
            {
                throw new NullReferenceException($"Комнаты с таким id {idRoom} нету.");
            }

            resultRoom.RoomOptionId = model.RoomOptionId;
            resultRoom.IsFree = model.IsFree;
            resultRoom.MaxCount = model.MaxCount;
            resultRoom.Price = model.Price;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список комнат
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetAsync()
        {
            return await _context.Rooms
                .Include(x => x.Visitors)
                .Include(x => x.TypeRoom)
                .ToListAsync(); 
        }
    }
}
