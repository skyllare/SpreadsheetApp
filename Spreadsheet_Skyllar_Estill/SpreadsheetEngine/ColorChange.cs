using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class ColorChange : Command
    {
        private uint currentColor;

        private uint previousColor;

        private int row;

        private int col;

        public uint CurrentColor
        {
            get
            {
                return currentColor;
            }
        }

        public uint PreviousColor
        {
            get
            {
                return previousColor;
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


        public ColorChange() { }

        public void Execute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            uint tempPrevousColor = cell.BGCOlor;
            this.currentColor = this.previousColor;
            this.previousColor = tempPrevousColor;
            cell.BGCOlor = this.currentColor;

        }


        public void UndoColor(uint currentColor, uint previousColor, int row, int col)
        {
            this.currentColor = currentColor;
            this.previousColor = previousColor;
            this.row = row;
            this.col = col;
        }


        public void Unexecute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            uint tempPrevousColor = cell.BGCOlor;
            this.currentColor = this.previousColor;
            this.previousColor = tempPrevousColor;
            cell.BGCOlor = this.currentColor;
        }
    }
}
