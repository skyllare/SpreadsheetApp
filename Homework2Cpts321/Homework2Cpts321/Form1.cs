using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework2Cpts321
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string outputString1 = "", outputString2 = "", outputString3 = "";
            RunDistinctIntegers(ref outputString1, ref outputString2, ref outputString3);
            textBox1.Text = outputString1 + Environment.NewLine + outputString2 + Environment.NewLine
                +outputString3;
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          
        }

        private static void RunDistinctIntegers(ref string outputString1, ref string outputString2, ref string outputString3)
        {
            ListOfRandomNumbers randomListObject = new ListOfRandomNumbers();
            List<int> randomList = new List<int>();
            randomListObject.GenerateRandomNumberList(ref randomList);
            int hashSetCount = randomListObject.HashSetImplementation(randomList);
            int constStorageCount = randomListObject.ConstantStorageImplementation(randomList);
            int sortListAndCountCount = randomListObject.SortListAndCount(randomList);
            outputString1 = "1. HashSet Method: " + hashSetCount + " unique numbers. " + "\n time complexityinfo";
            outputString2 = "2. O(1) storage method " + constStorageCount + " unique numbers. ";
            outputString3 = "3. Sorted method: " + sortListAndCountCount + " unique numbers.";
            
        }

       
    }
}
