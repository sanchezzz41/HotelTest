using System.Linq;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelTest.Web.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с комнатами
    /// </summary>
    [Route("Admin/Rooms")]
    [Authorize(Roles = nameof(RolesOptions.Admin))]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Возвращает список комнат
        [HttpGet]
        public async Task<object> GetRooms()
        {
            var resultList = await _roomService.GetAsync();
            return resultList.Select(x => x?.RoomView());
        }

        // Добавляет комнату в оттель
        [HttpPost]
        public async Task<object> AddRoom([FromBody] RoomModel roomModel)
        {
            return await _roomService.AddAsync(roomModel);
        }

        // Изменяет комнату 
        [HttpPut]
        public async Task EditRoom([FromBody] RoomModel roomModel, [FromQuery] int idRoom)
        {
            await _roomService.EditAsync(idRoom, roomModel);
        }
    }
}
