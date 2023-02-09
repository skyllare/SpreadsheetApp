using Homework3Cpts321;

namespace HW3Testing
{
    /// <summary>
    /// Testing class for all method testing
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Test to see if CalculateNumbers returns the correct calculation.
        /// </summary>
        [Test]
        public void TestCalculateNumbers()
        {
            FibonacciTextReader reader = new FibonacciTextReader(10);
            Assert.That(reader.CalculateNumbers(2,5), Is.EqualTo(10));
        }

        /// <summary>
        /// Test to see if CalculateNumbers returns positive numbers by using the first
        /// two numbers of the sequence to test.
        /// </summary>
        [Test]
        public void TestCalculateNumbersLow()
        {
            FibonacciTextReader reader = new FibonacciTextReader(10);
            Assert.That(reader.CalculateNumbers(0, 1), Is.GreaterThanOrEqualTo(0));
        }
    }
}