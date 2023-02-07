using MVCSocialMedia.Models;
using Xunit;
using System.Collections;

namespace MVCSocialMedia.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _sut;

        public CalculatorTests()
        {
            _sut = new Calculator();
        }

        [Theory]
        [ClassData(typeof(AdditionTestData))]
        public void AddTwoNumbersShouldEqualTheirEqualTheory(decimal expected, params decimal[] valuesToAdd)
        {
            foreach (var value in valuesToAdd)
            {
                _sut.Add(value);
            }
            Assert.Equal(expected, _sut.Value);
        }

        [Theory]
        [ClassData(typeof(MultiplicationTestData))]
        public void MultiplyManyNumbersTheory(decimal expected, params decimal[] valuesToMultiply)
        {
            foreach (var value in valuesToMultiply)
            {
                _sut.Multiply(value);
            }
            Assert.Equal(expected, _sut.Value);
        }

        [Theory]
        [ClassData(typeof(DivisionTestData))]
        public void DivideManyNumbersTheory(decimal expected, params decimal[] valuesToMultiply)
        {

            var result = _sut.Divide(valuesToMultiply[0], valuesToMultiply[1]);

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(SubtractionTestData))]
        public void SubtractManyNumbersTheory(decimal expected, params decimal[] valuesToMultiply)
        {
            foreach (var value in valuesToMultiply)
            {
                _sut.Subtract(value);
            }
            Assert.Equal(expected, _sut.Value);
        }
    }

    public class AdditionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4, new decimal[] { 5, -1, 0 } };
            yield return new object[] { 8, new decimal[] { 0, 5, 3 } };
            yield return new object[] { -2, new decimal[] { -4, 2, 0 } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SubtractionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, new decimal[] { 8, 2, 5 } };
            yield return new object[] { -7, new decimal[] { 0, 1, 6 } };
            yield return new object[] { 32, new decimal[] { 50, 18, 0 } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class DivisionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 30, new decimal[] { 60, 2 } };
            yield return new object[] { 0, new decimal[] { 0, 1 } };
            yield return new object[] { 1, new decimal[] { 50, 50 } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MultiplicationTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 25, new decimal[] { 5, 5 } };
            yield return new object[] { 0, new decimal[] { 0, 5 } };
            yield return new object[] { 90, new decimal[] { 15, 1, 6 } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
