using System;

namespace LegacyApp;

public class UserService
{
    private readonly CreditLimitService _creditLimitService;

    public UserService()
    {
        var userCreditService = new UserCreditService();
        _creditLimitService = new CreditLimitService(userCreditService);
    }

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        var clientRepository = new ClientRepository();
        var client = clientRepository.GetById(clientId);

        var user = new User(firstName, lastName, email, dateOfBirth, client);

        _creditLimitService.SetCreditLimit(user);

        UserDataAccess.AddUser(user);
        return true;
    }
}