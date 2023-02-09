// <copyright file="Form1.cs" company="Skyllar Estill 11750544">
// Copyright (c) Skyllar Estill 11750544. All rights reserved.
// </copyright>

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
using System.Xml;

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
        private void LoadFibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fiftyNumber = new FibonacciTextReader(50);
            this.LoadText(fiftyNumber);
        }

        /// <summary>
        /// Allows the user to load text from a chose file.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void LoadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "*.txt|";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(dialog.FileName);
                }

                System.IO.TextReader readFile = new StreamReader(dialog.FileName);
                this.LoadText(readFile);
            }
        }

        /// <summary>
        /// Allows the user to load the first 100 fibonacci numbers when then button is clicked
        /// in the menu dropdown.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void LoadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader hundredNumber = new FibonacciTextReader(100);
            this.LoadText(hundredNumber);
        }

        /// <summary>
        /// Allows the user to save to file when the button is clicked in the menu dropdown.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event.</param>
        /// <param name="e">Contains event data.</param>
        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "*.txt|";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Saving to " + dialog.FileName);
                }

                File.WriteAllText(dialog.FileName, this.textBox1.Text);
            }
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
