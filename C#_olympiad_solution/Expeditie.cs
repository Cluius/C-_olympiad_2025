using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C__olympiad_solution
{
    public partial class Expeditie : Form
    {
        private int cargo=0;
        private int food=0;
        private int gold = 0;
        public Expeditie(int nrexp)
        {
            InitializeComponent();
            food = 200 * nrexp;
            cargo = 90 * nrexp + cargo + food + gold;
            label1.Text = nrexp.ToString();
            label2.Text = food.ToString();
            label3.Text=0.ToString();
            label4.Text = cargo.ToString();
        }
    }
}
