using Battleships.ConsoleUI.Configuration;
using Battleships.Core.Board;
using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Game.Players;
using Battleships.Core.Game.Results;
using Battleships.Core.Ships;
using System;

namespace Battleships.ConsoleUI
{
    class Program
    {
        private static Game _game;
        private static GameConfiguration _gameConfiguration;
        private static readonly ShipLocationGenerator _shipLocationGenerator = new ShipLocationGenerator();

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();
            _gameConfiguration = ReadUserSettings();
            _game = SetupGame();

            int moves = LoopBattle();

            DisplayEndingMessage(moves);
        }

        private static void DisplayEndingMessage(int moves)
        {
            Console.WriteLine($"Congratulations '{_gameConfiguration.Name}', You WON!");
            Console.WriteLine($"You defeated '{_gameConfiguration.Opponent}' in {moves} moves");
        }

        private static int LoopBattle()
        {
            var _drawer = new OpponentsBoardDrawerDecorator(
                _game.GetPlayersOpponentBoard(_gameConfiguration.Name));

            int moves = 0;
            var result = ShootResult.Missed;
            while (result != ShootResult.Won)
            {
                try
                {
                    _drawer.Draw(_gameConfiguration.Opponent);

                    Console.WriteLine();
                    Console.WriteLine("Where do you want to shoot?");
                    Console.WriteLine("----------------------------------------");
                    string target = Console.ReadLine();
                    result = _game.Shoot(_gameConfiguration.Name, target);
                    Console.WriteLine(result);
                    Console.WriteLine("----------------------------------------");
                    moves++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid move: " + ex.Message);
                }
            }

            return moves;
        }

        private static Game SetupGame()
        {
            Console.WriteLine("Setting up the game");

            var gamePlayerBuilder = GameBuilderDirector
                  .NewGame
                  .AddPlayer(new Player(_gameConfiguration.Name));

            Console.WriteLine("Setting ships randomly");

            gamePlayerBuilder = gamePlayerBuilder.AddPlayer(new Player(_gameConfiguration.Opponent));

            foreach (var shipLocation in _shipLocationGenerator.GenerateRandomShipLocations())
            {
                gamePlayerBuilder.AddShip(shipLocation.Ship)
                           .WithCoordinatesStartingAt(shipLocation.Coordinate)
                           .AndDirection(shipLocation.Direction);
            }

            Console.WriteLine("Game has been setup");
            Console.WriteLine();
            Console.WriteLine("With given configuration");
            Console.WriteLine(GameSettings.Instance.ToString());
            Console.WriteLine();
            Console.WriteLine("STARTING THE GAME!");
            Console.WriteLine();

            return gamePlayerBuilder.Start();
        }

        private static GameConfiguration ReadUserSettings()
        {
            GameConfiguration gameConfiguration = new GameConfiguration();
            Console.WriteLine();
            Console.WriteLine("What is your name? ");
            gameConfiguration.Name = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("You will be shooting to? ");
            gameConfiguration.Opponent = Console.ReadLine();

            return gameConfiguration;
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome in pseudo Battleships Game!");
        }
    }
}
