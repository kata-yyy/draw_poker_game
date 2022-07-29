using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayingCards
{
    public partial class MenuForm : Form
    {
        public static int maxPlayerCount = 4;
        public static int maxGameCount = 10;

        public MenuForm()
        {
            InitializeComponent();

            TopMost = true;

            for (int i = 2; i <= maxPlayerCount; i++)
            {
                playerCountBox.Items.Add(i);
            }

            for (int i = 1; i <= maxGameCount; i++)
            {
                gameCountBox.Items.Add(i);
            }
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(playerCountBox.Text))
            {
                MessageBox.Show("プレイヤー数を選択してください");
                return;
            }
            if (string.IsNullOrWhiteSpace(gameCountBox.Text))
            {
                MessageBox.Show("ゲーム回数を選択してください");
                return;
            }

            PokerMain.maxCharacter = int.Parse(playerCountBox.Text);
            PokerMain.maxRound = int.Parse(gameCountBox.Text);
            Close();
            PokerMain.GameStart();
        }
    }
}
