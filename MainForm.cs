using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSQL
{
    public partial class MainForm : DODO_Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.Size = Sizer(this.close, 1.2f);
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.Size = Sizer(this.close, 1.2f, true);
        }
    }
}
