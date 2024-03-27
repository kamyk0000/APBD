using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{

    public class User
    {
        public Client Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
        //private ClientRepository _clientRepository { get; set; }

        public User(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            ValidateName(firstName, lastName);
            ValidateEmail(email);
            ValidateAge(dateOfBirth);
            
            var clientRepository = new ClientRepository();

            this.Client = clientRepository.GetById(clientId);
            this.DateOfBirth = dateOfBirth;
            this.EmailAddress = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        private static void ValidateName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("First name and last name cannot be null or empty.");
            }
        }
        
        private static void ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email address format.");
            }
        }

        private static void ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            if (age < 21)
            {
                throw new ArgumentException("User must be at least 21 years old.");
            }
        }
        
        //public int GetAge?
        
    }
    
}
