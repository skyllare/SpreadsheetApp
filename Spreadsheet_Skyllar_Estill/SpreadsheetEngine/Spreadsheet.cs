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
        

        private MyCell[,] cells;
       

        private int rowCount;
        private int columnCount;

        public Spreadsheet(int numRows, int numCols)
        {
            this.RowCount = numRows;
            this.ColumnCount = numCols;

            this.cells = new MyCell[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    this.cells[row, col] = new MyCell(row, col);
                    this.cells[row, col].PropertyChanged += MyCellPropertyChanged;
                }
            }
        }

        public event PropertyChangedEventHandler? CellPropertyChanged = delegate { };

        public int ColumnCount
        {
            get { return this.columnCount; }
            set { this.columnCount = value; }
        }

        public int RowCount
        {
            get { return this.rowCount; }
            set { this.rowCount = value; }
        }

        public Cell? GetCell(int numRow, int numCol)
        {
            if (numRow < 0 || numRow >= this.RowCount || numCol < 0 || numCol >= this.ColumnCount)
            {
                return null;
            }

            return this.cells[numRow, numCol];
        }

        private void MyCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MyCell? curCell = sender as MyCell;
            if (curCell != null)
            {
                if (e.PropertyName == "CellText")
                {
                    if (curCell.CellText[0] == '=')
                    {
                        char columnLetter = curCell.CellText[1];

                        int column = (int)columnLetter - 65;
                        string sRow = curCell.CellText.Substring(2);
                        int row = int.Parse(sRow);

                        curCell.CellValue = this.cells[row, column].CellValue;
                    }
                    else
                    {
                        curCell.CellValue = curCell.CellText;
                    }
                }
            }

            if (this.CellPropertyChanged != null)
            {
                this.CellPropertyChanged(sender, e);
            }
        }

        public class MyCell : Cell
        {
            public MyCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }

            public new string CellValue
            {
                get { return this.cellValue; }
                set { this.cellValue = value; }
            }

        }
    }
}
