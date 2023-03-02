using SpreadsheetEngine;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;
using static SpreadsheetEngine.Spreadsheet;

namespace Spreadsheet_Skyllar_Estill
{
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;
        public Form1()
        {
            InitializeComponent();
            spreadsheet = new Spreadsheet(50, 26);
            spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
            InitializeDataGrid();
        }

        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell curCell = sender as Cell;
            int row = curCell.RowIndex;
            int col = curCell.ColumnIndex;

            if (e.PropertyName == "CellText")
            {
                this.dataGridView1.Rows[row].Cells[col].Value = curCell.CellValue;
            }
        }


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

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i =0; i<50;i++)
            {
                Cell curCell = this.spreadsheet.GetCell(rnd.Next(0, 49), rnd.Next(0, 26));
                curCell.CellText = "Hello World!";
            }

            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 1);
                curCell.CellText = "This is cell B" + (i+1);
            }

            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 0);
                curCell.CellText = "=B" + (i);
            }
        }
    }
}