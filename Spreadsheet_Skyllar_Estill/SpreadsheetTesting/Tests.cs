// <copyright file="Tests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;
using SpreadsheetEngine;
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
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests that undo has the proper cell.
        /// </summary>
        [Test]
        public void TestUndo()
        {
            this.spreadsheet.AddUndoText(null, null, 1, 0);
            Command undo = this.spreadsheet.GetUndo();
            Assert.That(undo.GetCol, Is.EqualTo(0));
            Assert.That(undo.GetRow, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests if the data from the XML file is added to the spreadsheet properly.
        /// testFile1 tests that that a value that references another cell is properly set.
        /// testFile2 tests a general cell value.
        /// testFile3 tests a file with tags that should be ignored.
        /// </summary>
        /// <param name="filePath">xml file path.</param>
        /// <param name="value">expected value.</param>
        [TestCase("C:\\Users\\skyll\\OneDrive\\Desktop\\Code\\cpts321-hws\\Spreadsheet_Skyllar_Estill\\SpreadsheetTesting\\testFile2.xml", "22")]
        [TestCase("C:\\Users\\skyll\\OneDrive\\Desktop\\Code\\cpts321-hws\\Spreadsheet_Skyllar_Estill\\SpreadsheetTesting\\testFile1.xml", "22")]
        [TestCase("C:\\Users\\skyll\\OneDrive\\Desktop\\Code\\cpts321-hws\\Spreadsheet_Skyllar_Estill\\SpreadsheetTesting\\testFile3.xml", "6")]
        public void TestXMLFile(string filePath, string value)
        {
            string fileName = filePath;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            this.spreadsheet.LoadSpreadsheet(fileName);
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo(value));
        }


        [TestCase("=A11")]
        [TestCase("=ZZ1")]
        public void CellOutOfContext(string cell)
        {
            this.spreadsheet.GetCell(0, 0).CellText = cell;
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo("!(bad reference)"));
        }

    }
}