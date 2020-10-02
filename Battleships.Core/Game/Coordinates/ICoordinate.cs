namespace Battleships.Core.Game
{
    public interface ICoordinate
    {
        int Column { get; }
        int Row { get; }
    }
}