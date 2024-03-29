﻿// <copyright file="Tests.cs" company="Skyllar Estil">
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

        [Test]
        public void CircularReference()
        {
            Spreadsheet spreadsheet2 = new Spreadsheet(10, 10);
            spreadsheet2.GetCell(3, 6).CellText = "=G5";
            spreadsheet2.GetCell(4, 6).CellText = "=G6";
            spreadsheet2.GetCell(5, 6).CellText = "=G4";
            Assert.That(spreadsheet2.GetCell(5, 6).CellValue, Is.EqualTo("!(circular reference)"));
        }

        /// <summary>
        /// check is cell value of a cell the refereences another changes when the other changes.
        /// </summary>
        [Test]
        public void CheckChangedCells()
        {
            this.spreadsheet.GetCell(0, 2).CellText = "10";
            this.spreadsheet.GetCell(0, 1).CellText = "=C1";
            this.spreadsheet.GetCell(0, 2).CellText = "12";
            Assert.That(this.spreadsheet.GetCell(0, 1).CellValue, Is.EqualTo("12"));
        }

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
        /// tests that setting a cell equal to a cell with no value makes the cell null as well.
        /// </summary>
        [Test]
        public void TestNullCells()
        {
            this.spreadsheet.GetCell(0, 0).CellText = "=C4";
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo("0"));
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

        /*
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
        */

        /// <summary>
        /// Tests for cells that don't exsist.
        /// test 1 tests for reference to a row one more than in the spreadsheet.
        /// test 2 tests for multiple letter references.
        /// test 3 tests for columns that are not in the spreadsheet.
        /// </summary>
        /// <param name="cell">cell text.</param>
        [TestCase("=A11")]
        [TestCase("=AA1")]
        [TestCase("=Z1")]
        public void CellOutOfContext(string cell)
        {
            this.spreadsheet.GetCell(0, 0).CellText = cell;
            Assert.That(this.spreadsheet.GetCell(0, 0).CellValue, Is.EqualTo("!(bad reference)"));
        }

        [Test]
        public void SelfReference()
        {
            Spreadsheet spreadsheet2 = new Spreadsheet(10, 10);
            spreadsheet2.GetCell(0, 0).CellText = "=A1";
            Assert.That(spreadsheet2.GetCell(0, 0).CellValue, Is.EqualTo("!(self reference)"));
        }

        [Test]
        public void CircularReferenceChange()
        {
            Spreadsheet spreadsheet2 = new Spreadsheet(10, 10);
            spreadsheet2.GetCell(0, 0).CellText = "=B1";
            spreadsheet2.GetCell(0, 1).CellText = "=B2";
            spreadsheet2.GetCell(1, 1).CellText = "5";
            Assert.That(spreadsheet2.GetCell(0, 0).CellValue, Is.EqualTo("5"));
        }

        [Test]
        public void CircularReferenceChangeMul()
        {
            Spreadsheet spreadsheet2 = new Spreadsheet(10, 10);
            spreadsheet2.GetCell(0, 0).CellText = "=B1";
            spreadsheet2.GetCell(0, 1).CellText = "=B2";
            spreadsheet2.GetCell(1, 1).CellText = "5";
            spreadsheet2.GetCell(0, 0).CellText = "=B1*7";
            Assert.That(spreadsheet2.GetCell(0, 0).CellValue, Is.EqualTo("35"));
            Assert.That(spreadsheet2.GetCell(0, 1).CellValue, Is.EqualTo("5"));
            Assert.That(spreadsheet2.GetCell(1, 1).CellValue, Is.EqualTo("5"));
        }

        [Test]
        public void CircularReferenceChangeCh()
        {
            Spreadsheet spreadsheet2 = new Spreadsheet(10, 10);
            spreadsheet2.GetCell(0, 0).CellText = "=B1";
            spreadsheet2.GetCell(0, 1).CellText = "=B2";
            spreadsheet2.GetCell(1, 1).CellText = "=A2";
            spreadsheet2.GetCell(1, 0).CellText = "7";
            Assert.That(spreadsheet2.GetCell(0, 0).CellValue, Is.EqualTo("7"));
            Assert.That(spreadsheet2.GetCell(0, 1).CellValue, Is.EqualTo("7"));
            Assert.That(spreadsheet2.GetCell(1, 1).CellValue, Is.EqualTo("7"));
            Assert.That(spreadsheet2.GetCell(1, 0).CellValue, Is.EqualTo("7"));
        }

    }
}