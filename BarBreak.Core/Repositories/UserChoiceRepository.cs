using System;
using System.Collections.Generic;

namespace BarBreak.Core.Repositories
{
    public interface IUserChoiceRepository
    {
        void AddChoice(string userId, string courseName);
        void RemoveChoice(string userId);
        string GetChoice(string userId);
    }

    public class UserChoiceRepository : IUserChoiceRepository
    {
        // Наприклад, використаємо просте сховище (можна замінити на реальну базу даних)
        private readonly Dictionary<string, string> _userChoices = [];

        public void AddChoice(string userId, string courseName)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(courseName))
            {
                throw new ArgumentException("User ID and course name cannot be empty.");
            }

            _userChoices[userId] = courseName;
        }

        public void RemoveChoice(string userId)
        {
            if (!_userChoices.ContainsKey(userId))
            {
                throw new KeyNotFoundException("User choice not found.");
            }

            _userChoices.Remove(userId);
        }

        public string GetChoice(string userId)
        {
            if (!_userChoices.TryGetValue(userId, out string? value))
            {
                throw new KeyNotFoundException("User choice not found.");
            }

            return value;
        }
    }
}