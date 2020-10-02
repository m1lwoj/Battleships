using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Players;
using Battleships.Core.Game.Results;

namespace Battleships.Core.Game
{
    public interface IGame
    {
        IHiddenBoard GetPlayersOpponentBoard(string playerName);
        ShootResult Shoot(string shooterName, string coordinate);
        void AddPlayer(IPlayer player);
    }
}