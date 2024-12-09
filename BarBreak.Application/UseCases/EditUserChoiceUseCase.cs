using BarBreak.Core.Course;
using System;

namespace BarBreak.Application.UseCases
{
    public interface IUserChoiceRepository
    {
        void UpdateChoice(string username, string newCourseName);
    }

    public class EditUserChoiceUseCase
    {
        private readonly IUserChoiceRepository _repository;

        public EditUserChoiceUseCase(IUserChoiceRepository repository)
        {
            _repository = repository;
        }

        public void Execute(string username, string newCourseName)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(newCourseName))
            {
                throw new ArgumentException("New course name cannot be empty.");
            }

            _repository.UpdateChoice(username, newCourseName);
        }
    }
}