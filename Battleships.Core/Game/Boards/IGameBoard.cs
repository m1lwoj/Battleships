using Battleships.Core.Game.Boards.Fields;
using Battleships.Core.Game.Results;
using Battleships.Core.Game.Ships;

namespace Battleships.Core.Game.Boards
{
    public interface IGameBoard : IBoard<IField>
    {
        ShootResult Shoot(Coordinate coordinate);
        void SetShip(ShipLocation shipLocation);
        bool AnyShipsLeftInBattle();
    }
}