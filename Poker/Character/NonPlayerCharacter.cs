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
        /// <summary>
        /// NPCの行動決定に使用する乱数
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">NPCの名前</param>
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

            // フラグの初期化
            FlagSetAllHold();

            // 手札の各数字と各スートをカウントする
            int[] numCount = Judge.HandNumCount(Hand);
            int[] suitCount = Judge.HandSuitCount(Hand);

            // 手札５枚で役ができている場合、５枚とも残す
            if (IsHold5(numCount, suitCount))
            {
                ChangeHand();
                return;
            }

            // 手札４枚で役ができている場合、その４枚を残す
            if (IsHold4(numCount))
            {
                ChangeHand();
                return;
            }

            // 手札３枚で役ができている場合、その３枚を残す
            if (IsHold3(numCount))
            {
                ChangeHand();
                return;
            }

            // 手札２枚で役ができている場合、その２枚を残す
            if (IsHold2(numCount))
            {
                // 役以外の３枚に10以上のカードがある場合、そのカードも追加で残す
                FlagSetPlus1(10);

                ChangeHand();
                return;
            }

            // 役なしの場合、10以上のカードがあれば残し、なければすべて交換する
            FlagSetAllChange();
            FlagSetPlus1(10);

            ChangeHand();
        }

        /// <summary>
        /// フラッシュ、ストレート、フルハウスが成立しているかを判定する
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <param name="suitCount">手札にある各スートのカウント</param>
        /// <returns></returns>
        bool IsHold5(int[] numCount, int[] suitCount)
        {
            // フラッシュが成立しているか判定
            if (Judge.IsFlush(suitCount))
            {
                FlagSetAllHold();
                return true;
            }

            // ストレートが成立しているか判定
            if (Judge.IsStraight(numCount))
            {
                FlagSetAllHold();
                return true;
            }

            // フルハウスが成立しているか判定
            if (Judge.IsFullHouse(numCount))
            {
                FlagSetAllHold();
                return true;
            }

            return false;
        }

        /// <summary>
        /// フォーカード、ツーペアが成立しているかを判定する
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns></returns>
        bool IsHold4(int[] numCount)
        {
            // フォーカードが成立しているか判定
            if (Judge.IsFourOfKind(numCount))
            {
                FlagSetNumOfkind(numCount, 4);
                return true;
            }

            // ツーペアが成立しているか判定
            if (Judge.IsTwoPair(numCount))
            {
                FlagSetNumOfkind(numCount, 2);
                return true;
            }

            return false;
        }

        /// <summary>
        /// スリーカードが成立しているかを判定する
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns></returns>
        bool IsHold3(int[] numCount)
        {
            if (Judge.IsThreeOfKind(numCount))
            {
                FlagSetNumOfkind(numCount, 3);
                return true;
            }

            return false;
        }

        /// <summary>
        /// ワンペアが成立しているかを判定する
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns></returns>
        bool IsHold2(int[] numCount)
        {
            if (Judge.IsPair(numCount))
            {
                FlagSetNumOfkind(numCount, 2);
                return true;
            }

            return false;
        }

        /// <summary>
        /// ペア、スリーカード、フォーカードが成立している場合に、カード交換のフラグを設定する
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <param name="num">ペア：2、スリーカード：3、フォーカード：4</param>
        void FlagSetNumOfkind(int[] numCount, int num)
        {
            // フラグを全てtrueに
            FlagSetAllChange();

            // フォーカードを成立させている手札をfalseにする
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == num)
                {
                    for (int j = 0; j < Hand.Length; j++)
                    {
                        if (Hand[j].Number == i)
                        {
                            IsChangeHand[j] = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 手札交換しない場合に、カード交換のフラグを設定する
        /// </summary>
        void FlagSetAllHold()
        {
            for (int i = 0; i < IsChangeHand.Length; i++)
            {
                IsChangeHand[i] = false;
            }
        }

        /// <summary>
        /// 手札を全て交換する場合に、カード交換のフラグを設定する
        /// </summary>
        void FlagSetAllChange()
        {
            for (int i = 0; i < IsChangeHand.Length; i++)
            {
                IsChangeHand[i] = false;
            }
        }

        /// <summary>
        /// 交換するカードの中にn以上のカードがある場合、一番強いカードを追加で残す
        /// </summary>
        /// <param name="n">残すカードの基準</param>
        void FlagSetPlus1(int n)
        {
            // 交換するカードの数字のリストを作成する
            List<int> numList = new List<int>();

            for (int i = 0; i < IsChangeHand.Length; i++)
            {
                if (IsChangeHand[i] == true)
                {
                    numList.Add(Hand[i].Number);
                }
            }

            // 交換するカードの中で一番強い数字を取得する
            int highNumber = Judge.numConversion(numList.Max());

            // 一番強い数字がn以上なら、追加で残す
            if (highNumber > n)
            {
                for (int i = 0; i < IsChangeHand.Length; i++)
                {
                    if (IsChangeHand[i] == true && Hand[i].Number == highNumber)
                    {
                        IsChangeHand[i] = false;
                    }
                }
            }

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
