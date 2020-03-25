using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMessenger.ViewModels
{
    // Модель формы авторизации.
    public class LoginVM
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required(ErrorMessage ="Не введен логин.")]
        [Display(Name = "Логин")]
        public string LoginUser { get; set; }
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Не введен пароль.")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }
    }
}
