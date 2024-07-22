using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace WinFormsSQL
{
    public int a;
    public partial class RegisterForm : DODO_Form
    {
        private LoginForm loginForm;
        public int interval = 15;
        protected const string userNameFieldDefault = "Введите имя";
        protected const string userLoginFieldDefault = "Введите логин";
        protected const string passwordFieldDefault = "Введите пароль";
        protected const string rePasswordFieldDefault = "пароль+";
        public RegisterForm(LoginForm loginForm = null)
        {
            InitializeComponent();

            this.loginForm = loginForm;

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"D:\CSharp\WinFormsSQL\Attributes\Fonts\Spiegel_TT_SemiBold.ttf");
            label1.Font = new Font(pfc.Families[0], 32);
            label2.Font = new Font(pfc.Families[0], 15);
            int width = this.ClientSize.Width;
            this.buttonRegister.Left = width / 2 - buttonRegister.Width / 2;
            this.rePassField.Left = width / 2 + interval;
            this.passField.Left = rePassField.Left;

            this.pictureBox3.Left = width / 2 + rePassField.Width / 2 + interval - pictureBox3.Width / 2;
            this.pictureBox2.Left = width / 2 - (loginField.Width / 2 + interval + pictureBox2.Width / 2);

            this.loginField.Left = width / 2 - (loginField.Width + interval) ;
            this.userNameField.Left = loginField.Left;

            this.loginButton.Left = this.ClientSize.Width / 2 - loginButton.Width / 2;

            SetTextBox(userNameField, userNameFieldDefault);
            SetTextBox(rePassField, passwordFieldDefault);
            SetTextBox(passField, passwordFieldDefault);
            SetTextBox(loginField, userLoginFieldDefault);
        }
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            this.Cursor = Cursors.SizeAll;
        }

        private void RegisterForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.Size = Sizer(this.close, 1.2f);
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.Size = Sizer(this.close, 1.2f, true);
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            TextBoxEnter(userNameField, userNameFieldDefault);
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            TextBoxLeave(userNameField, userNameFieldDefault);
        }

        private void buttonRegister_MouseEnter(object sender, EventArgs e)
        {
            buttonRegister.Size = Sizer(this.buttonRegister, 1.01f);
        }

        private void buttonRegister_MouseLeave(object sender, EventArgs e)
        {
            buttonRegister.Size = Sizer(this.buttonRegister, 1.01f, true);
        }

        private void rePassField_Enter(object sender, EventArgs e)
        {
            TextBoxEnter(passField, passwordFieldDefault, true);
        }

        private void rePassField_Leave(object sender, EventArgs e)
        {
            TextBoxLeave(passField, passwordFieldDefault, true);
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            TextBoxEnter(rePassField, passwordFieldDefault, true);
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            TextBoxLeave(rePassField, passwordFieldDefault, true);
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            TextBoxEnter(loginField, userLoginFieldDefault);
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            TextBoxLeave(loginField, userLoginFieldDefault);
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if ((userNameField.Text == userNameFieldDefault || userNameField.Text.Trim().Length == 0) || 
                (loginField.Text == userLoginFieldDefault || loginField.Text.Trim().Length == 0) ||
                (passField.Text == passwordFieldDefault || passField.Text.Trim().Length == 0 ) ||
                rePassField.Text == passwordFieldDefault || rePassField.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ошибка в текстовых полях");
                return;
            }
            if (passField.Text != rePassField.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            if (isUserExists()) return;

            DB db = new DB();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `users` (`id`, `name`, `login`, `password`) VALUES (NULL, @name, @login, @password)", 
                                                db.GetConnection());

            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;

            db.openConnection();

            if(cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Всё норм");
                if (loginForm == null)
                {
                    loginForm = new LoginForm(this);
                }
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Шото не то");
            }

            db.closeConnection();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует");
                return true;
            }
            else return false;
        }

        private void loginButton_MouseEnter(object sender, EventArgs e)
        {
            loginButton.Font = new Font(loginButton.Font.FontFamily, 11f, loginButton.Font.Style);
        }

        private void loginButton_MouseLeave(object sender, EventArgs e)
        {
            loginButton.Font = new Font(loginButton.Font.FontFamily, 10f, loginButton.Font.Style);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (loginForm == null)
            {
                loginForm = new LoginForm(this);
            }
            loginForm.Show();
            this.Hide();
        }
    }
}
