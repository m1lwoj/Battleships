using Battleships.Core.Game.Results;
using Battleships.Core.Ships.Models;

namespace Battleships.Core.Game.Boards.Fields
{
    public class Field : IField
    {
        public OccupationType OccupationType { get; private set; }
        public IShip Ship { get; private set; }

        public bool IsFree => OccupationType == OccupationType.Empty;
        public bool CanBeHit => OccupationType == OccupationType.Empty || OccupationType == OccupationType.Occupied;
        private bool IsHit => OccupationType == OccupationType.Hit;

        public void SetShip(IShip ship)
        {
            Ship = ship;
            OccupationType = OccupationType.Occupied;
        }

        public ShootResult Shoot()
        {
            if (IsHit) return ShootResult.Hit;
            if (Ship == null && IsFree) return ShootResult.Missed;

            Ship.Shoot();

            OccupationType = OccupationType.Hit;

            if (Ship.IsSunk)
            {
                return ShootResult.Sunk;
            }

            return ShootResult.Hit;
        }
    }
}
