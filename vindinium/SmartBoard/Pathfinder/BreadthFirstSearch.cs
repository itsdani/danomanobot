using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium.SmartBoard.Pathfinder
{
    class BreadthFirstSearch : IPathFinder
    {
        public BreadthFirstSearch()
        {
        }

        public string GetDirection(Tile starTile, Tile destinationTile, Board board)
        {
            throw new NotImplementedException("BreadthFirstSearch GetDirection is not implemented.");
        }
    }
}
