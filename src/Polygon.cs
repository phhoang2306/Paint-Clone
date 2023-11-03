using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpGL;

namespace _19127409_Lab03
{
    class Polygon : Shape
    {
        public List<Point> list = new List<Point>();
        public Polygon(List<Point> list, float thickness, Color color)
        {
            this.list = new List<Point>(list);
            this.thickness = thickness;
            this.drawColor = color;
        }

        public override void Draw(OpenGL gl)
        {
            if(list.Count >= 2)
            {
                Line line;
                for (int i = 0; i < list.Count - 1; ++i)
                {
                    line = new Line(list[i], list[i + 1], this.thickness, this.drawColor);
                    line.Draw(gl);
                    this.Egdes.AddRange(line.Egdes);
                    this.Control.AddRange(line.Control);
                }
                line = new Line(list[0], list[list.Count - 1], this.thickness, this.drawColor);
                line.Draw(gl);
                this.Egdes.AddRange(line.Egdes);
                this.Control.AddRange(line.Control);

                // Draw Control Point
                if (drawControl == true)
                {
                    gl.PointSize(this.thickness + 5);
                    gl.Color(this.drawColor.R / 255.0, this.drawColor.G / 255.0, this.drawColor.B / 255.0);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Control.Count; ++i)
                        gl.Vertex(Control[i].X, gl.RenderContextProvider.Height - Control[i].Y);
                    gl.End();
                    gl.Flush();
                }
            }
        }

        public override void Move(Point start, Point end)
        {
            // Affine matrix
            AffineTransform affine = new AffineTransform();
            // Move
            affine.Translate(end.X - start.X, end.Y - start.Y);

            for (int i = 0; i < list.Count; ++i)
                list[i] = affine.TransformPoint(list[i]);
            for (int i = 0; i < this.Control.Count; ++i)
                Control[i] = affine.TransformPoint(Control[i]);
            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }

        public override void Rotate(Point start, Point end)
        {
            // Calculate center point
            float sum_X = 0;
            float sum_Y = 0;
            for(int i = 0; i < list.Count; ++i)
            {
                sum_X += list[i].X;
                sum_Y += list[i].Y;
            }
            this.pStart = new Point((int)Math.Round(sum_X / list.Count), (int)Math.Round(sum_Y / list.Count));

            // Calculate theta 
            double s1 = Distance(this.pStart, start);
            double s2 = Distance(this.pStart, end);
            double cost = (double)(start.X - this.pStart.X) * (double)(end.X - this.pStart.X) + (double)(start.Y - this.pStart.Y) * (double)(end.Y - this.pStart.Y);
            cost /= s1 * s2;
            float theta = (float)(Math.Acos(cost));
            if (((double)(start.X - this.pStart.X) * (double)(end.Y - this.pStart.Y) - (double)(start.Y - this.pStart.Y) * (double)(end.X - this.pStart.X)) < 0)
                theta = -theta;

            // Affine matrix
            AffineTransform affine = new AffineTransform();
            // Move
            affine.Translate(-pStart.X, -pStart.Y);
            affine.Rotate(theta);
            affine.Translate(pStart.X, pStart.Y);

            // Move Egdes Point
            for (int i = 0; i < this.list.Count; ++i)
                list[i] = affine.TransformPoint(list[i]);

            // Move Control Point
            for (int i = 0; i < this.Control.Count; ++i)
                this.Control[i] = affine.TransformPoint(this.Control[i]);

            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }
        public override void Scale(Point start, Point end)
        {
            // Calculate center point
            float sum_X = 0;
            float sum_Y = 0;
            for (int i = 0; i < list.Count; ++i)
            {
                sum_X += list[i].X;
                sum_Y += list[i].Y;
            }
            this.pStart = new Point((int)Math.Round(sum_X / list.Count), (int)Math.Round(sum_Y / list.Count));

            // Calculate sx, sy
            float sx = (float)(end.X - pStart.X) / (float)(start.X - pStart.X);
            float sy = (float)(end.Y - pStart.Y) / (float)(start.Y - pStart.Y);
            float s = sx > sy ? sx : sy;

            // Affine matrix
            AffineTransform affine = new AffineTransform();
            // Move
            affine.Translate(-pStart.X, -pStart.Y);
            affine.Scale(s, s);
            affine.Translate(pStart.X, pStart.Y);

            // Move Egdes Point
            for (int i = 0; i < this.list.Count; ++i)
                this.list[i] = affine.TransformPoint(this.list[i]);

            // Move Control Point
            for (int i = 0; i < this.Control.Count; ++i)
                this.Control[i] = affine.TransformPoint(this.Control[i]);

            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }
    }
}