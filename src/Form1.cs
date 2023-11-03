using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;


namespace _19127409_Lab03
{
    public partial class Form1 : Form
    {
        // Point setup
        Point pStart = new Point(); // Start Point
        Point pEnd = new Point();   // End Point
        Point pMouse = new Point(); // Mouse point
        List<Point> list = new List<Point>(); // List of drawing point
        float thickness = 2; // Thickness of point
        Color userColor; // Color of point

        // Shape setup
        List<Shape> ListShape = new List<Shape>();
        int shape; // Shape of each type
                   // 1_Line, 2_Rectangle, 3_Circle, 4_Ellipse, 5_Square, 6_Pentagon, 7_Hexagon, 8_Polygon
        bool draw = false;
        bool move = false;
        bool rotate = false;
        bool zoom = false;

        // Time running setup
        Timer time;
        Stopwatch stop;

        public Form1()
        {
            InitializeComponent();
            userColor = System.Drawing.Color.Purple;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            thickness = (float)numericUpDown1.Value;
            // Draw old shapes
            for (int i = 0; i < ListShape.Count; ++i)
                ListShape[i].Draw(gl);

            // Line
            if (shape == 1 && draw == true)
            {
                Line i = new Line(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0; 
            }

            // Rectangle
            if(shape == 2 && draw == true)
            {
                Rectangle i = new Rectangle(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }
            
            // Circle
            if(shape == 3 && draw == true)
            {
                Circle i = new Circle(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }

            // Ellipse
            if(shape == 4 && draw == true)
            {
                Ellipse i = new Ellipse(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }    

            // Square
            if(shape ==5 && draw == true)
            {
                Square i = new Square(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }

            // Pentagon
            if (shape == 6 && draw == true)
            {
                Pentagon i = new Pentagon(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }

            // Hexagon
            if (shape == 7 && draw == true)
            {
                Hexagon i = new Hexagon(pStart, pEnd, thickness, userColor);
                ListShape.Add(i);
                shape = 0;
            }

            // Polygon 
            if(shape == 8 && draw == true)
            {
                if (list.Count == 2)
                {
                    Line x = new Line(list[0], list[1], thickness, userColor);
                    ListShape.Add(x);
                }
                else if (list.Count > 2)
                {
                    ListShape.RemoveAt(ListShape.Count - 1);  // Delete old polygon
                    Polygon x = new Polygon(list, thickness, userColor);
                    ListShape.Add(x);
                }
            }    
            // Make sure don't draw anything without user click

            this.draw = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.draw = false;
            if (e.Button == MouseButtons.Left)
            {
                pStart = e.Location;
                pEnd = pStart;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.draw = true;
            if (e.Button == MouseButtons.Left)
                pEnd = e.Location;
            if(this.move == true)
            {
                for (int i = 0; i < ListShape.Count; ++i)
                    if (ListShape[i].drawControl == true)
                        ListShape[i].Move(pStart, pEnd);
                this.move = false;
            }
            if (this.rotate == true)
            {
                for (int i = 0; i < ListShape.Count; ++i)
                    if (ListShape[i].drawControl == true)
                        ListShape[i].Rotate(pStart, pEnd);
                this.rotate = false;
            }
            if (this.zoom == true)
            {
                for (int i = 0; i < ListShape.Count; ++i)
                    if (ListShape[i].drawControl == true)
                        ListShape[i].Scale(pStart, pEnd);
                this.zoom = false;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if (e.Button == MouseButtons.Left)
            {
                pMouse = e.Location;
                if (shape == 8) 
                    list.Add(e.Location);
                if (shape == 0 && this.move == false && this.zoom == false && this.rotate == false)
                {
                    for (int i = 0; i < ListShape.Count; i++)
                    {
                        ListShape[i].ControlPoint(openGLControl.OpenGL, pMouse);
                    }
                }
            }
            if (e.Button == MouseButtons.Right && shape == 8)
            {
                list.Clear();
                draw = false;
                shape = 0;
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y));
            }
        }

        private void Line_Click(object sender, EventArgs e)
        {
            shape = 1;
            draw = false;
        }

        private void Rectangle_Click(object sender, EventArgs e)
        {
            shape = 2;
            draw = false;
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            shape = 3;
            draw = false;
        }

        private void Ellipse_Click(object sender, EventArgs e)
        {
            shape = 4;
            draw = false;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            shape = 5;
            draw = false;
        }

        private void Pentagon_Click(object sender, EventArgs e)
        {
            shape = 6;
            draw = false;
        }

        private void Hexagon_Click(object sender, EventArgs e)
        {
            shape = 7;
            draw = false;
        }

        private void Polygon_Click(object sender, EventArgs e)
        {
            shape = 8;
            draw = false;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ListShape.Clear();
            list.Clear();
        }

        private void Color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColor = colorDialog1.Color;
            }
        }


        private void contextMenuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == contextMenuStrip1.Items[0])
            {
                this.move = true;
            }
            else if (e.ClickedItem == contextMenuStrip1.Items[1])
            {
                this.rotate = true;
            }
            else if (e.ClickedItem == contextMenuStrip1.Items[2])
            {
                this.zoom = true;
            }
        }
    }
}
