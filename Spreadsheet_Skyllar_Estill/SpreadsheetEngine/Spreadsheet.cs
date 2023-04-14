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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using ExpressionTreeEngine;
using static SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine
{
    /// <summary>
    /// class for spreadsheet that serves as a container for a 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// stack of undo commands.
        /// </summary>
        private Stack<Command> undo = new Stack<Command>();

        /// <summary>
        /// stack of redo commands.
        /// </summary>
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
        /// event for when a cell is changed.
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged = delegate { };

        /// <summary>
        /// Gets or sets changed cells list.
        /// </summary>
        public List<string> ChangedCells
        {
            get { return this.changedCells; }
            set { this.changedCells = value; }
        }

        /// <summary>
        /// Gets or sets the list of cells that reference another cell.
        /// </summary>
        public Dictionary<string, List<string>> ReferencedCells
        {
            get { return this.referencedCells; }
            set { this.referencedCells = value; }
        }

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
        /// Gets undo stack.
        /// </summary>
        public Stack<Command> Undo
        {
            get
            {
                return this.undo;
            }
        }

        /// <summary>
        /// Gets redo stack.
        /// </summary>
        public Stack<Command> Redo
        {
            get
            {
                return this.redo;
            }
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
        /// Adds undo action to the stack.
        /// </summary>
        /// <param name="currentText">text cell is going to be set to.</param>
        /// <param name="previousText">text the cell is.</param>
        /// <param name="row">row of cell.</param>
        /// <param name="col">column of cell.</param>
        public void AddUndoText(string currentText, string previousText, int row, int col)
        {
            TextChange undo = new TextChange();
            undo.UndoText(previousText, currentText, row, col);
            this.undo.Push(undo);
        }

        /// <summary>
        /// Adds undo action to the stack.
        /// </summary>
        /// <param name="currentColor">color cell is going to be set to.</param>
        /// <param name="previousColor">color the cell is.</param>
        /// <param name="row">row of cell.</param>
        /// <param name="col">column of cell.</param>
        public void AddUndoColor(uint currentColor, uint previousColor, int row, int col)
        {
            ColorChange undo = new ColorChange();
            undo.UndoColor(currentColor, previousColor, row, col);
            this.undo.Push(undo);
        }

        /// <summary>
        /// pops action off the redo stack.
        /// </summary>
        /// <returns>action.</returns>
        public Command GetRedo()
        {
            Command redo = this.redo.Pop();
            this.undo.Push(redo);
            return redo;
        }

        /// <summary>
        /// pops action off the undo stack.
        /// </summary>
        /// <returns>action.</returns>
        public Command GetUndo()
        {
            Command undo = this.undo.Pop();
            this.redo.Push(undo);
            return undo;
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
            string gkey = Convert.ToChar(curCell.ColumnIndex + 65).ToString() + (curCell.RowIndex + 1).ToString();
            if (curCell.CellText != null)
            {
                if (e.PropertyName == "CellText")
                {
                    if (curCell.CellText[0] == '=')
                    {
                        bool testBool = this.IsFormula(curCell.CellText);
                        Regex alphabetRegex = new Regex("[a-zA-Z]");
                        bool testChar = alphabetRegex.IsMatch(curCell.CellText);
                        if (testBool)
                        {
                            string equation = curCell.CellText[1..];
                            this.AddToDict(equation, curCell);
                            double? evaluation = this.EvaluateExpression(equation);
                            curCell.CellValue = evaluation.ToString();
                        }
                        else if (testChar)
                        {
                            char columnLetter = curCell.CellText[1];
                            int column = (int)columnLetter - 65;
                            string sRow = curCell.CellText.Substring(2);
                            int row = int.Parse(sRow) - 1;
                            string cell = (columnLetter + sRow).ToString();
                            this.AddToDict(cell, curCell);
                            curCell.CellValue = this.cells[row, column].CellValue;
                        }
                        else
                        {
                            curCell.CellValue = curCell.CellText.Substring(1);
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
                this.variables[gkey] = double.Parse(curCell.CellValue);
            }

            foreach (string key in this.referencedCells.Keys)
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
                    string tempKey = this.CellName(row, column);
                    this.variables[tempKey] = double.Parse(this.cells[row, column].CellValue);
                }
            }
        }

        /// <summary>
        /// evaluates equation.
        /// </summary>
        /// <param name="expression">equation.</param>
        /// <returns>evaluation.</returns>
        private double? EvaluateExpression(string expression)
        {
            ExpressionTree test = new ExpressionTree(expression, this.variables);
            test.MakeExpressionTree(expression);
            double? evaluation = test.Evaluate();
            return evaluation;
        }

        /// <summary>
        /// returns the row and column of the referencedCell values.
        /// </summary>
        /// <param name="row">row int.</param>
        /// <param name="col">column int.</param>
        /// <param name="key">key of dict.</param>
        /// <param name="i">value #.</param>
        private void GetRowCol(ref int row, ref int col, string key, int i)
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
        /// <param name="curCell">current cell.</param>
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
                    string cell = (Convert.ToChar(curCell.ColumnIndex + 65).ToString() + (curCell.RowIndex + 1)).ToString();
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
        /// saves spreadsheet data to XML file.
        /// </summary>
        public void SaveSpreadsheet(string name)
        {
            Stack<string> savedCells = new Stack<string>();
            XmlWriter xmlWriter = XmlWriter.Create(name);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("spreadsheet");
            while (this.undo.Count != 0)
            {
                //get rid of duplicates
                Command undo = this.undo.Pop();
                int row = undo.GetRow();
                int col = undo.GetCol();
                string? cellName = this.CellName(row, col);
                if (!savedCells.Contains(cellName))
                {
                    savedCells.Push(cellName);
                    Cell? tempCell = this.GetCell(row, col);
                    xmlWriter.WriteStartElement("cell");
                    xmlWriter.WriteAttributeString("name", cellName);
                    xmlWriter.WriteElementString("bgcolor", tempCell.BGCOlor.ToString());
                    xmlWriter.WriteElementString("text", tempCell.CellText);
                    xmlWriter.WriteEndElement();
                }
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public void LoadSpreadsheet()
        {

        }

        private string CellName(int row, int col)
        {
            return Convert.ToChar(col + 65).ToString() + (row + 1).ToString();
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
