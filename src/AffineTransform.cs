using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _19127409_Lab03
{
    class AffineTransform
    {
        List<float> Transform = new List<float>();

        public AffineTransform()
        {
            // Unit matrix
            this.Transform = new List<float> { 1, 0, 0,
                                               0, 1, 0,
                                               0, 0, 1};
        }

        public void Multiply(List<float> matrix)
        {
            List<float> result = new List<float> { 0,0,0,
                                                   0,0,0,
                                                   0,0,0};
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    for (int k = 0; k < 3; ++k)
                        result[i * 3 + j] += matrix[i * 3 + k] * this.Transform[k * 3 + j];

            this.Transform = new List<float>(result);
        }

        public double cos(float degres)
        {
            double radians = Math.PI * degres / 180.0;
            double cos = Math.Cos(radians);
            return cos;
        }

        public double sin(float degres)
        {
            double radians = Math.PI * degres / 180.0;
            double sin = Math.Round(Math.Sin(radians), 2);
            return sin;
        }

        public void Translate(float dx, float dy)
        {
            List<float> translate = new List<float> { 1, 0, dx,
                                                      0, 1, dy,
                                                      0, 0, 1};
            this.Multiply(translate);
        }

        public void Scale(float sx, float sy)
        {
            List<float> scale = new List<float> { sx, 0, 0,
                                                      0, sy, 0,
                                                      0, 0, 1};
            this.Multiply(scale);
        }

        public void Rotate(float theta)
        {
            float cos_a = (float)(Math.Cos(theta));
            float sin_a = (float)(Math.Sin(theta));
            List<float> rotate = new List<float>  { cos_a, -sin_a, 0,
                                                    sin_a, cos_a, 0,
                                                    0, 0, 1};
            this.Multiply(rotate);
        }    

        public Point TransformPoint(Point transform)
        {
            List<float> location = new List<float> { transform.X, transform.Y, (float)1 };
            List<float> result = new List<float>() { 0, 0, 0 };
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    result[i] += this.Transform[i * 3 + j] * location[j];

            Point s = new Point((int)(Math.Round(result[0])), (int)(Math.Round(result[1])));
            return s;
        }
    }
    
}
