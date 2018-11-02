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
       public List<Panel> TabPages{get;set;}
       public void SelectTab()
       {

       }

       public void DeselectTab()
       {

       }

       public override void Render(DrawingContext context)
        {
            DefaultStyles();
            base.Render(context);
        }       

        public void DefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145,10,220,224));
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,30,30));

        }
       
    }
}