using Battleships.Core.Game.Coordinates;
using System.Text.RegularExpressions;

namespace Battleships.Core.Game
{
    public class Coordinate
    {
        public int Row { get; }
        public int Column { get; }

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        ///     Creates coordinate object from single string
        ///     Coordinate in text in given manner: 'A4'
        ///     Where 'A' is row and '4' is column
        /// </summary>
        /// <param name="coordinate">Coordinate as single string (e.g. 'A4')</param>
        /// <returns>Coordinate object</returns>
        public static Coordinate Create(string coordinate)
        {
            CoordinateConverter.SplitLettersFromNumbers(coordinate, out string row, out int column);

            return new Coordinate(
                CoordinateConverter.ConvertRowNameToRowNumber(row),
                column);
        }

        /// <summary>
        ///     Creates coordinate object row and column
        /// </summary>
        /// <param name="row">Row (e.g. 'A' or 'D')</param>
        /// <param name="column">Column (e.g. '4', '9')</param>
        /// <returns>Coordinate object</returns>
        public static Coordinate Create(string row, int column)
        {
            return new Coordinate(
                CoordinateConverter.ConvertRowNameToRowNumber(row), 
                column);
        }
    }
}
