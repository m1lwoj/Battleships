using Battleships.Core.Game;
using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Coordinates;
using System;

namespace Battleships.Core.Board
{
    public class OpponentsBoardDrawerDecorator : IBoardDrawer
    {
        private readonly IHiddenBoard _board;

        public OpponentsBoardDrawerDecorator(IHiddenBoard board)
        {
            _board = board;
        }

        public void Draw(string playerName)
        {
            Console.WriteLine();
            Console.WriteLine($"{playerName} board:");

            for (int i = -1; i < _board.Board.GetLength(0); i++)
            {
                for (int j = -1; j < _board.Board.GetLength(1); j++)
                {
                    if (IsStartingPoint(i, j))
                        Console.Write("\t");
                    if (IsFirstColumn(i, j))
                        Console.Write(CoordinateConverter.ConvertBoardRowToUserInputRow(i) + "\t");
                    if (IsFirstRow(i, j))
                        Console.Write($" {j + 1}\t");
                    else if (j != -1 && i != -1)
                        Console.Write(DrawField(i, j));
                }
                Console.WriteLine();
            }
        }

        private bool IsFirstRow(int i, int j)
        {
            return i == -1 && j != -1;
        }

        private bool IsFirstColumn(int i, int j)
        {
            return j == -1 && i != -1;
        }

        private bool IsStartingPoint(int i, int j)
        {
            return j == -1 && i == -1;
        }

        private string DrawField(int row, int column)
        {
            var coordinates = Coordinate.Create(row, column);

            if (_board.IsMissed(coordinates))
            {
                return "[O]\t";
            }

            if (_board.IsHit(coordinates))
            {
                return "[X]\t";
            }

            return "[ ]\t";
        }
    }
}
