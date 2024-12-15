using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBreak.Infrastructure
{
    public class DatabaseTesterService
    {
        private readonly ApplicationDbContext _context;

        // Впровадження залежності через конструктор
        public DatabaseTesterService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для перевірки підключення і додавання тестових даних
        public async Task TestConnectionAsync()
        {
            try
            {
                // Перевірка підключення до бази даних
                if (_context.Database.CanConnect())
                {
                    Console.WriteLine("Успішно підключено до бази даних!");

                    // Додавання тестового користувача
                    var testUser = new UserEntity
                    {
                        Username = "TestUser",
                        Email = "testuser@example.com",
                        Password = "testpassword"
                    };

                    _context.Users.Add(testUser);
                    await _context.SaveChangesAsync();

                    Console.WriteLine("Дані успішно додано до бази!");
                }
                else
                {
                    Console.WriteLine("Не вдалося підключитися до бази даних.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
