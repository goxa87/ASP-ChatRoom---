using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMessenger.Data;
using TestTaskMessenger.Models;

namespace TestTaskMessenger.Services
{
    /// <summary>
    /// Отправка сообщений.
    /// </summary>
    public class PostMessageService : IPostMessageService
    {
        private readonly DataContext _context;

        public PostMessageService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        /// <param name="user">Пользователь отправляющий сообщение (валидация в контроллере).</param>
        /// <param name="value">текст сообщения</param>
        public async Task PostMessage(DataContext context, User user, string opponentId, string value)
        {
            var message = new Message
            {
                Text = value,
                SenderName = user.Pseudonym,
                SenderId=user.Id,
                Read = false,
                Date = DateTime.Now,
                ReciverId=opponentId
            };
            if (opponentId == "0")
                message.CommonChat = true;
            else
                message.CommonChat = false;

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Отправляет сообщения для конкретного пользователя.
        /// </summary>
        /// <param name="context">контекст БД.</param>
        /// <param name="user">Отправитель сообщения.</param>
        /// <param name="value">Текст сообщения.</param>
        /// <param name="OpponentId">ID оппонента.</param>
        /// <returns></returns>
        public async Task PostMessageToOpponent(DataContext context, User user, string value, string OpponentId)
        {
            // Не пригодился.))
            throw new NotImplementedException();
        }
    }
}
