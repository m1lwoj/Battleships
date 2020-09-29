using Battleships.Core.Board.Fields;

namespace Battleships.Core.Board
{
    public interface IBoard
    {
        Field[,] Board { get; }
    }
}