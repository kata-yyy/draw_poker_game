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
        public Role Role { get; set; }
        /// <summary>
        /// 前のキャラクター
        /// </summary>
        public Character PrevCharacter { get; set; }
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
            MyStatus = Status.Normal;
        }

        /// <summary>
        /// 参加費を払う
        /// </summary>
        public void EntryChip()
        {
            BetChip += PokerMain.entryChip;
            MyArea.BetChipDisplay(BetChip);
            HoldChip -= PokerMain.entryChip;
            MyArea.HoldChipDisplay(HoldChip);
        }

        /// <summary>
        /// チェックを選択した時の処理
        /// </summary>
        public void Check()
        {
            MyArea.ActionMessageDisplay(Action.Check);
            PokerMain.checkCount++;
            PokerMain.TurnEnd(this);
        }

        /// <summary>
        /// ベットを選択した時の処理
        /// </summary>
        /// <param name="betChip">ベットするチップ数</param>
        public void Bet(int betChip)
        {
            MyArea.ActionMessageDisplay(Action.Bet, betChip);
            BetChip += betChip;
            MyArea.BetChipDisplay(BetChip);
            HoldChip -= betChip;
            MyArea.HoldChipDisplay(HoldChip);
            PokerMain.betFlag = true;
            PokerMain.TurnEnd(this);
        }

        /// <summary>
        /// コールを選択した時の処理
        /// </summary>
        public void Call()
        {
            MyArea.ActionMessageDisplay(Action.Call);

            // 前のプレイヤーがベットしているチップ数が、自身の全チップ数より多い場合、全額をベットする
            if (PrevCharacter.BetChip > BetChip + HoldChip)
            {
                BetChip += HoldChip;
                MyArea.BetChipDisplay(BetChip);
                HoldChip = 0;
                MyArea.HoldChipDisplay(HoldChip);
            }
            else
            {
                HoldChip -= PrevCharacter.BetChip - BetChip;
                MyArea.HoldChipDisplay(HoldChip);
                BetChip = PrevCharacter.BetChip;
                MyArea.BetChipDisplay(BetChip);
            }

            PokerMain.TurnEnd(this);
        }

        /// <summary>
        /// レイズを選択した時の処理
        /// </summary>
        /// <param name="betChip">レイズするチップ数</param>
        public void Raise(int raiseChip)
        {
            MyArea.ActionMessageDisplay(Action.Raise, raiseChip);

            HoldChip -= PrevCharacter.BetChip - BetChip + raiseChip;
            MyArea.HoldChipDisplay(HoldChip);
            BetChip = PrevCharacter.BetChip + raiseChip;
            MyArea.BetChipDisplay(BetChip);
            PokerMain.raiseCharacter = this;
            PokerMain.TurnEnd(this);
        }

        /// <summary>
        /// フォールドを選択した時の処理
        /// </summary>
        public void Fold()
        {
            MyArea.ActionMessageDisplay(Action.Fold);
            MyStatus = Status.Fold;
            PrevCharacter.NextCharacter = NextCharacter;
            NextCharacter.PrevCharacter = PrevCharacter;
            MyArea.HandFrontDisplay();
            PokerMain.entryCharacter--;
            PokerMain.TurnEnd(this);
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
            MyArea.ActionMessageDisplay(Action.CardChange, changeHandCount);

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

            PokerMain.ChangeHandEnd(this);
        }

        abstract public void HandDisplay();
        abstract public void HandDisplay(int i);
        abstract public void ChangeHandSelect();
        abstract public void ActionSelectBeforeBet();
        abstract public void ActionSelectAfterBet();
    }
}
