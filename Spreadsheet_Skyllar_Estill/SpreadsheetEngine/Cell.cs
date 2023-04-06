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
        /// <summary>
        /// private read-only value for the row index.
        /// </summary>
        protected readonly int rowIndex;

        /// <summary>
        /// private read-only value for the column index.
        /// </summary>
        protected readonly int columnIndex;

        /// <summary>
        /// Represents the actual text that’s typed into the cell.
        /// </summary>
        protected string cellText;

        /// <summary>
        /// Represents evaluated value of the cell.
        /// </summary>
        protected string cellValue;

        /// <summary>
        /// color property. Defualt white.
        /// </summary>
        private uint BGColor = 0xFFFFFFFF;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="inputRowIndex">what row.</param>
        /// <param name="inputColumnIndex">what column.</param>
        public Cell(int inputRowIndex, int inputColumnIndex)
        {
            this.rowIndex = inputRowIndex;
            this.columnIndex = inputColumnIndex;
        }

        /// <summary>
        /// Event for when the cell text is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

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

        /// <summary>
        /// Gets or sets the cellText string value.
        /// </summary>
        public string CellText
        {
            get
            {
                return this.cellText;
            }

            set
            {
                if (this.cellText == value)
                {
                    return;
                }

                this.cellText = value;
                if (this.cellText != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("CellText"));
                }
            }
        }

        public uint BGCOlor
        {
            get
            {
                return this.BGColor; 
            }
            set 
            {
                if (this.BGColor == value)
                {
                    return;
                }
                else
                {
                    this.BGColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
                }
            }
        }

        /// <summary>
        /// Gets the cellValue.
        /// </summary>
        public string CellValue
        {
            get { return this.cellValue; }
        }

        
    }
}
