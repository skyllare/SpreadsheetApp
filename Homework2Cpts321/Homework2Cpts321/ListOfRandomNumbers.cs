using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Homework2Cpts321
{
    /// <summary>
    /// ListOfRandomNumbers class, which houses the methods 
    /// used in the generation and unique number counts
    /// </summary>
    public class ListOfRandomNumbers
    {
        /// <summary>
        /// GenerateRandomNumberList used to make the main random number lsit
        /// of 10000 numbers
        /// </summary>
        /// <param name="randomList"></param>
        public void GenerateRandomNumberList(ref List<int> randomList)
        {
            Random random = new Random();
            for (int i = 0; i < 10000; ++i) 
            {
                randomList.Add(random.Next(20000));

            }
        }

        /// <summary>
        /// HashSetImplementation, used to find the amount of unique numbers
        /// in the list with a hash set
        /// </summary>
        /// <param name="randomList"></param>
        /// <returns></returns>
        public int HashSetImplementation(List<int> randomList)
        {
            HashSet<int> uniqueRandomNumbers = new HashSet<int>();
            for (int i = 0; i < randomList.Count(); i++) 
            {
                uniqueRandomNumbers.Add(randomList[i]);
            }
            return uniqueRandomNumbers.Count();
        }

    }
}
