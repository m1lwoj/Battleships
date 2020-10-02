using Battleships.Core.Board.Validators;
using Battleships.Core.Game;
using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Ships;
using Battleships.Core.Ships.Models;
using System;
using System.Collections.Generic;

namespace Battleships.Core.Ships
{
    public class ShipLocationGenerator
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly BoardValidator _boardValidator;
        private readonly GameBoard _board;
        private readonly List<ShipLocation> _shipLocations;
        private readonly byte _destroyerShipLimit;
        private readonly byte _battleShipLimit;

        public ShipLocationGenerator()
        {
            _boardValidator = new BoardValidator();
            _board = new GameBoard();
            _shipLocations = new List<ShipLocation>(GameSettings.Instance.TotalNumberOfShips);
            _destroyerShipLimit = GameSettings.Instance.NumberOfDestroyerShips;
            _battleShipLimit = GameSettings.Instance.NumberOfBattleShips;
        }

        public IEnumerable<ShipLocation> GenerateRandomShipLocations()
        {
            GenerateShipLocations(_destroyerShipLimit, () => ShipsFactory.CreateDestroyerShip());
            GenerateShipLocations(_battleShipLimit, () => ShipsFactory.CreateBattleShip());

            return _shipLocations;
        }

        private void GenerateShipLocations(byte numberOfShips, Func<IShip> shipCreator)
        {
            for (int i = 0; i < numberOfShips; i++)
            {
                var shipLocation = GetRandomShipLocation(shipCreator());

                if (_boardValidator.CanShipBePlaced(_board.Board, shipLocation))
                {
                    _shipLocations.Add(shipLocation);
                    _board.SetShip(shipLocation);
                }
                else
                {
                    i--;
                }
            }
        }

        private ShipLocation GetRandomShipLocation(IShip ship)
        {
            Direction direction = GetRandomDirection();

            int row = direction == Direction.Horizontal
                    ? _random.Next(0, GameSettings.Instance.Height - 1)
                    : _random.Next(0, GameSettings.Instance.Height - ship.Size - 1);

            int column = direction == Direction.Horizontal
                    ? _random.Next(0, GameSettings.Instance.Width - ship.Size - 1)
                    : _random.Next(0, GameSettings.Instance.Width - 1);

            return new ShipLocation(ship, Coordinate.Create(row, column), direction);
        }

        private Direction GetRandomDirection()
        {
            var possibleDirections = Enum.GetValues(typeof(Direction));
            var direction = (Direction)possibleDirections.GetValue(_random.Next(possibleDirections.Length));

            return direction;
        }
    }
}
