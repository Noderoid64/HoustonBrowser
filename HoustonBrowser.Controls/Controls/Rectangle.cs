using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class Rectangle: BrowserControl
    {
        public Rectangle()
        {
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
        }

    }
}