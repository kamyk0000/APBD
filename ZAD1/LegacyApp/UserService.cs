using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyApp
{
    public interface ICreditLimiter
    {
        string ClientType { get; }

        void SetCreditLimit(User user);
    }

    public class VeryImportantClient : ICreditLimiter
    {
        public string ClientType => "VeryImportantClient";

        public void SetCreditLimit(User user)
        {
            user.HasCreditLimit = false;
        }
    }
    
    public class ImportantClient : ICreditLimiter
    {
        public string ClientType => "ImportantClient";

        public void SetCreditLimit(User user)
        {
            using var userCreditService = new UserCreditService();
            user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
        }
    }
    public class UserService
    {
        private readonly List<ICreditLimiter> _creditLimiters;

        public UserService()
        {
            _creditLimiters = new List<ICreditLimiter>
            {
                new ImportantClient(),
                new VeryImportantClient()
            };
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            /*
            
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            */

            /*
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            */
            
            var user = new User(firstName, lastName, email, dateOfBirth, clientId);
            
            
            if (ProcessCreditLimit(user)) { return false; }
                
            UserDataAccess.AddUser(user);
            
            return true;

            bool ProcessCreditLimit(User fUser)
            {
                var creditLimiters =_creditLimiters.FirstOrDefault(p => p.ClientType == user.Client.Type);
                creditLimiters?.SetCreditLimit(fUser);

                if (creditLimiters == null)
                {
                    SetDefaultCreditLimit(fUser);
                }

                return ValidateCreditLimit(fUser);
            }

            void SetDefaultCreditLimit(User fUser)
            {
                fUser.HasCreditLimit = true;
            
                using var userCreditService = new UserCreditService();
                fUser.CreditLimit = userCreditService.GetCreditLimit(fUser.LastName, fUser.DateOfBirth);
            }
            
            bool ValidateCreditLimit(User fUser)
            {
                return fUser.HasCreditLimit && fUser.CreditLimit < 500;
            }
        }
    }
}
