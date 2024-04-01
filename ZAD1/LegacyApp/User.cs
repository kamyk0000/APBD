using System;

namespace LegacyApp;

public class User
{
    public User(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
    {
        UserValidator.ValidateUser(firstName, lastName, email, dateOfBirth);

        Client = client;
        DateOfBirth = dateOfBirth;
        EmailAddress = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Client Client { get; internal set; }
    public DateTime DateOfBirth { get; internal set; }
    public string EmailAddress { get; internal set; }
    public string FirstName { get; internal set; }
    public string LastName { get; internal set; }
    public bool HasCreditLimit { get; internal set; }
    public int CreditLimit { get; internal set; }

    //public int GetAge?
}