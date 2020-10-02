using Battleships.Core.Game;
using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;
using System.Linq;

namespace Battleships.Tests.Games.Ships
{
    [TestFixture]
    public class ShipLocationGeneratorTests
    {
        private const int numberOfShips = 4;
        private ShipLocationGenerator _shipLocationGenerator;

        [OneTimeSetUp]
        public void SetUp()
        {
            GameSettings.Instance.SetNumberOfShips(numberOfShips, numberOfShips);
            _shipLocationGenerator = new ShipLocationGenerator();
        }

        [Test]
        public void CanGenerateRandomShipLocations()
        {
            var randomShipLocations = _shipLocationGenerator.GenerateRandomShipLocations();

            Assert.AreEqual(
                numberOfShips,
                randomShipLocations.Count(s => s.ShipType == ShipType.Battle),
                "There should be generated the same number of ships as settings limit");
            Assert.AreEqual(
                numberOfShips,
                randomShipLocations.Count(s => s.ShipType == ShipType.Destroyer),
                "There should be generated the same number of ships as settings limit");
        }
    }
}
