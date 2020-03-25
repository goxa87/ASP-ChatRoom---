using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskMessenger.Models;

namespace TestTaskMessenger.Data
{
    // Контекст БД.
    public class DataContext : IdentityDbContext<User>
    {
        // Для Работы с БД используйте минрации.
        // 1 - измение строку подключения для вашего сервера.
        // 2 - в консоли введите Update-Database

        /// <summary>
        /// Сообщения.
        /// </summary>
        public DbSet<Message> Messages { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Изменить строку подключения на вашу.
            builder.UseSqlServer(@"Server=georgiy-пк\sqlexpress;DataBase=MessengerTT;Trusted_Connection=True;");
        }
    }
}
