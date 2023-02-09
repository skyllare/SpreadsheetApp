namespace HW3Testing
{
    using Homework3Cpts321;

    /// <summary>
    /// Testing class for all method testing
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// test if our read line override is returning null values.
        /// </summary>
        [Test]
        public void TestReadLineOverride()
        {
            FibonacciTextReader reader = new FibonacciTextReader(10);
            Assert.That(reader.ReadLine(), Is.Not.SameAs(null));
        }

        /// <summary>
        /// test if FibonacciTextReader allows a negative max.
        /// </summary>
        [Test]
        public void TestMaxNegative()
        {
            FibonacciTextReader reader = new FibonacciTextReader(-1);
            Assert.That(reader.ReadLine(), Is.EqualTo(null));
        }

        /// <summary>
        /// test if ReadToEnd returns a null string when it should not.
        /// </summary>
        [Test]
        public void TestReadToEndReturn()
        {
            FibonacciTextReader reader = new FibonacciTextReader(10);
            string? line = reader.ReadToEnd();
            Assert.That(line, Is.Not.SameAs(null));
        }

        /// <summary>
        /// test the first 2 amounts of ReadToEnd as they are special cases.
        /// </summary>
        [Test]
        public void TestReadToEndFirstAmount()
        {
            FibonacciTextReader reader = new FibonacciTextReader(2);
            string? line = reader.ReadToEnd();
            Assert.That(line, Is.EqualTo("1. 0\r\n2. 1\r\n"));
        }
    }
}