namespace Battleships.Core.Ships.Models
{
    public class BattleShip : Ship
    {
        public override byte Size => 5;
        public override ShipType Type => ShipType.Battle;
    }
}
