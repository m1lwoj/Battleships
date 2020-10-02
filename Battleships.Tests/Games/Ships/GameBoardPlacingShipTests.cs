using Battleships.Core.Game;
using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Ships;
using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;
using System;

namespace Battleships.Tests.Games.Ships
{
    [TestFixture]
    public class GameBoardTests
    {
        private GameBoard _gameBoard;

        [OneTimeSetUp]
        public void SetUp()
        {
            _gameBoard = new GameBoard();
        }

        ///    1   2   3   4   5   6   7   8   9   10 
        /// A [V] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// B [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] [ ]
        /// C [V] [ ] [V] [V] [V] [V] [V] [ ] [ ] [ ]
        /// D [V] [ ] [ ] [V] [V] [V] [V] [V] [ ] [ ]
        /// E [V] [ ] [ ] [ ] [V] [V] [V] [V] [V] [ ] X
        /// F [ ] [V] [ ] [ ] [ ] [V] [V] [V] [V] [V] X
        /// G [ ] [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] X
        /// H [ ] [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] X
        /// I [ ] [V] [ ] [V] [V] [V] [V] [V] [ ] [ ] X
        /// J [X] [V] [X] [X] [X] [ ] [ ] [X] [X] [ ]
        ///                X   X   X   X   X   X
        ///                                    X
        [Test]
        [TestCase("C3", Direction.Horizontal, true)]
        [TestCase("D4", Direction.Horizontal, true)]
        [TestCase("E5", Direction.Horizontal, true)]
        [TestCase("F6", Direction.Horizontal, true)]
        [TestCase("I4", Direction.Horizontal, true)]
        [TestCase("F2", Direction.Vertical, true)]
        [TestCase("A1", Direction.Vertical, true)]
        [TestCase("J1", Direction.Horizontal, false)]
        [TestCase("K4", Direction.Horizontal, false)]
        [TestCase("B7", Direction.Vertical, false)]
        [TestCase("F8", Direction.Vertical, false)]
        [TestCase("J9", Direction.Vertical, false)]
        [TestCase("E11", Direction.Vertical, false)]
        [TestCase("XXX9000", Direction.Vertical, false)]
        public void CanPlaceShipOnBoard(string coordinate, Direction direction, bool isValid)
        {
            IShip ship = ShipsFactory.CreateBattleShip();

            if (isValid)
            {
                Assert.DoesNotThrow(() =>
                   _gameBoard.SetShip(new ShipLocation(ship, Coordinate.Create(coordinate), direction)),
                   $"It is not possible to setup ship on {coordinate}, with direction {direction}, with ship width {ship.Size}");
                Assert.Pass("All conditions passed");
            }

            Assert.Throws<InvalidOperationException>(() =>
                 _gameBoard.SetShip(new ShipLocation(ship, Coordinate.Create(coordinate), direction)),
                 $"It is possible to setup ship on {coordinate}, with direction {direction}, with ship width {ship.Size}, but shouldn't be");
        }
    }
}
