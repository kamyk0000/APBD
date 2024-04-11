using LegacyApp;
using System;
using Moq;
using Xunit;

namespace LegacyAppTests;

public class UserTests
{
    [Fact]
    public void User_Should_Create_With_Valid_Arguments()
    {
        // Arrange
        var client = new Client();
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var user = new User("John", "Doe", "john.doe@example.com", dateOfBirth, client);

        // Assert
        Assert.NotNull(user);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal("john.doe@example.com", user.EmailAddress);
        Assert.Equal(dateOfBirth, user.DateOfBirth);
        Assert.Same(client, user.Client);
    }
    
    [Fact]
    public void User_Should_Throw_ArgumentException_When_Missing_FirstName()
    {
        // Arange
        var clientMock = new Mock<Client>();
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(null, "Doe", "john.doe@example.com", dateOfBirth, clientMock.Object);
        });
    }
    
    [Fact]
    public void User_Should_Throw_ArgumentException_When_Missing_LastName()
    {
        // Arange
        var clientMock = new Mock<Client>();
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User("John", "", "john.doe@example.com", dateOfBirth, clientMock.Object);
        });
    }
    
    [Fact]
    public void User_Should_Throw_ArgumentException_When_Underage()
    {
        // Arange
        var clientMock = new Mock<Client>();
        var dateOfBirth = DateTime.Now.AddYears(-20);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User("John", "Doe", "john.doe@example.com", dateOfBirth, clientMock.Object);
        });
    }
    
    [Fact]
    public void User_Should_Throw_ArgumentException_When_Email_Missing_At_Or_Dot()
    {
        // Arange
        var clientMock = new Mock<Client>();
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => // Missing dot sign
        {
            var user = new User("John", "Doe", "john.doe@examplecom", dateOfBirth, clientMock.Object);
        });
        Assert.Throws<ArgumentException>(() => // Missing at sign
        {
            var user = new User("John", "Doe", "john.doe.example.com", dateOfBirth, clientMock.Object);
        });
    }
    
    [Fact]
    public void ClientRepository_GetById_Should_Throw_ArgumentException_When_Invalid_Id_Provided()
    {
        // Arrange
        var repository = new ClientRepository();
        int clientId = -1;
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => // Missing at sign
        {
            var client = repository.GetById(clientId);
        });
    }
    
    [Fact]
    public void UserCreditService_GetCreditLimit_Should_Throw_ArgumentException_When_Invalid_Client_Provided()
    {
        // Arrange
        var service = new UserCreditService();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
        {
            service.GetCreditLimit("-1", new DateTime(2000, 1, 1));
        });
    }
    
    [Fact]
    public void CreditLimitService_SetCreditLimit_Should_Throw_ArgumentException_When_Invalid_CreditLimit()
    {
        // Arrange
        var mockUserCreditService = new Mock<IUserCreditService>();
        mockUserCreditService.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(100);
        var user = new User("John", "Doe", "john.doe@example.com", new DateTime(1980, 1, 1),
            new Client { ClientId = 1, Name = "Doe", Address = "Test Address", Email = "john.doe@example.com", Type = "Other" });
        var service = new CreditLimitService(mockUserCreditService.Object);
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
        {
            service.SetCreditLimit(user);
        });
    }
    
}
    
