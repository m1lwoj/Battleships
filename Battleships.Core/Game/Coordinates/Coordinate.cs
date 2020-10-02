using Battleships.Core.Game.Coordinates;

namespace Battleships.Core.Game
{
    public class Coordinate : ICoordinate
    {
        public int Row { get; }
        public int Column { get; }

        private Coordinate(int row, int column)
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
                CoordinateConverter.ConvertUserInputRowToBoardRow(row),
                CoordinateConverter.ConvertUserInputNumberToBoardColumnNumber(column));
        }

        /// <summary>
        ///     Creates coordinate object from string row and number column
        /// </summary>
        /// <param name="row">Row (e.g. 'A' or 'D')</param>
        /// <param name="column">Column (e.g. '4', '9')</param>
        /// <returns>Coordinate object</returns>
        public static Coordinate Create(string row, int column)
        {
            return new Coordinate(
                CoordinateConverter.ConvertUserInputRowToBoardRow(row),
                CoordinateConverter.ConvertUserInputNumberToBoardColumnNumber(column));
        }

        /// <summary>
        ///     Creates coordinate object from number row and number column
        ///     Similar to array coordinates
        /// </summary>
        /// <param name="row">Row (e.g. 0 or 3)</param>
        /// <param name="column">Column (e.g. 0 or 3)</param>
        /// <returns>Coordinate object</returns>
        public static Coordinate Create(int row, int column)
        {
            return new Coordinate(row, column);
        }
    }
}
