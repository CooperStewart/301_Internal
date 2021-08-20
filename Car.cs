using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Dodge_Example
{
    class Car
    {
        // declare fields to use in the class
        public int x, y, width, height;//variables for the rectangle
        public Image CarImage;//variable for the Car's image

        public Rectangle CarRec;//variable for a rectangle to place our image in
        public int score;
        //Create a constructor (initialises the values of the fields)
        public Car(int spacing)
        {
            x = spacing;
            y = -100;
            width = 60;
            height = 20;
            //CarImage contains the plane1.png image
            CarImage = Properties.Resources.Car;
            CarRec = new Rectangle(x, y, width, height);
        }
        // Methods for the Car class
        public void DrawCar(Graphics g)
        {
            CarRec = new Rectangle(x, y, width, height);
            g.DrawImage(CarImage, CarRec);
        }
        public void MoveCar()
        {
            CarRec.Location = new Point(x, y);
        }

        public void DestroyCar()
        {
            CarImage = Properties.Resources.explosion;
        }
    }
}
