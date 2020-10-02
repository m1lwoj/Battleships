using Battleships.Core.Game.Results;
using Battleships.Core.Ships.Models;

namespace Battleships.Core.Game.Boards.Fields
{
    public interface IField
    {
        bool CanBeHit { get; }
        bool IsFree { get; }
        OccupationType OccupationType { get; }
        IShip Ship { get; }

        void SetShip(IShip ship);
        ShootResult Shoot();
    }
}