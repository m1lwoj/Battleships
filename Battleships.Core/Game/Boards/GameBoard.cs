using Battleships.Core.Board.Validators;
using Battleships.Core.Game.Boards.Fields;
using Battleships.Core.Game.Boards.Fields.Extensions;
using Battleships.Core.Game.Results;
using Battleships.Core.Game.Ships;
using Battleships.Core.Ships.Models;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core.Game.Boards
{
    public class GameBoard : IGameBoard
    {
        private readonly BoardValidator _validator = new BoardValidator();
        public IField[,] Board { get; private set; }

        public GameBoard()
        {
            Board = new IField[GameSettings.Instance.Width, GameSettings.Instance.Height];
            Board.PopulateFields();
        }

        public ShootResult Shoot(Coordinate coordinate)
        {
            _validator.ValidateCoordinates(Board, coordinate);

            var field = Board[coordinate.Row, coordinate.Column];

            _validator.ValidateFieldCanBeHit(coordinate, field);

            return field.Shoot();
        }

        public void SetShip(ShipLocation shipLocation)
        {
            _validator.ValidateCoordinates(Board, shipLocation.Coordinate);
            _validator.ValidateShipCanBePlacedOnBoard(Board, shipLocation);

            var fields = Board.GetFieldsForShip(shipLocation);
            SetShipOnFields(fields, shipLocation.Ship);
        }

        public bool AnyShipsLeftInBattle()
        {
            return Board.Cast<IField>().All(f => f.Ship == null || f.Ship.IsSunk);
        }

        private void SetShipOnFields(IEnumerable<IField> fields, IShip ship)
        {
            _validator.ValidateShipCanBePlaceOnFields(fields);

            foreach (var field in fields)
            {
                field.SetShip(ship);
            }
        }
    }
}
