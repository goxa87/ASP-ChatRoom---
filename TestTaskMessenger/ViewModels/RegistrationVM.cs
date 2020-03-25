using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMessenger.ViewModels
{
    /// <summary>
    /// Модель формы регистрации.
    /// </summary>
    public class RegistrationVM
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required(ErrorMessage = "Не введен логин.")]
        [MaxLength(16,ErrorMessage ="Логин должен быть не более 16 символов.")]
        [Display(Name = "Логин")]
        public string LoginUser { get; set; }
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Не введен пароль.")]
        [MinLength(4,ErrorMessage ="Пароль должен быть не менее 4 символов")]
        [MaxLength(10, ErrorMessage = "Пароль должен быть не более 10 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        /// <summary>
        /// Псевдоним.
        /// </summary>
        [Required(ErrorMessage = "Не введен псевдоним.")]
        [MaxLength(10, ErrorMessage = "Псевдоним должен быть не более 10 символов")]
        [Display(Name = "Псевдоним")]
        public string Pseudonym { get; set; }
    }
}
