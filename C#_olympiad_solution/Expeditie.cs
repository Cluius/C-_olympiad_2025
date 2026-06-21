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
        private double cargo=0;
        private int food=0;
        private int gold = 0;
        private string currBoatLocation;
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
                
            }
            boat.Location = destination;
        }
        private void GenerateIslandLabels()
        {
            foreach(Island isl in Database.getIslands().Values)
            {
                Label lbl = new Label();
                lbl.Name= "label" + isl.Id.ToString();
                lbl.Text = isl.Name;
                lbl.Location = new Point(findIslandById(isl.Id).Location.X-60,findIslandById(isl.Id).Location.Y+20);
                lbl.Visible = false;
                lbl.BackColor = Color.Transparent;
                lbl.AutoSize = true;
                this.Controls.Add(lbl);
                lbl.BringToFront();
            }
        }
        private void gameOver(Exploratori form2)
        {
            form2.Show();
            this.Close();
        }
        private void loadIslandData(string islandFile,int nrexp,Exploratori form2)
        {
            foreach (string line in System.IO.File.ReadLines(islandFile))
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
                    start.Click += (s, e) =>
                    {
                        MessageBox.Show("Start point");
                    };
                    continue;
                }
                int islandId = int.Parse(data[0]);
                PictureBox currIsland = findIslandById(islandId);
                currIsland.Click += (s, e) =>
                {
                    if (Database.checkIslandStatus(islandId))
                    {
                        int TripLengthDays = Database.getDistance(currBoatLocation, islandId) / 100;
                        int foodConsumed = TripLengthDays * 2 * nrexp;
                        if (foodConsumed <= food)
                        {
                            Database.setIslandVisited(islandId);
                            boatglide(currIsland.Location);
                            this.Controls.Find("label" + islandId.ToString(), true)[0].Visible = true;
                            MessageBox.Show("Ai navigat " + TripLengthDays.ToString() + "zile si ai consumat " + foodConsumed.ToString() +" kg de hrana");
                            currBoatLocation = Database.getIslandNameById(islandId);
                            food -= foodConsumed;
                            if (islandId > 1 && islandId < 8)
                            {
                                if (cargo + (double)((nrexp * 200 - food)/1000) <= 100)
                                {
                                    food = nrexp * 200;
                                }
                                else
                                {
                                    food+= (int)((100 - cargo) * 1000);
                                }
                            }
                            if (Database.isIslandInfected(islandId))
                            {
                                nrexp /= 2;
                                if (nrexp < 30)
                                {
                                    MessageBox.Show("Esuat, numar insuficient de exploratori");
                                    gameOver(form2);
                                }
                            }
                            cargo = (double)(nrexp * 90 + food + gold)/1000;
                            if (Database.islandHasGold(islandId))
                            {
                                int goldOnIsland= Random.Shared.Next(10, 100);
                                if (cargo + goldOnIsland > 100)
                                {
                                    gold+= (100 - (int)cargo)*1000;
                                    MessageBox.Show(Database.getIslandDesc(islandId) + "\nPe insula sunt " + goldOnIsland.ToString() + " tone de bogatii\nExploratorii incarca "+(100-cargo).ToString()+" tone de bogatii");
                                    cargo = 100;

                                }
                                else
                                {
                                    gold += goldOnIsland*1000;
                                    MessageBox.Show(Database.getIslandDesc(islandId) + "\nPe insula sunt " + goldOnIsland.ToString() + " tone de bogatii\nExploratorii incarca " + goldOnIsland.ToString() + " tone de bogatii");
                                    cargo += goldOnIsland;
                                }
                            }
                            else
                            {
                                MessageBox.Show(Database.getIslandDesc(islandId) + "\nPe insula sunt 0 tone de bogatii\nExploratorii incarca 0 tone de bogatii");
                            }
                            label1.Text = nrexp.ToString();
                            label2.Text = (food/1000.0).ToString();
                            label3.Text = (gold/1000.0).ToString();
                            label4.Text = cargo.ToString();

                        }
                        else
                        {
                            MessageBox.Show("Esuat, hrana insuficienta");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Interzis!");
                    }
                };
                currIsland.Location = new Point(int.Parse(data[2]), int.Parse(data[3]));
                Database.addIsland(islandId, data[1], int.Parse(data[4]), int.Parse(data[5]), data[6]);
            }
        }
        private void loadDistanceData(string distanceFile)
        {
            foreach (string line in System.IO.File.ReadLines(distanceFile))
            {
                if (line.StartsWith("Dist"))
                {
                    continue;
                }
                string[] data = line.Split('#');
                for (int i = 1; i < data.Length; i++)
                {
                    if (data[i] == "x")
                    {
                        continue;
                    }
                    Database.addDistance(data[0], i, int.Parse(data[i]));
                }
            }
        }
        public Expeditie(int nrexp,Exploratori form2)
        {
            InitializeComponent();
            food = 200 * nrexp;
            cargo = (90 * nrexp + cargo + food)/1000.0;
            currBoatLocation = "Cadiz";
            label1.Text = nrexp.ToString();
            label2.Text = (food/1000.0).ToString();
            label3.Text=0.ToString();
            label4.Text = cargo.ToString();
            string islandFile = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.FullName,"Resources","insule.txt");
            string distanceFile = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.FullName, "Resources", "distante.txt");
            loadIslandData(islandFile, nrexp,form2);
            loadDistanceData(distanceFile);
            GenerateIslandLabels();
        }
    }
}
