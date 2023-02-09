using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Homework3Cpts321
{
    /// <summary>
    /// Class for generation of Fibonacci numbers. Inherits System.IO.TextReader.
    /// </summary>
    public class FibonacciTextReader : System.IO.TextReader
    {
        /// <summary>
        /// keeps track of max numbers;
        /// </summary>
        private readonly int max;

        /// <summary>
        /// keeps track of current value.
        /// </summary>
        private BigInteger current;

        /// <summary>
        /// keeps track of previous value.
        /// </summary>
        private BigInteger previous;

        /// <summary>
        /// keeps track of count of values.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// Constructor for the FibonacciTextReader class.
        /// </summary>
        /// <param name="n">max number of lines available.</param>
        public FibonacciTextReader(int n)
        {
            this.max = n;
            this.current = 1;
            this.previous = 0;
            this.count = 0;
        }

        /// <summary>
        /// delivers the next number (as a string) in the Fibonaci sequence.
        /// </summary>
        /// <returns> ReadLine.</returns>
        public override string ReadLine()
        {
            if (this.count == this.max)
            {
                return null;
            }

            if (this.count < 2)
            {
                this.count++;
                return (this.count - 1).ToString();
            }

            BigInteger temp = this.current;
            this.current += this.previous;
            this.previous = temp;
            this.count++;
            return this.current.ToString();
        }

        /// <summary>
        /// epeatedly calls ReadLine and concatenates all the lines together.
        /// </summary>
        /// <returns>appended string. </returns>
        public override string ReadToEnd()
        {
            StringBuilder concat = new StringBuilder();
            string temp = this.ReadLine(); ;
            int count = 1;
            while (temp != null)
            {
                concat.Append(count + ". " + temp + Environment.NewLine);
                temp = this.ReadLine();
                count++;
            }

            return concat.ToString();
        }
    }
}