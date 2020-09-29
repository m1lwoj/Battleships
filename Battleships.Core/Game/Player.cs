using Battleships.Core.Board;
using Battleships.Core.Ships;
using System.Collections.Generic;

namespace Battleships.Core.Game
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public GameBoard OwnBoard { get; } = new GameBoard(GameSettings.Default());
        public GameBoard OpponentsBoard { get; } = new GameBoard(GameSettings.Default());
        public string Name { get; }

        public List<IShip> _ships = new List<IShip>();

        internal void PlaceShip(ShipLocation shipLocation)
        {
            _ships.Add(shipLocation.Ship);
            OwnBoard.SetShip(shipLocation);
        }

        internal bool Shoot(Coordinate coordinate)
        {
            return OwnBoard.Shoot(coordinate.Row, coordinate.Column);
        }
    }
}
