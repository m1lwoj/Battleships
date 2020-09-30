using System;
using System.Text.RegularExpressions;

namespace Battleships.Core.Game.Coordinates
{
    internal class CoordinateConverter
    {
        private const string SplitLettersFromNumbersPattern = @"^([a-zA-Z]+)(\d+)$";

        internal static void SplitLettersFromNumbers(string input, out string letters, out int numbers)
        {
            Regex re = new Regex(SplitLettersFromNumbersPattern);
            Match result = re.Match(input);

            if (!result.Success)
                throw new FormatException($"Invalid input format {input}, it shoulf follow pattern <NUMBER><LETTER>");

            letters = result.Groups[1].Value;
            numbers = int.Parse(result.Groups[2].Value);
        }

        internal static int ConvertRowNameToRowNumber(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal - 1;
        }
    }
}
