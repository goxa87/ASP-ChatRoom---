using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMessenger.Models;

namespace TestTaskMessenger.Services
{
    public interface IGetMessageService
    {
        /// <summary>
        /// Получает сообщения общего чата
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<Message> GetCommonChatMessageHistory(int Count = 20);
        /// <summary>
        /// Возвращает историю сообщений от пользователя.
        /// </summary>
        /// <param name="user">Пользователья для которого будет показ.</param>
        /// <param name="OpponentId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Message> GetOpponentMessageHistory(User user, string OpponentId, int count = 2);

        /// <summary>
        /// Возвращвет непрочитаные сообщения от пользователя.
        /// </summary>
        /// <param name="user">Пользователья для которого будет показ.</param>
        /// <param name="OpponentId"></param>
        /// <returns></returns>
        public List<Message> GetOpponentMessageNew(User user, string OpponentId, int lastMessageId);
    }
}
