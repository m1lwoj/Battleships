using Battleships.Core.Board.Fields;
using Battleships.Core.Ships;

namespace Battleships.Core.Game
{
    public class ShipLocation
    {
        public ShipLocation()
        {
        }

        public ShipLocation(IShip ship, Coordinate startingCoordinate, Direction direction)
        {
            Ship = ship;
            Coordinate = startingCoordinate;
            Direction = direction;
        }

        public IShip Ship { get; set; }
        public Coordinate Coordinate { get; set; }
        public Direction Direction { get; set; }
    }
}
