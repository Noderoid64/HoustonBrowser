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
        public override IBrush BackgroundBrush {get;set;} = new SolidColorBrush(new Color(0,0,0,0));
        public override void Render(DrawingContext context)
        {
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            base.Render(context);
        }       

    }
}