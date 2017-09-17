using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelTest.Web.Extensions
{
    /// <summary>
    /// Класс для отображения ошибок
    /// </summary>
    public class ErrorFilter : IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var result = new
            {
                IsError = true,
                context.Exception.Message
            };
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(result);
        }
    }
}
