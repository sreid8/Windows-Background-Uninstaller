using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Uninstaller0._01
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
        }

        private void agreeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var startingBar = new starting(true);
        }

        private void noAgreeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var startingBar = new starting(false);

        }

       


    }
}
