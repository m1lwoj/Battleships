using Battleships.Core.Board.Fields;
using Battleships.Core.Game;
using Battleships.Core.Ships;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core.Board
{
    public class GameBoard : IBoard
    {
        public Field[,] Board { get; private set; }

        public GameBoard(IGameSettings gameSettings)
        {
            Board = new Field[gameSettings.Width, gameSettings.Height];

            PopulateBoardFields();
        }

        public ShootResult Shoot(int x, int y)
        {
            if (!AreCoordinatesValid(x, y)) throw new Exception($"Invalid coordinates x: {x}, y: {y}.");

            var field = Board[x, y];
            if (!field.CanBeShot) throw new InvalidOperationException($"Coordinates x: {x}, y: {y} has been already shot.");

            return field.Shoot();
        }

        public void SetShip(ShipLocation shipLocation)
        {
            if (!AreCoordinatesValid(shipLocation.Coordinate.Row, shipLocation.Coordinate.Column))
                throw new InvalidOperationException(
                    $"Invalid coordinates row: {shipLocation.Coordinate.Row}, column: {shipLocation.Coordinate.Column}");

            if (!DoesShipFitOnBoard(shipLocation.Ship.Size, shipLocation.Coordinate.Row, shipLocation.Coordinate.Column, shipLocation.Direction))
                throw new InvalidOperationException(
                    $"Cannot place ship x: {shipLocation.Coordinate.Row}, y: {shipLocation.Coordinate.Column}");

            var fields = GetFieldsForShip(shipLocation.Ship.Size, shipLocation.Coordinate.Row, shipLocation.Coordinate.Column, shipLocation.Direction);
            SetShipOnFields(fields, shipLocation.Ship);
        }

        internal bool AnyShipsLeftOnTheBattlefield()
        {
            return Board.Cast<Field>().All(f => f.Ship == null || f.Ship.IsSunk);
        }

        private void PopulateBoardFields()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Board[i, j] = new Field();
                }
            }
        }

        private void SetShipOnFields(IEnumerable<Field> fields, IShip ship)
        {
            if (fields.Any(field => !field.IsFree)) throw new InvalidOperationException($"Cannot place ship");

            foreach (var field in fields)
            {
                field.SetShip(ship);
            }
        }

        private IEnumerable<Field> GetFieldsForShip(int size, int x, int y, Direction direction)
        {
            if (direction == Direction.Horizontal)
            {
                var fields = new List<Field>(x + size);
                for (int i = y; i <= y + size - 1; i++)
                {
                    fields.Add(Board[x, i]);
                }

                return fields;
            }
            else
            {
                var fields = new List<Field>(y + size);
                for (int i = x; i <= x + size - 1; i++)
                {
                    fields.Add(Board[i, y]);
                }

                return fields;
            }
        }

        private bool DoesShipFitOnBoard(int size, int x, int y, Direction direction)
        {
            if (direction == Direction.Horizontal)
            {
                return size + y - 1 < Board.GetLength(0);
            }
            else
            {
                return size + x - 1 < Board.GetLength(0);
            }
        }

        private bool AreCoordinatesValid(int x, int y)
        {
            return x >= 0 && x < Board.GetLength(0) &&
                   y >= 0 && y < Board.GetLength(1);
        }
    }
}
