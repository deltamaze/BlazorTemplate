using System;
using Xunit;
using HelloWorldLibrary;

namespace HelloWorldLibrary.Test
{
    public class CalculatorTest
    {
        [Fact]//Lets VS know we can run in Test Window
        public void Add_SimpleValuesShouldCalculate()
        {
            // Arrage
            double expected = 5;

            // Act
            double actual = Calculator.Add(3, 2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
