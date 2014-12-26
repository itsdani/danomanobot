using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium.SmartBoard.Pathfinder
{
    class RandomSearch : IPathFinder
    {
        private readonly Random _random = new Random();


        public string GetDirection(Tile starTile, Tile destinationTile, Board board)
        {
            
            switch(_random.Next(0, 5))
            {
                case 0:
                    return Direction.East;
                    break;
                case 1:
                    return Direction.North;
                    break;
                case 2:
                    return Direction.South;
                    break;
                case 3:
                    return Direction.West;
                    break;
                default:
                    return Direction.Stay;
                    break;
            }
        }
    }
}
