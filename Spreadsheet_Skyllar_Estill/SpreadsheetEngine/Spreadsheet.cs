// <copyright file="Spreadsheet.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// class for spreadsheet that serves as a container for a 2D array of cells.
    /// </summary>
    internal class Spreadsheet
    {

        private Cell[,] cells;


        protected Cell[,] Cells
        {
            get { return this.cells; }
            set { this.cells = value; }
        }

        /// <summary>
        /// for implementation of the INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        public Spreadsheet(int numRows, int numCols)
        {
            this.cells = new Cell[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    this.cells[row, col] = new MyCell(row, col);
                }
            }
        }

        public Cell GetCell(int numRows, int numCols)
        {
            if (this.cells[numRows, numCols] != null)
            {
                return this.cells[numRows, numCols];
            }
            else
            {
                return null;
            }
        }

  


        private class MyCell : Cell
        {
            public MyCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }

            protected override void SetValue(string newValue)
            {
                base.SetValue(newValue);
            }
        }

    }
}
