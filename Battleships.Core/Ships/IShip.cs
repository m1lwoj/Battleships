using System;

namespace Battleships.Core.Ships
{
    public interface IShip
    {
        public byte Size { get; }
        public bool IsSunk { get; }
        public byte Shots { get; }

        public void Shoot();
    }
}
