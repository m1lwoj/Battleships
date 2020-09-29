using Battleships.Core.Board;
using Battleships.Core.Board.Fields;
using Battleships.Core.Game;
using Battleships.Core.Ships;
using NUnit.Framework;
using System;

namespace Battleships.Tests
{
    [TestFixture]
    public class GameBoardTests
    {
        private static int rowsNumber = 10;
        private static int columnsNumber = 10;
        private GameBoard _gameBoard;

        [OneTimeSetUp]
        public void SetUp()
        {
            _gameBoard = new GameBoard(new GameSettings(rowsNumber, columnsNumber));
        }

        ///    Y0  Y1  Y2  Y3  Y4  Y5  Y6  Y7  Y8  Y9 
        /// X0 [V] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] [ ] 
        /// X1 [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] [ ]
        /// X2 [V] [ ] [V] [V] [V] [V] [V] [ ] [ ] [ ]
        /// X3 [V] [ ] [ ] [V] [V] [V] [V] [V] [ ] [ ]
        /// X4 [V] [ ] [ ] [ ] [V] [V] [V] [V] [V] [ ] X
        /// X5 [ ] [V] [ ] [ ] [ ] [V] [V] [V] [V] [V] X
        /// X6 [ ] [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] X
        /// X7 [ ] [V] [ ] [ ] [ ] [ ] [ ] [X] [ ] [ ] X
        /// X8 [ ] [V] [ ] [V] [V] [V] [V] [V] [ ] [ ] X
        /// X9 [X] [V] [X] [X] [X] [ ] [ ] [X] [X] [ ]
        ///                 X   X   X   X   X   X
        ///                                     X
        [Test]
        [TestCase(2, 2, Direction.Horizontal, true)]
        [TestCase(3, 3, Direction.Horizontal, true)]
        [TestCase(4, 4, Direction.Horizontal, true)]
        [TestCase(5, 5, Direction.Horizontal, true)]
        [TestCase(8, 3, Direction.Horizontal, true)]
        [TestCase(5, 1, Direction.Vertical, true)]
        [TestCase(0, 0, Direction.Vertical, true)]
        [TestCase(9, 0, Direction.Horizontal, false)]
        [TestCase(10, 3, Direction.Horizontal, false)]
        [TestCase(1, 6, Direction.Vertical, false)]
        [TestCase(5, 7, Direction.Vertical, false)]
        [TestCase(9, 8, Direction.Vertical, false)]
        [TestCase(4, 10, Direction.Vertical, false)]
        [TestCase(-1, 11, Direction.Vertical, false)]
        public void CanPlaceShipOnBoard(int x, int y, Direction direction, bool isValid)
        {
            IShip ship = ShipsFactory.CreateDestroyerShip();

            if (isValid)
            {
                Assert.DoesNotThrow(() =>
                   _gameBoard.SetShip(new ShipLocation(ship, new Coordinate(x, y), direction)),
                   $"It is not possible to setup ship on {x}, {y}, with direction {direction}, with ship width {ship.Size}");
                Assert.Pass("All conditions passed");
            }

            Assert.Throws<InvalidOperationException>(() =>
                 _gameBoard.SetShip(new ShipLocation(ship, new Coordinate(x, y), direction)),
                 $"It is possible to setup ship on {x}, {y}, with direction {direction}, with ship width {ship.Size}, but shouldn't be");
        }
    }
}
