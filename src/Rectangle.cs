using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _19127409_Lab03
{
    class Rectangle : Shape
    {
        public Rectangle(Point start, Point end, float thickness, Color color)
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thickness;
            this.drawColor = color;

            // Conner Point
            Point conner_1 = new Point(this.pStart.X, this.pEnd.Y);
            Point conner_2 = new Point(this.pEnd.X, this.pStart.Y);
            Point mid_1 = new Point((this.pStart.X + this.pEnd.X) / 2, this.pStart.Y);
            Point mid_2 = new Point((this.pStart.X + this.pEnd.X) / 2, this.pEnd.Y);
            Point mid_3 = new Point((conner_1.X), (conner_1.Y + conner_2.Y) / 2);
            Point mid_4 = new Point((conner_2.X), (conner_1.Y + conner_2.Y) / 2);

            // Point in Control List
            this.Control.Add(start);
            this.Control.Add(end);
            this.Control.Add(conner_1);
            this.Control.Add(conner_2);
            this.Control.Add(mid_1);
            this.Control.Add(mid_2);
            this.Control.Add(mid_3);
            this.Control.Add(mid_4);

            Line line = new Line(this.pStart, conner_2,thickness,color);
            this.Egdes.AddRange(line.Egdes);
            line = new Line(conner_2, this.pEnd, thickness,color);
            this.Egdes.AddRange(line.Egdes);
            line = new Line(this.pEnd, conner_1,thickness,color);
            this.Egdes.AddRange(line.Egdes);
            line = new Line(conner_1, this.pStart,thickness,color);
            this.Egdes.AddRange(line.Egdes);
        }
    } 
}
