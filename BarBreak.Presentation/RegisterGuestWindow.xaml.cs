using BarBreak.Core.DTOs;
using BarBreak.Core.Services;
using System;
using System.Windows;

namespace BarBreak.Presentation
{
    public partial class RegisterGuestWindow : Window
    {
        private readonly DatabaseService _databaseService;

        public RegisterGuestWindow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(@"Data Source=C:\Git\Program_engineering\BarBreak.Presentation\Database\BarBreak.db"); // Оновлений шлях
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string secretWord = SecretWordTextBox.Text; // Додане поле
            bool acceptTerms = AcceptTermsCheckBox.IsChecked ?? false;

            // Валідація введених даних
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(secretWord) || // Додана перевірка
                !acceptTerms)
            {
                MessageBox.Show("Please fill in all fields and accept the terms.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка наявності електронної пошти в базі даних
            if (_databaseService.IsEmailTaken(email))
            {
                MessageBox.Show("Email is already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Створення об'єкта користувача
            var user = new UserDTO
            {
                Email = email,
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                SecretWord = secretWord // Додаємо секретне слово
            };

            // Збереження користувача в базу даних
            try
            {
                _databaseService.SaveUser(user);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закриваємо вікно після успішної реєстрації
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
