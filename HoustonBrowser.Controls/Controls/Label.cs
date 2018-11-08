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
        public Label(){}
        public Label(bool isDefault, double left, double top)
        {
            this.Left=left;
            this.Top=top;
            if(isDefault)
            {
                SetDefaultStyles();
            }
        }
        public override void Render(DrawingContext context)
        {
            base.Render(context);
        }       

        private void SetDefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145, 41, 218, 144));
            this.Width=100;
            this.Height=60;
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            this.TextTypeface=new Typeface("Arial", 10);
            this.ForegroundBrush=new SolidColorBrush(new Color(255,0,0,0));
            this.AlignText=TextAlignment.Center;

        }             
    }
}