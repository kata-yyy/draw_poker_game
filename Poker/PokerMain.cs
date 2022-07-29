using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PlayingCards
{
    internal class PokerMain
    {
        static Random random = new Random();

        public static int startChip = 100;
        public static int minBetChip = 1;
        public static int maxBetChip = 5;
        public static int entryChip = 1;

        public static int maxCharacter = 4;
        public static int entryCharacter = 4;
        public static int aliveCharacter = 4;
        public static int maxRound = 1;
        public static int roundCount = 1;
        public static int changeHandCount = 0;
        public static int gameoverCount = 0;
        public static int checkCount = 0;
        public static bool betFlag = false;
        public static bool changeHandFlag = false;

        public static List<Character> characterList = new List<Character>();
        public static Character startCharacter;
        public static Character raiseCharacter;
        public static Character winner;

        /// <summary>
        /// 新しくポーカーを開始する
        /// </summary>
        public static void GameStart()
        {
            // キャラクター生成
            CharacterCreater.NewCreate(maxCharacter);

            // エリアの表示
            foreach (var character in characterList)
            {
                character.MyArea.AreaDisplay();
            }

            startCharacter = characterList[0];

            // フラグ、カウンタを初期化
            gameoverCount = 0;
            roundCount = 1;

            // 新しいラウンドを開始する
            RoundStart();
        }

        /// <summary>
        /// 全てのラウンドが終了
        /// </summary>
        public static void GameEnd()
        {

        }

        /// <summary>
        /// 新しいラウンドを開始する
        /// </summary>
        public static void RoundStart()
        {
            // フラグ、カウンタを初期化
            betFlag = false;
            changeHandFlag = false;
            checkCount = 0;
            changeHandCount = 0;
            entryCharacter = aliveCharacter;

            // 山札を作る
            Dealer.CreateDeck(0);

            // カードを配る
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300);
                foreach (var character in characterList)
                {
                    character.Hand[i] = Dealer.DealCard();
                    character.HandDisplay(i);
                }
            }

            // 参加費を払う
            foreach (var character in characterList)
            {
                character.EntryChip();
            }

            // 順番を決める
            TurnSet();

            // キャラクター毎のアクションを開始する
            TurnStart(startCharacter);
        }

        /// <summary>
        /// ラウンド終了
        /// </summary>
        public static void RoundEnd()
        {
            if (gameoverCount >= maxCharacter - 1)
            {
                // 参加可能なキャラクターが1人以下ならゲーム終了へ
                GameEnd();
                return;
            }

            if (roundCount >= maxRound)
            {
                // 全ラウンドが終了したならゲーム終了へ
                GameEnd();
            }
            else
            {
                // まだラウンドが残っているなら次のラウンドへ
                roundCount++;
                RoundStart();
            }
        }

        /// <summary>
        /// 各キャラクターがアクションを選択する
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void TurnStart(Character nowCharacter)
        {
            if (nowCharacter.MyStatus == Status.Normal)
            {
                if (betFlag)
                {
                    // ベット前のアクション選択
                    nowCharacter.ActionSelectAfterBet();
                }
                else
                {
                    // ベット後のアクション選択
                    nowCharacter.ActionSelectBeforeBet();
                }
            }
            else
            {
                // フォールド済、ゲームオーバーならアクション選択を行わない
                TurnEnd(nowCharacter);
            }
        }

        /// <summary>
        /// アクション終了後の処理
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void TurnEnd(Character nowCharacter)
        {
            // 全員がチェックを選択した場合
            if (checkCount == entryCharacter)
            {
                if (changeHandFlag)
                {
                    // カード交換済なら勝負する
                    BattleStart();
                }
                else
                {
                    // カード交換前なら場のチップを回収してラウンド終了
                    Dealer.ChipDelete();
                    RoundEnd();
                }
                return;
            }

            // レイズしないまま１週した場合
            if (raiseCharacter == nowCharacter.NextCharacter)
            {
                if (changeHandFlag)
                {
                    // カード交換済なら勝負する
                    BattleStart();
                }
                else
                {
                    // カード交換前ならカード交換を行う
                    ChangeHandStart(startCharacter);
                }
                return;
            }

            // １人を除いて全員がフォールドした場合
            if (entryCharacter <= 1)
            {
                // 残った一人を勝者とする
                winner = nowCharacter.NextCharacter;
                BattleEnd();
                return;
            }

            // 次のキャラクターがアクションを選択する
            TurnStart(nowCharacter.NextCharacter);
        }

        /// <summary>
        /// 各キャラクターがカード交換を行う
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void ChangeHandStart(Character nowCharacter)
        {
            if (nowCharacter.MyStatus == Status.Normal)
            {
                // ラウンドに参加中ならカード交換を行う
                nowCharacter.ChangeHandSelect();
            }
            else
            {
                // フォールド済、ゲームオーバーならカード交換を行わない
                ChangeHandEnd(nowCharacter);
            }
        }

        /// <summary>
        /// カード交換終了後の処理
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void ChangeHandEnd(Character nowCharacter)
        {
            changeHandCount++;

            if (changeHandCount >= entryCharacter)
            {
                // 全員がカード交換したら、アクション選択へ移る
                betFlag = false;
                changeHandFlag = true;
                checkCount = 0;
                TurnStart(startCharacter);
            }
            else
            {
                // 次のキャラクターがカード交換を行う
                ChangeHandStart(nowCharacter.NextCharacter);
            }
        }

        public static void BattleStart()
        {
            MessageBox.Show("勝負開始");
        }

        public static void BattleEnd()
        {
            MessageBox.Show("勝負終了");
        }

        /// <summary>
        /// 順番を決める
        /// </summary>
        public static void TurnSet()
        {
            characterList[0].PrevCharacter = characterList[3];
            characterList[0].NextCharacter = characterList[1];
            characterList[1].PrevCharacter = characterList[0];
            characterList[1].NextCharacter = characterList[2];
            characterList[2].PrevCharacter = characterList[1];
            characterList[2].NextCharacter = characterList[3];
            characterList[3].PrevCharacter = characterList[2];
            characterList[3].NextCharacter = characterList[0];
        }
    }
}
