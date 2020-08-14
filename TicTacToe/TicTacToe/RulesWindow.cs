using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class RulesWindow : Form
    {
        public RulesWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartWindow s = new StartWindow();
            s.ShowDialog();
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ModeWindow m = new ModeWindow();
            m.ShowDialog();
            this.Close();
        }
    }
}
