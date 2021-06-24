using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _2018_Lists_Tutorial
{
    class Missile
    {
        public int x, y, width, height;
        public int missileRotated;
        public double xSpeed, ySpeed;
        public Image missile;//variable for the missile's image
        public Rectangle missileRec;//variable for a rectangle to place our image in
        public Matrix matrixMissile;//matrix to enable us to rotate missile in the same angle as the spaceship
        Point centreMissile;
        // in the following constructor we pass in the values of spaceRec and the rotation angle of the spaceship
        // this gives us the position of the spaceship which we can then use to place the
        // missile where the spaceship is located and at the correct angle
        public Missile(Rectangle spaceRec, int missileRotate)
        {
            // if (missileRotate > 360) { missileRotate -= 360; } //if rotation angle is greater than 360 (ie. 361) take away 360 (so it equals 1)
            // if (missileRotate < 0) { missileRotate += 360; }//if rotation angle is negative, make it positive
            width = 10;
            height = 20;
            missile = Image.FromFile("missile_small.png");
            missileRec = new Rectangle(x, y, width, height);
            xSpeed = 30 * (Math.Cos((missileRotate - 90) * Math.PI / 180));
            ySpeed = 30 * (Math.Sin((missileRotate + 90) * Math.PI / 180));
            x = spaceRec.X + spaceRec.Width / 2; // move missile to middle of spaceship
            y = spaceRec.Y + spaceRec.Height / 2;
            missileRotated = missileRotate;

        }

        public void draw(Graphics g)
        {

            centreMissile = new Point(x, y);
            matrixMissile = new Matrix();
            matrixMissile.RotateAt(missileRotated, centreMissile);
            g.Transform = matrixMissile;
            g.DrawImage(missile, missileRec);

        }
        public void moveMissile(Graphics g)
        {
            x += (int)xSpeed;
            y -= (int)ySpeed;
            missileRec.Location = new Point(x, y);

        }
    }
}
