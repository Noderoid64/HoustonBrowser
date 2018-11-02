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
            DefaultStyles();
            base.Render(context);
        }       

        public void DefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145, 41, 218, 144));
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,30,30));

        }       
    }
}