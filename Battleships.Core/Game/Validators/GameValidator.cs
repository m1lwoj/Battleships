using Battleships.Core.Game.Players;
using Battleships.Core.Settings;
using System;
using System.Collections.Generic;

namespace Battleships.Core.Game.Validators
{
    internal class GameValidator
    {
        private readonly byte _playersLimit;

        public GameValidator(byte playersLimit)
        {
            _playersLimit = playersLimit;
        }

        internal bool CheckPlayerMove(string shooterName, string lastShooterName)
        {
            if (GameSettings.Instance.GameMode == GameMode.Single)
                return true;

            if (shooterName == lastShooterName)
                return false;

            return true;
        }

        internal void ValidateShoot(Dictionary<string, IPlayer> players, string shooter, string lastShooter)
        {
            ValidatePlayerIsRegistered(players, shooter);

            if (!CheckPlayerMove(shooter, lastShooter))
                throw new InvalidOperationException($"{shooter} can't shoots twice in a row");
        }

        internal void ValidatePlayerIsRegistered(Dictionary<string, IPlayer> players, string shooter)
        {
            if (!players.ContainsKey(shooter))
                throw new InvalidOperationException($"Player with name: {shooter} is not a registered player");
        }

        internal void ValidateAddingPlayer(Dictionary<string, IPlayer> players, IPlayer player)
        {
            if (players.Count + 1 > _playersLimit)
                throw new InvalidOperationException($"Maximum number of players reached: {_playersLimit}");

            if (players.ContainsKey(player.Name))
                throw new InvalidOperationException($"Player with name: {player.Name} has been already added");
        }
    }
}

