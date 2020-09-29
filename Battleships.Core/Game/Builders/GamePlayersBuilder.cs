using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Core.Game.Builders
{
    public class GamePlayersBuilder<T> : GameBuilder where T : GamePlayersBuilder<T>
    {
        protected Player _player;

        public T AddPlayer(Player player)
        {
            _player = player;
            _game.AddPlayer(player);
            return (T)this;
        }
    }
}
