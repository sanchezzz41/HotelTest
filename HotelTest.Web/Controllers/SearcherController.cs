using System;
using System.Collections.Generic;
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
    /// Контроллер для поиска(пока только для комнат)
    /// </summary>
    [Authorize]
    [Route("Searcher")]
    public class SearcherController : Controller
    {
        private readonly IRoomService _roomService;

        public SearcherController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        //Ищет комнаты
        [HttpPost("Rooms")]
        public async Task<object> GetVisitors([FromBody] RoomSearchModel model)
        {
            var resultList = await _roomService.SearchAsync(model);
            return resultList.Select(x => x?.RoomView());
        }
    }
}
