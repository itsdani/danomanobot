using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium.SmartBoard.Pathfinder
{
    interface IPathFinder
    {
        string GetDirection(Tile startTile, Tile destinationTile, Board board);
    }
}
