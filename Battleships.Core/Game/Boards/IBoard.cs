namespace Battleships.Core.Game.Boards
{
    public interface IBoard<T>
    {
        T[,] Board { get; }
    }
}