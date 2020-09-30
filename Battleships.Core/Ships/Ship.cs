using System;

namespace Battleships.Core.Ships
{
    public abstract class Ship : IShip
    {
        public bool IsSunk => Size == Shots;

        public byte Shots { get; private set; } = 0;

        public abstract byte Size { get; }

        public void Shoot()
        {
            if (IsSunk) throw new InvalidOperationException("Cannot shot sunk ship");
            Shots++;
        }
    }
}
