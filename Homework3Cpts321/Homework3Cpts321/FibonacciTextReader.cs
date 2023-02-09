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
            this.MaxLines = max;
        }

        /// <summary>
        /// Gets or sets Max Lines.
        /// </summary>
        public int MaxLines { get; set; }

        /// <summary>
        /// delivers the next number (as a string) in the Fibonaci sequence.
        /// </summary>
        /// <returns> ReadLine.</returns>
        public override string ReadLine()
        {
            return base.ReadLine();
        }

        /// <summary>
        /// Assert.IsTrue(File.Exists(fileName));
        /// </summary>
        /// <param name="previousNumber1">First previous number. </param>
        /// <param name="previousNumber2">Second previous number. </param>
        /// <returns>calculation. </returns>
        public int CalculateNumbers (int previousNumber1, int previousNumber2)
        {
            return 0;
        }
    }
}