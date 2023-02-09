using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework3Cpts321
{
    /// <summary>
    /// Class for generation of Fibonacci numbers. Inherits System.IO.TextReader.
    /// </summary>
    public class FibonacciTextReader : System.IO.TextReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// Constructor for the FibonacciTextReader class.
        /// </summary>
        /// <param name="max">max number of lines available.</param>
        public FibonacciTextReader(int max)
        {
            ReadLine();
        }

      

        /// <summary>
        /// delivers the next number (as a string) in the Fibonaci sequence.
        /// </summary>
        /// <returns> ReadLine.</returns>
        public override string ReadLine()
        {
            return null;
        }

       
    }
}