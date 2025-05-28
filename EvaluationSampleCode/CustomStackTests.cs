using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvaluationSampleCode;
using static EvaluationSampleCode.CustomStack;

namespace EvaluationSampleCode.Tests
{
  [TestClass]
  public class CustomStackTests
  {
    private CustomStack _stack;

    [TestInitialize]
    public void Setup()
    {
      _stack = new CustomStack();
    }

    [TestMethod]
    public void Count_EmptyStack_ReturnsZero()
    {
      var result = _stack.Count();

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Count_AfterOnePush_ReturnsOne()
    {
      _stack.Push(5);

      var result = _stack.Count();

      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void Count_AfterMultiplePushes_ReturnsCorrectCount()
    {
      _stack.Push(1);
      _stack.Push(2);
      _stack.Push(3);

      var result = _stack.Count();

      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void Push_SingleValue_IncreasesCount()
    {
      _stack.Push(10);

      Assert.AreEqual(1, _stack.Count());
    }

    [TestMethod]
    public void Push_MultipleValues_IncreasesCountCorrectly()
    {
      _stack.Push(1);
      _stack.Push(2);
      _stack.Push(3);

      Assert.AreEqual(3, _stack.Count());
    }

    [TestMethod]
    public void Push_NegativeValue_IncreasesCount()
    {
      _stack.Push(-5);

      Assert.AreEqual(1, _stack.Count());
    }

    [TestMethod]
    public void Push_Zero_IncreasesCount()
    {
      _stack.Push(0);

      Assert.AreEqual(1, _stack.Count());
    }

    [TestMethod]
    public void Pop_SingleElement_ReturnsElementAndDecreasesCount()
    {
      _stack.Push(42);

      var result = _stack.Pop();

      Assert.AreEqual(42, result);
      Assert.AreEqual(0, _stack.Count());
    }

    [TestMethod]
    public void Pop_MultipleElements_ReturnsLastInFirstOut()
    {
      _stack.Push(1);
      _stack.Push(2);
      _stack.Push(3);

      var first = _stack.Pop();
      var second = _stack.Pop();
      var third = _stack.Pop();

      Assert.AreEqual(3, first);
      Assert.AreEqual(2, second);
      Assert.AreEqual(1, third);
      Assert.AreEqual(0, _stack.Count());
    }

    [TestMethod]
    public void Pop_AfterPushAndPop_MaintainsCorrectOrder()
    {
      _stack.Push(10);
      _stack.Push(20);
      var firstPop = _stack.Pop();
      _stack.Push(30);

      var result = _stack.Pop();

      Assert.AreEqual(20, firstPop);
      Assert.AreEqual(30, result);
      Assert.AreEqual(1, _stack.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(StackCantBeEmptyException))]
    public void Pop_EmptyStack_ThrowsStackCantBeEmptyException()
    {
      _stack.Pop();
    }

    [TestMethod]
    public void Pop_EmptyStack_ThrowsExceptionWithCorrectMessage()
    {
      try
      {
        _stack.Pop();
        Assert.Fail("Expected StackCantBeEmptyException was not thrown.");
      }
      catch (StackCantBeEmptyException ex)
      {
        Assert.AreEqual("Can't call Pop on an empty stack.", ex.Message);
      }
    }

    [TestMethod]
    [ExpectedException(typeof(StackCantBeEmptyException))]
    public void Pop_AfterPoppingAllElements_ThrowsException()
    {
      _stack.Push(1);
      _stack.Pop();

      _stack.Pop();
    }

    [TestMethod]
    public void StackOperations_ComplexScenario_WorksCorrectly()
    {
      Assert.AreEqual(0, _stack.Count());

      _stack.Push(1);
      _stack.Push(2);
      Assert.AreEqual(2, _stack.Count());

      var popped1 = _stack.Pop();
      Assert.AreEqual(2, popped1);
      Assert.AreEqual(1, _stack.Count());

      _stack.Push(3);
      _stack.Push(4);
      Assert.AreEqual(3, _stack.Count());

      var popped2 = _stack.Pop();
      var popped3 = _stack.Pop();
      var popped4 = _stack.Pop();

      Assert.AreEqual(4, popped2);
      Assert.AreEqual(3, popped3);
      Assert.AreEqual(1, popped4);
      Assert.AreEqual(0, _stack.Count());
    }
  }
}