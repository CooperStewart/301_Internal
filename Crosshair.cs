﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Dodge_Example
{

    class Crosshair
    {
        // declare fields to use in the class

        public int x, y, width, height;//variables for the rectangle
        public Image crosshair;//variable for the planet's image

        public Rectangle spaceRec;//variable for a rectangle to place our image in

        //Create a constructor (initialises the values of the fields)
        public Crosshair()
        {
            x = 10;
            y = 360;
            width = 40;
            height = 40;
            crosshair = Properties.Resources.crosshair;
            spaceRec = new Rectangle(x, y, width, height);
        }
        //methods
        public void drawCrosshair(Graphics g)
        {
            g.DrawImage(crosshair, spaceRec);
        }

        public void moveCrosshair(int mouseX, int mouseY)
        {
            spaceRec.X = mouseX - (spaceRec.Width / 2);
            spaceRec.Y = mouseY - (spaceRec.Height / 2);

        }

    }
}

    


