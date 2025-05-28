using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvaluationSampleCode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSampleCode.Tests
{
  [TestClass]
  public class MathOperationsTests
  {
    private MathOperations _mathOps;

    [TestInitialize]
    public void Setup()
    {
      _mathOps = new MathOperations();
    }

    [TestMethod]
    [DataRow(2, 3, 5)]
    [DataRow(-1, 1, 0)]
    [DataRow(0, 0, 0)]
    [DataRow(-5, -3, -8)]
    [DataRow(100, 200, 300)]
    [DataRow(int.MaxValue, 0, int.MaxValue)]
    [DataRow(int.MinValue, 0, int.MinValue)]
    public void Add_ValidNumbers_ReturnsCorrectSum(int numberOne, int numberTwo, int expected)
    {
      var result = _mathOps.Add(numberOne, numberTwo);

      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(10, -5, 5)]
    [DataRow(-10, 5, -5)]
    [DataRow(-10, -5, -15)]
    public void Add_NegativeNumbers_ReturnsCorrectSum(int numberOne, int numberTwo, int expected)
    {
      var result = _mathOps.Add(numberOne, numberTwo);

      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(1000000, 2000000, 3000000)]
    [DataRow(-1000000, -2000000, -3000000)]
    [DataRow(1000000, -1000000, 0)]
    public void Add_LargeNumbers_ReturnsCorrectSum(int numberOne, int numberTwo, int expected)
    {
      var result = _mathOps.Add(numberOne, numberTwo);

      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Divide_PositiveNumbers_ReturnsCorrectQuotient()
    {
      var result = _mathOps.Divide(10, 2);

      Assert.AreEqual(5.0f, result, 0.001f);
    }

    [TestMethod]
    public void Divide_NegativeByPositive_ReturnsNegativeQuotient()
    {
      var result = _mathOps.Divide(-10, 2);

      Assert.AreEqual(-5.0f, result, 0.001f);
    }

    [TestMethod]
    public void Divide_PositiveByNegative_ReturnsNegativeQuotient()
    {
      var result = _mathOps.Divide(10, -2);

      Assert.AreEqual(-5.0f, result, 0.001f);
    }

    [TestMethod]
    public void Divide_NegativeByNegative_ReturnsPositiveQuotient()
    {
      var result = _mathOps.Divide(-10, -2);

      Assert.AreEqual(5.0f, result, 0.001f);
    }

    [TestMethod]
    public void Divide_ZeroByNonZero_ReturnsZero()
    {
      var result = _mathOps.Divide(0, 5);

      Assert.AreEqual(0.0f, result, 0.001f);
    }

    [TestMethod]
    public void Divide_NumbersWithRemainder_ReturnsFloatResult()
    {
      var result = _mathOps.Divide(7, 3);

      Assert.AreEqual(2.333333f, result, 0.000001f);
    }

    [TestMethod]
    public void Divide_OneByOne_ReturnsOne()
    {
      var result = _mathOps.Divide(1, 1);

      Assert.AreEqual(1.0f, result, 0.001f);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Divide_ByZero_ThrowsArgumentException()
    {
      _mathOps.Divide(10, 0);
    }

    [TestMethod]
    public void Divide_ByZero_ThrowsExceptionWithCorrectMessage()
    {
      try
      {
        _mathOps.Divide(10, 0);
        Assert.Fail("Expected ArgumentException was not thrown.");
      }
      catch (ArgumentException ex)
      {
        Assert.AreEqual("Second parameter can't be equal to zero", ex.Message);
      }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Divide_NegativeByZero_ThrowsArgumentException()
    {
      _mathOps.Divide(-10, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Divide_ZeroByZero_ThrowsArgumentException()
    {
      _mathOps.Divide(0, 0);
    }

    [TestMethod]
    public void GetOddNumbers_PositiveLimit_ReturnsCorrectOddNumbers()
    {
      var result = _mathOps.GetOddNumbers(10).ToList();

      var expected = new List<int> { 1, 3, 5, 7, 9 };
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetOddNumbers_Zero_ReturnsEmptyList()
    {
      var result = _mathOps.GetOddNumbers(0).ToList();

      Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void GetOddNumbers_One_ReturnsListWithOne()
    {
      var result = _mathOps.GetOddNumbers(1).ToList();

      var expected = new List<int> { 1 };
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetOddNumbers_Two_ReturnsListWithOne()
    {
      var result = _mathOps.GetOddNumbers(2).ToList();

      var expected = new List<int> { 1 };
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetOddNumbers_SmallEvenLimit_ReturnsCorrectOddNumbers()
    {
      var result = _mathOps.GetOddNumbers(6).ToList();

      var expected = new List<int> { 1, 3, 5 };
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetOddNumbers_SmallOddLimit_ReturnsCorrectOddNumbers()
    {
      var result = _mathOps.GetOddNumbers(7).ToList();

      var expected = new List<int> { 1, 3, 5, 7 };
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetOddNumbers_LargeLimit_ReturnsCorrectCount()
    {
      var result = _mathOps.GetOddNumbers(100).ToList();

      Assert.AreEqual(50, result.Count);
      Assert.AreEqual(1, result.First());
      Assert.AreEqual(99, result.Last());
    }

    [TestMethod]
    public void GetOddNumbers_LargeLimit_AllNumbersAreOdd()
    {
      var result = _mathOps.GetOddNumbers(20).ToList();

      foreach (var number in result)
      {
        Assert.AreEqual(1, number % 2, $"Number {number} is not odd");
      }
    }

    [TestMethod]
    public void GetOddNumbers_LargeLimit_NumbersAreInAscendingOrder()
    {
      var result = _mathOps.GetOddNumbers(20).ToList();

      for (int i = 1; i < result.Count; i++)
      {
        Assert.IsTrue(result[i] > result[i - 1], "Numbers are not in ascending order");
      }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetOddNumbers_NegativeLimit_ThrowsArgumentException()
    {
      _mathOps.GetOddNumbers(-1);
    }

    [TestMethod]
    public void GetOddNumbers_NegativeLimit_ThrowsExceptionWithCorrectMessage()
    {
      try
      {
        _mathOps.GetOddNumbers(-5);
        Assert.Fail("Expected ArgumentException was not thrown.");
      }
      catch (ArgumentException ex)
      {
        Assert.AreEqual("Limit argument can't be negative", ex.Message);
      }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetOddNumbers_LargeNegativeLimit_ThrowsArgumentException()
    {
      _mathOps.GetOddNumbers(-100);
    }

    [TestMethod]
    public void GetOddNumbers_ReturnsIEnumerable_CanBeEnumeratedMultipleTimes()
    {
      var result = _mathOps.GetOddNumbers(5);

      var firstEnumeration = result.ToList();
      var secondEnumeration = result.ToList();

      CollectionAssert.AreEqual(firstEnumeration, secondEnumeration);
    }

    [TestMethod]
    public void MathOperations_ComplexScenario_AllMethodsWorkTogether()
    {
      var sum = _mathOps.Add(10, 5);
      Assert.AreEqual(15, sum);

      var quotient = _mathOps.Divide(sum, 3);
      Assert.AreEqual(5.0f, quotient, 0.001f);

      var oddNumbers = _mathOps.GetOddNumbers((int)quotient).ToList();
      var expected = new List<int> { 1, 3, 5 };
      CollectionAssert.AreEqual(expected, oddNumbers);
    }
  }
}