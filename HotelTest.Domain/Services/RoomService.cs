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
            if (number > _options.RoomCount)
            {
                throw new InvalidOperationException($"Вы больше не можете добавить комнаты в оттель, " +
                                                    $"так как маскимальное кол-во комнат {_options.RoomCount}, а вы уже добавляете {number}.");
            }

            var resultRoom = new Room(number, model.MaxCount, model.IsFree, model.RoomOptionId);

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
                throw new NullReferenceException($"Комнаты с таким id: {idRoom} нету.");
            }

            resultRoom.RoomOptionId = model.RoomOptionId;
            resultRoom.IsFree = model.IsFree;
            resultRoom.MaxCount = model.MaxCount;

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

        /// <summary>
        /// Возвращает комнату по Id
        /// </summary>
        /// <returns></returns>
        public async Task<Room> FindByIdAsync(int id)
        {
            var resultRoom = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == id);
            if (resultRoom == null)
            {
                throw new NullReferenceException($"Комнаты с таким id: {id} нету.");
            }
            return resultRoom;
        }

        /// <summary>
        /// Осуществляет поиск по полям, заданным в модели
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<Room>> SearchAsync(RoomSearchModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            if (model.MinPeoplecount >= model.MaxPeopleCount)
            {
                throw new ArgumentException(
                    $"Минимум не может быть больше или равен максимуму({model.MinPeoplecount}>!{model.MaxPeopleCount})");
            }

            //Нужно для проверки на тип комнаты
            var list = new List<RoomOptions>();
            if (model.IsStandart)
            {
                list.Add(RoomOptions.Standard);
            }
            if (model.IsHalfLux)
            {
                list.Add(RoomOptions.HalfLux);
            }
            if (model.IsLux)
            {
                list.Add(RoomOptions.Lux);
            }

            var min = model.MinPeoplecount;
            var max = model.MaxPeopleCount;

            var result = Rooms.Where(x => x.IsFree
                                          && x.MaxCount >= min
                                          && x.MaxCount <= max);
            //Елси хотя бы 1 критерий выбран, то так же ищем по типу номера
            if (model.IsStandart || model.IsHalfLux || model.IsLux)
            {
                result = result.Where(x => list.Contains(x.RoomOptionId));
            }
            return result.ToList();
        }
    }
}
