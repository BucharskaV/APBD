using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_ReturnsFalse_WhenLastNameIsNull()
        {
            var userService = new UserService();
            
        }
        
    }
}