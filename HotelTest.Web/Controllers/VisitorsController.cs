using System;
using System.Linq;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using HotelTest.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelTest.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с записями
    /// </summary>
    [Route("Visitors")]
    [Authorize]
    public class VisitorsController : Controller
    {
        private readonly IVisitorService _visitorService;

        public VisitorsController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        //Отображает посетителей
        [HttpGet]
        public async Task<object> GetVisitors()
        {
            var result = await _visitorService.GetAsync();
            return result.Select(x => x?.VisitorView());
        }

        //Добавляет запись в бд
        [HttpPost]
        public async Task<object> AddVisitor ([FromQuery] int idRoom,
            [FromQuery] Guid idUser,[FromBody]VisitorModel model)
        {
            return await _visitorService.AddAsync(idRoom, idUser, model);
        }

        //Записывает дату выезда и возвращает цену за проживание
        [HttpDelete("Departure")]
        public async Task<object> Departure([FromQuery] Guid idVisitor, [FromBody]VisitorModel model)
        {
            return await _visitorService.DepartureAsync(idVisitor, model);
        }

        //Изменяет запись о посетителе
        [HttpPut]
        public async Task EditVisitors([FromQuery] Guid idVisitor, [FromBody]VisitorEditModel model)
        {
             await _visitorService.EditAsync(idVisitor, model);
        }

        //Удаляет запись по Id
        [HttpDelete]
        [Authorize(Roles = nameof(RolesOptions.Admin))]
        public async Task DeleteVisitor([FromQuery] Guid idVisitor)
        {
             await _visitorService.DeleteAsync(idVisitor);
        }
    }
}
