﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Dodge_Example
{
    class Planet
    {
        // declare fields to use in the class
        public int x, y, width, height;//variables for the rectangle
        public Image planetImage;//variable for the planet's image

        public Rectangle planetRec;//variable for a rectangle to place our image in
        public int score;
        //Create a constructor (initialises the values of the fields)
        public Planet(int spacing)
        {
            x = spacing;
            y = -100;
            width = 40;
            height = 20;
            //planetImage contains the plane1.png image
            planetImage = Properties.Resources.unnamed;
            planetRec = new Rectangle(x, y, width, height);
        }
        // Methods for the Planet class
        public void DrawPlanet(Graphics g)
        {
            planetRec = new Rectangle(x, y, width, height);
            g.DrawImage(planetImage, planetRec);
        }
        public void MovePlanet()
        {
            planetRec.Location = new Point(x, y);
        }

        public void DestroyPlanet()
        {
            planetImage = Properties.Resources.explosion;
        }
    }
}
