using System;
using System.Collections.Generic;
using System.Text;
using SharpGL;
using System.Drawing;

namespace _19127409_Lab03
{
    class Circle : Shape
    {
        public Circle(Point start, Point end, float thickness, Color color)
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thickness;
            this.drawColor = color;

            // Caculate radius
            int r = (int)(Math.Sqrt(Math.Pow(pEnd.X - pStart.X, 2) + Math.Pow(pEnd.Y - pStart.Y, 2)));
            int x = 0;
            int y = r;
            int p0 = 1 - r;

            // Add point in Egdes list
            this.Egdes.Add(new Point(start.X + x, start.Y + y));
            this.Egdes.Add(new Point(start.X + x, start.Y - y));
            this.Egdes.Add(new Point(start.X - x, start.Y + y));
            this.Egdes.Add(new Point(start.X - x, start.Y - y));
            while (x < y)
            {
                if (p0 < 0)
                {
                    p0 += 2 * x + 3;
                    x += 1;
                    // Octant 1
                    this.Egdes.Add(new Point(start.X + x,start.Y + y));
                    // Octant 2
                    this.Egdes.Add(new Point(start.X + y, start.Y + x));
                    // Octant 3 
                    this.Egdes.Add(new Point(start.X + y, start.Y - x));
                    // Octant 4
                    this.Egdes.Add(new Point(start.X + x, start.Y - y));
                    // Octant 5
                    this.Egdes.Add(new Point(start.X - x, start.Y - y));
                    // Octant 6
                    this.Egdes.Add(new Point(start.X - y, start.Y - x));
                    // Octant 7
                    this.Egdes.Add(new Point(start.X - y, start.Y + x));
                    // Octant 8
                    this.Egdes.Add(new Point(start.X - x, start.Y + y));

                }
                else
                {
                    p0 += 2 * x - 2 * y + 5;
                    x += 1;
                    y -= 1;
                    // Octant 1
                    this.Egdes.Add(new Point(start.X + x, start.Y + y));
                    // Octant 2
                    this.Egdes.Add(new Point(start.X + y, start.Y + x));
                    // Octant 3 
                    this.Egdes.Add(new Point(start.X + y, start.Y - x));
                    // Octant 4
                    this.Egdes.Add(new Point(start.X + x, start.Y - y));
                    // Octant 5
                    this.Egdes.Add(new Point(start.X - x, start.Y - y));
                    // Octant 6
                    this.Egdes.Add(new Point(start.X - y, start.Y - x));
                    // Octant 7
                    this.Egdes.Add(new Point(start.X - y, start.Y + x));
                    // Octant 8
                    this.Egdes.Add(new Point(start.X - x, start.Y + y));
                }
            }
            // 1 / 8
            this.Egdes.Add(new Point(start.X + x, start.Y + y));
            this.Egdes.Add(new Point(start.X + x, start.Y - y));
            this.Egdes.Add(new Point(start.X - x, start.Y + y));
            this.Egdes.Add(new Point(start.X - x, start.Y - y));

            // Point in Control List
            this.Control.Add(new Point(this.pStart.X - r, this.pStart.Y));
            this.Control.Add(new Point(this.pStart.X + r, this.pStart.Y));
            this.Control.Add(new Point(this.pStart.X, this.pStart.Y + r));
            this.Control.Add(new Point(this.pStart.X, this.pStart.Y - r));
            this.Control.Add(new Point(this.pStart.X + r, this.pStart.Y + r));
            this.Control.Add(new Point(this.pStart.X + r, this.pStart.Y - r));
            this.Control.Add(new Point(this.pStart.X - r, this.pStart.Y + r));
            this.Control.Add(new Point(this.pStart.X - r, this.pStart.Y - r));
        }
    }
}
