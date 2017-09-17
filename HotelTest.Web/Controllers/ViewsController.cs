using System.Linq;
using System.Threading.Tasks;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelTest.Web.Controllers
{
    /// <summary>
    /// Контроллер для отображения информации, которая доступна всем пользователям
    /// </summary>
    [Route("Views")]
    [Authorize]
    public class ViewsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IVisitorService _visitorService;

        public ViewsController(IVisitorService visitorService, IRoomService roomService, IUserService userService)
        {
            _visitorService = visitorService;
            _roomService = roomService;
            _userService = userService;
        }

        //Отображает пользователей
        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var result = await _userService.GetAsync();
            return result.Select(x => x?.UserView());
        }

        //Отображает комнаты
        [HttpGet("Rooms")]
        public async Task<object> GetRooms()
        {
            var result = await _roomService.GetAsync();
            return result.Select(x => x?.RoomView());
        }

        //Отображает посетителей
        [HttpGet("Visitors")]
        public async Task<object> GetVisitors()
        {
            var result = await _visitorService.GetAsync();
            return result.Select(x => x?.VisitorView());
        }

        //Ищет комнаты
        [HttpPost("Search")]
        public async Task<object> GetVisitors([FromBody] RoomSearchModel model)
        {
            var resultList = await _roomService.SearchAsync(model);
            return resultList.Select(x => x?.RoomView());
        }

    }
}
