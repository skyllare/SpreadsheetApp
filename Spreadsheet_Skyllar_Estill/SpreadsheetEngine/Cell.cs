// <copyright file="Cell.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    /// <summary>
    /// abstract class for singular cell of spreadsheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Used to allow spreadsheet class to set the value of the cell.
        /// </summary>
        protected string value;

        /// <summary>
        /// private read-only value for the row index.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// private read-only value for the column index.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// Represents the actual text that’s typed into the cell.
        /// </summary>
        private string cellText;

        /// <summary>
        /// Represents evaluated value of the cell.
        /// </summary>
        private string cellValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="inputRowIndex">what row.</param>
        /// <param name="inputColumnIndex">what column.</param>
        protected Cell(int inputRowIndex, int inputColumnIndex)
        {
            this.rowIndex = inputRowIndex;
            this.columnIndex = inputColumnIndex;
        }

        /// <summary>
        /// Gets the row index of this cell.
        /// </summary>
        protected int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets the column index of this cell.
        /// </summary>
        protected int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets the cellText string value.
        /// </summary>
        protected string CellText
        {
            get
            {
                return this.cellText;
            }

            set
            {
                if (this.cellText != value)
                {
                    this.cellText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CellText"));
                }
            }
        }

        protected string CellValue
        {
            get
            {
                if (this.cellText != null && this.cellText.StartsWith("="))
                {
                    return this.value;
                }

                return this.cellText;
            }
        }
    }
}
