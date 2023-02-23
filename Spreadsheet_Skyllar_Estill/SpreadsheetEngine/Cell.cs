// <copyright file="Cell.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// abstract class for singular cell of spreadsheet.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// private read-only value for the row index.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// private read-only value for the column index.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="inputRowIndex">what row</param>
        /// <param name="inputColumnIndex">what column</param>
        private Cell(int inputRowIndex, int inputColumnIndex)
        {
            this.rowIndex = inputRowIndex;
            this.columnIndex = inputColumnIndex;
        }

        /// <summary>
        /// Gets the row index of this cell.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets the column index of this cell.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }
    }

}


