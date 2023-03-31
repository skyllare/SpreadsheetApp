// <copyright file="Spreadsheet.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionTreeEngine;
using static SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine
{
    /// <summary>
    /// class for spreadsheet that serves as a container for a 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        Dictionary<string, double> variables = new();
        /// <summary>
        /// Uses MyCell to create a 2d array.
        /// </summary>
        private MyCell[,] cells;

        /// <summary>
        /// Member value for row.
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Member value for column.
        /// </summary>
        private int columnCount;


        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">Number of rows for the 2d array.</param>
        /// <param name="numCols">Number of columns for the 2d array.</param>
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
                    this.cells[row, col].PropertyChanged += this.MyCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// event for when a cell is changed.
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged = delegate { };

        

        /// <summary>
        /// Gets or sets the columnCount.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
            set { this.columnCount = value; }
        }

        /// <summary>
        /// Gets or sets the rowCount.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
            set { this.rowCount = value; }
        }

        /// <summary>
        /// finds a specific cell based on index.
        /// </summary>
        /// <param name="numRow">Row of cell looking for.</param>
        /// <param name="numCol">Column of cell looking for.</param>
        /// <returns>the cell.</returns>
        public Cell? GetCell(int numRow, int numCol)
        {
            if (numRow < 0 || numRow >= this.RowCount || numCol < 0 || numCol >= this.ColumnCount)
            {
                return null;
            }

            return this.cells[numRow, numCol];
        }

        /// <summary>
        /// Event handler for when a cell is changed.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void MyCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MyCell? curCell = sender as MyCell;
            if (curCell != null)
            {
                if (e.PropertyName == "CellText")
                {
                    if (curCell.CellText[0] == '=')
                    {
                        
                        bool testBool = this.isFormula(curCell.CellText);

                        if (testBool)
                        {
                            string equation = curCell.CellText.Substring(1);
                            ExpressionTree test = new ExpressionTree(equation, variables);
                            test.MakeExpressionTree(equation);
                            double evaluation = test.Evaluate();
                            curCell.CellValue = evaluation.ToString();
                        }
                        else
                        {
                            char columnLetter = curCell.CellText[1];
                            int column = (int)columnLetter - 65;
                            string sRow = curCell.CellText.Substring(2);
                            int row = int.Parse(sRow) - 1;
                            curCell.CellValue = this.cells[row, column].CellValue;
                        }
                    }
                    else
                    {
                        curCell.CellValue = curCell.CellText;
                    }
                }
            }
            this.CellPropertyChanged?.Invoke(sender, e);

            if (this.CellPropertyChanged != null)
            {
                this.CellPropertyChanged(sender, e);
            }
        }

        public bool isFormula(string expression)
        {
            List<string> operatorList = new List<string> { "+", "-", "/", "*", ")", "(" };
            bool containsString = operatorList.Any(s => expression.Contains(s));

            if (containsString)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Concrete class to make Cell methods accessible for the spreadsheet class.
        /// </summary>
        public class MyCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MyCell"/> class.
            /// </summary>
            /// <param name="rowIndex">Row.</param>
            /// <param name="columnIndex">Column.</param>
            public MyCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }

            /// <summary>
            /// Gets or sets the cellValue.
            /// </summary>
            public new string CellValue
            {
                get { return this.cellValue; }
                set { this.cellValue = value; }
            }
        }

    }
}
