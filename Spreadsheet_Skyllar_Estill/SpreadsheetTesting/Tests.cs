// <copyright file="Tests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using SpreadsheetEngine;

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
        Spreadsheet spreadsheet = new Spreadsheet(10, 10);

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

        [Test]
        public void CheckTextandValue()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "10";
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo("10"));
        }

        [Test]
        public void CheckChangedCells()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "10";
            this.spreadsheet.GetCell(0, 1).CellText = "=A1";
            this.spreadsheet.GetCell(0, 0).CellText = "12";
            Assert.That(this.spreadsheet.GetCell(0,1).CellValue, Is.EqualTo("12"));
        }

        [Test]
        public void CheckCellExpressions(/*string n, string m, string o, string p, string q*/)
        {
            this.spreadsheet.GetCell(5, 8).CellText = "(8+8-2)/7*2";
            Assert.That(this.spreadsheet.GetCell(1, 1).CellValue, Is.EqualTo("2"));
        }
    }
}