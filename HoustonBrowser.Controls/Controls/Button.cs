using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class Button: BrowserControl
    {
        public override void Render(DrawingContext context)
        {
            if(this.IsDefault)

            {
                SetDefaultStyles();
            }

            if(this.IsPressed)
            {
                IBrush borderBrush = new SolidColorBrush(new Color(145, 13, 13, 13));
                Pen borderPen=new Pen(borderBrush);
                Geometry borderForm=new RectangleGeometry(new Rect(this.Left-1,this.Top-1,this.Width+2,this.Height+2));

                context.DrawGeometry(null, borderPen, borderForm);
            }
            base.Render(context);
        }       

        private void SetDefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145,220,66,0));
            this.Width=this.Height=30;
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            this.TextTypeface=new Typeface("Arial", 10);
            this.ForegroundBrush=new SolidColorBrush(new Color(255,0,0,0));
            this.AlignText=TextAlignment.Center;
        }

    }
}