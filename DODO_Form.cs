using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSQL
{
    public class DODO_Form : Form
    {
        protected Point lastPoint;

        protected Color normalFieldColor = Color.Black;
        protected Color notEnterFieldColor = Color.Gray;

        protected void SetTextBox(TextBox textBox, string defaultText)
        {
            textBox.Text = defaultText;
            textBox.ForeColor = notEnterFieldColor;
        }
        protected void TextBoxEnter(TextBox textBox, string DefaultText, bool locked = false)
        {
            if (textBox.Text == DefaultText)
            {
                textBox.Text = String.Empty;
            }
            textBox.ForeColor = normalFieldColor;

            if(locked) textBox.UseSystemPasswordChar = true;
        }
        protected void TextBoxLeave(TextBox textBox, string DefaultText, bool unlocked = false)
        {
            if (textBox.Text == string.Empty)
            {
                textBox.Text = DefaultText;
                textBox.ForeColor = notEnterFieldColor;
                if (unlocked) textBox.UseSystemPasswordChar = false;
            }
        }
        protected Size Sizer(Control control, float modifer, bool divide = false)
        {
            var temp = control.Size;
            if (!divide)
            {
                temp.Width = (int)Math.Round(temp.Width * modifer);
                temp.Height = (int)Math.Round(temp.Height * modifer);
            }
            else
            {
                temp.Width = (int)Math.Round(temp.Width / modifer);
                temp.Height = (int)Math.Round(temp.Height / modifer);
            }
            return temp;
            
        }

    }
}
