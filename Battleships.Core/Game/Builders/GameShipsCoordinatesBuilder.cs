using Battleships.Core.Board.Fields;
using Battleships.Core.Ships;
using System;

namespace Battleships.Core.Game.Builders
{
    public class GameShipsCoordinatesBuilder<T> : GameShipsBuilder<GameShipsCoordinatesBuilder<T>> where T : GameShipsCoordinatesBuilder<T>
    {
        public T WithCoordinatesStartingAt(Coordinate coordinate)
        {
            _ship.Coordinate = coordinate;
            return (T)this;
        }

        public T WithCoordinatesStartingAt(string input)
        {
            _ship.Coordinate = Coordinate.Create(input);
            return (T)this;
        }

        public T AndDirection(Direction direction)
        {
            if (_ship.Coordinate.Equals(default(Coordinate)))
                throw new InvalidOperationException($"Coordinates has to be setup first, invoke {nameof(WithCoordinatesStartingAt)} method first.");
       
            _ship.Direction = direction;
            _player.PlaceShip(_ship);

            return (T)this;
        }
    }
}
