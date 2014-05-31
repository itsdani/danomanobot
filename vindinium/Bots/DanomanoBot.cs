using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vindinium.SmartBoard;

namespace Vindinium.Bots
{
    public class DanomanoBot : Bot
    {
        public DanomanoBot(ServerStuff serverStuff) : base(serverStuff)
        {
        }

        protected override void Play()
        {
            Random random = new Random();
            while (GameState.IsFinished == false && ServerStuff.Errored == false)
            {
                Position myPosition = GameState.MyHero.pos;
                Tile eastTile = Board.GetTile(new Position(myPosition.x, myPosition.y+1));
                Tile northTile = Board.GetTile(new Position(myPosition.x+1, myPosition.y));
                Tile westTile = Board.GetTile(new Position(myPosition.x, myPosition.y-1));
                Tile southTile = Board.GetTile(new Position(myPosition.x-1, myPosition.y));


                if (eastTile != null)
                {
                    ServerStuff.MoveHero(Direction.East);
                }
                else if (northTile != null)
                {
                    ServerStuff.MoveHero(Direction.North);
                }
                else if (westTile != null)
                {
                    ServerStuff.MoveHero(Direction.West);
                }
                else if (southTile != null)
                {
                    ServerStuff.MoveHero(Direction.South);
                }
                else
                {
                    ServerStuff.MoveHero(Direction.Stay);
                }

                Console.Out.WriteLine("completed turn " + ServerStuff.GameState.CurrentTurn);
            }

            if (ServerStuff.Errored)
            {
                Console.Out.WriteLine("error: " + ServerStuff.ErrorText);
            }

            Console.Out.WriteLine("danomano bot finished");
        }

        private void WriteBoardToFile()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\Users\dani\testfolder\vindinium.txt", true))
            {
                file.WriteLine("-----------------------------Turn {0}/{1}-----------------------------", GameState.CurrentTurn, GameState.MaxTurns);
                foreach (var row in ServerStuff.BoardArray)
                {
                    foreach (var item in row)
                    {
                        file.Write("{0}, ",item);
                    }
                    file.WriteLine();
                }
            }
        }      
    }
}
