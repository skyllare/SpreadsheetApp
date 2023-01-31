
using Homework2Cpts321;

namespace Homework2Cpts321Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGenerateRandomNumberList()
        {
            ListOfRandomNumbers testListObject = new ListOfRandomNumbers();
            List<int> testList= new List<int>();
            testListObject.GenerateRandomNumberList(ref testList);
            Assert.That(testList.Count(), Is.EqualTo(10000));
            //Assert.That();
            
        }

      
    }
}