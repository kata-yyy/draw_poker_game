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

            PokerMain.GameStart();
        }
    }
}
