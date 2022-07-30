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

        // 初期値
        public static int startChip = 100;
        public static int minBetChip = 1;
        public static int maxBetChip = 5;
        public static int entryChip = 1;

        public static int maxCharacter = 4;
        public static int maxRound = 1;

        public static int roundCount = 0;
        public static bool isAfterBet = false;
        public static bool isAfterChange = false;

        public static int callChip = 0;
        public static List<Character> characterList = new List<Character>();
        public static Character startCharacter;

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

            // 最初にアクションを起こすキャラクターを設定
            startCharacter = characterList[0];

            // ラウンド数を初期化
            roundCount = 0;

            // 新しいラウンドを開始する
            RoundStart();
        }

        /// <summary>
        /// 全てのラウンドが終了
        /// </summary>
        public static void GameEnd()
        {
            // 順位を表示
            RankingDisplay();
        }

        /// <summary>
        /// 新しいラウンドを開始する
        /// </summary>
        public static void RoundStart()
        {
            // フラグを初期化
            isAfterBet = false;
            isAfterChange = false;

            // ラウンド数をカウントアップ
            roundCount++;

            // 山札を作る
            Dealer.CreateDeck(0);

            // 手札を配る
            CreateHand();

            // 参加費を払う
            
            foreach (var character in characterList)
            {
                character.EntryChip();
            }

            // キャラクター毎のアクションを開始する
            TurnStart(startCharacter);
        }

        /// <summary>
        /// ラウンド終了
        /// </summary>
        public static void RoundEnd()
        {
            // 手札をを全て非表示にする
            HandClear();

            // ゲームオーバーになったキャラを判定する
            GameOverCheck();

            // 参加可能なキャラクターが1人以下ならゲーム終了へ
            if (IsAllGameOver())
            {
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
                RoundStart();
            }
        }

        /// <summary>
        /// 各キャラクターがアクションを選択する
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void TurnStart(Character nowCharacter)
        {
            // フォールド済、ゲームオーバーでないならアクションを選択
            if (nowCharacter.MyStatus != Status.Fold || nowCharacter.MyStatus != Status.GameOver)
            {
                if (isAfterBet)
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
            // ベット、又はレイズが行われた場合、他のステータスをデフォルトに戻す
            if (nowCharacter.MyStatus == Status.Raise)
            {
                isAfterBet = true;
                RaisedStatusReset(nowCharacter);
            }

            // 全員がチェックを選択した場合
            if (IsAllCheck())
            {
                if (isAfterChange)
                {
                    // カード交換済なら勝負する
                    BattleStart();
                }
                else
                {
                    // カード交換前なら場のチップを回収してラウンド終了
                    Dealer.ChipClear(characterList);
                    RoundEnd();
                }
                return;
            }

            // レイズしないまま１週した場合
            if (IsAllCall())
            {
                if (isAfterChange)
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
            if (IsAllFold())
            {
                // 残った一人を勝者とする
                Character winner = NonFoldCharacter();
                BattleEnd(winner);
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
            // フォールド済、ゲームオーバーでないならカード交換を行う
            if (nowCharacter.MyStatus != Status.Fold || nowCharacter.MyStatus != Status.GameOver)
            {
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
            if (IsAllChanged())
            {
                // 全員がカード交換したら、アクション選択へ移る
                isAfterBet = false;
                isAfterChange = true;
                TurnStart(startCharacter);
            }
            else
            {
                // 次のキャラクターがカード交換を行う
                ChangeHandStart(nowCharacter.NextCharacter);
            }
        }

        /// <summary>
        /// 勝負を開始する
        /// </summary>
        public static void BattleStart()
        {
            // フォールド、ゲームオーバーでないキャラクターのリストを作成
            List<Character> aliveCharacterList = GetAliveCharacterList();

            // 役を確定する
            foreach (var character in aliveCharacterList)
            {
                character.MyRole = Judge.GetRole(character.Hand);
                character.MyArea.HandFrontDisplay();
                character.MyArea.RoleDisplay();
            }

            // 勝者を決定する
            Character winner = Judge.WinCharacter(aliveCharacterList);

            // 勝負終了後の処理へ
            BattleEnd(winner);
        }

        /// <summary>
        /// 勝負終了後の処理
        /// </summary>
        /// <param name="winner"></param>
        public static void BattleEnd(Character winner)
        {
            // 場のチップを勝者へ移動する
            Dealer.ChipMove(winner, characterList);

            // ラウンド終了へ
            RoundEnd();
        }

        /// <summary>
        /// 全員がベットせずにチェックしたかを判定する
        /// </summary>
        /// <returns></returns>
        public static bool IsAllCheck()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.Check && character.MyStatus != Status.Fold &&
                    character.MyStatus != Status.GameOver)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 誰かがレイズ後、残りの参加者がレイズせずにコール（又はフォールド）したかを判定する
        /// </summary>
        /// <returns></returns>
        public static bool IsAllCall()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.Call && character.MyStatus != Status.Raise &&
                    character.MyStatus != Status.Fold && character.MyStatus != Status.GameOver)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// １人を除いて全員がフォールドした場合
        /// </summary>
        /// <returns></returns>
        public static bool IsAllFold()
        {
            int foldCount = 0;

            foreach (var character in characterList)
            {
                if (character.MyStatus == Status.Fold || character.MyStatus == Status.GameOver)
                {
                    foldCount++;
                }
            }

            if (foldCount >= characterList.Count - 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// １人を除いて全員がゲームオーバーの場合
        /// </summary>
        /// <returns></returns>
        public static bool IsAllGameOver()
        {
            int gameOverCount = 0;

            foreach (var character in characterList)
            {
                if (character.MyStatus == Status.GameOver)
                {
                    gameOverCount++;
                }
            }

            if (gameOverCount >= characterList.Count - 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// フォールドしていないキャラクターを返す（IsAllFoldがtrueだった場合に使う）
        /// </summary>
        /// <returns>フォールドしていないキャラクター</returns>
        public static Character NonFoldCharacter()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.Fold)
                {
                    return character;
                }
            }

            return characterList[0];
        }

        /// <summary>
        /// 全員がカード交換したかを判定する
        /// </summary>
        /// <returns></returns>
        public static bool IsAllChanged()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.ChangeHand && character.MyStatus != Status.Fold &&
                    character.MyStatus != Status.GameOver)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ラウンド開始時、キャラクターのステータスをデフォルトに戻す
        /// </summary>
        public static void NewRoundStatusReset()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus == Status.GameOver)
                {
                    character.MyStatus = Status.Default;
                }
            }
        }

        /// <summary>
        /// 誰かがレイズした時、他のキャラクターのステータスをデフォルトに戻す
        /// </summary>
        /// <param name="nowCharacter">レイズしたキャラクター</param>
        public static void RaisedStatusReset(Character nowCharacter)
        {
            foreach(var character in characterList)
            {
                if (character != nowCharacter && 
                    character.MyStatus != Status.Fold && character.MyStatus != Status.GameOver)
                {
                    character.MyStatus = Status.Default;
                }
            }
        }

        /// <summary>
        /// 勝負に参加できるキャラクターのリストを返す
        /// </summary>
        /// <returns></returns>
        public static List<Character> GetAliveCharacterList()
        {
            List<Character> aliveCharacterList = new List<Character>();

            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.Fold && character.MyStatus != Status.GameOver)
                {
                    aliveCharacterList.Add(character);
                }
            }

            return aliveCharacterList;
        }

        /// <summary>
        /// 手札を非表示にする
        /// </summary>
        public static void HandClear()
        {
            foreach (var character in characterList)
            {
                character.MyArea.HandHide();
            }
        }

        /// <summary>
        /// 手札を配る
        /// </summary>
        public static void CreateHand()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300);
                foreach (var character in characterList)
                {
                    if (character.MyStatus != Status.GameOver)
                    {
                        character.Hand[i] = Dealer.DealCard();
                        character.HandDisplay(i);
                    }
                }
            }
        }

        /// <summary>
        /// 参加費を払う
        /// </summary>
        public static void Entry()
        {
            foreach (var character in characterList)
            {
                if (character.MyStatus != Status.GameOver)
                {
                    character.EntryChip();
                }
            }
        }

        /// <summary>
        /// ゲームオーバーになったキャラを判定する
        /// </summary>
        public static void GameOverCheck()
        {
            foreach (var character in characterList)
            {
                if (character.HoldChip <= 0)
                {
                    character.MyStatus = Status.GameOver;
                }
            }
        }

        /// <summary>
        /// 各キャラクターの順位を表示する
        /// </summary>
        public static void RankingDisplay()
        {
            // 所持チップの多い順に並べ替える
            var ranking = characterList.OrderByDescending(x => x.HoldChip);

            int rank = 1;
            int keepRank = 1;
            int keepChip = 0;

            // 順番に表示する
            foreach (var character in ranking)
            {
                if (character.HoldChip == keepChip)
                {
                    character.MyArea.RankDisplay(keepRank);
                }
                else
                {
                    character.MyArea.RankDisplay(rank);
                    keepRank = rank;
                }

                rank++;
                keepChip = character.HoldChip;
            }
        }
    }
}
