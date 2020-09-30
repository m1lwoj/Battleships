using Battleships.Core.Game.Coordinates;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
        [TestCase("CE", 82)]
        [TestCase("CW", 100)]
        [TestCase("XEU", 16374)]
        public void CanParseLetterIntoRowNumber(string rowName, int expectedRowNumber)
        {
            int result = CoordinateConverter.ConvertRowNameToRowNumber(rowName);

            Assert.AreEqual(expectedRowNumber, result);
        }
    }
}
