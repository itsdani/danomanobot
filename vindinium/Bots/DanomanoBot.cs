using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vindinium.SmartBoard;
using Vindinium.SmartBoard.Pathfinder;

namespace Vindinium.Bots
{
    public class DanomanoBot : Bot
    {
        private IPathFinder PathFinder { get; set; }

        public DanomanoBot(ServerStuff serverStuff) : base(serverStuff)
        {
            PathFinder = new BreadthFirstSearch();
        }

        protected override void Play()
        {
            while (GameState.IsFinished == false && ServerStuff.Errored == false)
            {
                var myHeroTile = Board.GetTile(GameState.MyHero.pos);
                var destinationTile = Board.GetTile(new Position(6, 6));
                Console.WriteLine("Want to go: {0} --> {1}", myHeroTile, destinationTile);
                string direction = PathFinder.GetDirection(myHeroTile, destinationTile, Board);
                Console.WriteLine("Moving in direction: {0}", direction);
                ServerStuff.MoveHero(direction);
                Console.Out.WriteLine("completed turn " + ServerStuff.GameState.CurrentTurn);
            }
            #region commented not-random
            //Random random = new Random();
            //while (GameState.IsFinished == false && ServerStuff.Errored == false)
            //{
            //    Position myPosition = GameState.MyHero.pos;
            //    Tile eastTile = Board.GetTile(new Position(myPosition.x, myPosition.y+1));
            //    Tile northTile = Board.GetTile(new Position(myPosition.x+1, myPosition.y));
            //    Tile westTile = Board.GetTile(new Position(myPosition.x, myPosition.y-1));
            //    Tile southTile = Board.GetTile(new Position(myPosition.x-1, myPosition.y));
            //    if (eastTile != null)
            //    {
            //        ServerStuff.MoveHero(Direction.East);
            //    }
            //    else if (northTile != null)
            //    {
            //        ServerStuff.MoveHero(Direction.North);
            //    }
            //    else if (westTile != null)
            //    {
            //        ServerStuff.MoveHero(Direction.West);
            //    }
            //    else if (southTile != null)
            //    {
            //        ServerStuff.MoveHero(Direction.South);
            //    }
            //    else
            //    {
            //        ServerStuff.MoveHero(Direction.Stay);
            //    }
            //    Console.Out.WriteLine("completed turn " + ServerStuff.GameState.CurrentTurn);
            //}
            #endregion

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
                        file.Write("{0}, ", item);
                    }
                    file.WriteLine();
                }
            }
        }
    }
}
