using Battleships.Core.Game.Players.Validators;
using Battleships.Core.Ships;
using Battleships.Core.Ships.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Battleships.Tests.Games.Players.Validator
{
    [TestFixture]
    public class PlayerValidatorTests
    {
        private PlayerValidator _playerValidator;

        [OneTimeSetUp]
        public void SetUp()
        {
            _playerValidator = new PlayerValidator(3, 3);
        }

        [Test]
        public void CannotAddMoreShipsThanInSettings()
        {
            var ships = new List<IShip>()
            {
                ShipsFactory.CreateBattleShip(),
                ShipsFactory.CreateBattleShip(),
                ShipsFactory.CreateBattleShip()
            };

            Assert.Throws<InvalidOperationException>(
                () => _playerValidator.ValidateNumberOfPlacedShips(ShipsFactory.CreateBattleShip(), ships),
                "It shouldn't be possible to add more ships than in settings");
        }
    }
}
