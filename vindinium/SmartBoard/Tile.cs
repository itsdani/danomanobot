using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vindinium;

namespace Vindinium.SmartBoard
{
    public class Tile
    {
        public int ID { get; private set; }
        public Position Position {get; set;}
        public TileType Type { get; set; }

        private List<Tile> _neighbours;
        public List<Tile> Neighbours
        {
            get
            {
                if (_neighbours == null)
                {
                    _neighbours = new List<Tile>();
                }
                return _neighbours;
            }
            private set
            {
                _neighbours = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                if (this.Type == TileType.FREE ||
                    this.Type == TileType.HERO_1 ||
                    this.Type == TileType.HERO_2 ||
                    this.Type == TileType.HERO_3 ||
                    this.Type == TileType.HERO_4)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsGoldMine
        {
            get
            {
                if (this.Type == TileType.GOLD_MINE_NEUTRAL ||
                   this.Type == TileType.GOLD_MINE_1 ||
                   this.Type == TileType.GOLD_MINE_2 ||
                   this.Type == TileType.GOLD_MINE_3 ||
                   this.Type == TileType.GOLD_MINE_4)
                {
                    return true;
                }
                return false;
            }
        }

        public int XPos 
        {
            get 
            {
                return Position.x;
            }
            set 
            {
                Position.x = value;
            }
        }
        public int YPos 
        {
            get 
            {
                return Position.y;
            }
            set 
            {
                Position.y = value;
            }
        }

        public Tile(int x, int y, TileType tileType, int id)
        {
            this.ID = id;
            this.Type = tileType;
            Position = new Position(x, y);
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            Tile other = obj as Tile;
            if (other == null)
            {
                return false;
            }
            if (other.ID == this.ID && other.Position.Equals(this.Position) && this.Type == other.Type)
            {
                return true;
            }
            return false;
        }
    }
}
