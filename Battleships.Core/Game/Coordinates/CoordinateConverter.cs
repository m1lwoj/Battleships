using System;
using System.Text.RegularExpressions;

namespace Battleships.Core.Game.Coordinates
{
    public class CoordinateConverter
    {
        private const string SplitLettersFromNumbersPattern = @"^([a-zA-Z]+)(\d+)$";

        internal static void SplitLettersFromNumbers(string input, out string letters, out int numbers)
        {
            Regex re = new Regex(SplitLettersFromNumbersPattern);
            Match result = re.Match(input);

            if (!result.Success)
                throw new FormatException($"Invalid input format {input}, it should follow pattern <NUMBER><LETTER>");

            letters = result.Groups[1].Value;
            numbers = int.Parse(result.Groups[2].Value);
        }

        internal static int ConvertUserInputRowToBoardRow(string row)
        {
            int retVal = 0;
            string r = row.ToUpper();
            for (int iChar = r.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = r[iChar];
                int colNum = colPiece - 64;
                retVal += colNum * (int)Math.Pow(26, r.Length - (iChar + 1));
            }

            return retVal - 1;
        }

        public static string ConvertBoardRowToUserInputRow(int row)
        {
            int dividend = row + 1;
            string rowName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                rowName = Convert.ToChar(65 + modulo).ToString() + rowName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return rowName;
        }

        internal static int ConvertUserInputNumberToBoardColumnNumber(int column)
        {
            return column - 1;
        }

        internal static int ConvertBoardColumnNumberToUserInputNumber(int column)
        {
            return column + 1;
        }
    }
}
