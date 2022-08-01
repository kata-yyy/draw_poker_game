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
        // エリア
        public static Area1 area1;
        public static Area2 area2;
        public static Area3 area3;
        public static Area4 area4;

        // キャラクター
        public static PlayerCharacter player;
        public static NonPlayerCharacter cpu1;
        public static NonPlayerCharacter cpu2;
        public static NonPlayerCharacter cpu3;

        /// <summary>
        /// 親を決めるための乱数
        /// </summary>
        static Random random = new Random();
        /// <summary>
        /// ゲーム開始時の所持チップ
        /// </summary>
        public static int startChip = 10;
        /// <summary>
        /// ベット、レイズ時の最低チップ額
        /// </summary>
        public static int minBetChip = 1;
        /// <summary>
        /// ベット、レイズ時の最大チップ額
        /// </summary>
        public static int maxBetChip = 5;
        /// <summary>
        /// ラウンド開始時に払う参加費
        /// </summary>
        public static int entryChip = 1;
        /// <summary>
        /// プレイヤーが操作するためのコントローラー
        /// </summary>
        public static Controller controller;
        /// <summary>
        /// 参加人数
        /// </summary>
        public static int maxCharacter = 4;
        /// <summary>
        /// 全ラウンド数
        /// </summary>
        public static int maxRound = 1;
        /// <summary>
        /// 現在のラウンド
        /// </summary>
        public static int roundCount = 0;
        /// <summary>
        /// ベット済かを判定するフラグ
        /// </summary>
        public static bool isAfterBet = false;
        /// <summary>
        /// カード交換済かを判定するフラグ
        /// </summary>
        public static bool isAfterChange = false;
        /// <summary>
        /// コールした場合、ベットすることになるチップ数
        /// </summary>
        public static int callChip = 0;
        /// <summary>
        /// ゲームに参加しているキャラクターのリスト
        /// </summary>
        public static List<Character> characterList = new List<Character>();
        /// <summary>
        /// そのラウンドで一番最初にアクションを行うキャラクター
        /// </summary>
        public static Character startCharacter;
        /// <summary>
        /// 現在アクションを行っているキャラクター
        /// </summary>
        public static Character nowCharacter;
        /// <summary>
        /// そのラウンドの勝者
        /// </summary>
        public static Character winner;
        /// <summary>
        /// 現在のフェーズ
        /// </summary>
        public static Phase nowPhase;

        /// <summary>
        /// 起動時に行う処理
        /// </summary>
        public static void StartUp()
        {
            // 回転したイメージ画像を用意しておく
            Image.CardImageRotate90();
            Image.CardImageRotate180();
            Image.CardImageRotate270();

            // エリアのインスタンス生成
            area1 = new Area1();
            area2 = new Area2();
            area3 = new Area3();
            area4 = new Area4();
            controller = new Controller();

            // エリアを非表示
            area1.AreaHide();
            area2.AreaHide();
            area3.AreaHide();
            area4.AreaHide();

            // キャラクターのインスタンス生成
            player = new PlayerCharacter("プレイヤー");
            cpu1 = new NonPlayerCharacter("CPU1");
            cpu2 = new NonPlayerCharacter("CPU2");
            cpu3 = new NonPlayerCharacter("CPU3");

            // コントローラーとプレイヤーの紐づけ
            player.MyController = controller;
            controller.MyPlayer = player;
        }

        /// <summary>
        /// 新しくポーカーを開始する
        /// </summary>
        public static void GameStart()
        {
            // 全てのエリアを非表示
            area1.AreaHide();
            area2.AreaHide();
            area3.AreaHide();
            area4.AreaHide();

            // 参加人数に応じて、キャラクターの設定を行う
            CharacterSetting();

            // エリアの表示
            foreach (var character in characterList)
            {
                character.MyArea.NameDisplay();
                character.HoldChip = startChip;
                character.MyArea.HoldChipDisplay();
                character.BetChip = 0;
                character.MyArea.BetChipDisplay();
            }

            // 最初にアクションを起こすキャラクターを設定
            StartCharacterSet();

            // ラウンド数を初期化
            roundCount = 0;

            // メッセージを表示後、ラウンドスタートへ
            nowPhase = Phase.GameStart;
            controller.SystemMessageDisplay();
        }

        /// <summary>
        /// 全てのラウンドが終了
        /// </summary>
        public static void GameEnd()
        {
            // 順位を表示
            RankingDisplay();

            // メッセージ表示後、次のゲームへ
            nowPhase = Phase.NextGame;
            controller.SystemMessageDisplay();
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

            // アクションメッセージを非表示
            ActionMessageClear();

            // 山札を作る
            Dealer.CreateDeck(0);

            // 手札を配る
            CreateHand();

            // 参加費を払う
            Entry();

            // ステータスをデフォルトに戻す
            NewRoundStatusReset();

            // メッセージ表示後、キャラクター毎のアクションを開始する
            nowCharacter = startCharacter;
            nowPhase = Phase.RoundStart;
            controller.SystemMessageDisplay();
        }

        /// <summary>
        /// ラウンド終了
        /// </summary>
        public static void RoundEnd()
        {
            // 手札を非表示にする
            HandClear();

            // ゲームオーバーになったキャラを判定する
            GameOverCheck();

            // 参加可能なキャラクターが1人以下ならゲーム終了へ
            if (IsAllGameOver())
            {
                nowPhase = Phase.AllGameOver;
                controller.SystemMessageDisplay();
                return;
            }

            if (roundCount >= maxRound)
            {
                // 全ラウンドが終了したならゲーム終了へ
                nowPhase = Phase.GameEnd;
                controller.SystemMessageDisplay();
            }
            else
            {
                // まだラウンドが残っているなら次のラウンドへ
                startCharacter = startCharacter.NextCharacter;
                RoundStart();
            }
        }

        /// <summary>
        /// 各キャラクターがアクションを選択する
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void TurnStart()
        {
            // フォールド済、ゲームオーバーでないならアクションを選択
            if (nowCharacter.MyStatus != Status.Fold && nowCharacter.MyStatus != Status.GameOver)
            {
                if (isAfterBet)
                {
                    // ベット後のアクション選択
                    nowCharacter.ActionSelectAfterBet();
                }
                else
                {
                    // ベット前アクション選択
                    nowCharacter.ActionSelectBeforeBet();
                }
            }
            else
            {
                // フォールド済、ゲームオーバーならアクション選択を行わない
                TurnEnd();
            }
        }

        /// <summary>
        /// アクション終了後の処理
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void TurnEnd()
        {
            // ベット、又はレイズが行われた場合、他のステータスをデフォルトに戻す
            if (nowCharacter.MyStatus == Status.Raise)
            {
                isAfterBet = true;
                RaisedStatusReset();
            }

            // １人を除いて全員がフォールドした場合
            if (IsAllFold())
            {
                // 残った一人を勝者とする
                winner = NonFoldCharacter();
                nowPhase = Phase.AllFold;
                controller.SystemMessageDisplay();
                return;
            }

            // 全員がチェックを選択した場合
            if (IsAllCheck())
            {
                // カード交換済なら勝負、そうでなければラウンド終了
                if (isAfterChange)
                {
                    // カード交換済なら勝負する
                    nowPhase = Phase.BattleStart;
                    controller.SystemMessageDisplay();
                }
                else
                {
                    // メッセージ表示後、てラウンド終了
                    Dealer.ChipClear(characterList);
                    nowPhase = Phase.AllCheck;
                    controller.SystemMessageDisplay();
                }
                return;
            }

            // レイズしないまま１週した場合
            if (IsAllCall())
            {
                // カード交換済なら勝負、そうでなければカード交換
                if (isAfterChange)
                {
                    // カード交換済なら勝負する
                    nowPhase = Phase.BattleStart;
                    controller.SystemMessageDisplay();
                }
                else
                {
                    // メッセージ表示後、カード交換を行う
                    nowCharacter = startCharacter;
                    nowPhase = Phase.ChangeHandStart;
                    controller.SystemMessageDisplay();
                }
                return;
            }

            // 次のキャラクターがアクションを選択する
            nowCharacter = nowCharacter.NextCharacter;
            TurnStart();
        }

        /// <summary>
        /// 各キャラクターがカード交換を行う
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void ChangeHandStart()
        {
            // フォールド済、ゲームオーバーでないならカード交換を行う
            if (nowCharacter.MyStatus != Status.Fold && nowCharacter.MyStatus != Status.GameOver)
            {
                nowCharacter.ChangeHandSelect();
            }
            else
            {
                // フォールド済、ゲームオーバーならカード交換を行わない
                ChangeHandEnd();
            }
        }

        /// <summary>
        /// カード交換終了後の処理
        /// </summary>
        /// <param name="nowCharacter"></param>
        public static void ChangeHandEnd()
        {
            // 全員がカード交換したら、各キャラクターのアクション選択へ
            if (IsAllChanged())
            {
                // メッセージ表示後、各キャラクターのアクション選択へ
                isAfterBet = false;
                isAfterChange = true;
                nowCharacter = startCharacter;
                nowPhase = Phase.ChangeHandEnd;
                controller.SystemMessageDisplay();
            }
            else
            {
                // 次のキャラクターがカード交換を行う
                nowCharacter = nowCharacter.NextCharacter;
                ChangeHandStart();
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
            winner = Judge.WinCharacter(aliveCharacterList);

            // メッセージ表示後、勝負終了後の処理へ
            nowPhase = Phase.BattleEnd;
            controller.SystemMessageDisplay();
        }

        /// <summary>
        /// 勝負終了後の処理
        /// </summary>
        /// <param name="winner"></param>
        public static void BattleEnd()
        {
            // 場のチップを勝者へ移動する
            Dealer.ChipMove(winner, characterList);

            // ラウンド終了へ
            RoundEnd();
        }

        /// <summary>
        /// 参加人数に応じてキャラクターの設定を行う
        /// </summary>
        public static void CharacterSetting()
        {
            characterList.Clear();

            // プレイヤーの設定
            player.MyArea = area1;
            player.NextCharacter = cpu1;
            area1.MyCharacter = player;
            area1.NameLabel.Text = player.Name;
            characterList.Add(player);

            // 参加人数に応じてNPCの設定を行う
            switch (maxCharacter)
            {
                case 2:
                    // cpu1の設定
                    cpu1.MyArea = area2;
                    cpu1.NextCharacter = player;
                    area2.MyCharacter = cpu1;
                    area2.NameLabel.Text = cpu1.Name;
                    characterList.Add(cpu1);
                    // 使わないエリアを非表示
                    area3.AreaHide();
                    area4.AreaHide();
                    return;
                case 3:
                    // cpu1の設定
                    cpu1.MyArea = area3;
                    cpu1.NextCharacter = cpu2;
                    area3.MyCharacter = cpu1;
                    area3.NameLabel.Text = cpu1.Name;
                    characterList.Add(cpu1);
                    // cpu2の設定
                    cpu2.MyArea = area2;
                    cpu2.NextCharacter = player;
                    area2.MyCharacter = cpu2;
                    area2.NameLabel.Text = cpu2.Name;
                    characterList.Add(cpu2);
                    // 使わないエリアを非表示
                    area4.AreaHide();
                    return;
                case 4:
                    // cpu1の設定
                    cpu1.MyArea = area3;
                    cpu1.NextCharacter = cpu2;
                    area3.MyCharacter = cpu1;
                    area3.NameLabel.Text = cpu1.Name;
                    characterList.Add(cpu1);
                    // cpu2の設定
                    cpu2.MyArea = area2;
                    cpu2.NextCharacter = cpu3;
                    area2.MyCharacter = cpu2;
                    area2.NameLabel.Text = cpu2.Name;
                    characterList.Add(cpu2);
                    // cpu3の設定
                    cpu3.MyArea = area4;
                    cpu3.NextCharacter = player;
                    area4.MyCharacter = cpu3;
                    area4.NameLabel.Text = cpu3.Name;
                    characterList.Add(cpu3);
                    return;
            }
        }

        /// <summary>
        /// 最初にアクションを起こすキャラクターを設定
        /// </summary>
        public static void StartCharacterSet()
        {
            int randomNum = random.Next(characterList.Count);

            startCharacter = characterList[randomNum];
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

            if (foldCount == characterList.Count - 1)
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
                if (character.MyStatus != Status.GameOver)
                {
                    character.MyStatus = Status.Default;
                }
            }
        }

        /// <summary>
        /// 誰かがレイズした時、他のキャラクターのステータスをデフォルトに戻す
        /// </summary>
        /// <param name="nowCharacter">レイズしたキャラクター</param>
        public static void RaisedStatusReset()
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
        /// アクションメッセージをクリアにする
        /// </summary>
        public static void ActionMessageClear()
        {
            foreach (var character in characterList)
            {
                character.MyArea.ActionMessageClear();
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
