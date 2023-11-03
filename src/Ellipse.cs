using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _19127409_Lab03
{
    class Ellipse : Shape
    {
        public Ellipse(Point start, Point end, float thickness, Color color)
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thickness;
            this.drawColor = color;

            // radius
            int rx = Math.Abs(pEnd.X - pStart.X) / 2;
            int ry = Math.Abs(pEnd.Y - pStart.Y) / 2;
            int x = 0;
            int y = ry;
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
            Point center = new Point(x_center, y_center);

            // Point in control list
            this.Control.Add(new Point(center.X, center.Y - ry));
            this.Control.Add(new Point(center.X, center.Y + ry));
            this.Control.Add(new Point(center.X + rx, center.Y));
            this.Control.Add(new Point(center.X - rx, center.Y));

            this.Control.Add(new Point(center.X + rx, center.Y - ry));
            this.Control.Add(new Point(center.X + rx, center.Y + ry));
            this.Control.Add(new Point(center.X - rx, center.Y - ry));
            this.Control.Add(new Point(center.X - rx, center.Y + ry));

            // Point in egdes list
            float p10 = (float)(Math.Pow(ry, 2) - Math.Pow(rx, 2) * ry + (float)(1 / 4) * Math.Pow(rx, 2));
            double y_x = 2 * Math.Pow(ry, 2) * x;
            double x_y = 2 * Math.Pow(rx, 2) * y;
            while (y_x < x_y)
            {
                if (p10 < 0)
                {
                    y_x += 2 * Math.Pow(ry, 2);
                    p10 += (float)(y_x + Math.Pow(ry, 2));
                    x += 1;
                    // Quarter 1
                    this.Egdes.Add(new Point(center.X + x, center.Y - y));
                    // Quarter 2
                    this.Egdes.Add(new Point(center.X + x, center.Y + y));
                    // Quater 3
                    this.Egdes.Add(new Point(center.X - x, center.Y - y));
                    // Quater 4
                    this.Egdes.Add(new Point(center.X - x, center.Y + y));
                }
                else
                {
                    y_x += 2 * Math.Pow(ry, 2);
                    x_y -= 2 * Math.Pow(rx, 2);
                    p10 += (float)(y_x - x_y + Math.Pow(ry, 2));
                    x += 1;
                    y -= 1;
                    // Quarter 1
                    this.Egdes.Add(new Point(center.X + x, center.Y - y));
                    // Quarter 2
                    this.Egdes.Add(new Point(center.X + x, center.Y + y));
                    // Quater 3
                    this.Egdes.Add(new Point(center.X - x, center.Y - y));
                    // Quater 4
                    this.Egdes.Add(new Point(center.X - x, center.Y + y));
                }
            }

            this.Egdes.Add(new Point(center.X + x, center.Y - y)); // 1/4 top point
            this.Egdes.Add(new Point(center.X + x, center.Y + y)); // 1/4 bot point
            this.Egdes.Add(new Point(center.X - x, center.Y - y)); // 1/4 right point
            this.Egdes.Add(new Point(center.X - x, center.Y + y)); // 1/4 left point
        
            float p20 = (float)(Math.Pow(ry, 2) * (float)Math.Pow(x + 1 / 2, 2) + Math.Pow(rx, 2) * Math.Pow(y - 1, 2) - Math.Pow(rx, 2) * Math.Pow(ry, 2));
            while (y != 0)
            {
                if (p20 > 0)
                {
                    x_y -= 2 * Math.Pow(rx, 2);
                    p20 = (float)(p20 - x_y + Math.Pow(rx, 2));
                    y -= 1;
                    // Quarter 1
                    this.Egdes.Add(new Point(center.X + x, center.Y - y));
                    // Quarter 2
                    this.Egdes.Add(new Point(center.X + x, center.Y + y));
                    // Quater 3
                    this.Egdes.Add(new Point(center.X - x, center.Y - y));
                    // Quater 4
                    this.Egdes.Add(new Point(center.X - x, center.Y + y));
                }
                else
                {
                    y_x += 2 * Math.Pow(ry, 2);
                    x_y -= 2 * Math.Pow(rx, 2);
                    p20 += (float)(y_x - x_y + Math.Pow(rx, 2));
                    x += 1;
                    y -= 1;
                    // Quarter 1
                    this.Egdes.Add(new Point(center.X + x, center.Y - y));
                    // Quarter 2
                    this.Egdes.Add(new Point(center.X + x, center.Y + y));
                    // Quater 3
                    this.Egdes.Add(new Point(center.X - x, center.Y - y));
                    // Quater 4
                    this.Egdes.Add(new Point(center.X - x, center.Y + y));
                }
            }

        }
    }
}
