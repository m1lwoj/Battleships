using Battleships.Core.Game.Ships;
using Battleships.Core.Ships.Models;
using System.Collections.Generic;

namespace Battleships.Core.Game.Boards.Fields.Extensions
{
    public static class FieldExtension
    {
        internal static IEnumerable<IField> GetFieldsForShip(this IField[,] fields, ShipLocation shipLocation)
        {
            int changingStartingIndex = shipLocation.Direction == Direction.Horizontal ? shipLocation.Column : shipLocation.Row;

            var fieldList = new List<IField>(shipLocation.ShipSize);

            for (int i = changingStartingIndex; i <= changingStartingIndex + shipLocation.ShipSize - 1; i++)
            {
                int row = shipLocation.Direction == Direction.Horizontal ? shipLocation.Row : i;
                int column = shipLocation.Direction == Direction.Horizontal ? i : shipLocation.Column;

                fieldList.Add(fields[row, column]);
            }

            return fieldList;
        }

        internal static void PopulateFields(this IField[,] fields)
        {
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field();
                }
            }
        }
    }
}
