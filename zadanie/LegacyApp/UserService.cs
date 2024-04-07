using System;

namespace LegacyApp
{
    public class UserService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserCreditService _userCreditService;

    public UserService() : this(new ClientRepository(), new UserCreditService())
    {
    }
    public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        _clientRepository = clientRepository;
        _userCreditService = userCreditService;
    }

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!IsValidName(firstName, lastName) || !IsValidEmail(email) || !IsAgeAtLeast(21, dateOfBirth))
        {
            return false;
        }

        var client = _clientRepository.GetById(clientId);
        var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

        if (ShouldAssignCreditLimit(client))
        {
            AssignCreditLimit(user, lastName, dateOfBirth);
            if (!IsCreditLimitSufficient(user))
            {
                return false;
            }
        }

        UserDataAccess.AddUser(user);
        return true;
    }

    private bool IsValidName(string firstName, string lastName)
    {
        return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private bool IsAgeAtLeast(int age, DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int userAge = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            userAge--;
        return userAge >= age;
    }

    private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
    {
        return new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName,
            HasCreditLimit = true
        };
    }

    private bool ShouldAssignCreditLimit(Client client)
    {
        return client.Type != "VeryImportantClient";
    }

    private void AssignCreditLimit(User user, string lastName, DateTime dateOfBirth)
    {
        int creditLimit = _userCreditService.GetCreditLimit(lastName, dateOfBirth);
        if (user.Client.Type == "ImportantClient")
        {
            user.CreditLimit = creditLimit * 2;
        }
        else
        {
            user.CreditLimit = creditLimit;
        }
    }

    private bool IsCreditLimitSufficient(User user)
    {
        return !user.HasCreditLimit || user.CreditLimit >= 500;
    }
}

}
