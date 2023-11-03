using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _19127409_Lab03
{
    class Square : Shape
    {
        public Square(Point start, Point end, float thickness, Color color)
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thickness;
            this.drawColor = color;

            int r = (int)(Math.Sqrt(Math.Pow(this.pEnd.X - pStart.X, 2) + Math.Pow(pEnd.Y - pStart.Y, 2)) / 2);
            int rx = Math.Abs(pEnd.X - pStart.X) / 2;
            int ry = Math.Abs(pEnd.Y - pStart.Y) / 2;

            // Center point
            int x_center, y_center;
            if (pStart.X < pEnd.X)
                x_center = pStart.X + rx;
            else
                x_center = pEnd.X + rx;
            if (pStart.Y < pEnd.Y)
                y_center = pStart.Y + ry;
            else
                y_center = pEnd.Y + ry;




            // Calculate point in Egdes List
            Point[] a = new Point[4];
            for (int i = 0; i < a.Length; ++i)
            {
                float x = (float)(r * this.cos(45 + 90 * i));
                float y = (float)(r * this.sin(45 + 90 * i));
                a[i].X = x_center + (int)x;
                a[i].Y = y_center + (int)y;
            }
            this.Control.Add(new Point((a[0].X + a[2].X) / 2, a[0].Y));
            this.Control.Add(new Point((a[0].X + a[2].X) / 2, a[2].Y));
            this.Control.Add(new Point(a[1].X, (a[1].Y + a[3].Y) / 2));
            this.Control.Add(new Point(a[3].X, (a[1].Y + a[3].Y) / 2));

            // Draw line
            Line line = new Line(a[0], a[a.Length - 1], thickness, color);
            this.Egdes.AddRange(line.Egdes);
            this.Control.AddRange(line.Control);
            for (int i = 0; i < a.Length - 1; ++i)
            {
                line = new Line(a[i], a[i + 1], thickness, color);
                this.Egdes.AddRange(line.Egdes);
                this.Control.AddRange(line.Control);
            }

        }
    }
}
