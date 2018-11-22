using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Render
{
    public class Rectangle
    {
        double left;
        double top;
        double width;
        double height;

        public Rectangle(double left, double top, double width, double height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
        }

        public double Height { get => height; set => height = value; }
        public double Width { get => width; set => width = value; }
        public double Top { get => top; set => top = value; }
        public double Left { get => left; set => left = value; }
    }
}
