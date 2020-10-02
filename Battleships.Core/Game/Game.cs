using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Players;
using Battleships.Core.Game.Results;
using Battleships.Core.Game.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

///Adds coupling between two projects
[assembly: InternalsVisibleTo("Battleships.Tests")]
namespace Battleships.Core.Game
{
    public class Game : IGame
    {
        private static readonly byte PlayersLimit = 2;

        private string _lastShooterName;
        private readonly GameValidator _gameValidator;
        private readonly Dictionary<string, IPlayer> Players = new Dictionary<string, IPlayer>(PlayersLimit);

        public Game()
        {
            _gameValidator = new GameValidator(PlayersLimit);
        }

        public ShootResult Shoot(string shooterName, string coordinate)
        {
            var coord = Coordinate.Create(coordinate);
            _gameValidator.ValidateShoot(Players, shooterName, _lastShooterName);

            var result = ShootToPlayer(GetPlayerOpponent(shooterName), coord);
            _lastShooterName = shooterName;

            return result;
        }

        public IHiddenBoard GetPlayersOpponentBoard(string playerName)
        {
            _gameValidator.ValidatePlayerIsRegistered(Players, playerName);
            string opponent = GetPlayerOpponent(playerName);
            return Players[opponent].HiddenBoard;
        }

        public void AddPlayer(IPlayer player)
        {
            _gameValidator.ValidateAddingPlayer(Players, player);

            Players.Add(player.Name, player);
        }

        private ShootResult ShootToPlayer(string playerName, Coordinate coordinate)
        {
            _gameValidator.ValidatePlayerIsRegistered(Players, playerName);

            return Players[playerName].Shoot(coordinate);
        }

        private string GetPlayerOpponent(string player)
        {
            return Players.Single(p => p.Key != player).Value.Name;
        }
    }
}
