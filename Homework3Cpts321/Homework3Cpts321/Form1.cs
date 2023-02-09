using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 *  System.IO.TextReader reader = new StreamReader("C:\\Users\\skyll\\OneDrive\\Desktop\\cpts321-hws\\Homework3Cpts321\\Homework3Cpts321\\LoadTextFile.txt");
            LoadText(reader);
*/
namespace Homework3Cpts321
{
    /// <summary>
    /// Dosumentation for the form.
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Includes what is run once the Form loads.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Allows the user to load the first 50 fibonacci numbers when then button is clicked
        /// in the menu dropdown.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void loadFibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Allows the user to load text from a chose file.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.TextReader reader = new StreamReader("C:\\Users\\skyll\\OneDrive\\Desktop\\cpts321-hws\\Homework3Cpts321\\Homework3Cpts321\\LoadTextFile.txt");
            LoadText(reader);
        }

        /// <summary>
        /// Allows the user to load the first 100 fibonacci numbers when then button is clicked
        /// in the menu dropdown.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Allows the user to save to file when the button is clicked in the menu dropdown.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Reads text from the TextReader object and puts it into the text box
        /// in Form1.
        /// </summary>
        /// <param name="sr"> TextReader object.</param>
        private void LoadText(TextReader sr)
        {
            try
            {
                string line = null;
                line = sr.ReadToEnd();
                this.textBox1.Text = line;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
