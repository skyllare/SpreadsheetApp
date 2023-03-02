// <copyright file="SpreadsheetTest.cs" company="Skyllar Estil">
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
        /// test that the spreadsheet constructor has the correct number of rows.
        /// </summary>
        [Test]
        public void Constructor_CreatesRowsOfCorrectSize()
        {
            int numRows = 3;
            int numCols = 0;
            Spreadsheet spreadsheet = new Spreadsheet(numRows, numCols);
            int actualNumRows = spreadsheet.RowCount;
            int actualNumCols = spreadsheet.ColumnCount;
            Assert.That(actualNumRows, Is.EqualTo(numRows));
        }

        /// <summary>
        /// test that the spreadsheet constructor has the correct number of columns.
        /// </summary>
        [Test]
        public void Constructor_CreatesColumnsOfCorrectSize()
        {
            int numRows = 0;
            int numCols = 4;
            Spreadsheet spreadsheet = new Spreadsheet(numRows, numCols);
            int actualNumRows = spreadsheet.RowCount;
            int actualNumCols = spreadsheet.ColumnCount;
            Assert.That(actualNumCols, Is.EqualTo(numCols));
        }
    }
}