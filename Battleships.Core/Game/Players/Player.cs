using Battleships.Core.Game.Boards;
using Battleships.Core.Game.Players.Validators;
using Battleships.Core.Game.Results;
using Battleships.Core.Game.Ships;
using Battleships.Core.Ships.Models;
using System.Collections.Generic;

namespace Battleships.Core.Game.Players
{
    public class Player : IPlayer
    {
        readonly PlayerValidator _playerValidator;

        public Player(string name)
        {
            Name = name;
            _playerValidator = new PlayerValidator(
                GameSettings.Instance.NumberOfBattleShips,
                GameSettings.Instance.NumberOfDestroyerShips);
        }

        public IHiddenBoard HiddenBoard { get; } = new HiddenBoard();
        public IGameBoard OwnBoard { get; } = new GameBoard();
        public string Name { get; }

        public List<IShip> _ships = new List<IShip>();

        public void PlaceShip(ShipLocation shipLocation)
        {
            _playerValidator.ValidateNumberOfPlacedShips(shipLocation.Ship, _ships);

            _ships.Add(shipLocation.Ship);
            OwnBoard.SetShip(shipLocation);
        }

        public ShootResult Shoot(Coordinate coordinate)
        {
            var result = OwnBoard.Shoot(coordinate);
            MarkResultsOnOpponentsBoard(coordinate, result);

            if (result == ShootResult.Sunk && OwnBoard.AnyShipsLeftInBattle())
            {
                return ShootResult.Won;
            }

            return result;
        }

        private void MarkResultsOnOpponentsBoard(Coordinate coordinate, ShootResult result)
        {
            if (result == ShootResult.Missed)
            {
                HiddenBoard.MarkMissed(coordinate);
            }
            else
            {
                HiddenBoard.MarkHit(coordinate);
            }
        }
    }
}
