using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingCards
{
    internal class Judge
    {
        public static Role GetRole(Card[] hand)
        {

        }

        /// <summary>
        /// ロイヤルストレートフラッシュの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <param name="suitCount">手札にある各スートのカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsRoyalFlush(int[] numCount, int[] suitCount)
        {
            // マークが全て同じでなければfalse
            if (suitCount[(int)Suit.Spade] != 5 && suitCount[(int)Suit.Heart] != 5 &
                suitCount[(int)Suit.Diamond] != 5 & suitCount[(int)Suit.Club] != 5)
            {
                return false;
            }

            // 数字が「１０」、「１１」、「１２」、「１３」、「１」でなければfalse
            if (numCount[10] != 1 || numCount[11] != 1 || numCount[12] != 1 || numCount[13] != 1 || numCount[1] != 1)
            {
                return false;
            }

            return true;
        }

        public static bool StraightFlush(int[] numCount, int[] suitCount)
        {

        }
    }
}
