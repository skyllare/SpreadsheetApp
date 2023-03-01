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
    public class Cell : INotifyPropertyChanged
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
        /// Represents the actual text that’s typed into the cell.
        /// </summary>
        private string cellText;

        /// <summary>
        /// Represents evaluated value of the cell.
        /// </summary>
        private string cellValue;

        /// <summary>
        /// 
        /// </summary>
        protected string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="inputRowIndex">what row.</param>
        /// <param name="inputColumnIndex">what column.</param>
        private Cell(int inputRowIndex, int inputColumnIndex)
        {
            this.rowIndex = inputRowIndex;
            this.columnIndex = inputColumnIndex;
        }

        /// <summary>
        /// for implementation of the INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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
                if (value == this.cellText)
                {
                    return;
                }

                this.cellText = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CellText)));
                this.EvaluateFormula();
            }
        }

        public string Value
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

        protected virtual void SetValue(string newValue)
        {
            this.value = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
        }

        protected virtual void EvaluateFormula()
        {
            if (this.cellText != null && this.cellText.StartsWith("="))
            {
                
               this.SetValue("10");
            }
        }

    }
}
