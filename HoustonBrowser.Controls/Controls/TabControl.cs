using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class TabControl: BrowserControl
    {
        public TabControl(){}
        public TabControl(bool isDefault)
        {
            if(isDefault)
            {
                SetDefaultStyles();
            }
        }
       public List<Panel> TabPages{get;set;}
       public void SelectTab()
       {

       }

       public void DeselectTab()
       {

       }

       public override void Render(DrawingContext context)
        {
            if(this.IsDefault)
            {
                SetDefaultStyles();
            }
            base.Render(context);
        }       

        private void SetDefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145,10,220,224));
            this.Width=this.Height=30;
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            this.TextTypeface=new Typeface("Arial", 10);
            this.ForegroundBrush=new SolidColorBrush(new Color(255,0,0,0));
            this.AlignText=TextAlignment.Center;

        }
       
    }
}