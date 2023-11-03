using System;
using System.Collections.Generic;
using System.Text;
using SharpGL;
using System.Drawing;

namespace _19127409_Lab03
{
    class Line : Shape
    {
        public Line(Point start, Point end, float thickness, Color color)
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thickness;
            this.drawColor = color;

            // Point in Control List
            this.Control.Add(start);
            this.Control.Add(end);

            // Calculate point in Egdes List
            int delta_x = this.pEnd.X - this.pStart.X;
            int delta_y = this.pEnd.Y - this.pStart.Y;
            int step_x = 1; // Default step if delta_x > 0
            int step_y = 1; // Default step if delta_y > 0
            if (delta_x < 0)
            {
                delta_x = Math.Abs(delta_x);
                step_x = -1; // Set step when delta_x < 0
            }
            if (delta_y < 0)
            {
                {
                    delta_y = Math.Abs(delta_y);
                    step_y = -1; // Set step when delta_y < 0
                }
            }
            int x = this.pStart.X;
            int y = this.pStart.Y;
            if (delta_x > delta_y)
            {
                int p0 = 2 * delta_y - delta_x;
                while (x != this.pEnd.X)
                {
                    if (p0 < 0)
                    {
                        p0 = p0 + 2 * delta_y;
                        x += step_x;
                        Egdes.Add(new Point(x, y));
                    }
                    else
                    {
                        p0 = p0 + 2 * delta_y - 2 * delta_x;
                        x += step_x;
                        y += step_y;
                        Egdes.Add(new Point(x, y));
                    }
                }
            }
            else
            {
                int p0 = 2 * delta_x - delta_y;
                while (y != this.pEnd.Y)
                {
                    if (p0 < 0)
                    {
                        p0 = p0 + 2 * delta_x;
                        y += step_y;
                        Egdes.Add(new Point(x, y));
                    }
                    else
                    {
                        p0 = p0 + 2 * delta_x - 2 * delta_y;
                        x += step_x;
                        y += step_y;
                        Egdes.Add(new Point(x, y));
                    }
                }
            }
        }
    }
}
