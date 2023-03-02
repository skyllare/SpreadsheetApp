using SpreadsheetEngine;

namespace SpreadsheetTesting
{
    public class Tests
    {

        // test that the constructor makes a 2d array with the given parameters
        [Test]
        public void Constructor_CreatesArrayOfCorrectSize()
        {
            // Arrange
            int numRows = 3;
            int numCols = 4;
            Spreadsheet spreadsheet = new Spreadsheet(numRows, numCols);

            // Act
            int actualNumRows = spreadsheet.Cells.GetLength(0);
            int actualNumCols = spreadsheet.Cells.GetLength(1);

            // Assert
            Assert.AreEqual(numRows, actualNumRows);
            Assert.AreEqual(numCols, actualNumCols);
        }
    }
}