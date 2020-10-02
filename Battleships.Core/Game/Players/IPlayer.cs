using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Results;
using Battleships.Core.Game.Ships;

namespace Battleships.Core.Game.Players
{
    public interface IPlayer
    {
        IHiddenBoard HiddenBoard { get; }
        string Name { get; }
        IGameBoard OwnBoard { get; }

        public void PlaceShip(ShipLocation shipLocation);
        public ShootResult Shoot(Coordinate coordinate);
    }
}