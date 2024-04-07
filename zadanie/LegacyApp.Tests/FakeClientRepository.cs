using System;
using LegacyApp;

// Prosta implementacja IClientRepository dla celów testowych
public class FakeClientRepository : IClientRepository
{
    public Client GetById(int clientId)
    {
        // Zwracaj tutaj fałszywego klienta z odpowiednimi danymi dla twoich testów
        return new Client { ClientId = clientId, Name = "Test", Email = "test@example.com", Address = "Test Address", Type = "NormalClient" };
    }
}

// Prosta implementacja IUserCreditService dla celów testowych
public class FakeUserCreditService : IUserCreditService
{
    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        // Zwracaj tutaj fałszywą wartość limitu kredytowego dla twoich testów
        return 1000;
    }

    public void Dispose()
    {
    }
}