using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMessenger.ViewModels
{
    /// <summary>
    /// Содержит даные для входа на страницу комнаты.
    /// </summary>
    public class ChatRoomVM
    {
        /// <summary>
        /// Логин для формы для запросов.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Псевдоним для отображения.
        /// </summary>
        public string Pseudonym { get; set; }
        /// <summary>
        /// Пароль для API.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Список доступных собеседников.
        /// </summary>
        public List<Opponent> Opponents { get; set; }
        /// <summary>
        /// Список сообщений для начальной загрузки.
        /// </summary>
        public List<TestTaskMessenger.Models.Message> Messages { get; set; }
    }
}
