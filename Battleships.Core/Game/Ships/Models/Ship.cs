using System;

namespace Battleships.Core.Ships.Models
{
    public abstract class Ship : IShip
    {
        public bool IsSunk => Size == Hits;
        public byte Hits { get; private set; } = 0;
        public abstract byte Size { get; }
        public abstract ShipType Type { get; }

        public void Shoot()
        {
            if (IsSunk) throw new InvalidOperationException("Cannot hit already sunk ship");
            Hits++;
        }
    }
}
