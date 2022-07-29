using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PlayingCards
{
    public partial class PokerForm : Form
    {
        public static PokerForm Instance;
        public PokerForm()
        {
            InitializeComponent();

            Instance = this;

            Image.CardImageRotate90();
            Image.CardImageRotate180();
            Image.CardImageRotate270();

            //MenuForm menuForm = new MenuForm();
            //menuForm.Show();

            //PokerMain.GameStart();

            //***********************************************************************
            // キャラクター生成
            CharacterCreater.NewCreate(4);

            // エリアの表示
            foreach (var character in PokerMain.characterList)
            {
                character.MyArea.AreaDisplay();
            }

            PokerMain.characterList[0].Hand[0] = new Card(Suit.Spade, 13);
            PokerMain.characterList[0].Hand[1] = new Card(Suit.Spade, 12);
            PokerMain.characterList[0].Hand[2] = new Card(Suit.Heart, 12);
            PokerMain.characterList[0].Hand[3] = new Card(Suit.Spade, 2);
            PokerMain.characterList[0].Hand[4] = new Card(Suit.Club, 2);

            PokerMain.characterList[1].Hand[0] = new Card(Suit.Club, 13);
            PokerMain.characterList[1].Hand[1] = new Card(Suit.Diamond, 13);
            PokerMain.characterList[1].Hand[2] = new Card(Suit.Heart, 13);
            PokerMain.characterList[1].Hand[3] = new Card(Suit.Club, 12);
            PokerMain.characterList[1].Hand[4] = new Card(Suit.Spade, 8);

            PokerMain.characterList[2].Hand[0] = new Card(Suit.Diamond, 1);
            PokerMain.characterList[2].Hand[1] = new Card(Suit.Diamond, 2);
            PokerMain.characterList[2].Hand[2] = new Card(Suit.Diamond, 3);
            PokerMain.characterList[2].Hand[3] = new Card(Suit.Diamond, 4);
            PokerMain.characterList[2].Hand[4] = new Card(Suit.Diamond, 5);

            PokerMain.characterList[3].Hand[0] = new Card(Suit.Heart, 1);
            PokerMain.characterList[3].Hand[1] = new Card(Suit.Heart, 2);
            PokerMain.characterList[3].Hand[2] = new Card(Suit.Heart, 3);
            PokerMain.characterList[3].Hand[3] = new Card(Suit.Heart, 4);
            PokerMain.characterList[3].Hand[4] = new Card(Suit.Heart, 6);

            foreach (var chara in PokerMain.characterList)
            {
                chara.Role = Judge.GetRole(chara.Hand);
                chara.MyArea.AreaDisplay();
                chara.MyArea.RoleDisplay();
                chara.MyArea.HandFrontDisplay();
            }

            Character winner = Judge.WinCharacter(PokerMain.characterList);

            switch (winner.Name)
            {
                case "プレイヤー":
                    MessageBox.Show("プレイヤーの勝利");
                    break;
                case "CPU1":
                    MessageBox.Show("CPU1の勝利");
                    break;
                case "CPU2":
                    MessageBox.Show("CPU2の勝利");
                    break;
                case "CPU3":
                    MessageBox.Show("CPU3の勝利");
                    break;
            }

            //**************************************************************************
        }
    }
}
