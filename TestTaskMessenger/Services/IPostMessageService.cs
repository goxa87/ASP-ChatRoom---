using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMessenger.Data;
using TestTaskMessenger.Models;

namespace TestTaskMessenger.Services
{
    public interface IPostMessageService
    {
        /// <summary>
        /// Добавляет сообщения в общий чат.
        /// </summary>
        /// <param name="context"></param>
        public Task PostMessage(DataContext context, User user, string OpponentId, string value);

        /// <summary>
        /// Добавляет сообщения к чату с пользователем.
        /// </summary>
        /// <param name="context"></param>
        public Task PostMessageToOpponent(DataContext context, User user, string value, string OpponentId);

    }
}
