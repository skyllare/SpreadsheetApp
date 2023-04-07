// <copyright file="Spreadsheet.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.IO;
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
        private Stack<Command> undo = new Stack<Command>();

        private Stack<Command> redo = new Stack<Command>();

        /// <summary>
        /// values of the cells.
        /// </summary>
        private Dictionary<string, double> variables = new ();

        /// <summary>
        /// dictionary of cells that are referenced by other cells.
        /// </summary>
        private Dictionary<string, List<string>> referencedCells = new ();

        /// <summary>
        /// list of supported operators.
        /// </summary>
        private List<string> operatorList = new List<string> { "+", "-", "/", "*" };

        /// <summary>
        /// List of cells that are changed.
        /// </summary>
        private List<string> changedCells = new List<string>();

        public void AddUndoText(string currentText, string previousText, int row, int col)
        {
            TextChange undo = new TextChange();
            undo.UndoText(previousText, currentText, row, col);
            this.undo.Push(undo);
        }

        public void AddUndoColor(uint currentColor, uint previousColor, int row, int col)
        {
            ColorChange undo = new ColorChange();
            undo.UndoColor(currentColor, previousColor, row, col);
            this.undo.Push(undo);
        }

        public Command GetRedo()
        {
            Command redo = this.redo.Pop();
            this.undo.Push(redo);
            return redo;
        }

        public Command GetUndo()
        {
            Command undo = this.undo.Pop();
            this.redo.Push(undo);
            return undo;
        }

        /// <summary>
        /// Gets or sets changed cells list.
        /// </summary>
        public List<string> ChangedCells
        {
            get { return this.changedCells; }
            set { this.changedCells = value; }
        }

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
        /// Gets or sets the list of cells that reference another cell.
        /// </summary>
        public Dictionary<string, List<string>> ReferencedCells
        {
            get { return this.referencedCells; }
            set { this.referencedCells = value; }
        }

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
        /// checks if the expression is a formula by checking if it includes operators.
        /// </summary>
        /// <param name="expression">expression entered into cell.</param>
        /// <returns>true if it is, false otherwise.</returns>
        private bool IsFormula(string expression)
        {
            bool containsString = this.operatorList.Any(s => expression.Contains(s));

            if (containsString)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Event handler for when a cell is changed.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void MyCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MyCell? curCell = sender as MyCell;
            string key = Convert.ToChar(curCell.ColumnIndex + 65).ToString() + (curCell.RowIndex + 1).ToString();
            if (curCell.CellText != null)
            {
                if (e.PropertyName == "CellText")
                {
                    if (curCell.CellText[0] == '=')
                    {
                        bool testBool = this.IsFormula(curCell.CellText);

                        if (testBool)
                        {
                            string equation = curCell.CellText[1..];
                            AddToDict(equation, curCell);
                            double? evaluation = this.EvaluateExpression(equation);
                            curCell.CellValue = evaluation.ToString();
                        }
                        else
                        {
                            char columnLetter = curCell.CellText[1];
                            int column = (int)columnLetter - 65;
                            string sRow = curCell.CellText.Substring(2);
                            int row = int.Parse(sRow) - 1;
                            string cell = (columnLetter + sRow).ToString();
                            this.AddToDict(cell, curCell);
                            curCell.CellValue = this.cells[row, column].CellValue;
                        }
                    }
                    else
                    {
                        curCell.CellValue = curCell.CellText;
                    }
                }
            }
            else
            {
                curCell.CellValue = null;
            }

            this.CellPropertyChanged?.Invoke(sender, e);

            if (this.CellPropertyChanged != null && e.PropertyName != "BGColor")
            {
                this.CellPropertyChanged(sender, e);
            }
            if (curCell.CellValue != null) 
            {
                this.variables[key] = double.Parse(curCell.CellValue);
            }

            if (this.referencedCells.ContainsKey(key))
            {
                for (int i = 0; i < this.referencedCells[key].Count; i++)
                {
                    int column = 0, row = 0;
                    this.GetRowCol(ref row, ref column, key, i);
                    string equation = this.cells[row, column].CellText;
                    double? evaluation = this.EvaluateExpression(equation);
                    this.cells[row, column].CellValue = evaluation.ToString();
                    string col = column.ToString();
                    string rows = row.ToString();
                    this.changedCells.Add(col + rows);
                }

            }
        }

        private double? EvaluateExpression (string expression)
        {
            ExpressionTree test = new ExpressionTree(expression, this.variables);
            test.MakeExpressionTree(expression);
            double? evaluation = test.Evaluate();
            return evaluation;
        }

        /// <summary>
        /// returns the row and column of the referencedCell values
        /// </summary>
        /// <param name="row">row int.</param>
        /// <param name="col">column int.</param>
        /// <param name="key">key of dict.</param>
        /// <param name="i">value #.</param>
        private void GetRowCol (ref int row, ref int col, string key, int i)
        {
            char columnLetter = this.referencedCells[key][i][0];
            col = (int)columnLetter - 65;
            string sRow = this.referencedCells[key][i].Substring(1);
            row = int.Parse(sRow) - 1;
        }

        /// <summary>
        /// adds the cells that reference other cells to the dictionary.
        /// </summary>
        /// <param name="expression">expression entered into cell. </param>
        private void AddToDict(string expression, MyCell curCell)
        {
            ExpressionTree test = new ExpressionTree(expression, this.variables);
            List<string> sExpression = test.ShuntingYardAlgorithm(expression);
            List<string> holder = new List<string>();
            for (int i = 0; i < sExpression.Count; i++) 
            {
                bool isLetter = char.IsLetter(sExpression[i][0]);
                if (isLetter)
                {
                    string cell = (Convert.ToChar(curCell.ColumnIndex + 65).ToString() + (curCell.RowIndex+1)).ToString();
                    if (!this.referencedCells.ContainsKey(sExpression[i]))
                    {
                        holder.Add(cell);
                        this.referencedCells.Add(sExpression[i], holder);
                    }
                    else
                    {
                        this.referencedCells[sExpression[i]].Append(cell);
                    }
                }
            }
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
