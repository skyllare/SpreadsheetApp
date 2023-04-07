using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class TextChange : Command
    {
        private string currentText;

        private string previousText;

        private int row;

        private int col;

        public string CurrentText
        {
            get
            {
                return currentText;
            }
        }

        public string PreviousText
        {
            get
            {
                return previousText;
            }
        }

        public int GetRow()
        {
            return this.row;
        }

        public int GetCol()
        {
            return this.col;
        }


        public TextChange() { }

        public void Execute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            string tempPrevousText = cell.CellText;
            this.currentText = this.previousText;
            this.previousText = tempPrevousText;
            cell.CellText = this.currentText;

        }


        public void UndoText(string currentText, string previousText, int row, int col)
        {
            this.currentText = currentText;
            this.previousText = previousText;
            this.row = row;
            this.col = col;
        }


        public void Unexecute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            string tempPreviousText = cell.CellText;
            this.currentText = this.previousText;
            this.previousText = tempPreviousText;
            cell.CellText = this.currentText;
        }
    }
}
