using Battleships.Core.Ships;

namespace Battleships.Core.Game.Builders
{
    public class GameShipsBuilder<T> : GamePlayersBuilder<GameShipsBuilder<T>> where T : GameShipsBuilder<T>
    {
        protected ShipLocation _ship;
        public T AddShip(IShip ship)
        {
            _ship = new ShipLocation()
            {
                Ship = ship
            };

            return (T)this;
        }
    }
}
