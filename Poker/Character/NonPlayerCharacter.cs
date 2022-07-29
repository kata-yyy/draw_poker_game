using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlayingCards
{
    internal class NonPlayerCharacter : Character
    {
        Random random = new Random();

        public NonPlayerCharacter(string name) : base(name)
        {
        }

        /// <summary>
        /// 手札を表示する（裏側）
        /// </summary>
        public override void HandDisplay()
        {
            MyArea.HandBackDisplay();
        }

        /// <summary>
        /// 左からｉ番目の手札を表示する（裏側）
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandDisplay(int i)
        {
            MyArea.HandBackDisplay(i);
        }

        /// <summary>
        /// 交換するカードを選ぶ
        /// </summary>
        public override void ChangeHandSelect()
        {
            Thread.Sleep(500);
            IsChangeHand[1] = true;
            IsChangeHand[2] = true;
            IsChangeHand[3] = true;
        }

        /// <summary>
        /// カード交換前、誰もベットしていない時の行動選択
        /// </summary>
        public override void ActionSelectBeforeBet()
        {
            Thread.Sleep(500);

            int randomNum = random.Next(2);
            if (randomNum == 0)
            {
                Bet(1);
            }
            else
            {
                Bet(1);
            }
        }

        /// <summary>
        /// カード交換前、誰かがベットした後の行動選択
        /// </summary>
        public override void ActionSelectAfterBet()
        {
            Thread.Sleep(500);

            int randomNum = random.Next(3);
            if (randomNum == 0)
            {
                Call();
            }
            else if (randomNum == 1)
            {
                Raise(1);
            }
            else
            {
                Call();
            }
        }
    }
}
