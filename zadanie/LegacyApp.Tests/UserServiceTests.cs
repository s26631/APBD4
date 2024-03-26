using LegacyApp.Tests;


namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnFalseWhenNameIsEmpty()
    {
        //Arrange
        var userService = new UserService();
        
        //Act
        var result = userService.AddUser(
            null, 
            "Kowalski",
            "kowalski@gmail.com",
            DateTime.Parse("2000-01-01"),
            1);
        
        Action action = () => userService.AddUser(null, 
                "Kowalski",
                "kowalski@gmail.com",
                DateTime.Parse("2000-01-01"),
                1);


        //Assert
        Assert.Equal(false, result);
        Assert.False(result);
        Assert.Throws<ArgumentException>(action);
    }
}