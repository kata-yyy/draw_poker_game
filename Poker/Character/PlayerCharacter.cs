using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlayingCards
{
    internal class PlayerCharacter : Character
    {
        /// <summary>
        /// プレイヤーが操作するためのコントローラー
        /// </summary>
        public Controller MyController { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">プレイヤーの名前</param>
        public PlayerCharacter(string name) : base(name)
        {
        }

        /// <summary>
        /// 手札を表示する（表側）
        /// </summary>
        public override void HandDisplay()
        {
            MyArea.HandFrontDisplay();
        }

        /// <summary>
        /// 左からｉ番目の手札を表示する
        /// </summary>
        /// <param name="i">手札の添え字</param>
        public override void HandDisplay(int i)
        {
            MyArea.HandFrontDisplay(i);
        }

        /// <summary>
        /// 交換するカードを選ぶ
        /// </summary>
        public override void ChangeHandSelect()
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                MyController.ChangeButtons[i].Enabled = true;
            }

            MyController.CardChangeButton.Enabled = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// カード交換前、誰もベットしていない時の行動選択
        /// </summary>
        public override void ActionSelectBeforeBet()
        {
            MyController.PlusButton.Enabled = true;
            MyController.MinusButton.Enabled = true;
            MyController.BetButton.Enabled = true;
            MyController.CheckButton.Enabled = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// カード交換前、誰かがベットした後の行動選択
        /// </summary>
        public override void ActionSelectAfterBet()
        {
            MyController.PlusButton.Enabled = true;
            MyController.MinusButton.Enabled = true;
            MyController.CallButton.Enabled = true;
            MyController.RaiseButton.Enabled = true;
            MyController.FoldButton.Enabled = true;
            PokerForm.Instance.Refresh();
        }
    }
}
