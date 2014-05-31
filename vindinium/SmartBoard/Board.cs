using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vindinium;

namespace Vindinium.SmartBoard
{
    public class Board
    {
        private Dictionary<Position, Tile> _tiles;
        public Dictionary<Position, Tile> Tiles
        {
            get
            {
                if (_tiles == null)
                {
                    _tiles = new Dictionary<Position, Tile>();
                }
                return _tiles;
            }
            private set
            {
                _tiles = value;
            }
        }
        private List<List<TileType>> boardArray;
        public int Size { get; private set; }

        public Board(List<List<TileType>> boardArray)
        {
            this.boardArray = boardArray;
            Size = boardArray.Count;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Tile tile = new Tile(i, j, boardArray[i][j], Size * i + j);
                    if (tile.Type != TileType.IMPASSABLE_WOOD)
                    {
                        Tiles.Add(tile.Position, tile);
                    }
                }
            }

            foreach (Tile tile in Tiles.Values)
            {
                TryToAddNeighbours(tile);
            }
        }

        private void TryToAddNeighbours(Tile tile)
        {
            if (tile.IsPassable)
            {
                if (tile.XPos != 0)
                {
                    TryToAddNeighbourInDirection(tile, DirectionEnum.NORTH);
                }
                if (tile.XPos != this.Size - 1)
                {
                    TryToAddNeighbourInDirection(tile, DirectionEnum.SOUTH);
                }
                if (tile.YPos != 0)
                {
                    TryToAddNeighbourInDirection(tile, DirectionEnum.WEST);
                }
                if (tile.YPos != this.Size - 1)
                {
                    TryToAddNeighbourInDirection(tile, DirectionEnum.EAST);
                }
            }
        }

        private void TryToAddNeighbourInDirection(Tile tile, DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.STAY:
                    throw new InvalidOperationException();
                    break;
                case DirectionEnum.NORTH:
                    Tile possibleNorthNeighbour = GetTile(new Position(tile.XPos - 1, tile.YPos));
                    AddNeighbour(tile, possibleNorthNeighbour);
                    break;
                case DirectionEnum.EAST:
                    Tile possibleEastNeighbour = GetTile(new Position(tile.XPos, tile.YPos + 1));
                    AddNeighbour(tile, possibleEastNeighbour);
                    break;
                case DirectionEnum.SOUTH:
                    Tile possibleSouthNeighbour = GetTile(new Position(tile.XPos + 1, tile.YPos));
                    AddNeighbour(tile, possibleSouthNeighbour);
                    break;
                case DirectionEnum.WEST:
                    Tile possibleWestNeighbour = GetTile(new Position(tile.XPos, tile.YPos - 1));
                    AddNeighbour(tile, possibleWestNeighbour);
                    break;
                default:
                    break;
            }
        }

        private static void AddNeighbour(Tile tile, Tile possibleNeighbour)
        {
            if (possibleNeighbour != null && possibleNeighbour.Type != TileType.IMPASSABLE_WOOD)
            {
                tile.Neighbours.Add(possibleNeighbour);
            }
        }

        public Tile GetTile(Position position)
        {
            if (Tiles.ContainsKey(position))
            {
                return Tiles[position];
            }
            return null;
        }
        public Tile GetTile(int x, int y)
        {
            return GetTile(new Position(x, y));
        }
        public Tile GetTile(int id)
        {
            return Tiles.Values.SingleOrDefault(t => t.ID == id);
        }

        internal Board Refresh(List<List<TileType>> boardArray)
        {
            foreach (var mine in Tiles.Values.Where(t => t.IsGoldMine))
            {
                mine.Type = boardArray[mine.XPos][mine.YPos];
            }
            return this;
        }
    }
}
