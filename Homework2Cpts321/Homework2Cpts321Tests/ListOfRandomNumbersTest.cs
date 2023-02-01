
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
        /// Test that the max value in the list is less than or equal to 
        /// 20000
        /// </summary>
        [Test]
        public void TestListMaxValue()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            Assert.That(testList.Max(), Is.LessThanOrEqualTo(20000));
        }
        /// <summary>
        /// Test that the minimum value in the list is greater than or equal to
        /// 0
        /// </summary>
        [Test]
        public void TestListMinValue()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            Assert.That(testList.Max(), Is.GreaterThanOrEqualTo(0));
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

        /// <summary>
        /// Tests that the number of distinct numbers found by the 
        /// ConstantStorageImplementation() method matches the list.distinct.count
        /// number
        /// </summary>
        [Test]
        public void TestConstantStorageImplementation()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            int testUniqueValues = testList.Distinct().Count();
            Assert.That(testListObject.ConstantStorageImplementation(testList), Is.EqualTo(testUniqueValues));
        }

        /// <summary>
        /// Tests if the ConstantStorageImplementation() finds any values
        /// </summary>
        [Test]
        public void TestConstantStorageImplementationMinReturn()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            testListObject.ConstantStorageImplementation(testList);
            Assert.That(testListObject.ConstantStorageImplementation(testList), Is.GreaterThan(0));
        }


        /// <summary>
        /// Tests if the ConstantStorageImplementation() doesn't go over
        /// the size of the original list (10000)
        /// </summary>
        [Test]
        public void TestConstantStorageImplementationMaxReturn()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList = new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            testListObject.ConstantStorageImplementation(testList);
            Assert.That(testListObject.ConstantStorageImplementation(testList), Is.LessThanOrEqualTo(10000));
        }


    }
}