using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Dodge_Example
{
    class Spaceship
    {
        // declare fields to use in the class

        public int x, y, width, height;//variables for the rectangle
        public Image spaceship;//variable for the planet's image
        public int rotationAngle;
        public Matrix matrix;
        Point centre;

        public Rectangle spaceRec;//variable for a rectangle to place our image in

        //Create a constructor (initialises the values of the fields)
        public Spaceship()
        {
            x = 10;
            y = 300;
            width = 100;
            height = 130;
            rotationAngle = 0;
            spaceship = Properties.Resources.cybertron;

            spaceRec = new Rectangle(x, y, width, height);
        }
        //methods
        public void DrawSpaceship(Graphics g)
        {

            //find the centre point of spaceRec
            centre = new Point(spaceRec.X + width / 2, spaceRec.Y + width / 2);
            //instantiate a Matrix object called matrix
            matrix = new Matrix();
            //rotate the matrix (spaceRec) about its centre
            matrix.RotateAt(rotationAngle, centre);
            //Set the current draw location to the rotated matrix point
            g.Transform = matrix;
            //draw the spaceship


            g.DrawImage(spaceship, spaceRec);
        }

        public void RotateSpaceship(int mouseX, int mouseY)
        {
            rotationAngle = (mouseX/10) - 30 -  (spaceRec.X/10);

        }
        public void MoveSpaceship(string move)
        {
            spaceRec.Location = new Point(x, y);

            if (move == "right")
            {
                if (spaceRec.Location.X > 450) // is spaceship within 50 of right side
                {

                    x = 450;
                   // spaceRec.Location = new Point(x, y);
                }
                else
                {
                    x += 10;
                   // spaceRec.Location = new Point(x, y);
                }

            }

            if (move == "left")
            {
                if (spaceRec.Location.X < 10) // is spaceship within 10 of left side
                {

                    x = 10;
                   // spaceRec.Location = new Point(x, y);
                }
                else
                {
                    x -= 10;
                    //spaceRec.Location = new Point(x, y);
                }

            }


            if (move == "transform")
            {
                spaceship = Properties.Resources.cannon;

            }
            if (move == "transform2")
            {
                spaceship = Properties.Resources.Galvatron;

            }



        }

    }
}
