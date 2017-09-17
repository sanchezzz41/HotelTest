using System.Threading.Tasks;
using HotelTest.Domain.Entities;
using HotelTest.Domain.Interfaces;
using HotelTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = HotelTest.Domain.Interfaces.IAuthorizationService;

namespace HotelTest.Web.Controllers
{
    /// <summary>
    /// Контроллер для регистрации, входа и выхода с сайта
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorization;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, IUserService userService,
            IAuthorizationService authorization)
        {
            _userService = userService;
            _authorization = authorization;
            _signInManager = signInManager;
        }

        //Регистрация пользователя, которую может делать только админ
        [HttpPost("Register")]
        public async Task<object> RegisterUser([FromBody] UserRegisterModel model)
        {
            return await _userService.AddAsync(model);
        }

        //Вход на сайт
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
           var resultUser =  await _authorization.AuthorizationAsync(model.Email, model.Password);
            if (resultUser != null)
            {
                await _signInManager.SignInAsync(resultUser, false);
                return "Авторизация прошла успешно!";
            }
            return "Авторизаций провалилась!";
        }

        //Выход с сайта
        [Authorize]
        [HttpDelete]
        public async Task<object> LogOff()
        {
            await _signInManager.SignOutAsync();
            return "Вы вышли с аккаунта.";
        }

    }
}
