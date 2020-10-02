using Battleships.Core.Ships.Models;

namespace Battleships.Core.Game.Ships
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

        internal byte ShipSize => Ship.Size;
        internal ShipType ShipType => Ship.Type;
        internal int Column => Coordinate.Column;
        internal int Row => Coordinate.Row;
    }
}
