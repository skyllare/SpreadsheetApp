// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// class for spreadsheet that serves as a container for a 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        public event PropertyChangedEventHandler CellPropertyChanged;

        private MyCell[,] cells;

        public Spreadsheet(int numRows, int numCols)
        {
            this.cells = new MyCell[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    this.cells[row, col] = new MyCell(row, col);
                }
            }
        }
        public MyCell[,] Cells
        {
            get { return this.cells; }
        }

        public int ColumnCount
        {
            get { return this.cells.GetLength(1); }
        }

        public int RowCount
        {
            get { return this.cells.GetLength(0); }
        }

        public Cell GetCell(int numRow, int numCol)
        {
            if (numRow < 0 || numRow >= this.cells.GetLength(0) || numCol < 0 || numCol >= this.cells.GetLength(1))
            {
                return null;
            }

            return this.cells[numRow, numCol];
        }

        public class MyCell : Cell
        {
            public MyCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }

            public static MyCell createCell(int rowIndex, int columnIndex)
            {
                return new MyCell(rowIndex, columnIndex);
            }

            public void setValue(MyCell[,] cellGrid)
            {
                string thisText = this.CellText;

                if (thisText[0] == '=')
                {
                    byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(thisText);

                    this.value = cellGrid[asciiBytes[1] - 65, asciiBytes[2]].CellValue;
                }
                else
                {
                    this.value = thisText;
                }
            }
        }
        protected void OnCellPropertyChanged(Cell c, string text)
        {
            PropertyChangedEventHandler handler = this.CellPropertyChanged;

            if (handler != null)
            {
                handler(c, new PropertyChangedEventArgs(text));
            }
        }

        private void handler(object sender, PropertyChangedEventArgs e)
        {
            MyCell c = sender as MyCell;

            c.setValue(this.cells);

            OnCellPropertyChanged(sender as Cell, "CellValue");
        }
    }
}
