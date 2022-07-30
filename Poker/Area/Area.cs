﻿using System;
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
        public Label Name { get; set; }
        /// <summary>
        /// 所持チップを表示するラベル
        /// </summary>
        public Label HoldChip { get; set; }
        /// <summary>
        /// 場に出したチップを表示するラベル
        /// </summary>
        public Label BetChip { get; set; }
        /// <summary>
        /// 実行メッセージを表示するラベル
        /// </summary>
        public Label ActionMessage { get; set; }
        /// <summary>
        /// 手札を表示するピクチャーボックス
        /// </summary>
        public List<PictureBox> Hand { get; set; } = new List<PictureBox>();
        /// <summary>
        /// このエリアを所持するプレイヤー
        /// </summary>
        public Character MyCharacter { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">プレイヤーの名前</param>
        /// <param name="myPlayer">このエリアを所持するプレイヤー</param>
        public Area(Character myPlayer)
        {
            MyCharacter = myPlayer;
            CreateName(MyCharacter.Name);
            CreateHoldChip();
            CreateBetChip();
            CreateActionMessage();
            CreateHand();
        }

        /// <summary>
        /// 名前を表示するラベルを生成
        /// </summary>
        /// <param name="name"></param>
        void CreateName(string name)
        {
            Name = new Label();
            Name.Font = new Font("MS UI Gothic", 25);
            Name.Text = name;
            Name.AutoSize = true;
            Name.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// 残チップを表示するラベルを生成
        /// </summary>
        void CreateHoldChip()
        {
            HoldChip = new Label();
            HoldChip.Font = new Font("MS UI Gothic", 20);
            HoldChip.Text = $"残チップ [{MyCharacter.HoldChip}]枚";
            HoldChip.AutoSize = true;
            HoldChip.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// 場に出しているチップを表示するラベルを生成
        /// </summary>
        void CreateBetChip()
        {
            BetChip = new Label();
            BetChip.Font = new Font("MS UI Gothic", 20);
            BetChip.Text = $"総ベット額 [{MyCharacter.BetChip}]枚";
            BetChip.AutoSize = true;
            BetChip.Parent = PokerForm.Instance;
        }

        /// <summary>
        /// アクションメッセージを表示するラベルを生成
        /// </summary>
        void CreateActionMessage()
        {
            ActionMessage = new Label();
            ActionMessage.Font = new Font("MS UI Gothic", 20);
            ActionMessage.AutoSize = true;
            ActionMessage.TextAlign = ContentAlignment.MiddleCenter;
            ActionMessage.Parent = PokerForm.Instance;
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
                Hand.Add(card);
            }
        }

        /// <summary>
        /// 所持しているチップ数を更新して表示する
        /// </summary>
        /// <param name="holdChip">表示するチップ数</param>
        public void HoldChipDisplay()
        {
            HoldChip.Text = $"残チップ [{MyCharacter.HoldChip}]枚";
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 場に出したチップ数を更新して表示する
        /// </summary>
        /// <param name="betChip">表示するチップ数</param>
        public void BetChipDisplay()
        {
            BetChip.Text = $"総ベット額 [{MyCharacter.BetChip}]枚";
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
                    ActionMessage.Text = "チェック";
                    PokerForm.Instance.Refresh();
                    return;
                case Action.Call:
                    ActionMessage.Text = "　コール";
                    PokerForm.Instance.Refresh();
                    return;
                case Action.Fold:
                    ActionMessage.Text = "フォールド";
                    PokerForm.Instance.Refresh();
                    return;
                default:
                    ActionMessage.Text = "";
                    PokerForm.Instance.Refresh();
                    return;
            }
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
                    ActionMessage.Text = $"ベット\nチップ{chip}枚";
                    PokerForm.Instance.Refresh();
                    return;
                case Action.Raise:
                    ActionMessage.Text = $"レイズ\nチップ{chip}枚";
                    PokerForm.Instance.Refresh();
                    return;
                default:
                    ActionMessage.Text = "";
                    PokerForm.Instance.Refresh();
                    return;
            }
        }

        /// <summary>
        /// 手札交換時のメッセージを表示する
        /// </summary>
        /// <param name="cardCount">交換枚数</param>
        public void ChangeMessageDisplay(int cardCount)
        {
            ActionMessage.Text = $"カード交換\n{cardCount}枚";
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
                    ActionMessage.Text = "Rストレート\nフラッシュ";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.StraightFlush:
                    ActionMessage.Text = "ストレート\nフラッシュ";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.FourOfKind:
                    ActionMessage.Text = "フォーカード";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.FullHouse:
                    ActionMessage.Text = "フルハウス";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.Flush:
                    ActionMessage.Text = "フラッシュ";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.Straight:
                    ActionMessage.Text = "ストレート";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.ThreeOfKind:
                    ActionMessage.Text = "スリーカード";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.TwoPair:
                    ActionMessage.Text = "ツーペア";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.Pair:
                    ActionMessage.Text = "ワンペア";
                    PokerForm.Instance.Refresh();
                    return;
                case Role.HighCard:
                    ActionMessage.Text = "ハイカード";
                    PokerForm.Instance.Refresh();
                    return;
                default:
                    ActionMessage.Text = "";
                    PokerForm.Instance.Refresh();
                    return;
            }
        }

        /// <summary>
        /// 順位を表示する
        /// </summary>
        /// <param name="rank">順位</param>
        public void RankDisplay(int rank)
        {
            ActionMessage.Text = $"　　{rank}位";
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全て表示する
        /// </summary>
        public void AreaDisplay()
        {
            Name.Visible = true;
            HoldChip.Visible = true;
            BetChip.Visible = true;
            ActionMessage.Visible = true;

            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].Visible = true;
            }
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 全て非表示にする
        /// </summary>
        public void AreaHide()
        {
            Name.Visible = false;
            HoldChip.Visible = false;
            BetChip.Visible = false;
            ActionMessage.Visible = false;

            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].Visible = false;
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
