using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;
using System;

namespace Battleships.Tests.Ships
{
    [TestFixture]
    public class ShipTests
    {
        [Test]
        public void CannotHitSunkShip()
        {
            IShip ship = ShipsFactory.CreateDestroyerShip();

            for (int i = 0; i < ship.Size; i++)
            {
                ship.Shoot();
            }

            Assert.Throws<InvalidOperationException>(
                () => ship.Shoot(),
                "It shouldn't be possible to hit already sunk ship.");
        }

        [Test]
        public void WhenShipIsHitThenShipHitsCounterIsIncreased()
        {
            IShip ship = ShipsFactory.CreateDestroyerShip();
            int startingHits = ship.Hits;

            ship.Shoot();

            Assert.AreEqual(startingHits + 1, ship.Hits, "Shooting to ship should increase hits number");
        }
    }
}
