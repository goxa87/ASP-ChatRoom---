using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskMessenger.Data;
using TestTaskMessenger.Models;
using TestTaskMessenger.Services;

namespace TestTaskMessenger.Controllers
{
    /// <summary>
    /// API для доступа к сервису.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;
        // Внедрение зависимостей.
        private readonly IGetMessageService _messageService;
        private readonly IPostMessageService _messageServicePost;


        public MessageApiController(
            SignInManager<User> signInManager,
            DataContext context,
            IGetMessageService messageService,
            IPostMessageService postMessageService
            )
        {
            _signInManager = signInManager;
            _context = context;
            _messageService = messageService;
            _messageServicePost = postMessageService;
        }


        /// <summary>
        /// Получение определнного количества последних сообщений из чата
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="OpponenmtId"></param>
        /// <returns></returns>
        [Route("GetMessages")]
        public async Task<List<Message>> GetMessages(string login, string password, string opponentId)
        {          
            // Валидация пользовательских данных.
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == login);
            if (user == null)
            {
                // Пользователь не нашелся.
                this.HttpContext.Response.StatusCode = 403;
                return null;
            }
            else
            {                
                var signInRezult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                // неверный пароль.
                if (signInRezult.Succeeded)
                {
                    // Для получения сообщений общего чата.
                    if (opponentId == "0")
                        return _messageService.GetCommonChatMessageHistory(20);
                    else
                    {
                        // Сообщения приватные.
                        return _messageService.GetOpponentMessageHistory(user, opponentId, 20);
                    }
                }
                else
                {
                    this.HttpContext.Response.StatusCode = 401;
                    return null;
                }
            }
        }
        /// <summary>
        /// Получение сообщений приватного чата с пометкой непрочитано.
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <param name="opponentId">id собеседника</param>
        /// <returns></returns>
        [Route("GetCurrentUnreadMessages")]
        public async Task<List<Message>> GetCurrentUnreadMessages(string login, string password, string opponentId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == login);
            if (user == null)
            {
                this.HttpContext.Response.StatusCode = 403;
                return null;
            }
            else
            {
                var signInRezult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (signInRezult.Succeeded)
                {
                    if (opponentId == "0")
                        return null;
                    else
                    {
                        var rez = await  _context.Messages.Where(e=> e.Read == false && e.SenderId==opponentId &&  e.ReciverId==user.Id).OrderByDescending(e=>e.Date).ToListAsync();
                        // Поставить пометку о том что сообщения прочитаны.
                        foreach (var e in rez)
                            e.Read = true;

                        _context.UpdateRange(rez);
                         await _context.SaveChangesAsync();
                        return rez;
                    }
                }
                else
                {
                    this.HttpContext.Response.StatusCode = 401;
                    return null;
                }
            }
        }

        // ***************************** POST methods
        /// <summary>
        /// Отправка сообщения собеседнику.
        /// </summary>
        /// <param name="login">логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="opponentId">ID собеседника.</param>
        /// <param name="text">Текст сообщения.</param>
        /// <returns></returns>
        [Route("SendMessage")]
        public async Task<StatusCodeResult> SendMessage(string login, string password, string opponentId, string text)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == login);
            if (user == null)
            {
                //this.HttpContext.Response.StatusCode = 404;
                return StatusCode(403);
            }
            else
            {
                var signInRezult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (signInRezult.Succeeded)
                {                    
                    await _messageServicePost.PostMessage(_context, user,opponentId, text);                    
                    return StatusCode(200);
                }
                else
                {
                    this.HttpContext.Response.StatusCode = 401;
                    return StatusCode(401);
                }
            }
        }

    }
}