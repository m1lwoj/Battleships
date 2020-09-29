using Battleships.Core.Board;
using Battleships.Core.Board.Fields;
using Battleships.Core.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships.Core.Game
{
    public class Game
    {
        private static byte PlayersLimit = 2;

        private IGameSettings _gameSettings;
        private readonly GameBoard _board;
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

        public bool Shoot(string shooterName, Coordinate coordinate)
        {
            if (!Players.ContainsKey(shooterName))
                throw new InvalidOperationException($"Player with name: {shooterName} does not belong to the game");

            return ShootToPlayer(Players.Single(p => p.Key != shooterName).Value.Name, coordinate);
        }

        private bool ShootToPlayer(string playerName, Coordinate coordinate)
        {
            if (Players.ContainsKey(playerName))
                throw new InvalidOperationException($"Player with name: {playerName} does not belong to the game");

            return Players[playerName].Shoot(coordinate);
        }
    }
}
