using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PlayingCards
{
    abstract class Area
    {
        /// <summary>
        /// 名前を表示するラベル
        /// </summary>
        public Label NameLabel { get; set; }
        /// <summary>
        /// 所持チップを表示するラベル
        /// </summary>
        public Label HoldChipLabel { get; set; }
        /// <summary>
        /// 場に出したチップを表示するラベル
        /// </summary>
        public Label BetChipLabel { get; set; }
        /// <summary>
        /// 実行メッセージを表示するラベル
        /// </summary>
        public Label ActionMessageLabel { get; set; }
        /// <summary>
        /// 手札を表示するピクチャーボックス
        /// </summary>
        public List<PictureBox> HandPictureBox { get; set; } = new List<PictureBox>();
        /// <summary>
        /// このエリアを所持するプレイヤー
        /// </summary>
        public Character MyCharacter { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Area()
        {
            CreateName();
            CreateHoldChip();
            CreateBetChip();
            CreateActionMessage();
            CreateHand();
        }

        /// <summary>
        /// 名前を表示するラベルを生成
        /// </summary>
        /// <param name="name"></param>
        void CreateName()
        {
            NameLabel = new Label();
            NameLabel.Font = new Font("MS UI Gothic", 25);
            NameLabel.Text = "";
            NameLabel.AutoSize = true;
            NameLabel.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// 残チップを表示するラベルを生成
        /// </summary>
        void CreateHoldChip()
        {
            HoldChipLabel = new Label();
            HoldChipLabel.Font = new Font("MS UI Gothic", 20);
            HoldChipLabel.Text = $"残チップ []枚";
            HoldChipLabel.AutoSize = true;
            HoldChipLabel.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// 場に出しているチップを表示するラベルを生成
        /// </summary>
        void CreateBetChip()
        {
            BetChipLabel = new Label();
            BetChipLabel.Font = new Font("MS UI Gothic", 20);
            BetChipLabel.Text = $"総ベット額 []枚";
            BetChipLabel.AutoSize = true;
            BetChipLabel.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// アクションメッセージを表示するラベルを生成
        /// </summary>
        void CreateActionMessage()
        {
            ActionMessageLabel = new Label();
            ActionMessageLabel.Font = new Font("MS UI Gothic", 20);
            ActionMessageLabel.AutoSize = true;
            ActionMessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            ActionMessageLabel.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// 手札を表示するピクチャーボックスを生成
        /// </summary>
        void CreateHand()
        {
            for (int i = 0; i < 5; i++)
            {
                PictureBox card = new PictureBox();
                card.BackColor = Color.Transparent;
                card.SizeMode = PictureBoxSizeMode.StretchImage;
                card.Visible = false;
                card.Parent = PokerForm.Instance;
                HandPictureBox.Add(card);
            }
        }

        /// <summary>
        /// 名前を表示する
        /// </summary>
        public void NameDisplay()
        {
            NameLabel.Text = MyCharacter.Name;
            NameLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 所持しているチップ数を更新して表示する
        /// </summary>
        /// <param name="holdChip">表示するチップ数</param>
        public void HoldChipDisplay()
        {
            HoldChipLabel.Text = $"残チップ [{MyCharacter.HoldChip}]枚";
            HoldChipLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 場に出したチップ数を更新して表示する
        /// </summary>
        /// <param name="betChip">表示するチップ数</param>
        public void BetChipDisplay()
        {
            BetChipLabel.Text = $"総ベット額 [{MyCharacter.BetChip}]枚";
            BetChipLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// チェック、コール、フォールド時のメッセージを表示する
        /// </summary>
        /// <param name="action">選択したアクション</param>
        public void ActionMessageDisplay(Action action)
        {
            switch (action)
            {
                case Action.Check:
                    ActionMessageLabel.Text = "チェック";
                    break;
                case Action.Call:
                    ActionMessageLabel.Text = "　コール";
                    break;
                case Action.Fold:
                    ActionMessageLabel.Text = "フォールド";
                    break;
                default:
                    ActionMessageLabel.Text = "";
                    break;
            }

            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// ベット、レイズ時のメッセージを表示する
        /// </summary>
        /// <param name="action">選択したアクション</param>
        /// <param name="chip">チップ数</param>
        public void ActionMessageDisplay(Action action, int chip)
        {
            switch (action)
            {
                case Action.Bet:
                    ActionMessageLabel.Text = $"ベット\nチップ{chip}枚";
                    break;
                case Action.Raise:
                    ActionMessageLabel.Text = $"レイズ\nチップ{chip}枚";
                    break;
                default:
                    ActionMessageLabel.Text = "";
                    break;
            }

            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 手札交換時のメッセージを表示する
        /// </summary>
        /// <param name="cardCount">交換枚数</param>
        public void ChangeMessageDisplay(int cardCount)
        {
            ActionMessageLabel.Text = $"カード交換\n{cardCount}枚";
            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 役を表示する
        /// </summary>
        /// <param name="role">役</param>
        public void RoleDisplay()
        {
            switch (MyCharacter.MyRole)
            {
                case Role.RoyalFlush:
                    ActionMessageLabel.Text = "Rストレート\nフラッシュ";
                    break;
                case Role.StraightFlush:
                    ActionMessageLabel.Text = "ストレート\nフラッシュ";
                    break;
                case Role.FourOfKind:
                    ActionMessageLabel.Text = "フォーカード";
                    break;
                case Role.FullHouse:
                    ActionMessageLabel.Text = "フルハウス";
                    break;
                case Role.Flush:
                    ActionMessageLabel.Text = "フラッシュ";
                    break;
                case Role.Straight:
                    ActionMessageLabel.Text = "ストレート";
                    break;
                case Role.ThreeOfKind:
                    ActionMessageLabel.Text = "スリーカード";
                    break;
                case Role.TwoPair:
                    ActionMessageLabel.Text = "ツーペア";
                    break;
                case Role.Pair:
                    ActionMessageLabel.Text = "ワンペア";
                    break;
                case Role.HighCard:
                    ActionMessageLabel.Text = "ハイカード";
                    break;
                default:
                    ActionMessageLabel.Text = "";
                    break;
            }

            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 順位を表示する
        /// </summary>
        /// <param name="rank">順位</param>
        public void RankDisplay(int rank)
        {
            ActionMessageLabel.Text = $"　　{rank}位";
            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// アクションメッセージをクリアする
        /// </summary>
        public void ActionMessageClear()
        {
            ActionMessageLabel.Text = "";
            ActionMessageLabel.Visible = true;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全て表示する
        /// </summary>
        public void AreaDisplay()
        {
            NameLabel.Visible = true;
            HoldChipLabel.Visible = true;
            BetChipLabel.Visible = true;
            ActionMessageLabel.Visible = true;

            for (int i = 0; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Visible = true;
            }
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全て非表示にする
        /// </summary>
        public void AreaHide()
        {
            NameLabel.Visible = false;
            HoldChipLabel.Visible = false;
            BetChipLabel.Visible = false;
            ActionMessageLabel.Visible = false;

            for (int i = 0; i < HandPictureBox.Count; i++)
            {
                HandPictureBox[i].Visible = false;
            }
            PokerForm.Instance.Refresh();
        }

        public abstract void HandFrontDisplay(int n);
        public abstract void HandFrontDisplay();
        public abstract void HandBackDisplay(int n);
        public abstract void HandBackDisplay();
        public abstract void HandHide(int n);
        public abstract void HandHide();
    }
}
