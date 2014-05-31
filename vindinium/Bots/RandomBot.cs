using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vindinium.Bots
{
    class RandomBot : Bot
    {
        private ServerStuff serverStuff;

        public RandomBot(ServerStuff serverStuff) : base(serverStuff)
        {
        }

        //starts everything
        protected override void Play()
        {
            Random random = new Random();
            while (serverStuff.GameState.IsFinished == false && serverStuff.Errored == false)
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

                Console.Out.WriteLine("completed turn " + serverStuff.GameState.CurrentTurn);
            }

            if (serverStuff.Errored)
            {
                Console.Out.WriteLine("error: " + serverStuff.ErrorText);
            }

            Console.Out.WriteLine("random bot finished");
        }
    }
}
