using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class ModeWindow : Form
    {
        public ModeWindow()
        {
            InitializeComponent();
        }

        public class Globals
        {
            public static string selectedMode;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            char numberButton = btn.Name[btn.Name.Length - 1];
            if (numberButton == '1')
            {
                Globals.selectedMode = "1 Player";
            }
            else
            {
                Globals.selectedMode = "2 Players";
            }
            this.Hide();
            GameWindow g = new GameWindow();
            g.ShowDialog();
            this.Close();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartWindow s = new StartWindow();
            s.ShowDialog();
            this.Close();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
