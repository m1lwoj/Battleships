using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;
using System;

namespace Battleships.Tests.Games
{
    [TestFixture]
    public class GameInitializationTests
    {
        private const string Player1 = "Player1";
        private const string Player2 = "Player2";

        [Test]
        public void CannotAddMoreThanTwoPlayers()
        {
            Assert.Throws<InvalidOperationException>(() =>
                    GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Core.Game.Players.Player(Player1))
                   .AddPlayer(new Core.Game.Players.Player(Player2))
                   .AddPlayer(new Core.Game.Players.Player("Player3")),
                   $"For given configuration it should not be possible to add more than two players");
        }

        [Test]
        public void CannotAddPlayersWithTheSameName()
        {
            Assert.Throws<InvalidOperationException>(() =>
                    GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Core.Game.Players.Player(Player1))
                   .AddPlayer(new Core.Game.Players.Player(Player1)),
                   $"User names should be unique");
        }

        [Test]
        public void CanBuildValidGame()
        {
            GameSettings.Instance.SetNumberOfShips(numberOfBattleships: 1, numberOfDestroyerShips: 4);

            var game = GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Core.Game.Players.Player(Player1))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A1")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A2")
                            .AndDirection(Direction.Vertical)
                   .AddPlayer(new Core.Game.Players.Player(Player2))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A1")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A2")
                            .AndDirection(Direction.Vertical)
                    .Start();

            Assert.IsNotNull(game);
        }
    }
}
