using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace WinFormsSQL
{
    public partial class LoginForm : DODO_Form
    {
        private RegisterForm registerForm;
        public LoginForm(RegisterForm regform = null)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"D:\CSharp\WinFormsSQL\Attributes\Fonts\Spiegel_TT_SemiBold.ttf");
            InitializeComponent();

            registerForm = regform;

            label1.Font = new Font(pfc.Families[0], 32);
            this.buttonLogin.Left = this.ClientSize.Width / 2 - buttonLogin.Width / 2;
            this.passField.Size = this.loginField.Size;
            this.registerButton.Left = this.ClientSize.Width / 2 - registerButton.Width / 2;
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

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            this.Cursor = Cursors.SizeAll;
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;
            string passUser = passField.Text;

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else MessageBox.Show("Ошибка");
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (registerForm == null)
            {
                registerForm = new RegisterForm(this);
            }
            registerForm.Show();
            this.Hide();       
        }

        private void registerButton_MouseEnter(object sender, EventArgs e)
        {
            registerButton.Font = new Font(registerButton.Font.FontFamily, 11f, registerButton.Font.Style);
        }

        private void registerButton_MouseLeave(object sender, EventArgs e)
        {
            registerButton.Font = new Font(registerButton.Font.FontFamily, 10f, registerButton.Font.Style);
        }
    }
}
