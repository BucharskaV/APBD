using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UnitTest1
    {
        private Calculator _sut;

        public UnitTest1()
        {
            _sut = new Calculator();
        }
        
        [Fact]
        public void Test1()
        {
            //Arrange
            //Act
            //Assert
        }
        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(-2, -3)]
        public void Test2(int a, int b)
        {
            
        }
    }
}