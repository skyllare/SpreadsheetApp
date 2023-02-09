using Homework3Cpts321;

namespace HW3Testing
{
    /// <summary>
    /// Testing class for all method testing
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// test if our read line override is returning null values
        /// </summary>
        [Test]
        public void TestReadLineOverride()
        {
            FibonacciTextReader reader = new FibonacciTextReader(10);
            Assert.That(reader.ReadLine(), Is.Not.SameAs(null));
        }
    }
}