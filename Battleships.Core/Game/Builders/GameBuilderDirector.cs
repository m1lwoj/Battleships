namespace Battleships.Core.Game.Builders
{
    public class GameBuilderDirector : GameShipsCoordinatesBuilder<GameBuilderDirector>
    {
        public static GameBuilderDirector NewGame => new GameBuilderDirector();
    }
}
