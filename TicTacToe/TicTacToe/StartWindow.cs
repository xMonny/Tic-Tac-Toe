using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ModeWindow m = new ModeWindow();
            m.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RulesWindow r = new RulesWindow();
            r.ShowDialog();
            this.Close();
        }
    }
}
