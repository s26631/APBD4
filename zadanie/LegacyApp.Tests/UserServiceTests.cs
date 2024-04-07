using Xunit;
using LegacyApp;
using System;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_ReturnFalseWhenNameIsEmpty()
        {
            // Używamy prostych implementacji zamiast mocków
            var fakeClientRepository = new FakeClientRepository();
            var fakeUserCreditService = new FakeUserCreditService();
            var userService = new UserService(fakeClientRepository, fakeUserCreditService);

            var result = userService.AddUser("", "Kowalski", "kowalski@gmail.com", new DateTime(2000, 1, 1), 1);

            Assert.False(result);
        }
    }
}