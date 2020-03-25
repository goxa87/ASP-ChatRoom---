using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMessenger.Data;
using TestTaskMessenger.Models;

namespace TestTaskMessenger.Services
{
    /// <summary>
    /// Методы лоя получения сообщений из базы
    /// </summary>
    public class GetMessageService : IGetMessageService
    {
        private readonly DataContext _context;

        public GetMessageService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает сообщения из истори для общего чата.
        /// </summary>
        /// <param name="Count">Количество сообщений для загрузки</param>
        /// <returns>Список сообщений.</returns>
        public List<Message> GetCommonChatMessageHistory(int Count = 20)
        {
            return _context.Messages.Where(e => e.CommonChat == true)
                .OrderByDescending(e => e.Date).Take(Count).ToList();
        }
        /// <summary>
        /// Возвращает историю сообщений от пользователя в личном чате.
        /// </summary>
        /// <param name="user">Пользователья для которого будет показ.</param>
        /// <param name="OpponentId">ID пользователя.</param>
        /// <param name="count">Количество сообщений.</param>
        /// <returns>Список сообщений.</returns>
        public List<Message> GetOpponentMessageHistory(User user,  string OpponentId, int count = 20)
        {
            // ИЛИ потому-что сообщения должны быть и полученные и отправленные.
            var rez = _context.Messages.Where(e => (e.SenderId==user.Id && e.ReciverId==OpponentId) || (e.SenderId == OpponentId && e.ReciverId == user.Id))
                .OrderByDescending(e => e.Date).Take(count).ToList();           
            return rez;
        }

        /// <summary>
        /// Получает непрочитанные сообщения от пользователя.
        /// </summary>
        /// <param name="user">Пользователья для которого будет показ.</param>
        /// <param name="OpponentId">ID Пользователя.</param>
        /// <returns>Список сообщений.</returns>
        public List<Message> GetOpponentMessageNew(User user, string OpponentId, int lastMessageId)
        {
            var rez = _context.Messages.Where(e => e.SenderId == user.Id && e.ReciverId == OpponentId && e.MessageId > lastMessageId)
                .OrderByDescending(e => e.Date).ToList();

            return rez;
        }
    }
}
