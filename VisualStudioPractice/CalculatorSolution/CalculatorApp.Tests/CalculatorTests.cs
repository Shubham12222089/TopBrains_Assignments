using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework;
using CalculatorApp;

namespace CalculatorApp.Tests
{
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_ValidNumbers_ReturnsSum()
        {
            int result = _calculator.Add(10, 5);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void Subtract_ValidNumbers_ReturnsDifference()
        {
            int result = _calculator.Subtract(10, 5);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Multiply_ValidNumbers_ReturnsProduct()
        {
            int result = _calculator.Multiply(10, 5);
            Assert.AreEqual(50, result);
        }

        [Test]
        public void Divide_ValidNumbers_ReturnsQuotient()
        {
            int result = _calculator.Divide(10, 5);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() =>
                _calculator.Divide(10, 0));
        }
    }
}