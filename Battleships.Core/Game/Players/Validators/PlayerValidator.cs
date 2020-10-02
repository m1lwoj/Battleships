using Battleships.Core.Ships.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core.Game.Players.Validators
{
    internal class PlayerValidator
    {
        readonly Dictionary<ShipType, byte> _limits;

        internal PlayerValidator(byte battleShipsLimit, byte destroyerShipsLimit)
        {
            _limits = new Dictionary<ShipType, byte>
            {
                { ShipType.Battle, battleShipsLimit },
                { ShipType.Destroyer, destroyerShipsLimit }
            };
        }

        internal void ValidateNumberOfPlacedShips(IShip ship, IEnumerable<IShip> addedShips)
        {
            bool isNumberOfShipsOverLimit = addedShips.Count(s => s.Type == ship.Type) >= _limits[ship.Type];

            if (isNumberOfShipsOverLimit)
            {
                throw new InvalidOperationException(
                    $"Cannot add more ships with type {ship.Type} due to limit: {_limits[ship.Type]}");
            }
        }
    }
}
