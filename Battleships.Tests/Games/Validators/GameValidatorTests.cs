using Battleships.Core.Game.Players;
using Battleships.Core.Game.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Battleships.Tests.Games.Validators
{
    [TestFixture]
    public class GameValidatorTests
    {
        private const string Player1 = "Player1";
        private const string Player2 = "Player2";
        private GameValidator _gameValidator;
        private Dictionary<string, IPlayer> _players;

        [OneTimeSetUp]
        public void SetUp()
        {
            _gameValidator = new GameValidator(2);
            _players = new Dictionary<string, IPlayer>()
            {
                { Player1, new Player(Player1) },
                { Player2, new Player(Player2) },
            };
        }

        [Test]
        public void WhenShooterIsNotRegisteredPlayerThenError()
        {
            Assert.Throws<InvalidOperationException>(
                () => _gameValidator.ValidatePlayerIsRegistered(_players, "Player90"),
                "Not registered player shouldn't be able to shoot");
        }

        [Test]
        public void WhenShooterIsRegisteredPlayerThenOk()
        {
            Assert.DoesNotThrow(
                () => _gameValidator.ValidatePlayerIsRegistered(_players, Player1),
                "Registered player should be able to shoot");
        }

        [Test]
        public void CannotAddMorePlayersThanInSettings()
        {
            Assert.Throws<InvalidOperationException>(
                () => _gameValidator.ValidateAddingPlayer(_players, new Player("Player3")),
               "It shouldn't be possible to more players than in settings");
        }

        [Test]
        public void CannotAddTwoPlayersWithTheSameName()
        {
            Assert.Throws<InvalidOperationException>(
                 () => _gameValidator.ValidateAddingPlayer(_players, new Player("Player3")),
                "It shouldn't be possible to add multiple players with the same name");
        }

        [Test]
        public void PlayerIsAllowedToShoot()
        {
            Assert.DoesNotThrow(
                () => _gameValidator.ValidateShoot(_players, Player1, Player1),
                "Player should be allowed to shoot");
        }
    }
}
