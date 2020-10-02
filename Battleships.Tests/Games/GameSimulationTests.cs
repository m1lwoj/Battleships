using Battleships.Core.Game;
using Battleships.Core.Game.Builders;
using Battleships.Core.Game.Results;
using Battleships.Core.Settings;
using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;

namespace Battleships.Tests.Games
{
    [TestFixture]
    public class GameSimulationTests
    {
        private const string Player1 = "Player1";
        private const string Player2 = "Player2";
        private IGame _game;

        /// Player1
        ///    1   2   3   4   5   6   7   8   9   10
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
        ///    1   2   3   4   5   6   7   8   9   10
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
            GameSettings.Instance.SetNumberOfShips(numberOfBattleships: 5, numberOfDestroyerShips: 1);

            _game = GameBuilderDirector
                   .NewGame
                   .AddPlayer(new Core.Game.Players.Player(Player1))
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A1")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A3")
                            .AndDirection(Direction.Vertical)
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A5")
                            .AndDirection(Direction.Vertical)
                       .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A7")
                            .AndDirection(Direction.Vertical)
                       .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A9")
                            .AndDirection(Direction.Vertical)
                   .AddPlayer(new Core.Game.Players.Player(Player2))
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("A1")
                            .AndDirection(Direction.Horizontal)
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("C1")
                            .AndDirection(Direction.Horizontal)
                        .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("E1")
                            .AndDirection(Direction.Horizontal)
                       .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("G1")
                            .AndDirection(Direction.Horizontal)
                       .AddShip(ShipsFactory.CreateBattleShip())
                            .WithCoordinatesStartingAt("I1")
                            .AndDirection(Direction.Horizontal)
                    .Start();
        }

        [Test]
        public void WhenPlayerDoesTwoHitsInARowAndGameInSingleModeThenPass()
        {
            var result = _game.Shoot(Player2, "J10");
            Assert.DoesNotThrow(() => _game.Shoot(Player2, "I9"));
            Assert.AreEqual(GameMode.Single, GameSettings.Instance.GameMode);
        }

        [Test]
        public void WhenShipIsNotHitThenMiss()
        {
            var result = _game.Shoot(Player2, "J10");

            Assert.AreEqual(ShootResult.Missed, result);
        }

        [Test]
        public void WhenShipIsHitThenShootResultIsHit()
        {
            var result = _game.Shoot(Player2, "E9");

            Assert.AreEqual(ShootResult.Hit, result);
        }

        [Test]
        public void WhenShipIsHitOnItsFullLengthThenSink()
        {
            _game.Shoot(Player2, "A1");
            _game.Shoot(Player1, "A10");
            _game.Shoot(Player2, "B1");
            _game.Shoot(Player1, "J10");
            _game.Shoot(Player2, "C1");
            _game.Shoot(Player1, "C10");
            _game.Shoot(Player2, "D1");
            _game.Shoot(Player1, "D10");
            var result = _game.Shoot(Player2, "E1");

            Assert.AreEqual(ShootResult.Sunk, result);
        }

        [Test]
        public void WhenAllShipsAreSunkThenGameEnds()
        {
            var size = ShipsFactory.CreateBattleShip().Size;

            Assert.AreEqual(ShootResult.Sunk, ShootHorizontally("A", 1, size));
            Assert.AreEqual(ShootResult.Sunk, ShootHorizontally("C", 1, size));
            Assert.AreEqual(ShootResult.Sunk, ShootHorizontally("E", 1, size));
            Assert.AreEqual(ShootResult.Sunk, ShootHorizontally("G", 1, size));
            Assert.AreEqual(ShootResult.Won, ShootHorizontally("I", 1, size));
        }

        private ShootResult ShootHorizontally(string row, int startingColumn, byte size)
        {
            var lastShot = ShootResult.Missed;
            for (int i = startingColumn; i < size + startingColumn; i++)
            {
                lastShot = _game.Shoot(Player1, row + i);
            }

            return lastShot;
        }
    }
}
