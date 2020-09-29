
namespace Battleships.Core.Game
{
    public class GameSettings : IGameSettings
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Ships { get; private set; }

        public static IGameSettings Default()
        {
            return new GameSettings(10, 10, 10);
        }

        public GameSettings(int width = 10, int height = 10, int ships = 10)
        {
            Width = width;
            Height = height;
            Ships = ships;
        }
    }
}
