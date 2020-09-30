using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships.Tests")]
namespace Battleships.Core.Game
{
    public class Game
    {
        private static byte PlayersLimit = 2;

        private IGameSettings _gameSettings;
        private string _lastShooterName;
        private readonly Dictionary<string, Player> Players = new Dictionary<string, Player>(PlayersLimit);

        public static Game Create(IGameSettings settings)
        {
            return new Game(settings);
        }

        public static Game Create()
        {
            return new Game(GameSettings.Default());
        }

        public Game(IGameSettings settings)
        {
            _gameSettings = settings;
        }

        internal Game()
        {
        }

        public void Start()
        {
        }
        
        public ShootResult Shoot(string shooterName, string coordinate)
        {
            var coord = Coordinate.Create(coordinate);

            if (!Players.ContainsKey(shooterName))
                throw new InvalidOperationException($"Player with name: {shooterName} does not belong to the game");
            
            if (shooterName == _lastShooterName)
                throw new InvalidOperationException($"{shooterName} can't do two shoots in a row");

            var result = ShootToPlayer(Players.Single(p => p.Key != shooterName).Value.Name, coord);
            _lastShooterName = shooterName;

            return result;
        }

        internal void AddPlayer(Player player)
        {
            if (Players.Count + 1 > PlayersLimit)
                throw new InvalidOperationException($"Maximum number of players riched: {PlayersLimit}");

            if (Players.ContainsKey(player.Name))
                throw new InvalidOperationException($"Player with name: {player.Name} has been already added");

            Players.Add(player.Name, player);
        }

        public Player GetPlayer(string playerName)
        {
          
            
            return Players[playerName];
        }

        private ShootResult ShootToPlayer(string playerName, Coordinate coordinate)
        {
            if (!Players.ContainsKey(playerName))
                throw new InvalidOperationException($"Player with name: {playerName} does not belong to the game");

            return Players[playerName].Shoot(coordinate);
        }
    }
}
