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
        public override void Render(DrawingContext context)
        {
            base.Render(context);
        }       

        public void DefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145, 41, 218, 144));
            this.Width=this.Height=30;
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));

        }       
    }
}