using System;
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
        // declare space for an array of 7 objects called Car 
        Car[] Car = new Car[4];
        ball[] ball = new ball[4];
        Random yspeed = new Random();
        Random ypos = new Random();
        Player Player = new Player();
        Crosshair crosshair = new Crosshair();
        Arm arm = new Arm();

        bool left, right, transform, cooldown;
        string move;
        int score, lives, Wait, Cooldown;
        public FrmDodge()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                int x = 10 + (i * 170);
                Car[i] = new Car(x);
                ball[i] = new ball(x);

            }

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });



        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            //get the graphics used to paint on the panel control
            g = e.Graphics;
            //call the Car class's DrawCar method to draw the image Car1 
            for (int i = 0; i < 4; i++)
            {
                // generate a random number from 5 to 20 and put it in rndmspeed
                int rndmspeed = yspeed.Next(1, 3)+score/20;
                Car[i].y += rndmspeed;
                ball[i].y += rndmspeed;



                //call the Car class's drawCar method to draw the images
                ball[i].DrawBall(g);
                Car[i].DrawCar(g);

            }
            crosshair.drawCrosshair(g);
            Player.DrawPlayer(g);
            if (transform == false)
            {
                arm.DrawArm(g);
            }


        }

        private void FrmDodge_KeyDown(object sender, KeyEventArgs e)
        {
            if (transform == false)
            {
                if (e.KeyData == Keys.Left) { left = true; }
                if (e.KeyData == Keys.A) { left = true; }
                if (e.KeyData == Keys.Right) { right = true; }
                if (e.KeyData == Keys.D) { right = true; }
            }
            if (e.KeyData == Keys.Space) if (Wait<20)
                {
                    if (cooldown == false)
                    {
                        { transform = true; TmrWait.Enabled = true; }
                    }
                }
                else
                {
                    transform = false; if (cooldown == false) { Wait +=30;  } cooldown = true;
                }

        }

        private void FrmDodge_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.A) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }
            if (e.KeyData == Keys.D) { right = false; }
            if (e.KeyData == Keys.Space) { transform = false; if (cooldown == false) { Wait += 30; } cooldown = true; }


        }

        private void TmrShip_Tick(object sender, EventArgs e)
        {

            Invalidate();

            if (right) // if right arrow key pressed
            {
                move = "right";
                Player.MovePlayer(move);
                arm.MoveArm(move);
            }
            if (left) // if left arrow key pressed
            {
                move = "left";
                Player.MovePlayer(move);
                arm.MoveArm(move);
            }
            if (transform) // if left arrow key pressed
            {
               
                    move = "transform";
                    Player.MovePlayer(move);
                    right = false;
                    left = false;
                

            }
            else
            {
                move = "transform2";
                Player.MovePlayer(move);
            }
        }

        private void FrmDodge_Load(object sender, EventArgs e)
        {
            lives = int.Parse(txtLives.Text);// pass lives entered from textbox to lives variable
            MessageBox.Show("Use the left and right arrow keys to move the Player. \n Don't get hit by the Cars! \n Every Car that gets past scores a point. \n If a Car hits a Player a life is lost! \n \n Enter your Name press tab and enter the number of lives \n Click Start to begin", "Game Instructions");

        }

        private void MnuStart_Click(object sender, EventArgs e)
        {
            score = 0;
            lblScore.Text = score.ToString();
            lives = 5;
            TmrCar.Enabled = true;
            TmrShip.Enabled = true;

        }

        private void MnuStop_Click(object sender, EventArgs e)
        {
            TmrShip.Enabled = false;
            TmrCar.Enabled = false;

        }

        private void MnuStart_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            score = 0;
            lblScore.Text = score.ToString();
            lives = 5;
            TmrCar.Enabled = true;
            TmrShip.Enabled = true;
            label4.Visible = false;
            pictureBox1.Visible = false;
            textBox1.Visible = false;
        }

        private void TmrCar_Tick(object sender, EventArgs e)
        {
            if (cooldown == false)
            {
                TransformBar.Width = 32 * Wait;
            }
            else
            {
                TransformBar.Width = 13 * Wait;
            }



            // score = 0;
            for (int i = 0; i < 4; i++)
            {
                Car[i].MoveCar();
                Car[i].height = 2 * Car[i].y / 5 + 20;
                Car[i].width = 3 * Car[i].y / 5 + 30;
                int rndmy = ypos.Next(0, 200);

                if (ball[i].y > 100)
                {
                    if (ball[i].y < 250)
                    {
                        ball[i].x += (Player.spaceRec.X - ball[i].x + 20) / 20;
                    }
                    else
                    {
                        ball[i].x += (Player.spaceRec.X - ball[i].x + 20) / 25;
                    }
                    ball[i].y += 5;
                }
                else
                {
                    ball[i].x = Car[i].x + 10;
                    ball[i].y = Car[i].y;
                }

                if (Player.spaceRec.IntersectsWith(ball[i].ballRec))
                {

                    //reset Car[i] back to top of panel
                    ball[i].y = 30; // set  y value of CarRec
                    lives -= 1;// lose a life
                    txtLives.Text = lives.ToString();// display number of lives
                    CheckLives();

                }
                //if a Car reaches the bottom of the Game Area reposition it at the top
                if (Car[i].y >= PnlGame.Height)
                {
                    //reset Car[i] back to top of panel
                    Car[i].y = 30; // set  y value of CarRec
                    lives -= 1;// lose a life
                    txtLives.Text = lives.ToString();// display number of lives
                    CheckLives();


                }
                if (ball[i].y >= PnlGame.Height)
                {
                    //reset Car[i] back to top of panel
                    ball[i].y = Car[i].y; // set  y value of CarRec



                }
                // score += Car[i].score;// get score from Car class (in moveCar method)
                //lblScore.Text = score.ToString();// display score

            }

            PnlGame.Invalidate();//makes the paint event fire to redraw the panel
        }

        private void FrmDodge_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            arm.RotateArm(e.X, e.Y);

            crosshair.moveCrosshair(e.X, e.Y);
            for (int i = 0; i < 4; i++)
            {
                ball[i].MoveBall(e.X, e.Y);
            }
            
        }

        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
                            int rndmypos = ypos.Next(-100, 0);

            for (int i = 0; i < 4; i++)
            {
                if (crosshair.spaceRec.IntersectsWith(Car[i].CarRec))
                {
                    if (transform == false)
                    {
                        score += 1;
                        lblScore.Text = score.ToString();
                        Car[i].y = rndmypos;
                        Car[i].height = 20;
                        Car[i].width = 20;
                    }
                    else
                    {
                        score += 3;
                        lblScore.Text = score.ToString();
                        Car[i].y = rndmypos;
                        Car[i].height = 20;
                        Car[i].width = 20;
                        if (crosshair.spaceRec.IntersectsWith(Car[0].CarRec))
                        { }
                        else
                        {
                            Car[i - 1].y = rndmypos;
                            Car[i - 1].height = 20;
                            Car[i - 1].width = 20;
                        }
                        if (crosshair.spaceRec.IntersectsWith(Car[3].CarRec))
                        { }
                        else { 
                            Car[i + 1].y = rndmypos;
                            Car[i + 1].height = 20;
                            Car[i + 1].width = 20;
                        }
                    }

                }
            }
        }

        private void TmrWait_Tick(object sender, EventArgs e)
        {
            if (transform==true) { Wait += 1; }
            else
            {
                if (Wait > 0)
                {
                    Wait -= 1;
                }
            }
            
            if(Wait == 0)
            {
                cooldown = false;
            }

            if (Wait > 20)
            {
                cooldown = true;
            }

            if(cooldown == true)
            {
                transform = false;
            }
}

        private void TmrPlanet_Tick(object sender, EventArgs e)
        {

           
        }
        private void CheckLives()
        {
            if (lives == 0)
            {
                TmrCar.Enabled = false;
                TmrShip.Enabled = false;
                MessageBox.Show("Game Over");
                label4.Text = score.ToString();
                pictureBox1.Visible = true;
                label4.Visible = true;
                Energy.Visible = false;
                label1.Visible = true;
                label1.Text = "Cooper";
            }
        }

    }
}
