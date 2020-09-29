using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Core.Ships
{
    public class ShipsFactory
    {
        public static IShip CreateDestroyerShip()
        {
            return new DestroyerShip();
        }
    }
}
