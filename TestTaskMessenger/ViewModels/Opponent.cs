using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMessenger.ViewModels
{
    /// <summary>
    /// Модель для заполнения списка собеседников.
    /// </summary>
    public class Opponent
    {
        /// <summary>
        ///  ID в БД.
        /// </summary>
        public string OpponentId { get; set; }
        /// <summary>
        /// Псевдоним для отображения.
        /// </summary>
        public string Pseudonym { get; set; }
    }
}
