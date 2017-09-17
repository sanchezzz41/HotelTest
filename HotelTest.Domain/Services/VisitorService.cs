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
    /// Класс реализующий IVisitorService
    /// </summary>
    public class VisitorService : IVisitorService
    {
        /// <summary>
        /// Список записей
        /// </summary>
        public List<Visitor> Visitors => _context.Visitors
            .Include(x => x.Room)
            .Include(x => x.User)
            .ToList();

        private readonly DatabaseContext _context;
        private readonly IUserService _userService;

        private readonly IRoomService _roomService;

        //Настрокйи для отеля
        private readonly HotelOptions _hotelOptions;

        //Словарь для сопоставления типа комнаты и её цены
        private readonly Dictionary<RoomOptions, int> _prices;

        public VisitorService(DatabaseContext context, IRoomService roomService, IUserService userService,
            HotelOptions hotelOptions)
        {
            _context = context;
            _roomService = roomService;
            _userService = userService;
            _hotelOptions = hotelOptions;

            _prices = new Dictionary<RoomOptions, int>
            {
                {RoomOptions.Standard, _hotelOptions.PriceForStandard},
                {RoomOptions.HalfLux, _hotelOptions.PriceForHalfLux},
                {RoomOptions.Lux, _hotelOptions.PriceForLux},
            };
        }

        /// <summary>
        /// Добавляет запись о заселение поситителей
        /// </summary>
        /// <param name="idRoom">Id комнаты</param>
        /// <param name="idUser">Id пользователя</param>
        /// <param name="model">Модель для добавления</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(int idRoom, Guid idUser, VisitorModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultRoom = await _roomService.FindByIdAsync(idRoom);

            var resultUser = await _userService.FindByIdAsync(idUser);
            if (resultRoom.IsFree)
                //Занимаем комнату
                resultRoom.IsFree = false;
            else
                throw new ArgumentException($"Комната под id:{idRoom} уже занята.");

            var resultVisitor = new Visitor(resultRoom.Id, resultUser.Id, model.Date);
            await _context.Visitors.AddAsync(resultVisitor);
            await _context.SaveChangesAsync();

            return resultVisitor.Id;
        }

        /// <summary>
        /// Изменяет запись о поситители
        /// </summary>
        /// <param name="idVisitor">Id записи</param>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        public async Task EditAsync(Guid idVisitor, VisitorEditModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultVisitor = await _context.Visitors.SingleOrDefaultAsync(x => x.Id == idVisitor);
            if (resultVisitor == null)
            {
                throw new NullReferenceException($"Записи с таким id:{idVisitor} нету.");
            }

            resultVisitor.RoomId = model.RoomId;
            resultVisitor.UserId = model.UserId;
            resultVisitor.ArrivalDate = model.ArrivalDate;
            resultVisitor.DateOfDeparture = model.DateOfDeparture;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Изменяет запись, добавляя информацию о выезде
        /// </summary>
        /// <returns>Возвращает сумму, которую надо заплатить(в рублях)</returns>
        public async Task<int> DepartureAsync(Guid idVisitor, VisitorModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultVisitor =  Visitors.SingleOrDefault(x => x.Id == idVisitor);
            if (resultVisitor == null)
            {
                throw new NullReferenceException($"Записи с таким id:{idVisitor} нету.");
            }

            resultVisitor.DateOfDeparture = model.Date;
            //Освобождаем комнату
            resultVisitor.Room.IsFree = true;

            //Данные необходимые для подсчёта
            var days = resultVisitor.DateOfDeparture.DayOfYear - resultVisitor.ArrivalDate.DayOfYear;
            var peopleCount = resultVisitor.Room.MaxCount;
            var priceForType = _prices[resultVisitor.Room.RoomOptionId];
            //Формула для подсчёта сцены(почти произвольная)
            var resultPrice = peopleCount * priceForType + days * priceForType;

            //Записывается в запись итоговая цена
            resultVisitor.Price = resultPrice;
            await _context.SaveChangesAsync();

            return resultPrice;
        }

        /// <summary>
        /// Возвращает список всех записей
        /// </summary>
        /// <returns></returns>
        public async Task<List<Visitor>> GetAsync()
        {
            return await _context.Visitors
                .Include(x => x.Room)
                .Include(x => x.User)
                .ToListAsync();
            ;
        }
    }
}
