using Battleships.Core.Board;
using Battleships.Core.Board.Fields;
using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Ships;
using System;

namespace Battleships.ConsoleUI
{
    class Program
    {
        private const string player1Name = "Player1";
        private const string player2Name = "Player2";

        static void Main(string[] args)
        {
            var game = GameBuilderDirector
                  .NewGame
                  .AddPlayer(new Player(player1Name))
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                           .WithCoordinatesStartingAt(new Coordinate(0, 0))
                           .AndDirection(Direction.Vertical)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                           .WithCoordinatesStartingAt(new Coordinate(0, 1))
                           .AndDirection(Direction.Vertical)
                  .AddPlayer(new Player(player2Name))
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                           .WithCoordinatesStartingAt(new Coordinate(0, 0))
                           .AndDirection(Direction.Horizontal)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                           .WithCoordinatesStartingAt(new Coordinate(2, 4))
                           .AndDirection(Direction.Vertical)
                   .Build();

            new GameBoardDrawerDecorator(game.GetPlayer(player1Name).OwnBoard).Draw(player1Name);
            new GameBoardDrawerDecorator(game.GetPlayer(player2Name).OwnBoard).Draw(player2Name);
            Console.ReadLine();
        }
    }
}
