using System;
using System.Collections.Generic;
using System.Text;
using SharpGL;
using System.Drawing;

namespace _19127409_Lab03
{
    abstract class Shape
    {
        // Point setup
        public Point pStart; // Start point
        public Point pEnd; // End point
        public List<Point> Egdes = new List<Point>(); // Point in edges
        public List<Point> Control = new List<Point>(); // Control point

        public Color drawColor; // Egdes color
        public float thickness; // Thickness of point

        public bool drawControl = false; // Drawing control point

        public Shape()
        {

        }

        public Shape(Point start, Point end, float thick, Color color )
        {
            this.pStart = start;
            this.pEnd = end;
            this.thickness = thick;
            this.drawColor = color;
        }

        // Draw Shape
        public virtual void Draw(OpenGL gl)
        {
            // Draw Edge
            gl.PointSize(this.thickness);
            gl.Color(this.drawColor.R / 255.0, this.drawColor.G / 255.0, this.drawColor.B / 255.0);
            gl.Begin(OpenGL.GL_POINTS);
            for(int i = 0; i < Egdes.Count; ++i)
                gl.Vertex(Egdes[i].X, gl.RenderContextProvider.Height - Egdes[i].Y);
            gl.End();
            gl.Flush();

            // Draw Control Point
            if(drawControl == true)
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

        // Draw Control Point
        public double Distance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }
        public virtual void ControlPoint(OpenGL gl, Point mouse)
        {
            if (Egdes.Count == 0) // without edges 
                return;

            float epsilon = 10;
            for (int i = 0; i < Egdes.Count; ++i)
            {
                // click to egde point
                if(Distance(mouse,Egdes[i]) <= epsilon)
                {
                    drawControl = true;
                    return;
                }
            }
            drawControl = false;
        }

        // Sin cos
        // Cos of degree
        public double cos(int degres)
        {
            double radians = Math.PI * degres / 180.0;
            double cos = Math.Cos(radians);
            return cos;
        }

        // Sin of degree
        public double sin(int degres)
        {
            double radians = Math.PI * degres / 180.0;
            double sin = Math.Round(Math.Sin(radians), 2);
            return sin;
        }

        // Move
        public virtual void Move(Point start, Point end)
        {
            // Affine matrix
            AffineTransform affine = new AffineTransform();
            // Move
            affine.Translate(end.X - start.X, end.Y - start.Y);

            // Move Egdes Point
            for(int i = 0; i < this.Egdes.Count; ++i)
                this.Egdes[i] = affine.TransformPoint(this.Egdes[i]);

            // Move Control Point
            for (int i = 0; i < this.Control.Count; ++i)
                this.Control[i] = affine.TransformPoint(this.Control[i]);

            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }

        // Rotate
        public virtual void Rotate(Point start, Point end)
        {
            if (Distance(pStart, start) < Distance(pEnd, start))
            {
                Point temp = pStart;
                pStart = pEnd;
                pEnd = temp;
            }

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
            for (int i = 0; i < this.Egdes.Count; ++i)
                this.Egdes[i] = affine.TransformPoint(this.Egdes[i]);

            // Move Control Point
            for (int i = 0; i < this.Control.Count; ++i)
                this.Control[i] = affine.TransformPoint(this.Control[i]);

            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }

        // Scale
        public virtual void Scale(Point start, Point end)
        {
            if(Distance(pStart,start) < Distance(pEnd,start))
            {
                Point temp = pStart;
                pStart = pEnd;
                pEnd = temp;
            }    
            // Calculate sx, sy
            float sx =(float)(end.X - pStart.X) / (float)(start.X - pStart.X);
            float sy = (float) (end.Y - pStart.Y) / (float)(start.Y - pStart.Y);
            float s = sx > sy ? sx : sy;

            // Affine matrix
            AffineTransform affine = new AffineTransform();
            // Move
            affine.Translate(-pStart.X, -pStart.Y);
            affine.Scale(s, s);
            affine.Translate(pStart.X, pStart.Y);

            // Move Egdes Point
            for (int i = 0; i < this.Egdes.Count; ++i)
                this.Egdes[i] = affine.TransformPoint(this.Egdes[i]);

            // Move Control Point
            for (int i = 0; i < this.Control.Count; ++i)
                this.Control[i] = affine.TransformPoint(this.Control[i]);

            // Move Start and End Point
            this.pStart = affine.TransformPoint(this.pStart);
            this.pEnd = affine.TransformPoint(this.pEnd);
        }
    }
}
