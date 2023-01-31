
using Homework2Cpts321;

namespace Homework2Cpts321Tests
{
    /// <summary>
    /// The main Tests class.
    /// Contains all methods used for testing
    /// </summary>
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests that GenerateRandomNumberList()
        /// generates 10000 random numbers
        /// </summary>
        [Test]
        public void TestGenerateRandomNumberList()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList= new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            Assert.That(testList.Count(), Is.EqualTo(10000));              
        }

        /// <summary>
        /// Tests that HashSetImplementation() finds 
        /// the correct number of unique items
        /// </summary>
        [Test]
        public void TestHashSetImplementation()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            int testUniqueValues = testList.Distinct().Count();
            Assert.That(testListObject.HashSetImplementation(testList), Is.EqualTo(testUniqueValues));
        }



    }
}