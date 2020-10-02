using Battleships.Core.Game.Boards.Fields;

namespace Battleships.Core.Game.Boards
{
    public interface IHiddenBoard : IBoard<OccupationType>
    {
        void MarkHit(Coordinate coordinate);
        void MarkMissed(Coordinate coordinate);
        bool IsHit(Coordinate coordinate);
        bool IsMissed(Coordinate coordinate);
    }
}