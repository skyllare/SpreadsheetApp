// <copyright file="ColorChange.cs" company="Skyllar Estil">
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
    /// class for when the color of a cell changed for redo/undo.
    /// </summary>
    public class ColorChange : Command
    {
        /// <summary>
        /// cell's current color.
        /// </summary>
        private uint currentColor;

        /// <summary>
        /// cell's previous color.
        /// </summary>
        private uint previousColor;

        /// <summary>
        /// cell's row.
        /// </summary>
        private int row;

        /// <summary>
        /// cell's column.
        /// </summary>
        private int col;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChange"/> class.
        /// </summary>
        public ColorChange() { }

        /// <summary>
        /// Gets cell's current color.
        /// </summary>
        public uint CurrentColor
        {
            get
            {
                return this.currentColor;
            }
        }

        /// <summary>
        /// Gets cell's previous color.
        /// </summary>
        public uint PreviousColor
        {
            get
            {
                return this.previousColor;
            }
        }

        /// <summary>
        /// gets the row value.
        /// </summary>
        /// <returns>row.</returns>
        public int GetRow()
        {
            return this.row;
        }

        /// <summary>
        /// gets the column value.
        /// </summary>
        /// <returns>col.</returns>
        public int GetCol()
        {
            return this.col;
        }

        /// <summary>
        /// redo function.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        public void Execute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            uint tempPrevousColor = cell.BGCOlor;
            this.currentColor = this.previousColor;
            this.previousColor = tempPrevousColor;
            cell.BGCOlor = this.currentColor;

        }

        /// <summary>
        /// function to add the proper action for undoing a color.
        /// </summary>
        /// <param name="currentColor">current cell color.</param>
        /// <param name="previousColor">previous cell color.</param>
        /// <param name="row">cell row.</param>
        /// <param name="col">cell column.</param>
        public void UndoColor(uint currentColor, uint previousColor, int row, int col)
        {
            this.currentColor = currentColor;
            this.previousColor = previousColor;
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// function for the undo feature.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        public void Unexecute(Spreadsheet s)
        {
            Cell cell = s.GetCell(this.row, this.col);
            uint tempPrevousColor = cell.BGCOlor;
            this.currentColor = this.previousColor;
            this.previousColor = tempPrevousColor;
            cell.BGCOlor = this.currentColor;
        }
    }
}
