using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inertia3_WF
{
    class Game
    {
        public bool win = false;
        public bool gameStatus = true;

        public void Win()
        {
            gameStatus = false;
            win = true;
            return;
        }

        public void Lose()
        {
            gameStatus = false;
            return;
        }

        public bool GameIsEnd()
        {
            if (gameStatus == false)
                return true;
            return false;
        }

        public bool WinOrLose()
        {
            return win || gameStatus;
        }
    }
}
