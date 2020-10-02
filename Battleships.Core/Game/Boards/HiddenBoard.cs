using Battleships.Core.Game.Boards.Fields;

namespace Battleships.Core.Game.Boards
{
    public class HiddenBoard : IHiddenBoard
    {
        public OccupationType[,] Board { get; private set; }

        public HiddenBoard()
        {
            Board = new OccupationType[GameSettings.Instance.Width, GameSettings.Instance.Height];
        }

        public void MarkHit(Coordinate coordinate)
        {
            Board[coordinate.Row, coordinate.Column] = OccupationType.Hit;
        }

        public void MarkMissed(Coordinate coordinate)
        {
            Board[coordinate.Row, coordinate.Column] = OccupationType.Missed;
        }

        public bool IsHit(Coordinate coordinate)
        {
            return Board[coordinate.Row, coordinate.Column] == OccupationType.Hit;
        }

        public bool IsMissed(Coordinate coordinate)
        {
            return Board[coordinate.Row, coordinate.Column] == OccupationType.Missed;
        }
    }
}
