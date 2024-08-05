using CalculatorLibrary;
using System;
using Xunit;

namespace CalculatorLibrary.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsCorrectSum()
        {
        
            var calculator = new Calculator();
            double num1 = 5;
            double num2 = 3;
            string operation = "a";

            double result = calculator.DoOperation(num1, num2, operation);

            Assert.Equal(8, result);
        }

        [Fact]
        public void Subtract_ReturnsCorrectDifference()
        {
            var calculator = new Calculator();
            double num1 = 5;
            double num2 = 3;
            string operation = "s";

            double result = calculator.DoOperation(num1, num2, operation);

            Assert.Equal(2, result);
        }

        [Fact]
        public void Multiply_ReturnsCorrectProduct()
        {
            var calculator = new Calculator();
            double num1 = 5;
            double num2 = 3;
            string operation = "m";

            double result = calculator.DoOperation(num1, num2, operation);

            Assert.Equal(15, result);
        }

        [Fact]
        public void Divide_ReturnsCorrectQuotient()
        {
            var calculator = new Calculator();
            double num1 = 6;
            double num2 = 3;
            string operation = "d";

            double result = calculator.DoOperation(num1, num2, operation);

            Assert.Equal(2, result);
        }

        [Fact]
        public void Divide_ByZero_ReturnsNaN()
        {
            var calculator = new Calculator();
            double num1 = 6;
            double num2 = 0;
            string operation = "d";

            double result = calculator.DoOperation(num1, num2, operation);

            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void InvalidOperation_ReturnsNaN()
        {
            var calculator = new Calculator();
            double num1 = 6;
            double num2 = 3;
            string operation = "x"; // invalid operation

            double result = calculator.DoOperation(num1, num2, operation);

            // Assert
            Assert.Equal(double.NaN, result);
        }
    }
}
