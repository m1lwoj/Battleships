using Battleships.Core.Ships.Models;

namespace Battleships.Core.Ships
{
    public class ShipsFactory
    {
        public static IShip CreateDestroyerShip()
        {
            return new DestroyerShip();
        }

        public static IShip CreateBattleShip()
        {
            return new BattleShip();
        }
    }
}
