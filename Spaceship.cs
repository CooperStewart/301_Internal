﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Dodge_Example
{
    class Spaceship
    {
        // declare fields to use in the class

        public int x, y, width, height;//variables for the rectangle
        public Image spaceship;//variable for the planet's image

        public Rectangle spaceRec;//variable for a rectangle to place our image in

        //Create a constructor (initialises the values of the fields)
        public Spaceship()
        {
            x = 10;
            y = 300;
            width = 100;
            height = 130;
            spaceship = Properties.Resources.Galvatron;
            spaceRec = new Rectangle(x, y, width, height);
        }
        //methods
        public void DrawSpaceship(Graphics g)
        {

            g.DrawImage(spaceship, spaceRec);
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
