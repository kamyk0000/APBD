using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyApp;

public class CreditLimitService(IUserCreditService userCreditService)
{
    private readonly List<ICreditLimiter> _creditLimiters =
    [
        new ImportantClient(),
        new VeryImportantClient()
    ];

    public void SetCreditLimit(User user)
    {
        var creditLimiter = _creditLimiters.FirstOrDefault(p => p.ClientType == user.Client.Type);
        creditLimiter?.SetCreditLimit(user);

        if (creditLimiter == null) SetDefaultCreditLimit(user);

        ValidateCreditLimit(user);
    }

    private void SetDefaultCreditLimit(User user)
    {
        user.HasCreditLimit = true;
        user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
    }

    private static void ValidateCreditLimit(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < 500) throw new ArgumentException("Invalid credit limit");
    }
}

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