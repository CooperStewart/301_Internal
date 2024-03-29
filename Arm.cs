﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Dodge_Example
{
    class Arm
    {
        // declare fields to use in the class

        public int x, y, width, height;//variables for the rectangle
        public Image arm;//variable for the planet's image
        public int rotationAngle;
        public Matrix matrix;
        Point centre;

        public Rectangle spaceRec;//variable for a rectangle to place our image in

        //Create a constructor (initialises the values of the fields)
        public Arm()
        {
            x = 65;
            y = 299;
            width = 50;
            height = 100;
            rotationAngle = 0;
            arm = Properties.Resources.Arm;

            spaceRec = new Rectangle(x, y, width, height);
        }
        //methods
        public void DrawArm(Graphics g)
        {

            //find the centre point of spaceRec
            centre = new Point(spaceRec.X + width / 2, spaceRec.Y + width / 2);
            //instantiate a Matrix object called matrix
            matrix = new Matrix();
            //rotate the matrix (spaceRec) about its centre
            matrix.RotateAt(rotationAngle, centre);
            //Set the current draw location to the rotated matrix point
            g.Transform = matrix;
            //draw the arm


            g.DrawImage(arm, spaceRec);
        }

        public void RotateArm(int mouseX, int mouseY)
        {
            rotationAngle = (mouseX / 10) + 170 - (spaceRec.X / 10);

        }
        public void MoveArm(string move)
        {
            spaceRec.Location = new Point(x, y);

            if (move == "right")
            {
                if (spaceRec.Location.X > 450) // is arm within 50 of right side
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
                if (spaceRec.Location.X < 10) // is arm within 10 of left side
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


           


        }

    }
}
