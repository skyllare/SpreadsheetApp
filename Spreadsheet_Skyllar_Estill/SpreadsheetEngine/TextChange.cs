// <copyright file="TextChange.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// class for when the cell of a text is changed for redo/undo.
    /// </summary>
    public class TextChange : Command
    {
        /// <summary>
        /// cell's current text.
        /// </summary>
        private string currentText;

        /// <summary>
        /// cell's previous text.
        /// </summary>
        private string previousText;

        /// <summary>
        /// cell's row.
        /// </summary>
        private int row;

        /// <summary>
        /// cell's column.
        /// <summary>
        private int col;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChange"/> class.
        /// </summary>
        public TextChange() { }

        /// <summary>
        /// Gets ell's current text.
        /// </summary>
        public string CurrentText
        {
            get
            {
                return this.currentText;
            }
        }

        /// <summary>
        /// gets cell previous text.
        /// </summary>
        public string PreviousText
        {
            get
            {
                return this.previousText;
            }
        }

        /// <summary>
        /// gets cell row.
        /// </summary>
        /// <returns>row.</returns>
        public int GetRow()
        {
            return this.row;
        }

        /// <summary>
        /// gets cell column.
        /// </summary>
        /// <returns>column.</returns>
        public int GetCol()
        {
            return this.col;
        }

        /// <summary>
        /// Redo functionality.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        public void Execute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            string tempPrevousText = cell.CellText;
            this.currentText = this.previousText;
            this.previousText = tempPrevousText;
            cell.CellText = this.currentText;

        }

        /// <summary>
        /// sets the undo text.
        /// </summary>
        /// <param name="currentText">cell's current text.</param>
        /// <param name="previousText">cell's previous text.</param>
        /// <param name="row">cell's row.</param>
        /// <param name="col">cell's column.</param>
        public void UndoText(string currentText, string previousText, int row, int col)
        {
            this.currentText = currentText;
            this.previousText = previousText;
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Undo cell functionality.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        public void Unexecute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            string tempPreviousText = cell.CellText;
            this.currentText = this.previousText;
            this.previousText = tempPreviousText;
            cell.CellText = this.currentText;
        }
    }
}
