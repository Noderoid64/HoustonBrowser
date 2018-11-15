using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class Label: BrowserControl
    {
        public override IBrush BackgroundBrush {get;set;} = new SolidColorBrush(new Color(0,0,0,0));
        public override void Render(DrawingContext context)
        {
            if(this.Form==null)
            {
                this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            }
            base.Render(context);
        }       

    }
}