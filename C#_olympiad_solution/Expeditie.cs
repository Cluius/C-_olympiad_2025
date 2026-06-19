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
        private PictureBox findIslandById(int id)
        {
            Control[] matches = this.Controls.Find("insula" + id.ToString(), true);
            return matches.Length>0 ? (PictureBox)matches[0] : null;
        }
        private void boatglide(Point destination)
        {
            int addToX = (destination.X - boat.Location.X) / 20;
            int addToY = (destination.Y - boat.Location.Y) / 20;
            for (int i = 0; i < 20; i++)
            {
                boat.Location = new Point(boat.Location.X+addToX, boat.Location.Y+addToY);
                boat.BringToFront();
                this.Refresh();
                System.Threading.Thread.Sleep(15);
            }
            boat.Location = destination;
        }
        public Expeditie(int nrexp)
        {
            InitializeComponent();
            food = 200 * nrexp;
            cargo = 90 * nrexp + cargo + food + gold;
            label1.Text = nrexp.ToString();
            label2.Text = food.ToString();
            label3.Text=0.ToString();
            label4.Text = cargo.ToString();
            string islandFile = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.FullName,"Resources","insule.txt");
            foreach(string line in System.IO.File.ReadLines(islandFile))
            {
                if (line.StartsWith("Id")) 
                {
                    continue;
                }
                string[] data = line.Split("#");
                if (line.StartsWith("1#"))
                {
                    start.Location = new Point(int.Parse(data[2]), int.Parse(data[3]));
                    boat.Location = start.Location;
                    start.Click+=(s, e) =>
                    {
                        MessageBox.Show("Start point");
                    };
                    continue;
                }
                int islandId= int.Parse(data[0]);
                PictureBox currIsland=findIslandById(islandId);
                currIsland.Click += (s, e) =>
                {
                    if(Database.checkIslandStatus(islandId))
                    {
                        Database.setIslandVisited(islandId);
                        boatglide(currIsland.Location);
                    }
                    else
                    {
                        MessageBox.Show("Interzis!");
                    }
                };
                currIsland.Location = new Point(int.Parse(data[2]), int.Parse(data[3]));
                Database.addIsland(islandId, data[1], int.Parse(data[4]), int.Parse(data[5]));
            }
        }
    }
}
