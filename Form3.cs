using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZombieGUIGame.BL;
using ZombieGUIGame.DL;

namespace ZombieGUIGame
{


    public partial class Form3 : Form
    {
        //Some Variable Are Declared 
        bool GoLeft, GoRight, GoUp, GoDown, GameOver;
        //Initially Over Player Face is  Up But It face changed when it Changes Direction
        string PlayerFacing = "up";
        int Health = 100;
        int speed = 15;
        int Ammo = 10;
        int zombieSpeed = 4;
        int Score = 0;
        public Form3()
        {
           
            InitializeComponent();
            RestartGame();
            label2.Text = Properties.Settings.Default.HighScore2;
        }

        private void back_Click(object sender, EventArgs e)
        {
            start1 start = new start1();
            start.Show();
            Visible = false;
        }

        private void IsKeyDown(object sender, KeyEventArgs e)
        {
            if (GameOver == true)
            {
                return;
            }


            if (e.KeyCode == Keys.Left)
            {
                GoLeft = true;
                PlayerFacing = "left";
                Shooter.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                GoRight = true;
                PlayerFacing = "right";
                Shooter.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                GoUp = true;
                PlayerFacing = "up";
                Shooter.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                GoDown = true;
                PlayerFacing = "down";
                Shooter.Image = Properties.Resources.down;
            }


        }

        private void IsKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                GoLeft = false;

            }

            if (e.KeyCode == Keys.Right)
            {
                GoRight = false;

            }

            if (e.KeyCode == Keys.Up)
            {
                GoUp = false;

            }

            if (e.KeyCode == Keys.Down)
            {
                GoDown = false;
            }
            if (e.KeyCode == Keys.Space && Ammo > 0 && GameOver == false)
            {
                Ammo--;
                Bullet bullet = new Bullet();
                bullet.shootBullet(this, PlayerFacing, Shooter.Left, Shooter.Top, Shooter.Width, Shooter.Height);

                if (Ammo < 1)
                {
                    Ammo makeammo = new Ammo();
                    makeammo.DroppingAmmo(this);
                }
            }

            if (e.KeyCode == Keys.Enter && GameOver == true)
            {
                RestartGame();
            }

        }

        private void GameTimmerEvent(object sender, EventArgs e)
        {
            // Player Health control 
            if (Health > 1)
            {
                PlayerHealth.Value = Health;
            }
            else
            {
                GameOver = true;
                Shooter.Image = Properties.Resources.dead;
                GameLoop.Stop();

                // High Score Updated and Display 
                int tempscore = Int32.Parse(label2.Text);
                if (Score > tempscore)
                {
                    label2.Text = Score.ToString();
                    Properties.Settings.Default.HighScore2 = label2.Text;
                    Properties.Settings.Default.Save();
                }
                MessageBox.Show("The Game Is Over Press Enter to  Restart The Game........");

                back.Visible = true;
            }
            ammo.Text = "Ammo: " + Ammo;
            Kill.Text = "Kills: " + Score;

            if (GoLeft == true && Shooter.Left > 0)
            {
                Shooter.Left -= speed;
            }
            if (GoRight == true && Shooter.Left + Shooter.Width < this.ClientSize.Width)
            {
                Shooter.Left += speed;
            }
            if (GoUp == true && Shooter.Top > 68)
            {
                Shooter.Top -= speed;
            }
            if (GoDown == true && Shooter.Top + Shooter.Height < this.ClientSize.Height)
            {
                Shooter.Top += speed;
            }






            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (Shooter.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        Ammo += 10;

                    }
                }


                if (x is PictureBox && (string)x.Tag == "zombie")
                {

                    if (Shooter.Bounds.IntersectsWith(x.Bounds))
                    {
                        Health -= 3;
                    }


                    if (x.Left > Shooter.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (x.Left < Shooter.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (x.Top > Shooter.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < Shooter.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }

                }



                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            Score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            ZoombiesDL zom = new ZoombiesDL();
                            Zoombies z = new Zoombies();
                            ZoombiesDL.zombiesList.Remove(((PictureBox)x));
                            z.MakeZoombies(this);
                        }
                    }
                }



            }



        }

        public void RestartGame()
        {
            ZoombiesDL zomb = new ZoombiesDL();
            Zoombies Zombi = new Zoombies();
            Shooter.Image = Properties.Resources.up;

            foreach (PictureBox i in ZoombiesDL.zombiesList)
            {
                this.Controls.Remove(i);
            }

            ZoombiesDL.zombiesList.Clear();

            for (int i = 0; i < 5; i++)
            {
                Zombi.MakeZoombies(this);
            }



            GoRight = false;
            GoUp = false;
            GoLeft = false;
            GoDown = false;
            GameOver = false;

           

            back.Visible = false;


            Health = 100;
            Score = 0;
            Ammo = 15;

            GameLoop.Start();
        }
    }
}
