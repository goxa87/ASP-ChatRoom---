using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskMessenger.Data;
using TestTaskMessenger.Models;
using TestTaskMessenger.ViewModels;

namespace TestTaskMessenger.Controllers
{
    /// <summary>
    /// Представляет непосредственно форму чата.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;


        public HomeController(ILogger<HomeController> logger,
            DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Использует класс UserParam для начальной инициализации данных в представлении.
        /// </summary>
        /// <param name="param">Параметры формирования страницы</param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index(UserParam param)
        {
            // Эти параметры берутся только после перехода с формы логина или регистрации. 
            // Защищает от того, чтоб не получилось так что форма загружена, а логина пароля для апи нет.
            if (param.Password == null || param.Password==null) return RedirectToAction("Login", "Account");

            // Логика для формирования страницы чата.
            var chatRoomData = new ChatRoomVM();
            chatRoomData.Login = param.Login;
            chatRoomData.Password = param.Password;
            // Пользователь авторизованый.
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserName.ToLower() == param.Login.ToLower());
            chatRoomData.Pseudonym = user.Pseudonym;
            // Список собеседников.
            chatRoomData.Opponents = new List<Opponent>();
            chatRoomData.Opponents.Add(new Opponent { OpponentId = "0", Pseudonym = "Общий чат" });
            chatRoomData.Opponents.AddRange(_context.Users.Select(e => new Opponent { OpponentId = e.Id, Pseudonym = e.Pseudonym }).OrderBy(e=>e.Pseudonym).ToList());
            // Список сообщений для общего чата.
            chatRoomData.Messages = await _context.Messages.Where(e => e.CommonChat == true).OrderByDescending(e => e.Date).Take(12).ToListAsync();
            
            return View(chatRoomData);
        }           
    }
}
