using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlayingCards
{
    abstract class Character
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所持しているチップ
        /// </summary>
        public int HoldChip { get; set; }
        /// <summary>
        /// 場に出したチップ
        /// </summary>
        public int BetChip { get; set; }
        /// <summary>
        /// 手札
        /// </summary>
        public Card[] Hand { get; set; } = new Card[5];
        /// <summary>
        /// true：交換する、　false：交換しない
        /// </summary>
        public bool[] IsChangeHand { get; set; } = new bool[5];
        /// <summary>
        /// できた役
        /// </summary>
        public Role MyRole { get; set; }
        /// <summary>
        /// 次のキャラクター
        /// </summary>
        public Character NextCharacter { get; set; }
        /// <summary>
        /// 現在のステータス
        /// </summary>
        public Status MyStatus { get; set; }
        /// <summary>
        /// 自身が所有するエリア
        /// </summary>
        public Area MyArea { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public Character(string name)
        {
            Name = name;
            HoldChip = PokerMain.startChip;
            BetChip = 0;
            IsChangeHand = new bool[5] { false, false, false, false, false };
            MyStatus = Status.Default;
        }

        /// <summary>
        /// 参加費を払う
        /// </summary>
        public void EntryChip()
        {
            BetChip += PokerMain.entryChip;
            MyArea.BetChipDisplay();
            HoldChip -= PokerMain.entryChip;
            MyArea.HoldChipDisplay();
        }

        /// <summary>
        /// チェックを選択した時の処理
        /// </summary>
        public void Check()
        {
            MyArea.ActionMessageDisplay(Action.Check);

            MyStatus = Status.Check;

            PokerMain.TurnEnd();
        }

        /// <summary>
        /// ベットを選択した時の処理
        /// </summary>
        /// <param name="betChip">ベットするチップ数</param>
        public void Bet(int betChip)
        {
            MyArea.ActionMessageDisplay(Action.Bet, betChip);
            BetChip += betChip;
            MyArea.BetChipDisplay();
            HoldChip -= betChip;
            MyArea.HoldChipDisplay();

            PokerMain.callChip = BetChip;
            MyStatus = Status.Raise;

            PokerMain.TurnEnd();
        }

        /// <summary>
        /// コールを選択した時の処理
        /// </summary>
        public void Call()
        {
            MyArea.ActionMessageDisplay(Action.Call);

            // コールに必要なチップ数が自身の全チップ数より多い場合、全額をベットする
            if (PokerMain.callChip > BetChip + HoldChip)
            {
                BetChip += HoldChip;
                MyArea.BetChipDisplay();
                HoldChip = 0;
                MyArea.HoldChipDisplay();
            }
            else
            {
                HoldChip -= PokerMain.callChip - BetChip;
                MyArea.HoldChipDisplay();
                BetChip = PokerMain.callChip;
                MyArea.BetChipDisplay();
            }

            MyStatus = Status.Call;

            PokerMain.TurnEnd();
        }

        /// <summary>
        /// レイズを選択した時の処理
        /// </summary>
        /// <param name="betChip">レイズするチップ数</param>
        public void Raise(int raiseChip)
        {
            MyArea.ActionMessageDisplay(Action.Raise, raiseChip);
            HoldChip -= PokerMain.callChip - BetChip + raiseChip;
            MyArea.HoldChipDisplay();
            BetChip = PokerMain.callChip + raiseChip;
            MyArea.BetChipDisplay();

            PokerMain.callChip = BetChip;
            MyStatus = Status.Raise;

            PokerMain.TurnEnd();
        }

        /// <summary>
        /// フォールドを選択した時の処理
        /// </summary>
        public void Fold()
        {
            MyArea.ActionMessageDisplay(Action.Fold);
            MyArea.HandFrontDisplay();

            MyStatus = Status.Fold;

            PokerMain.TurnEnd();
        }

        /// <summary>
        /// カード交換
        /// </summary>
        public void ChangeHand()
        {
            // アクションメッセージの表示
            int changeHandCount = 0;
            for (int i = 0; i < Hand.Length; i++)
            {
                if (IsChangeHand[i])
                {
                    changeHandCount++;
                }
            }
            MyArea.ChangeMessageDisplay(changeHandCount);

            // カードの破棄
            for (int i = 0; i < Hand.Length; i++)
            {
                if (IsChangeHand[i])
                {
                    Thread.Sleep(300);
                    MyArea.HandHide(i);
                    Dealer.Discard(Hand[i]);
                }
            }

            // カードの追加
            for (int i = 0; i < Hand.Length; i++)
            {
                if (IsChangeHand[i])
                {
                    Thread.Sleep(300);
                    Hand[i] = Dealer.DealCard();
                    HandDisplay(i);
                }
                IsChangeHand[i] = false;
            }

            MyStatus = Status.ChangeHand;

            PokerMain.ChangeHandEnd();
        }

        abstract public void HandDisplay();
        abstract public void HandDisplay(int i);
        abstract public void ChangeHandSelect();
        abstract public void ActionSelectBeforeBet();
        abstract public void ActionSelectAfterBet();
    }
}
