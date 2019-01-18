using System;
using Xunit;
using HelloWorldLibrary;

namespace HelloWorldLibrary.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData(4,3,7)]
        [InlineData(21, 5.25, 26.25)]
        public void Add_SimpleValuesShouldCalculate(double x, double y, double expected)
        {
            
            // Arrage
            //double expected = 5;

            // Act
            double actual = Calculator.Add(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8, 4, 2)]
        [InlineData(14, 2, 7)]
        public void Divide_SimpleValuesShouldCalculate(double x, double y, double expected)
        {

            // Arrage
            //double expected = 5;

            // Act
            double actual = Calculator.Divide(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Divide_DivideByZero()
        {

            // Arrage
            double expected = 0;

            // Act
            double actual = Calculator.Divide(5, 0);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
