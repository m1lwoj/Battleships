namespace Battleships.Core.Ships.Models
{
    public interface IShip
    {
        public byte Size { get; }
        public bool IsSunk { get; }
        public byte Hits { get; }
        public ShipType Type { get; }

        public void Shoot();
    }
}
