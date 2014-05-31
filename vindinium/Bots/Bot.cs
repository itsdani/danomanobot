using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vindinium.SmartBoard;

namespace Vindinium.Bots
{
    public abstract class Bot
    {
        protected ServerStuff ServerStuff;

        protected GameState GameState
        {
            get
            {
                return ServerStuff.GameState;
            }
        }
        protected Board Board
        {
            get
            {
                return ServerStuff.GameState.Board;
            }
        }

        public Bot(ServerStuff serverStuff)
        {
            this.ServerStuff = serverStuff;
        }

        //starts everything
        public void Run()
        {
            Console.Out.WriteLine("bot running");

            ServerStuff.CreateGame();

            if (ServerStuff.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                new Thread(delegate()
                {
                    System.Diagnostics.Process.Start(ServerStuff.ViewURL);
                }).Start();
            }

            Play();
        }

        protected abstract void Play();
    }
}
