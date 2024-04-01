using System;
using System.Text.RegularExpressions;

namespace LegacyApp;

public static class UserValidator
{
    public static void ValidateUser(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        ValidateName(firstName, lastName);
        ValidateEmail(email);
        ValidateAge(dateOfBirth);
    }

    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            throw new ArgumentException("First name and last name cannot be null or empty.");
    }

    private static void ValidateEmail(string email)
    {
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(email, pattern)) throw new ArgumentException("Invalid email address format.");
    }

    private static void ValidateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21) throw new ArgumentException("User must be at least 21 years old.");
    }
}