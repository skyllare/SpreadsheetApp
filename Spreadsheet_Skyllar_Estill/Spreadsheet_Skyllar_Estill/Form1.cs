// <copyright file="Form1.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Windows.Forms.VisualStyles;
using SpreadsheetEngine;
using static SpreadsheetEngine.Spreadsheet;

namespace Spreadsheet_Skyllar_Estill
{
    /// <summary>
    /// class for the form the spreadsheet is built in.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// creates a spreadsheet object.
        /// </summary>
        private Spreadsheet? spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Constructor for Form1.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
           // this.spreadsheet.CellPropertyChanged += this.dataGridView1_CellEndEdit;
            this.InitializeDataGrid();
        }

        /// <summary>
        /// runs when the form runs.
        /// </summary>
        /// <param name="sender">reference to object that raise event.</param>
        /// <param name="e">contains event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// adds rows 1-50 and columns A-Z to the spreadsheet.
        /// </summary>
        private void InitializeDataGrid()
        {
            for (char i = 'A'; i <= 'Z'; i++)
            {
                this.dataGridView1.Columns.Add(new DataGridViewColumn() { HeaderText = i.ToString(), CellTemplate = new DataGridViewTextBoxCell() });
            }

            for (int i = 1; i <= 50; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView1);
                row.HeaderCell.Value = i.ToString();
                this.dataGridView1.Rows.Add(row);
            }
        }

        /// <summary>
        /// Runs the demo when the button is pressed.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(rnd.Next(0, 49), rnd.Next(0, 26));
                if (curCell != null)
                {
                    curCell.CellText = "Hello World!";
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 1);
                if (curCell != null)
                {
                    curCell.CellText = "This is cell B" + (i + 1);
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 0);
                if (curCell != null)
                {
                    curCell.CellText = "=B" + i;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string msg = String.Format("Editing Cell at ({0}, {1})",e.ColumnIndex + 1, e.RowIndex + 1);
            this.Text = msg;
            /*int row = e.RowIndex; int col = e.ColumnIndex;
            Cell selectedCell = this.spreadsheet.GetCell(row, col + 1);
            this.dataGridView1.Rows[row].Cells[col].Value = selectedCell.CellText;*/
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string msg = String.Format("Finished Editing Cell at ({0}, {1})", e.ColumnIndex + 1, e.RowIndex + 1);
            this.Text = msg;
            string text;
            int row = e.RowIndex, col = e.ColumnIndex;
            Cell editedCell = this.spreadsheet.GetCell(row, col);

            if (this.dataGridView1.Rows[row].Cells[col].Value != null)
            {
                text = this.dataGridView1.Rows[row].Cells[col].Value.ToString();
            }
            else
            {
                text = string.Empty;
            }

            editedCell.CellText = text;
            this.dataGridView1.Rows[row].Cells[col].Value = editedCell.CellValue;

        }

        /// <summary>
        /// Event handler when the cell property changes.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell curCell = sender as Cell;

            if (curCell != null)
            {
                int row = curCell.RowIndex;
                int col = curCell.ColumnIndex;

                if (e.PropertyName == "CellText")
                {
                    this.dataGridView1.Rows[row].Cells[col].Value = curCell.CellValue;
                }
            }

        }

    }
}