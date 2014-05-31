using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium
{
    [DataContract]
    public class GameResponse
    {
        [DataMember]
        internal Game game;

        [DataMember]
        internal Hero hero;

        [DataMember]
        internal string token;

        [DataMember]
        internal string viewUrl;

        [DataMember]
        internal string playUrl;
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        internal string id;

        [DataMember]
        internal int turn;

        [DataMember]
        internal int maxTurns;

        [DataMember]
        internal List<Hero> heroes;

        [DataMember]
        internal BasicBoard board;

        [DataMember]
        internal bool finished;
    }

    [DataContract]
    public class Hero
    {
        [DataMember]
        internal int id;

        [DataMember]
        internal string name;

        [DataMember]
        internal int elo;

        [DataMember]
        internal Position pos;

        [DataMember]
        internal int life;

        [DataMember]
        internal int gold;

        [DataMember]
        internal int mineCount;

        [DataMember]
        internal Position spawnPos;

        [DataMember]
        internal bool crashed;
    }

    [DataContract]
    public class Position
    {
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        [DataMember]
        internal int x;

        [DataMember]
        internal int y;

        public override int GetHashCode()
        {
            return 127*(13+x)+y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                Position pos = (Position)obj;
                if (pos.x==this.x && pos.y == this.y)
                {
                    return true;
                }
            }
            return false;
        }
    }

    [DataContract]
    public class BasicBoard
    {
        [DataMember]
        internal int size;

        [DataMember]
        internal string tiles;
    }
}
