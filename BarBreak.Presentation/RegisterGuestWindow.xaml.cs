using BarBreak.Core.DTOs;
using BarBreak.Core.User;
using System;
using System.Windows;
using System.Windows.Controls;
using BarBreak.Infrastructure; 
using BarBreak.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarBreak.Presentation
{
    public partial class RegisterGuestWindow : Window
    {
        private readonly IUserService _userService;
        public RegisterGuestWindow()
        {
            InitializeComponent();
            //_userService = userService;
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            bool acceptTerms = AcceptTermsCheckBox.IsChecked ?? false;

            // Валідація введених даних
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                !acceptTerms)
            {
                MessageBox.Show("Please fill in all fields and accept the terms.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseNpgsql("Host=localhost;Database=BarBreak;Username=postgres;Password=1909;");

                var newUser = new UserEntity
                {
                    Email = email,
                    Username = username,
                    Password = password, 
                    FirstName = firstName,
                    LastName = lastName,
                    RegistrationDate = DateTime.Now
                };

                // Створення контексту та збереження даних
                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
