using System;
using System.Collections.Generic;

namespace BarBreak.Core.Repositories;
{
    public class UserChoiceRepository : IUserChoiceRepository
    {
        private readonly Dictionary<string, string> _userChoices = new Dictionary<string, string>();

        public void UpdateChoice(string username, string newCourseName)
        {
            if (_userChoices.ContainsKey(username))
            {
                Console.WriteLine($"User '{username}' is switching from '{_userChoices[username]}' to '{newCourseName}'.");
                _userChoices[username] = newCourseName;
            }
            else
            {
                Console.WriteLine($"User '{username}' is enrolling in the course '{newCourseName}' for the first time.");
                _userChoices.Add(username, newCourseName);
            }
        }

        public string GetUserChoice(string username)
        {
            if (_userChoices.TryGetValue(username, out var courseName))
            {
                return courseName;
            }
            throw new ArgumentException($"User '{username}' does not exist.");
        }
    }
}