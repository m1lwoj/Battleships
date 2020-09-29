using Battleships.Core.Board;
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

        public bool Shoot()
        {
            if (IsShot) throw new Exception("Field already shot");

            if(OccupationType == OccupationType.Occupied)
            {
                OccupationType = OccupationType.Shot;
                return true;
            }

            return false;
        }
    }
}
