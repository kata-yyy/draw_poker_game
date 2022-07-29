using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PlayingCards
{
    internal class Area2 : Area
    {
        public static Point nameLocation = new Point(550, 13);
        public static Point holdChipLocation = new Point(505, 53);
        public static Point betChipLocation = new Point(505, 230);
        public static Point actionMessageLocation = new Point(550, 260);
        public static Point handLocation = new Point(390, 110);
        public static Size cardSize = new Size(80, 100);
        public static int cardGap = 5;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">エリアを所有するプレイヤーの名前</param>
        /// <param name="myPlayer">エリアを所有するプレイヤー</param>
        public Area2(Character myPlayer) : base(myPlayer)
        {
            Name.Location = nameLocation;
            HoldChip.Location = holdChipLocation;
            BetChip.Location = betChipLocation;
            ActionMessage.Location = actionMessageLocation;
            ActionMessage.ForeColor = Color.Blue;
            Hand[0].Location = handLocation;
            Hand[0].Size = cardSize;

            for (int i = 1; i < Hand.Count; i++)
            {
                Hand[i].Location = new Point(Hand[i - 1].Location.X + cardSize.Width + cardGap,
                                             Hand[i - 1].Location.Y);
                Hand[i].Size = cardSize;
            }
        }

        /// <summary>
        /// 左からｉ番目の手札を表示する（表側）
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandFrontDisplay(int i)
        {
            Hand[i].Image = Image.GetCardImageRotate180(MyCharacter.Hand[i]);
            Hand[i].Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を表示する（表側）
        /// </summary>
        public override void HandFrontDisplay()
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].Image = Image.GetCardImageRotate180(MyCharacter.Hand[i]);
                Hand[i].Visible = true;
                PokerForm.Instance.Refresh();
            }
        }

        /// <summary>
        /// 左からｉ番目の手札を表示する（裏側）
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandBackDisplay(int i)
        {
            Hand[i].Image = Image.cardBackRotate180;
            Hand[i].Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を表示する（裏側）
        /// </summary>
        public override void HandBackDisplay()
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].Image = Image.cardBackRotate180;
                Hand[i].Visible = true;
                PokerForm.Instance.Refresh();
            }
        }

        /// <summary>
        /// 左からｉ番目の手札を非表示にする
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandHide(int i)
        {
            Hand[i].Visible = false;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を非表示にする
        /// </summary>
        public override void HandHide()
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].Visible = false;
                PokerForm.Instance.Refresh();
            }
        }
    }
}
