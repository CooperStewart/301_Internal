using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dodge_Example
{
    class ball
    {
        // declare fields to use in the class
        public int x, y, width, height;//variables for the rectangle
        public Image planetImage;//variable for the planet's image

        public Rectangle ballRec;//variable for a rectangle to place our image in
        public int score;
        //Create a constructor (initialises the values of the fields)
        public ball(int spacing)
        {
            x = spacing;
            y = -100;
            width = 20;
            height = 20;
            //planetImage contains the plane1.png image
            planetImage = Properties.Resources.ball;
            ballRec = new Rectangle(x, y, width, height);
        }

        public void DrawBall(Graphics g)
        {
            ballRec = new Rectangle(x, y, width, height);
            g.DrawImage(planetImage, ballRec);
        }

        public void MoveBall(int mouseX, int mouseY)
        {
            ballRec.Location = new Point(x, y);

        }
    }


}
