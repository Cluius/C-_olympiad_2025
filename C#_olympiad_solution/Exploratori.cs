using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C__olympiad_solution
{
    public partial class Exploratori : Form
    {
        public Exploratori()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int nrexp))
            {
                if (nrexp > 30 && nrexp < 200)
                {
                    Expeditie form3 = new Expeditie();
                    this.Hide();
                    form3.Show();
                }
                else
                {
                    MessageBox.Show("Numarul de exploratori trebuie sa fie intre 30 si 200!");
                    textBox1.Clear();

                }
            }
        }
    }
}
