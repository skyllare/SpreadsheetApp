// <copyright file="Spreadsheet.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
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
        /// stack of cell names that reference another cell in its text.
        /// </summary>
        private List<string> referencingCells = new List<string>();

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
        /// saves spreadsheet data to XML file.
        /// </summary>
        /// <param name="name">file name.</param>
        public void SaveSpreadsheet(string name)
        {
            Stack<string> savedCells = new Stack<string>();
            XmlWriter xmlWriter = XmlWriter.Create(name);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("spreadsheet");
            while (this.undo.Count != 0)
            {
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

        /// <summary>
        /// loads a file to the spreadsheet.
        /// </summary>
        /// <param name="name">file name.</param>
        public void LoadSpreadsheet(string name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(name);

            // Find all "cell" elements
            XmlNodeList cells = xmlDoc.SelectNodes("//cell");

            // Loop through each "cell" element
            foreach (XmlNode cell in cells)
            {
                // Get the "name" attribute value
                string cellName = cell.Attributes["name"].Value;

                // Get the "bgcolor" element value
                uint bgColor = uint.Parse(cell.SelectSingleNode("bgcolor").InnerText);

                // Get the "text" element value
                string text = cell.SelectSingleNode("text").InnerText;
                cellName = this.CellName(cellName);
                int trow = int.Parse(cellName.Substring(1)) - 1;
                int tcol = cellName[0] - 48;
                Cell temp = this.GetCell(trow, tcol);
                temp.CellText = text;
                temp.BGCOlor = bgColor;
            }

            this.undo.Clear();
            this.redo.Clear();
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
        /// solves a formula from the spreadsheet and save the cell's value to
        /// the evaluation.
        /// </summary>
        /// <param name="formula">the formula.</param>
        /// <param name="cell">current cell.</param>
        private void SolveFormula(string formula, MyCell? curCell)
        {
            if (curCell != null)
            {
                this.AddToRefDict(formula, curCell);
                double? evaluation = this.EvaluateExpression(formula);
                if (evaluation != null)
                {
                    curCell.CellValue = evaluation.ToString();

                }
                else
                {
                    curCell.CellValue = "0";
                }

                this.AddToVarDict(curCell);
            }
        }

        /// <summary>
        /// checks if the cell text contains 1 letter.
        /// if it contains more than one, it is referencing a cell
        /// that doesn't exsist in the spreadsheet.
        /// </summary>
        /// <param name="text">cell text.</param>
        /// <returns>true if contains 1 letter and a number in the correct range.</returns>
        private bool IsCellReference(string text)
        {
            List<string> sExpression = this.GetCellsFromText(text);
            for (int i = 0; i < sExpression.Count; i++)
            {
                int value = sExpression[i].Count(char.IsLetter);
                if (value == 1)
                {
                    if (Convert.ToInt32(sExpression[i][0]) - 65 > this.columnCount)
                    {
                        return false;
                    }

                    int row = int.Parse(sExpression[i].Substring(1));
                    if (row < 0 || row > this.rowCount)
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// if the expression in the cell references itself.
        /// </summary>
        /// <param name="text">cell text.</param>
        /// <param name="curCell">current cell.</param>
        /// <returns>true if it contains reference to itself.</returns>
        private bool IsSelfReference(MyCell? curCell)
        {
            string text = curCell.CellText;
            ExpressionTree test = new ExpressionTree(text.Substring(1), this.variables);
            List<string> sExpression = test.ShuntingYardAlgorithm(text.Substring(1));
            string cellName = this.CellName(curCell.RowIndex, curCell.ColumnIndex);
            if (sExpression.Contains(cellName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// sets the value of the cell to the cell that it is referencing.
        /// </summary>
        /// <param name="row">row of referenced cell.</param>
        /// <param name="col">column of referenced cell..</param>
        /// <param name="curCell">current cell being edited.</param>
        private void SolveCellReference(int row, int col, MyCell? curCell)
        {
            if (curCell != null)
            {
                string cell = this.CellName(row, col);
                this.AddToRefDict(cell, curCell);
                if (this.cells[row, col].CellValue == null)
                {
                    curCell.CellValue = "0";
                }
                else
                {
                    curCell.CellValue = this.cells[row, col].CellValue;
                }

                this.AddToVarDict(curCell);
            }
        }

        /// <summary>
        /// Event handler for when a cell is changed.
        /// </summary>
        /// <param name="sender">The cell being modified.</param>
        /// <param name="e">The property being modified.</param>
        private void MyCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MyCell? curCell = sender as MyCell;
            bool testCircular = false;
            if (curCell != null && curCell.CellText != null)
            {
                if (e.PropertyName == "CellText")
                {
                    if (!string.IsNullOrWhiteSpace(curCell.CellText))
                    {
                        if (curCell.CellText[0] != '=')
                        {
                            curCell.CellValue = curCell.CellText;
                            this.AddToVarDict(curCell);
                        }
                        else
                        {
                            testCircular = this.IsCircularReference(curCell.CellText);
                            bool testSelfReference = this.IsSelfReference(curCell);
                            if (testSelfReference)
                            {
                                curCell.CellValue = "!(self reference)";
                            }
                            else
                            {
                                bool testFormula = this.IsFormula(curCell.CellText);
                                bool testChar = this.IsCellReference(curCell.CellText);
                                this.referencingCells.Add(this.MakeKey(curCell.RowIndex, curCell.ColumnIndex));
                                if (testCircular)
                                {
                                    this.AddToRefDict(curCell.CellText, curCell);
                                }
                                else if (testFormula)
                                {
                                    this.SolveFormula(curCell.CellText[1..], curCell);
                                }
                                else if (testChar)
                                {
                                    int column = this.LetterToNumber(curCell.CellText[1]);
                                    int row = this.StringToInt(curCell.CellText.Substring(2));
                                    this.SolveCellReference(row, column, curCell);
                                }
                                else if (!testChar)
                                {
                                    curCell.CellValue = "!(bad reference)";
                                }
                                else
                                {
                                    curCell.CellValue = curCell.CellText.Substring(1);
                                    //this.referencingCells.Remove(this.MakeKey(curCell.RowIndex, curCell.ColumnIndex));
                                }
                            }
                        }
                    }
                }
                else if (this.CellPropertyChanged != null && e.PropertyName != "BGColor")
                {
                    this.CellPropertyChanged(sender, e);
                }
            }
            else
            {
                if (curCell != null)
                {
                    curCell.CellValue = null;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, e);
            this.ChangeReferenceValues(curCell.RowIndex, curCell.ColumnIndex);
            if (testCircular)
            {
                curCell.CellValue = "!(circular reference)";
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
        /// changes the values of the cells that reference another cell.
        /// </summary>
        private void ChangeReferenceValues(int curCellRow, int curCellCol)
        {
            foreach (string key in this.referencedCells.Keys)
            {
                for (int i = 0; i < this.referencedCells[key].Count; i++)
                {
                    string tempKey1 = key;
                    int curColumn = 0, curRow = 0;
                    this.GetRowCol(ref curRow, ref curColumn, tempKey1, i);
                    if (this.referencedCells.Any(kvp => kvp.Value.Contains(key)))
                    {
                        tempKey1 = this.referencedCells.Last(kvp => kvp.Value.Contains(key)).Key;
                        if (this.variables.ContainsKey(tempKey1))
                        {
                            this.variables[key] = this.variables[tempKey1];
                        }
                    }

                    int valColumn = 0, valRow = 0;
                    double? evaluation = 0.0;
                    if (i < this.referencedCells[tempKey1].Count && !this.IsFormula(this.cells[curRow, curColumn].CellText))
                    {
                        this.GetRowCol(ref valRow, ref valColumn, tempKey1, i);
                        string equation = this.cells[valRow, valColumn].CellText;
                        evaluation = this.EvaluateExpression(equation);                 
                    }
                    else
                    {
                        string equation = this.cells[curRow, curColumn].CellText;
                        evaluation = this.EvaluateExpression(equation);
                    }

                    if (evaluation != null && (curRow != curCellRow || curColumn != curCellCol))
                    {
                        this.cells[curRow, curColumn].CellValue = evaluation.ToString();
                    }

                    string colRow = this.MakeKey(curRow, curColumn);
                    this.changedCells.Add(colRow);
                    string tempKey = this.CellName(curRow, curColumn);
                    if (this.cells[curRow, curColumn].CellValue != string.Empty && (this.cells[curRow, curColumn].CellValue != null))
                    {
                        this.variables[tempKey] = double.Parse(this.cells[curRow, curColumn].CellValue);
                    }
                }
            }
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
        /// takes row and column value and returns a key for the variables dictionary.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">column.</param>
        /// <returns>key.</returns>
        private string MakeKey(int row, int col)
        {
            string column = Convert.ToChar(col + 65).ToString();
            string rows = (row + 1).ToString();
            return column + rows;
        }

        /// <summary>
        /// checks if a cell is using circular references
        /// </summary>
        /// <param name="text">cell text.</param>
        /// <returns>true if it is a circular reference.</returns>
        private bool IsCircularReference(string text)
        {
            List<string> references = this.GetCellsFromText(text.Substring(1));
            if (this.referencingCells.Count != 0)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    if (references.Contains(this.referencingCells[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// takes cell text and returns all the cell references in it.
        /// </summary>
        /// <param name="text">cell text.</param>
        /// <returns>cells referenced in the cell text.</returns>
        private List<string> GetCellsFromText(string text)
        {
            List<string> references = new List<string>();
            ExpressionTree test = new ExpressionTree(text, this.variables);
            List<string> sExpression = test.ShuntingYardAlgorithm(text);
            for (int i = 0; i < sExpression.Count; i++)
            {
                if (char.IsLetter(sExpression[i][0]))
                {
                    references.Add(sExpression[i]);
                }
            }

            return references;
        }

        /// <summary>
        /// converts a letter value to it's number value.
        /// </summary>
        /// <param name="letter">the letter.</param>
        /// <returns>number value.</returns>
        private int LetterToNumber(char letter)
        {
            int column = (int)letter - 65;
            return column;
        }

        /// <summary>
        /// takes a string of numbers and returns them as an int.
        /// </summary>
        /// <param name="number">number string.</param>
        /// <returns>numbers as int.</returns>
        private int StringToInt(string number)
        {
            int result = Convert.ToInt32(number) - 1;
            return result;
        }

        /// <summary>
        /// adds the cells that reference other cells to the dictionary.
        /// </summary>
        /// <param name="expression">expression entered into cell. </param>
        /// <param name="curCell">current cell.</param>
        private void AddToRefDict(string expression, MyCell curCell)
        {
            ExpressionTree test = new ExpressionTree(expression, this.variables);
            List<string> sExpression = test.ShuntingYardAlgorithm(expression);
            List<string> holder = new List<string>();
            for (int i = 0; i < sExpression.Count; i++)
            {
                bool isLetter = char.IsLetter(sExpression[i][0]);
                if (isLetter)
                {
                    string cell = this.CellName(curCell.RowIndex, curCell.ColumnIndex);
                    if (!this.referencedCells.ContainsKey(sExpression[i]))
                    {
                        holder.Add(cell);
                        this.referencedCells.Add(sExpression[i], holder);
                    }
                    else
                    {
                        this.referencedCells[sExpression[i]].Add(cell);
                    }
                }
            }
        }

        /// <summary>
        /// Adds values to the variable dictionary.
        /// </summary>
        /// <param name="curCell">current cell.</param>
        private void AddToVarDict(MyCell? curCell)
        {
            if (curCell != null)
            {
                string key = this.MakeKey(curCell.RowIndex, curCell.ColumnIndex);
                if (curCell.CellValue != null)
                {
                    this.variables[key] = double.Parse(curCell.CellValue);
                }
                else
                {
                    this.variables[key] = 0;
                }

            }
        }

        /// <summary>
        /// Converts to the letter number cell name.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">columns.</param>
        /// <returns>cell. ex. A1.</returns>
        private string CellName(int row, int col)
        {
            return Convert.ToChar(col + 65).ToString() + (row + 1).ToString();
        }

        /// <summary>
        /// returns the cell in form letter and numbers.
        /// </summary>
        /// <param name="cellName">row col cell values.</param>
        /// <returns>ex"A1".</returns>
        private string CellName(string cellName)
        {
            string name;
            name = (cellName[0] - 'A').ToString();
            name += cellName.Substring(1);
            return name;
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
            public new string? CellValue
            {
                get { return this.cellValue; }
                set { this.cellValue = value; }
            }
        }
    }
}
