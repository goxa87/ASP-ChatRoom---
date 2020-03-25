using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMessenger.Models
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int MessageId { get; set; }
        /// <summary>
        /// ID отправителя.
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// ID получателя.
        /// </summary>
        public string ReciverId { get; set; }
        /// <summary>
        /// Псевдоним отправителя, чтоб не обращаться к бд лишний раз.
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// Флаг прочитаности.
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Дата отправки.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Флаг общего чата.
        /// </summary>
        public bool CommonChat { get; set; }
    }
}
