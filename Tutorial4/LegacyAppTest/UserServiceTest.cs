using LegacyApp;

namespace LegacyAppTest;

public class UserServiceTest
{
    [Fact]
    public void AddUser_WhenLastNameIsEmpty_ShouldReturnFalse()
    {
        var userService = new UserService();

        var result = userService.AddUser(
            null,
            "Kowalski",
            "kowalski@wp.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_WhenEmailDoesNotContainAppropriateSymbols_ShouldReturnFalse()
    {
        var userService = new UserService();

        var result = userService.AddUser(
            "Ivan",
            "Kowalski",
            "kowalskiwppl",
            DateTime.Parse("2000-01-01"),
            1
        );

        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_WhenUserUnder21_ShouldReturnFalse()
    {
        var userService = new UserService();
        var result = userService.AddUser(
            "Ivan",
            "Kowalski",
            "kowalski@wp.pl",
            DateTime.Parse("2020-01-01"),
            1
        );
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_WhenVeryImportantClient_ShouldReturnTrue()
    {
        var userService = new UserService();
        var result = userService.AddUser(
            "Jan",
            "Malewski",
            "malewski@gmail.com",
            DateTime.Parse("2000-01-01"),
            2
        );
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_WhenImportantClient_ShouldReturnTrue()
    {
        var userService = new UserService();
        var result = userService.AddUser(
            "Ivan",
            "Smith",
            "smith@gmail.com",
            DateTime.Parse("2000-01-01"),
            3
        );
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_WhenNormalClient_ShouldReturnTrue()
    {
        var userService = new UserService();
        var result = userService.AddUser(
            "Jan",
            "Kwiatkowski",
            "kwiatkowski@gmail.com",
            DateTime.Parse("2000-01-01"),
            5
        );
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_WhenUserDoesNotExist_ShouldThrowException()
    {
        var userService = new UserService();
        Assert.Throws<ArgumentException>(() =>
        {
            _ = userService.AddUser("Unknown", "Unknown", "unknown@wp.pl", new DateTime(1980, 1, 1), 100);
        });
    }
}