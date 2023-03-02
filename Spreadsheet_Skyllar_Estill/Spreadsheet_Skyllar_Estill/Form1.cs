using SpreadsheetEngine;
namespace Spreadsheet_Skyllar_Estill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
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
    }
}