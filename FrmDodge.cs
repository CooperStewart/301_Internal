﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Dodge_Example
{
    public partial class FrmDodge : Form
    {
        Graphics g; //declare a graphics object called g
        // declare space for an array of 7 objects called planet 
        Planet[] planet = new Planet[5];
        Random yspeed = new Random();
        Random ypos = new Random();
        Spaceship spaceship = new Spaceship();
        Crosshair crosshair = new Crosshair();

        bool left, right;
        string move;
        int score, lives, Wait;
        public FrmDodge()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                int x = 10 + (i * 110);
                planet[i] = new Planet(x);
            }

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });



        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            //get the graphics used to paint on the panel control
            g = e.Graphics;
            //call the Planet class's DrawPlanet method to draw the image planet1 
            for (int i = 0; i < 5; i++)
            {
                // generate a random number from 5 to 20 and put it in rndmspeed
                int rndmspeed = yspeed.Next(1, 3)+score/10;
                planet[i].y += rndmspeed;

                //call the Planet class's drawPlanet method to draw the images
                planet[i].DrawPlanet(g);
               
            }
            spaceship.DrawSpaceship(g);
            crosshair.drawSpaceship(g);


        }

        private void FrmDodge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = true; }
            if (e.KeyData == Keys.Right) { right = true; }
        }

        private void FrmDodge_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }

        }

        private void TmrShip_Tick(object sender, EventArgs e)
        {
            Invalidate();
            label5.Text = Wait.ToString();
            if (right) // if right arrow key pressed
            {
                move = "right";
                spaceship.MoveSpaceship(move);
            }
            if (left) // if left arrow key pressed
            {
                move = "left";
                spaceship.MoveSpaceship(move);
            }
        }

        private void FrmDodge_Load(object sender, EventArgs e)
        {
            lives = int.Parse(txtLives.Text);// pass lives entered from textbox to lives variable
            MessageBox.Show("Use the left and right arrow keys to move the spaceship. \n Don't get hit by the planets! \n Every planet that gets past scores a point. \n If a planet hits a spaceship a life is lost! \n \n Enter your Name press tab and enter the number of lives \n Click Start to begin", "Game Instructions");
            txtName.Focus();

        }

        private void MnuStart_Click(object sender, EventArgs e)
        {
            score = 0;
            lblScore.Text = score.ToString();
            lives = int.Parse(txtLives.Text);// pass lives entered from textbox to lives variable
            TmrPlanet.Enabled = true;
            TmrShip.Enabled = true;

        }

        private void MnuStop_Click(object sender, EventArgs e)
        {
            TmrShip.Enabled = false;
            TmrPlanet.Enabled = false;

        }

        private void MnuStart_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void FrmDodge_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            crosshair.moveSpaceship(e.X, e.Y);

        }

        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
                            int rndmypos = ypos.Next(-10, 100);

            for (int i = 0; i < 5; i++)
            {
                if (crosshair.spaceRec.IntersectsWith(planet[i].planetRec))
                {
                    planet[i].planetImage = Properties.Resources.explosion;
                    score += 1;
                    lblScore.Text = score.ToString();
                    planet[i].y = rndmypos;
                    planet[i].planetImage = Properties.Resources.planet1;
                    planet[i].height =20;
                    planet[i].width =20;

                }
            }
        }

        private void TmrWait_Tick(object sender, EventArgs e)
        {
            Wait += 1;
}

        private void TmrPlanet_Tick(object sender, EventArgs e)
        {
            
           // score = 0;
            for (int i = 0; i < 5; i++)
            {
                planet[i].MovePlanet();
                planet[i].height = planet[i].y;
                planet[i].width = planet[i].y;
              

                if (spaceship.spaceRec.IntersectsWith(planet[i].planetRec))
                {
                   
                //reset planet[i] back to top of panel
                planet[i].y = 30; // set  y value of planetRec
                lives -= 1;// lose a life
                txtLives.Text = lives.ToString();// display number of lives
                CheckLives();
            
                }
                //if a planet reaches the bottom of the Game Area reposition it at the top
                if (planet[i].y >= PnlGame.Height)
                {
                    lblScore.Text = score.ToString();// display score
                    planet[i].y = 30;
                    planet[i].height = 20;
                    planet[i].width = 20;


                }
                // score += planet[i].score;// get score from Planet class (in movePlanet method)
                //lblScore.Text = score.ToString();// display score

            }

            PnlGame.Invalidate();//makes the paint event fire to redraw the panel
        }
        private void CheckLives()
        {
            if (lives == 0)
            {
                TmrPlanet.Enabled = false;
                TmrShip.Enabled = false;
                MessageBox.Show("Game Over");

            }
        }

    }
}
