namespace Battleships.Core.Ships.Models
{
    public class DestroyerShip : Ship
    {
        public override byte Size => 4;
        public override ShipType Type => ShipType.Destroyer;
    }
}
