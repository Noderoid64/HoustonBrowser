using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class MyPanel: Canvas
    {
       public List<BrowserControl> Controls {get;set;}

       public override void Render(DrawingContext context)
       {
           base.Render(context);
           foreach(BrowserControl bc in Controls)
           {
               bc.Render(context);
           }
       }
    }
}