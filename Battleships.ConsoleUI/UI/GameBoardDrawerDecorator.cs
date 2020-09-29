using System;

namespace Battleships.Core.Board
{
    public class GameBoardDrawerDecorator
    {
        private IBoard _board;

        public GameBoardDrawerDecorator(IBoard board)
        {
            _board = board;
        }

        public void Draw(string playerName)
        {
            Console.WriteLine();
            Console.WriteLine($"{playerName} board:");

            for (int i = 0; i < _board.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Board.GetLength(1); j++)
                {
                    Console.Write(_board.Board[i, j].IsFree ? "[ ]\t" : "[X]\t");
                }
                Console.WriteLine();
            }
        }
    }
}
