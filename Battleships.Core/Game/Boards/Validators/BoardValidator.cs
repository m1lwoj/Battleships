using Battleships.Core.Game;
using Battleships.Core.Game.Boards.Fields;
using Battleships.Core.Game.Boards.Fields.Extensions;
using Battleships.Core.Game.Ships;
using Battleships.Core.Ships.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core.Board.Validators
{
    public class BoardValidator
    {
        internal bool CanPlaceShip(IField[,] board, ShipLocation shipLocation)
        {
            return board.GetFieldsForShip(shipLocation)
                .All(f => f.IsFree);
        }

        internal bool DoesShipFitOnBoard(IField[,] fields, ShipLocation shipLocation)
        {
            return shipLocation.Direction == Direction.Horizontal
                ? shipLocation.ShipSize + shipLocation.Column - 1 < fields.GetLength(1)
                : shipLocation.ShipSize + shipLocation.Row - 1 < fields.GetLength(0);
        }

        internal void ValidateCoordinates(IField[,] board, Coordinate coordinate)
        {
            bool doesCoordinateFitOnBoard = coordinate.Row >= 0 && coordinate.Row < board.GetLength(0) &&
                     coordinate.Column >= 0 && coordinate.Column < board.GetLength(1);

            if (!doesCoordinateFitOnBoard)
                throw new InvalidOperationException($"Invalid coordinates x: {coordinate.Row}, y: {coordinate.Column}.");
        }

        internal void ValidateFieldCanBeHit(Coordinate coordinate, IField field)
        {
            if (!field.CanBeHit)
                throw new InvalidOperationException($"Coordinates x: {coordinate.Row}, y: {coordinate.Column} has been already shot.");
        }

        internal void ValidateShipCanBePlacedOnBoard(IField[,] board, ShipLocation shipLocation)
        {
            if (!DoesShipFitOnBoard(board, shipLocation))
                throw new InvalidOperationException(
                    $"Cannot place ship x: {shipLocation.Row}, y: {shipLocation.Column}");
        }

        internal void ValidateShipCanBePlaceOnFields(IEnumerable<IField> fields)
        {
            if (fields.Any(field => !field.IsFree))
                throw new InvalidOperationException($"Cannot place ship, field is already occupied by another ship");
        }

        internal bool CanShipBePlaced(IField[,] board, ShipLocation shipLocation)
        {
            return board.GetFieldsForShip(shipLocation)
                .All(f => f.IsFree);
        }
    }
}
