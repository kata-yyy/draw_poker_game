using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PlayingCards
{
    internal class Area4 : Area
    {
        // 各コントロールのLocation
        public static Point nameLocation = new Point(1030, 350);
        public static Point holdChipLocation = new Point(985, 390);
        public static Point betChipLocation = new Point(650, 390);
        public static Point actionMessageLocation = new Point(700, 330);
        public static Point handLocation = new Point(870, 170);

        // カードのSize
        public static Size cardSize = new Size(100, 80);

        // カード間の隙間
        public static int cardGap = 5;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">エリアを所有するプレイヤーの名前</param>
        /// <param name="myPlayer">エリアを所有するプレイヤー</param>
        public Area4() : base()
        {

            NameLabel.Location = nameLocation;
            HoldChipLabel.Location = holdChipLocation;
            BetChipLabel.Location = betChipLocation;
            ActionMessageLabel.Location = actionMessageLocation;
            ActionMessageLabel.ForeColor = Color.Green;
            HandPictureBox[0].Location = handLocation;
            HandPictureBox[0].Size = cardSize;

            for (int i = 1; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Location = new Point(HandPictureBox[i - 1].Location.X, HandPictureBox[i - 1].Location.Y + cardSize.Height + cardGap);
                HandPictureBox[i].Size = cardSize;
                PokerForm.Instance.Refresh();
            }
        }

        /// <summary>
        /// 上からｉ番目の手札を表示する（表側）
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandFrontDisplay(int i)
        {
            HandPictureBox[i].Image = Image.GetCardImageRotate270(MyCharacter.Hand[i]);
            HandPictureBox[i].Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を表示する（表側）
        /// </summary>
        public override void HandFrontDisplay()
        {
            for (int i = 0; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Image = Image.GetCardImageRotate270(MyCharacter.Hand[i]);
                HandPictureBox[i].Visible = true;
                PokerForm.Instance.Refresh();
            }
        }

        /// <summary>
        /// 上からｉ番目の手札を表示する（裏側）
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandBackDisplay(int i)
        {
            HandPictureBox[i].Image = Image.cardBackRotate270;
            HandPictureBox[i].Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を表示する（裏側）
        /// </summary>
        public override void HandBackDisplay()
        {
            for (int i = 0; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Image = Image.cardBackRotate270;
                HandPictureBox[i].Visible = true;
                PokerForm.Instance.Refresh();
            }
        }

        /// <summary>
        /// 上からｉ番目の手札を非表示にする
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandHide(int i)
        {
            HandPictureBox[i].Visible = false;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全ての手札を非表示にする
        /// </summary>
        public override void HandHide()
        {
            for (int i = 0; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Visible = false;
                PokerForm.Instance.Refresh();
            }
        }
    }
}
