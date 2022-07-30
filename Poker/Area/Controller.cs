using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PlayingCards
{
    internal class Controller
    {
        public static Point betChipBoxLocation = new Point(930, 645);
        public static Point betButtonLocation = new Point(880, 680);
        public static Point checkButtonLocation = new Point(880, 715);
        public static Point cardChangeButtonLocation = new Point(720, 700);

        public static Size changeButtonSize = new Size(80, 30);
        public static Size cardChangeButtonSize = new Size(90, 30);
        public static Size betChipBoxSize = new Size(35, 30);
        public static Size betChipLabel1Size = new Size(70, 30);
        public static Size betChipLabel2Size = new Size(30, 30);
        public static Size plusButtonSize = new Size(30, 30);
        public static Size betButtonSize = new Size(80, 30);
        public static Size raiseButtonSize = new Size(80, 30);
        public static Size checkButtonSize = new Size(80, 30);
        public static Size callButtonSize = new Size(80, 30);
        public static Size foldButtonSize = new Size(90, 30);

        /// <summary>
        /// 交換するカードを選ぶためのボタン（手札の枚数分）
        /// </summary>
        public List<Button> ChangeButtons { get; set; } = new List<Button>();
        /// <summary>
        /// カード交換を実行するボタン
        /// </summary>
        public Button CardChangeButton { get; set; }
        /// <summary>
        /// ベット、レイズ額を表示するテキストボックス
        /// </summary>
        public TextBox BetChipBox { get; set; }
        /// <summary>
        /// BetChipBoxの前に付けるラベル
        /// </summary>
        public Label BetChipLabel1 { get; set; }
        /// <summary>
        /// BetChipBoxの後ろに付けるラベル
        /// </summary>
        public Label BetChipLabel2 { get; set; }
        /// <summary>
        /// ベット、レイズ額を増加させるボタン
        /// </summary>
        public Button PlusButton { get; set; }
        /// <summary>
        /// ベット、レイズ額を減少させるボタン
        /// </summary>
        public Button MinusButton { get; set; }
        /// <summary>
        /// ベットを実行するためのボタン
        /// </summary>
        public Button BetButton { get; set; }
        /// <summary>
        /// レイズを実行するためのボタン
        /// </summary>
        public Button RaiseButton { get; set; }
        /// <summary>
        /// チェックを実行するためのボタン
        /// </summary>
        public Button CheckButton { get; set; }
        /// <summary>
        /// コールを実行するためのボタン
        /// </summary>
        public Button CallButton { get; set; }
        /// <summary>
        /// フォールドを実行するためのボタン
        /// </summary>
        public Button FoldButton { get; set; }
        /// <summary>
        /// 操作エリアを所有するプレイヤー
        /// </summary>
        public PlayerCharacter MyPlayer { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="myPlayer">操作エリアを所有するプレイヤー</param>
        public Controller(PlayerCharacter myPlayer)
        {
            MyPlayer = myPlayer;

            CreateChangeButton();
            CreateControlArea1();
            CreateControlArea2();
            CreateControlArea3();
        }

        /// <summary>
        /// 交換用のボタンを生成
        /// </summary>
        void CreateChangeButton()
        {
            // カード交換を実行するボタン
            CardChangeButton = new Button();
            CardChangeButton.Font = new Font("MS UI Gothic", 12);
            CardChangeButton.Text = "交換実行";
            CardChangeButton.Size = cardChangeButtonSize;
            CardChangeButton.Location = cardChangeButtonLocation;
            CardChangeButton.Enabled = false;
            CardChangeButton.Parent = PokerForm.Instance;
            CardChangeButton.Click += new EventHandler(CardChangeButtonClicked);

            // 交換するカードを選ぶためのボタン群
            for (int i = 0; i < MyPlayer.MyArea.Hand.Count; i++)
            {
                Button changeButton = new Button();
                changeButton.Font = new Font("MS UI Gothic", 10);
                changeButton.Text = "チェンジ";
                changeButton.Size = changeButtonSize;
                changeButton.Location = new Point(MyPlayer.MyArea.Hand[i].Location.X,
                             MyPlayer.MyArea.Hand[i].Location.Y + MyPlayer.MyArea.Hand[i].Size.Height + 5);
                changeButton.Enabled = false;
                changeButton.Parent = PokerForm.Instance;
                ChangeButtons.Add(changeButton);
            }

            ChangeButtons[0].Click += new EventHandler(ChangeButton1Clicked);
            ChangeButtons[1].Click += new EventHandler(ChangeButton2Clicked);
            ChangeButtons[2].Click += new EventHandler(ChangeButton3Clicked);
            ChangeButtons[3].Click += new EventHandler(ChangeButton4Clicked);
            ChangeButtons[4].Click += new EventHandler(ChangeButton5Clicked);
        }

        /// <summary>
        /// コントロールエリア１行目を生成
        /// </summary>
        void CreateControlArea1()
        {
            // ベット、レイズ額を表示するテキストボックス
            BetChipBox = new TextBox();
            BetChipBox.ReadOnly = true;
            BetChipBox.Font = new Font("MS UI Gothic", 15);
            BetChipBox.Text = "1";
            BetChipBox.BackColor = Color.White;
            BetChipBox.TextAlign = HorizontalAlignment.Right;
            BetChipBox.Size = betChipBoxSize;
            BetChipBox.Location = betChipBoxLocation;
            BetChipBox.Enabled = false;
            BetChipBox.Parent = PokerForm.Instance;
            // BetChipBoxの前に付けるラベル
            BetChipLabel1 = new Label();
            BetChipLabel1.Font = new Font("MS UI Gothic", 15);
            BetChipLabel1.Text = "チップ";
            BetChipLabel1.Size = betChipLabel1Size;
            BetChipLabel1.Location = new Point(betChipBoxLocation.X - 55, betChipBoxLocation.Y + 3);
            BetChipLabel1.Parent = PokerForm.Instance;
            // BetChipBoxの後ろに付けるラベル
            BetChipLabel2 = new Label();
            BetChipLabel2.Font = new Font("MS UI Gothic", 15);
            BetChipLabel2.Text = "枚";
            BetChipLabel2.Size = betChipLabel2Size;
            BetChipLabel2.Location = new Point(betChipBoxLocation.X + 40, betChipBoxLocation.Y + 3);
            BetChipLabel2.Parent = PokerForm.Instance;

            MinusButton = new Button();
            MinusButton.Font = new Font("MS UI Gothic", 15);
            MinusButton.Text = "-";
            MinusButton.Size = plusButtonSize;
            MinusButton.Location = new Point(betChipBoxLocation.X + 80, betChipBoxLocation.Y);
            MinusButton.Enabled = false;
            MinusButton.Parent = PokerForm.Instance;
            MinusButton.Click += new EventHandler(MinusButtonClicked);

            PlusButton = new Button();
            PlusButton.Font = new Font("MS UI Gothic", 15);
            PlusButton.Text = "+";
            PlusButton.Size = plusButtonSize;
            PlusButton.Location = new Point(betChipBoxLocation.X + 115, betChipBoxLocation.Y);
            PlusButton.Enabled = false;
            PlusButton.Parent = PokerForm.Instance;
            PlusButton.Click += new EventHandler(PlusButtonClicked);
        }

        /// <summary>
        /// コントロールエリア２行目を生成
        /// </summary>
        void CreateControlArea2()
        {
            // ベットを実行するためのボタン
            BetButton = new Button();
            BetButton.Font = new Font("MS UI Gothic", 15);
            BetButton.Text = "ベット";
            BetButton.Size = betButtonSize;
            BetButton.Location = betButtonLocation;
            BetButton.Enabled = false;
            BetButton.Parent = PokerForm.Instance;
            BetButton.Click += new EventHandler(BetButtonClicked);
            // レイズを実行するためのボタン
            RaiseButton = new Button();
            RaiseButton.Font = new Font("MS UI Gothic", 15);
            RaiseButton.Text = "レイズ";
            RaiseButton.Size = raiseButtonSize;
            RaiseButton.Location = new Point(betButtonLocation.X + 90, betButtonLocation.Y);
            RaiseButton.Enabled = false;
            RaiseButton.Parent = PokerForm.Instance;
            RaiseButton.Click += new EventHandler(RaiseButtonClicked);
        }

        /// <summary>
        /// コントロールエリア３行目を生成
        /// </summary>
        void CreateControlArea3()
        {
            // チェックを実行するためのボタン
            CheckButton = new Button();
            CheckButton.Font = new Font("MS UI Gothic", 15);
            CheckButton.Text = "チェック";
            CheckButton.Size = checkButtonSize;
            CheckButton.Location = checkButtonLocation;
            CheckButton.Enabled = false;
            CheckButton.Parent = PokerForm.Instance;
            CheckButton.Click += new EventHandler(CheckButtonClicked);
            // コールを実行するためのボタン
            CallButton = new Button();
            CallButton.Font = new Font("MS UI Gothic", 15);
            CallButton.Text = "コール";
            CallButton.Size = callButtonSize;
            CallButton.Location = new Point(checkButtonLocation.X + 90, checkButtonLocation.Y);
            CallButton.Enabled = false;
            CallButton.Parent = PokerForm.Instance;
            CallButton.Click += new EventHandler(CallButtonClicked);
            // フォールドを実行するためのボタン
            FoldButton = new Button();
            FoldButton.Font = new Font("MS UI Gothic", 15);
            FoldButton.Text = "フォールド";
            FoldButton.Size = foldButtonSize;
            FoldButton.Location = new Point(checkButtonLocation.X + 180, checkButtonLocation.Y);
            FoldButton.Enabled = false;
            FoldButton.Parent = PokerForm.Instance;
            FoldButton.Click += new EventHandler(FoldButtonClicked);
        }

        /// <summary>
        /// 全てのボタンを無効にする
        /// </summary>
        public void AllButtonInvalid()
        {
            for (int i = 0; i < ChangeButtons.Count; i++)
            {
                ChangeButtons[i].Enabled = false;
            }

            CardChangeButton.Enabled = false;
            PlusButton.Enabled = false;
            MinusButton.Enabled = false;
            BetButton.Enabled = false;
            RaiseButton.Enabled = false;
            CallButton.Enabled = false;
            CheckButton.Enabled = false;
            FoldButton.Enabled = false;
            PokerForm.Instance.Refresh();
        }

        /// <summary>
        /// 「カード交換」ボタンクリック時
        /// </summary>
        public void CardChangeButtonClicked(object sender, EventArgs e)
        {
            AllButtonInvalid();
            MyPlayer.ChangeHand();
        }

        /// <summary>
        /// 「チェンジ１」ボタンクリック時
        /// </summary>
        public void ChangeButton1Clicked(object sender, EventArgs e)
        {
            if (MyPlayer.IsChangeHand[0] == false)
            {
                MyPlayer.MyArea.HandBackDisplay(0);
                ChangeButtons[0].Text = "戻す";
                MyPlayer.IsChangeHand[0] = true;
            }
            else
            {
                MyPlayer.MyArea.HandFrontDisplay(0);
                ChangeButtons[0].Text = "チェンジ";
                MyPlayer.IsChangeHand[0] = false;
            }
        }

        /// <summary>
        /// 「チェンジ２」ボタンクリック時
        /// </summary>
        public void ChangeButton2Clicked(object sender, EventArgs e)
        {
            if (MyPlayer.IsChangeHand[1] == false)
            {
                MyPlayer.MyArea.HandBackDisplay(1);
                ChangeButtons[1].Text = "戻す";
                MyPlayer.IsChangeHand[1] = true;
            }
            else
            {
                MyPlayer.MyArea.HandFrontDisplay(1);
                ChangeButtons[1].Text = "チェンジ";
                MyPlayer.IsChangeHand[1] = false;
            }
        }

        /// <summary>
        /// 「チェンジ３」ボタンクリック時
        /// </summary>
        public void ChangeButton3Clicked(object sender, EventArgs e)
        {
            if (MyPlayer.IsChangeHand[2] == false)
            {
                MyPlayer.MyArea.HandBackDisplay(2);
                ChangeButtons[2].Text = "戻す";
                MyPlayer.IsChangeHand[2] = true;
            }
            else
            {
                MyPlayer.MyArea.HandFrontDisplay(2);
                ChangeButtons[2].Text = "チェンジ";
                MyPlayer.IsChangeHand[2] = false;
            }
        }

        /// <summary>
        ///「チェンジ４」ボタンクリック時
        /// </summary>
        public void ChangeButton4Clicked(object sender, EventArgs e)
        {
            if (MyPlayer.IsChangeHand[3] == false)
            {
                MyPlayer.MyArea.HandBackDisplay(3);
                ChangeButtons[3].Text = "戻す";
                MyPlayer.IsChangeHand[3] = true;
            }
            else
            {
                MyPlayer.MyArea.HandFrontDisplay(3);
                ChangeButtons[3].Text = "チェンジ";
                MyPlayer.IsChangeHand[3] = false;
            }
        }

        /// <summary>
        /// 「チェンジ５」ボタンクリック時
        /// </summary>
        public void ChangeButton5Clicked(object sender, EventArgs e)
        {
            if (MyPlayer.IsChangeHand[4] == false)
            {
                MyPlayer.MyArea.HandBackDisplay(4);
                ChangeButtons[4].Text = "戻す";
                MyPlayer.IsChangeHand[4] = true;
            }
            else
            {
                MyPlayer.MyArea.HandFrontDisplay(4);
                ChangeButtons[4].Text = "チェンジ";
                MyPlayer.IsChangeHand[4] = false;
            }
        }

        /// <summary>
        /// 「-」ボタンクリック時
        /// </summary>
        public void MinusButtonClicked(object sender, EventArgs e)
        {
            if (int.Parse(BetChipBox.Text) > 1)
            {
                BetChipBox.Text = (int.Parse(BetChipBox.Text) - 1).ToString();
            }
            else
            {
                BetChipBox.Text = "1";
            }
        }

        /// <summary>
        ///  「+」ボタンクリック時
        /// </summary>
        public void PlusButtonClicked(object sender, EventArgs e)
        {
            if (int.Parse(BetChipBox.Text) < PokerMain.maxBetChip)
            {
                BetChipBox.Text = (int.Parse(BetChipBox.Text) + 1).ToString();
            }
            else
            {
                BetChipBox.Text = PokerMain.maxBetChip.ToString();
            }
        }

        /// <summary>
        /// 「ベット」ボタンクリック時
        /// </summary>
        public void BetButtonClicked(object sender, EventArgs e)
        {
            int betChip = int.Parse(BetChipBox.Text);

            if (betChip > MyPlayer.HoldChip)
            {
                MessageBox.Show("チップが足りません");
            }
            else
            {
                AllButtonInvalid();
                MyPlayer.Bet(betChip);
            }
        }

        /// <summary>
        /// 「レイズ」ボタンクリック時
        /// </summary>
        public void RaiseButtonClicked(object sender, EventArgs e)
        {
            int raiseChip = int.Parse(BetChipBox.Text);

            if (PokerMain.callChip + raiseChip > MyPlayer.HoldChip)
            {
                MessageBox.Show("チップが足りません");
            }
            else
            {
                AllButtonInvalid();
                MyPlayer.Raise(raiseChip);
            }
        }

        /// <summary>
        /// 「チェック」ボタンクリック時
        /// </summary>
        public void CheckButtonClicked(object sender, EventArgs e)
        {
            AllButtonInvalid();
            MyPlayer.Check();
        }

        /// <summary>
        /// 「コール」ボタンクリック時
        /// </summary>
        public void CallButtonClicked(object sender, EventArgs e)
        {
            AllButtonInvalid();
            MyPlayer.Call();
        }

        /// <summary>
        /// 「フォールド」ボタンクリック時
        /// </summary>
        public void FoldButtonClicked(object sender, EventArgs e)
        {
            AllButtonInvalid();
            MyPlayer.Fold();
        }
    }
}
