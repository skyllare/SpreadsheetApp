// <copyright file="Tests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using SpreadsheetEngine;
using System.Diagnostics.CodeAnalysis;
using static System.Net.Mime.MediaTypeNames;

namespace SpreadsheetTesting
{
    /// <summary>
    /// class that holds tests for spreadsheet methods.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// base spreadsheet to use in tests.
        /// </summary>
        private Spreadsheet spreadsheet = new Spreadsheet(10, 10);

        /// <summary>
        /// test that the spreadsheet constructor has the correct number of rows.
        /// </summary>
        [Test]
        public void Constructor_CreatesRowsOfCorrectSize()
        {
            int actualNumRows = this.spreadsheet.RowCount;
            Assert.That(actualNumRows, Is.EqualTo(10));
        }

        /// <summary>
        /// test that the spreadsheet constructor has the correct number of columns.
        /// </summary>
        [Test]
        public void Constructor_CreatesColumnsOfCorrectSize()
        {
            int actualNumCols = this.spreadsheet.ColumnCount;
            Assert.That(actualNumCols, Is.EqualTo(10));
        }

        /// <summary>
        /// tests if text and value get set correctly.
        /// </summary>
        [Test]
        public void CheckTextandValue()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "10";
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo("10"));
        }

        /// <summary>
        /// check is cell value of a cell the refereences another changes when the other changes.
        /// </summary>
        [Test]
        public void CheckChangedCells()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "10";
            this.spreadsheet.GetCell(0, 1).CellText = "=A1";
            this.spreadsheet.GetCell(0, 0).CellText = "12";
            Assert.That(this.spreadsheet.GetCell(0, 1).CellValue, Is.EqualTo("12"));
        }

        /// <summary>
        /// tests that setting a cell equal to a cell with no value makes the cell null as well.
        /// </summary>
        [Test]
        public void TestNullCells()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "=C4";
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo(null));
        }

        /// <summary>
        /// Tests that undo has the proper cell.
        /// </summary>
        public void TestUndo()
        {
            this.spreadsheet.AddUndoText(null, null, 1, 0);
            Command undo = this.spreadsheet.GetUndo();
            Assert.That(undo.GetCol, Is.EqualTo(0));
            Assert.That(undo.GetRow, Is.EqualTo(1));
        }
    }
}