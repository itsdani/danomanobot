using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vindinium.Bots
{
    public class DanomanoBot : Bot
    {


        protected override void Play()
        {
            Random random = new Random();
            while (GameState.IsFinished == false && ServerStuff.Errored == false)
            {
                Position myPosition = GameState.MyHero.pos;
                Position eastPosition = new Position(myPosition.x, myPosition.y+1);
                Position northPosition = new Position(myPosition.x+1, myPosition.y);
                Position westPosition = new Position(myPosition.x, myPosition.y-1);
                Position southPosition = new Position(myPosition.x-1, myPosition.y);
                Console.WriteLine("My pos: ({0}, {1})", myPosition.x, myPosition.y);
                WriteGamestateToFile();
                Console.WriteLine("East ({0}, {1})", eastPosition.x, eastPosition.y);
                Console.WriteLine("North ({0}, {1})", northPosition.x, northPosition.y);
                Console.WriteLine("West ({0}, {1})", westPosition.x, westPosition.y);
                Console.WriteLine("South ({0}, {1})", southPosition.x, southPosition.y);

                if (Board.GetTile(eastPosition) != null)
                {
                    Console.WriteLine("Going east! Destination pos: ({0}, {1}), destination type: {2}",
                        eastPosition.x, eastPosition.y, Board.GetTile(eastPosition).Type);
                    ServerStuff.MoveHero(Direction.East);
                }
                else if (Board.GetTile(northPosition) != null)
                {
                    ServerStuff.MoveHero(Direction.North);
                    Console.WriteLine("Going north! Destination pos: ({0}, {1}), destination type: {2}",
                        northPosition.x, northPosition.y, Board.GetTile(northPosition).Type);
                }
                else if (Board.GetTile(westPosition) != null)
                {
                    ServerStuff.MoveHero(Direction.West);
                    Console.WriteLine("Going west! Destination pos: ({0}, {1}), destination type: {2}",
                        westPosition.x, westPosition.y, Board.GetTile(westPosition).Type);
                }
                else if (Board.GetTile(southPosition) != null)
                {
                    ServerStuff.MoveHero(Direction.South);
                    Console.WriteLine("Going south! Destination pos: ({0}, {1}), destination type: {2}",
                        southPosition.x, southPosition.y, Board.GetTile(southPosition).Type);
                }
                else
                {
                    ServerStuff.MoveHero(Direction.Stay);
                    Console.WriteLine("staying!");
                }

                Console.Out.WriteLine("completed turn " + ServerStuff.GameState.CurrentTurn);
            }

            if (ServerStuff.Errored)
            {
                Console.Out.WriteLine("error: " + ServerStuff.ErrorText);
            }

            Console.Out.WriteLine("danomano bot finished");
        }

        private void WriteGamestateToFile()
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

        public DanomanoBot(ServerStuff serverStuff)
            : base(serverStuff)
        {
        }
    }
}
