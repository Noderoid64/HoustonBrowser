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
        public Rectangle(){}
        public Rectangle(bool isDefault)
        {
            if(isDefault)
            {
                SetDefaultStyles();
            }
        }
        public override void Render(DrawingContext context)
        {
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            base.Render(context);
        }       

        private void SetDefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145, 204, 0, 153));
            this.Width = 200;
            this.Height = 60;
            this.TextTypeface=new Typeface("Arial", 10);
            this.ForegroundBrush=new SolidColorBrush(new Color(255,0,0,0));
            this.AlignText=TextAlignment.Center;

        }             

    }
}