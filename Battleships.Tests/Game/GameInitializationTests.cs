using Battleships.Core.Board;
using Battleships.Core.Board.Fields;
using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Ships;
using NUnit.Framework;
using System;

namespace Battleships.Tests
{
    [TestFixture]
    public class GameInitializationTests
    {
        private static int rowsNumber = 10;
        private static int columnsNumber = 10;
        private GameBoard _gameBoard;

        [OneTimeSetUp]
        public void SetUp()
        {
            _gameBoard = new GameBoard(new GameSettings(rowsNumber, columnsNumber));
        }

        [Test]
        public void CannotAddMoreThanTwoPlayers()
        {
            Assert.Throws<InvalidOperationException>(() =>
                    GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Player("Player1"))
                   .AddPlayer(new Player("Player2"))
                   .AddPlayer(new Player("Player3")),
                   $"For given configuration it should not be possible to add more than two players"); 
        }

        [Test]
        public void CannotAddPlayersWithTheSameName()
        {
            Assert.Throws<InvalidOperationException>(() =>
                    GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Player("Player1"))
                   .AddPlayer(new Player("Player1")),
                   $"User names should be unique");
        }


        [Test]
        public void CanBuildValidGame()
        {
            var game = GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Player("Player1"))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt(new Coordinate(0, 0))
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt(new Coordinate(0, 1))
                            .AndDirection(Direction.Vertical)
                   .AddPlayer(new Player("Player2"))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt(new Coordinate(0, 0))
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt(new Coordinate(0, 1))
                            .AndDirection(Direction.Vertical)
                    .Build();

            Assert.IsNotNull(game);
        }
    }
}
