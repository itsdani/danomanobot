using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium.SmartBoard.Pathfinder
{
    class BreadthFirstSearch : IPathFinder
    {
        private class OpenSetData
        {
            private HashSet<TileData> openSet = new HashSet<TileData>();

            public bool IsEmpty
            {
                get { return openSet.Count == 0; }
            }

            public TileData MinCostTile
            {
                get { return openSet.First(t => t.FullCost == openSet.Min(m => m.FullCost)); }
            }


            public void Clear()
            {
                openSet.Clear();
            }

            public void Add(TileData tileData)
            {
                if (openSet.Contains(tileData))
                {
                    return;
                }
                openSet.Add(tileData);
            }

            public void Remove(TileData tileData)
            {
                if (openSet.Contains(tileData))
                {
                    openSet.Remove(tileData);
                }
            }

            public TileData GetTileData(Tile tile)
            {
                if (openSet.Any(t => t.Tile.Equals(tile)))
                {
                    return openSet.Single(t => t.Tile.Equals(tile));
                }
                return null;
            }

            public bool Contains(Tile tile)
            {
                if (openSet.Any(t => t.Tile.Equals(tile)))
                {
                    return true;
                }
                return false;
            }
        }

        private class TileData
        {
            public Tile Tile { get; set; }
            public TileData CameFrom { get; set; }
            public int PathCost { get; set; }
            public int HeuristicCost { get; set; }

            public int FullCost
            {
                get { return PathCost + HeuristicCost; }
            }
        }

        private OpenSetData openSet = new OpenSetData();
        private HashSet<Tile> closedSet = new HashSet<Tile>();

        public string GetDirection(Tile startTile, Tile destinationTile, Board board)
        {
            openSet.Clear();
            closedSet.Clear();

            openSet.Add(new TileData
                {
                    Tile = startTile, 
                    PathCost = 0, 
                    HeuristicCost = 0,
                    CameFrom = null
                });
            while (!openSet.IsEmpty)
            {
                TileData current = openSet.MinCostTile;
                if (current.Tile.Equals(destinationTile))
                {
                    List<Tile> path = ReconstructPath(current);
                    return GetDirectionOfTile(startTile, path.First());
                    //TODO : Rossz$$$$
                }

                openSet.Remove(current);
                closedSet.Add(current.Tile);

                foreach (Tile neighbour in current.Tile.Neighbours)
                {
                    if (closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    var tentativePathCost = current.PathCost + 1;


                    if (!openSet.Contains(neighbour))
                    {
                        TileData neighbourData = new TileData
                            {
                                Tile = neighbour,
                                PathCost = tentativePathCost,
                                HeuristicCost = 0,
                                CameFrom = current
                            };
                        openSet.Add(neighbourData);    
                    }
                    else if (tentativePathCost<openSet.GetTileData(neighbour).PathCost)
                    {
                        TileData neighbourData = openSet.GetTileData(neighbour);
                        neighbourData.CameFrom = current;
                        neighbourData.PathCost = tentativePathCost;
                        neighbourData.HeuristicCost = 0;
                    }
                }
            }
            return Direction.Stay;
        }

        private string GetDirectionOfTile(Tile startTile, Tile first)
        {
            if (startTile.XPos == first.XPos)
            {
                if (startTile.YPos == first.YPos + 1)
                {
                    return Direction.North;
                }
                if (startTile.YPos == first.YPos - 1)
                {
                    return Direction.South;
                }
            }
            else if (startTile.YPos == first.YPos)
            {
                if (startTile.XPos == first.XPos + 1)
                {
                    return Direction.East;
                }
                if (startTile.XPos == first.XPos - 1)
                {
                    return Direction.West;
                }
            }
            return Direction.Stay;
        }

        private List<Tile> ReconstructPath(TileData destination)
        {
            List<Tile> fullPath = new List<Tile>();
            TileData current = destination;
            
            fullPath.Add(current.Tile);
            while (current.CameFrom != null)
            {
                current = current.CameFrom;
                fullPath.Add(current.Tile);
            }

            return fullPath;

        }
    }
}
