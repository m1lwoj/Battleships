using Battleships.Core.Board;
using Battleships.Core.Game;
using Battleships.Core.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Core.Board.Fields
{
    public class Field
    {
        public OccupationType OccupationType { get; private set; }
        public IShip Ship { get; private set; }

        public bool IsFree => OccupationType == OccupationType.Empty;
        public bool CanBeShot => OccupationType == OccupationType.Empty || OccupationType == OccupationType.Occupied;
        private bool IsShot => OccupationType == OccupationType.Shot;

        public void SetShip(IShip ship)
        {
            Ship = ship;
            OccupationType = OccupationType.Occupied;
        }

        public ShootResult Shoot()
        {
            if (IsShot) throw new Exception("Field already shot");

            if (Ship == null && IsFree) return ShootResult.Missed;

            Ship.Shoot();

            if (Ship.IsSunk)
            {
                return ShootResult.Sunk;
            }

            return ShootResult.Shot;
        }
    }
}
