namespace Battleships.Core.Game.Builders
{
    public abstract class GameBuilder
    {
        protected Game _game;
        public GameBuilder()
        {
            _game = new Game();
        }
        public virtual Game Start() => _game;
    }
}
