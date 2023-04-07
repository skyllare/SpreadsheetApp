// <copyright file="Form1.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Windows.Forms;
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

        /// <summary>
        /// not implemented.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// what occurs when you click on a cell to edit.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string msg = string.Format("Editing Cell at ({0}, {1})", e.ColumnIndex + 1, e.RowIndex + 1);
            this.Text = msg;
            /*int row = e.RowIndex; int col = e.ColumnIndex;
            Cell selectedCell = this.spreadsheet.GetCell(row, col + 1);
            this.dataGridView1.Rows[row].Cells[col].Value = selectedCell.CellText;*/
        }

        /// <summary>
        /// what happens when you enter data into the cell.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string msg = string.Format("Finished Editing Cell at ({0}, {1})", e.ColumnIndex + 1, e.RowIndex + 1);
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
            for (int i = 0; i < this.spreadsheet.ChangedCells.Count; i++)
            {
                col = int.Parse(this.spreadsheet.ChangedCells[i].Substring(0, 1));
                row = int.Parse(this.spreadsheet.ChangedCells[i].Substring(1));
                editedCell = this.spreadsheet.GetCell(row, col);
                this.dataGridView1.Rows[row].Cells[col].Value = editedCell.CellValue;
            }
            this.spreadsheet.ChangedCells.Clear();
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
                else if (e.PropertyName == "BGColor")
                {
                    uint colorValue = curCell.BGCOlor;
                    Color color = Color.FromArgb((int)colorValue);
                    this.dataGridView1.Rows[row].Cells[col].Style.BackColor = color;
                }
            }
        }

        private void changeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();

            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                Color color = myDialog.Color;

                foreach (DataGridViewCell cell in this.dataGridView1.SelectedCells)
                {
                    /*
                     * Cell editedCell = this.spreadsheet.GetCell(row, col);
                     **/
                    Cell editedCell = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex);
                    editedCell.BGCOlor = (uint)(color.ToArgb());
                }
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}