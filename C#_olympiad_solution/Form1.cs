using System.IO;
using System.Drawing;
namespace C__olympiad_solution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Database.validateUser(UserBox.Text, PasswordBox.Text))
            {
                Exploratori formexp = new Exploratori();
                this.Hide();
                formexp.Show();
            }
            else
            {
                MessageBox.Show("Ceva nu a mers bine, mai incercati!");
                UserBox.Clear();
                PasswordBox.Clear();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Top -= 2;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Top += 2;
            pictureBox1.BorderStyle = BorderStyle.None;
        }

        private void UserBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                PasswordBox.Focus();
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                pictureBox1_Click(sender, e);
            }
        }
    }
}
