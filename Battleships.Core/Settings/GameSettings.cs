
using Battleships.Core.Settings;

namespace Battleships.Core.Game
{
    public class GameSettings
    {
        public static int DefaultBoardWidth = 10;
        public static int DefaultBoardHeight = 10;
        public static byte DefaultNumberOfBattleships = 1;
        public static byte DefaultNumberOfDestroyers = 2;
        public static GameMode DefaultGameMode = GameMode.Single;

        private static GameSettings instance = null;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public byte NumberOfDestroyerShips { get; private set; }

        public byte NumberOfBattleShips { get; private set; }

        public GameMode GameMode { get; private set; }

        public int TotalNumberOfShips => NumberOfDestroyerShips + NumberOfBattleShips;

        public static GameSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Default();
                }

                return instance;
            }
        }

        private GameSettings(
            int width,
            int height,
            byte numberOfBattleShips,
            byte numberOfDestroyers,
            GameMode gameMode)
        {
            Width = width;
            Height = height;
            NumberOfBattleShips = numberOfBattleShips;
            NumberOfDestroyerShips = numberOfDestroyers;
            GameMode = gameMode;
        }

        internal static GameSettings Default()
        {
            return new GameSettings(
                DefaultBoardWidth,
                DefaultBoardHeight,
                DefaultNumberOfBattleships,
                DefaultNumberOfDestroyers,
                DefaultGameMode);
        }

        public void SetNumberOfShips(byte numberOfBattleships, byte numberOfDestroyerShips)
        {
            NumberOfBattleShips = numberOfBattleships;
            NumberOfDestroyerShips = numberOfDestroyerShips;
        }

        public override string ToString()
        {
            return @$"GameMode: {GameMode}
Board size: {Width}x{Height}
Battleships: {NumberOfBattleShips}
Destroyers: {NumberOfDestroyerShips}";
        }
    }
}
