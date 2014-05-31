using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vindinium.SmartBoard;

namespace Vindinium
{
    public class GameState
    {
        public Hero MyHero { get; private set; }
        public List<Hero> Heroes { get; private set; }

        public int CurrentTurn { get; private set; }
        public int MaxTurns { get; private set; }
        public bool IsFinished { get; private set; }

        public Board Board { get; private set; }

        public GameState(Hero myHero, List<Hero> heroes, int currentTurn, int maxTurns, bool isFinished, Board board)
        {
            // TODO: Complete member initialization
            this.MyHero = myHero;
            this.Heroes = heroes;
            this.CurrentTurn = currentTurn;
            this.MaxTurns = maxTurns;
            this.IsFinished = isFinished;
            this.Board = board;
        }

    }
}
