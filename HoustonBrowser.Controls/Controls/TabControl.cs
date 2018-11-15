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
       public override IBrush BackgroundBrush {get;set;} = new SolidColorBrush(new Color(0,10,220,224));
       public List<Panel> TabPages{get;set;}
       public void SelectTab()
       {

       }

       public void DeselectTab()
       {

       }

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