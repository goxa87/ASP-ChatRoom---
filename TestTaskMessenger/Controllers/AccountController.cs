using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskMessenger.Models;
using TestTaskMessenger.ViewModels;

namespace TestTaskMessenger.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInMamager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            SignInManager<User> signInMamager,
            UserManager<User> userManager
            ) 
        {
            _signInMamager = signInMamager;
            _userManager = userManager;
        }
        /// <summary>
        /// Форма авторизации.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Форма регистрации.
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            return View();
        }
        
        /// <summary>
        /// POST авторизации.
        /// </summary>
        /// <param name="model">Модель из представления.</param>
        /// <returns></returns>
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model=null)
        {
            if (ModelState.IsValid)
            {
                var regRezult = await _signInMamager.PasswordSignInAsync(model.LoginUser, model.Password, false, false);
                if (regRezult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new UserParam { Login = model.LoginUser, Password = model.Password });
                }
                else
                {
                    ModelState.AddModelError("", "Неверные данные");
                    return View(model);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Неверные данные");
                return View(model);
            }
            
        }
        /// <summary>
        /// POST на регистрацию.
        /// </summary>
        /// <param name="model">Модель из представления.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            if (await _userManager.Users.AnyAsync(e => e.Pseudonym == model.Pseudonym))
            {
                ModelState.AddModelError("", "Пользователь с таким псевдонимом уже существует");
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.LoginUser,
                    Pseudonym = model.Pseudonym
                };

                var createRezult = await _userManager.CreateAsync(user, model.Password);
                if (createRezult.Succeeded)
                {
                    await _signInMamager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home",new UserParam { Login = model.LoginUser, Password=model.Password});
                }
                else
                {
                    foreach (var err in createRezult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Неверные данные.");
                return View(model);
            }
        }

        /// <summary>
        /// Разлог.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInMamager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}