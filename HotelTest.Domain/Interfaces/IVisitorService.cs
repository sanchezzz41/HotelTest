using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Models;

namespace HotelTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с записями о поситителямях
    /// </summary>
    public interface IVisitorService
    {
        /// <summary>
        /// Список записей
        /// </summary>
        List<Visitor> Visitors { get; }

        /// <summary>
        /// Добавляет запись о заселение поситителей
        /// </summary>
        /// <param name="idRoom">Id комнаты</param>
        /// <param name="idUser">Id пользователя</param>
        /// <param name="model">Модель для добавления</param>
        /// <returns></returns>
        Task<Guid> AddAsync(int idRoom, Guid idUser, VisitorModel model);

        /// <summary>
        /// Изменяет запись о поситители
        /// </summary>
        /// <param name="idVisitor">Id записи</param>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        Task EditAsync(Guid idVisitor, VisitorEditModel model);

        /// <summary>
        /// Изменяет запись, добавляя информацию о выезде
        /// </summary>
        /// <returns>Возвращает сумму, которую надо заплатить(в рублях)</returns>
        Task<int> DepartureAsync(Guid idVisitor, VisitorModel model);

        /// <summary>
        /// Возвращает список всех записей
        /// </summary>
        /// <returns></returns>
        Task<List<Visitor>> GetAsync();
    }
}
