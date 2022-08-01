using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingCards
{
    enum Phase
    {
        GameStart,
        RoundStart,
        AllCheck,
        ChangeHandStart,
        ChangeHandEnd,
        AllFold,
        BattleStart,
        BattleEnd,
        AllGameOver,
        GameEnd,
        NextGame
    }
}
