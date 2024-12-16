using System;
using Microsoft.Data.Sqlite;
using BarBreak.Core.DTOs;

namespace BarBreak.Core.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Email TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL,
                        SecretWord TEXT,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL
                    );
                ";
                command.ExecuteNonQuery();
            }
        }

        public void SaveUser(UserDTO user)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Users (Email, Username, Password, SecretWord, FirstName, LastName)
                    VALUES ($email, $username, $password, $secretWord, $firstName, $lastName);
                ";
                command.Parameters.AddWithValue("$email", user.Email);
                command.Parameters.AddWithValue("$username", user.Username);
                command.Parameters.AddWithValue("$password", user.Password);
                command.Parameters.AddWithValue("$secretWord", user.SecretWord);
                command.Parameters.AddWithValue("$firstName", user.FirstName);
                command.Parameters.AddWithValue("$lastName", user.LastName);
                command.ExecuteNonQuery();
            }
        }

        public bool IsEmailTaken(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(1) FROM Users WHERE Email = $email;";
                command.Parameters.AddWithValue("$email", email);
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
