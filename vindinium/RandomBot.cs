using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vindinium
{
    class RandomBot
    {
        private ServerStuff serverStuff;

        public RandomBot(ServerStuff serverStuff)
        {
            this.serverStuff = serverStuff;
        }

        //starts everything
        public void Run()
        {
            Console.Out.WriteLine("random bot running");

            serverStuff.CreateGame();

            if (serverStuff.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                new Thread(delegate()
                {
                    System.Diagnostics.Process.Start(serverStuff.ViewURL);
                }).Start();
            }
            
            Random random = new Random();
            while (serverStuff.IsFinished == false && serverStuff.Errored == false)
            {
                switch(random.Next(0, 6))
                {
                    case 0:
                        serverStuff.MoveHero(Direction.East);
                        break;
                    case 1:
                        serverStuff.MoveHero(Direction.North);
                        break;
                    case 2:
                        serverStuff.MoveHero(Direction.South);
                        break;
                    case 3:
                        serverStuff.MoveHero(Direction.Stay);
                        break;
                    case 4:
                        serverStuff.MoveHero(Direction.West);
                        break;
                }

                Console.Out.WriteLine("completed turn " + serverStuff.CurrentTurn);
            }

            if (serverStuff.Errored)
            {
                Console.Out.WriteLine("error: " + serverStuff.ErrorText);
            }

            Console.Out.WriteLine("random bot finished");
        }
    }
}
