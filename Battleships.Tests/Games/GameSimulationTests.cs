using Battleships.Core.Board.Fields;
using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Ships;
using NUnit.Framework;
using System;
using Game = Battleships.Core.Game.Game;

namespace Battleships.Tests.Games
{
    [TestFixture]
    public class GameSimulationTests
    {
        private Game _game;

        /// Player1
        ///    0   1   2   3   4   5   6   7   8   9 
        /// A [V] [ ] [V] [ ] [V] [ ] [V] [ ] [V] [ ] 
        /// B [V] [ ] [V] [ ] [V] [ ] [V] [ ] [V] [ ]
        /// C [V] [ ] [V] [ ] [V] [ ] [V] [ ] [V] [ ]
        /// D [V] [ ] [V] [ ] [V] [ ] [V] [ ] [V] [ ]
        /// E [V] [ ] [V] [ ] [V] [ ] [V] [ ] [V] [ ] 
        /// F [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// G [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// H [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// I [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// J [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ]
        /// 

        /// Player2
        ///    0   1   2   3   4   5   6   7   8   9 
        /// A [V] [V] [V] [V] [V] [ ] [ ] [ ] [ ] [ ] 
        /// B [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ]
        /// C [V] [V] [V] [V] [V] [ ] [ ] [ ] [ ] [ ]
        /// D [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ]
        /// E [V] [V] [V] [V] [V] [ ] [ ] [ ] [ ] [ ] 
        /// F [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// G [V] [V] [V] [V] [V] [ ] [ ] [ ] [ ] [ ] 
        /// H [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// I [V] [V] [V] [V] [V] [ ] [ ] [ ] [ ] [ ]
        /// J [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ]
        /// 
        [SetUp]
        public void SetUp()
        {
            _game = GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Player("Player1"))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A0")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A2")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A4")
                            .AndDirection(Direction.Vertical)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A6")
                            .AndDirection(Direction.Vertical)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A8")
                            .AndDirection(Direction.Vertical)
                   .AddPlayer(new Player("Player2"))
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("A0")
                            .AndDirection(Direction.Horizontal)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("C0")
                            .AndDirection(Direction.Horizontal)
                        .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("E0")
                            .AndDirection(Direction.Horizontal)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("G0")
                            .AndDirection(Direction.Horizontal)
                       .AddShip(ShipsFactory.CreateDestroyerShip())
                            .WithCoordinatesStartingAt("I0")
                            .AndDirection(Direction.Horizontal)
                    .Build();

            _game.Start();
        }

        [Test]
        public void WhenPlayerDoesTwoShotsInARowThenError()
        {
            var result = _game.Shoot("Player2", "J9");
            Assert.Throws<InvalidOperationException>(() => _game.Shoot("Player2", "I8"));
        }

        [Test]
        public void WhenShipIsNotHitThenMiss()
        {
            var result = _game.Shoot("Player2", "J9");

            Assert.AreEqual(ShootResult.Missed, result);
        }

        [Test]
        public void WhenShipIsHitThenShipIShot()
        {
            var result = _game.Shoot("Player2", "E8");

            Assert.AreEqual(ShootResult.Shot, result);
        }

        [Test]
        public void WhenShipIsHitOnItsFullLengthThenSink()
        {
            _game.Shoot("Player2", "A0");
            _game.Shoot("Player1", "A9");
            _game.Shoot("Player2", "B0");
            _game.Shoot("Player1", "J9");
            _game.Shoot("Player2", "C0");
            _game.Shoot("Player1", "C9");
            _game.Shoot("Player2", "D0");
            _game.Shoot("Player1", "D9");
            var result = _game.Shoot("Player2", "E0");

            Assert.AreEqual(ShootResult.Sunk, result);
        }
    }
}
