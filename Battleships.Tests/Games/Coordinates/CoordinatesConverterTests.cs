using Battleships.Core.Game.Coordinates;
using NUnit.Framework;
using System;

namespace Battleships.Tests.Games.Coordinates
{
    [TestFixture]
    public class CoordinatesConverterTests
    {
        [Test]
        [TestCase("A3", "A", 3)]
        [TestCase("Z12", "Z", 12)]
        [TestCase("AGS3412", "AGS", 3412)]
        [TestCase("AGEWDWRS3421212", "AGEWDWRS", 3421212)]
        public void CanSplitDigitsFromText(string input, string expectedLetters, int expectedNumber)
        {
            CoordinateConverter.SplitLettersFromNumbers(input, out string lettersResult, out int numberResult);

            Assert.AreEqual(expectedNumber, numberResult);
            Assert.AreEqual(expectedLetters, lettersResult);
        }

        [Test]
        [TestCase("A3S4")]
        [TestCase("A!#@3")]
        [TestCase("A!#@")]
        [TestCase("!84dwq6qd6#@")]
        public void CantSplitIncorrectCoordinates(string input)
        {
            Assert.Throws<FormatException>(() =>
                CoordinateConverter.SplitLettersFromNumbers(input, out string lettersResult, out int numberResult));
        }

        [Test]
        [TestCase("A", 0)]
        [TestCase("B", 1)]
        [TestCase("C", 2)]
        [TestCase("D", 3)]
        [TestCase("Z", 25)]
        [TestCase("AA", 26)]
        [TestCase("AB", 27)]
        [TestCase("ce", 82)]
        [TestCase("CW", 100)]
        [TestCase("AAA", 702)]
        public void CanConvertUserInputRowToBoardRow(string lettersRow, int expectedRowNumber)
        {
            int result = CoordinateConverter.ConvertUserInputRowToBoardRow(lettersRow);

            Assert.AreEqual(expectedRowNumber, result);
        }

        [Test]
        [TestCase(0, "A")]
        [TestCase(1, "B")]
        [TestCase(2, "C")]
        [TestCase(3, "D")]
        [TestCase(25, "Z")]
        [TestCase(26, "AA")]
        [TestCase(27, "AB")]
        [TestCase(82, "CE")]
        [TestCase(100, "CW")]
        [TestCase(702, "AAA")]
        public void CanConvertBoardRowNumberToLettersRow(int rowNumber, string expectedLettersRow)
        {
            string result = CoordinateConverter.ConvertBoardRowToUserInputRow(rowNumber);

            Assert.AreEqual(expectedLettersRow, result);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(8, 9)]
        [TestCase(20, 21)]
        [TestCase(99998, 99999)]
        public void CanConvertBoardColumnNumberToUserInputNumberr(int boardColumnNumber, int expectedUserInputColumnNumber)
        {
            int result = CoordinateConverter.ConvertBoardColumnNumberToUserInputNumber(boardColumnNumber);

            Assert.AreEqual(expectedUserInputColumnNumber, result);
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(99999, 99998)]
        public void CanConvertUserInputNumberToBoardColumnNumber(int userInputColumnNumber, int expectedGameColumnNumber)
        {
            int result = CoordinateConverter.ConvertUserInputNumberToBoardColumnNumber(userInputColumnNumber);

            Assert.AreEqual(expectedGameColumnNumber, result);
        }
    }
}
