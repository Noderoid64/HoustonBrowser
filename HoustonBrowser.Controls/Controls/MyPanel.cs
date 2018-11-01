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
       public List<IBrowserControl> Controls {get;set;}

       public override void Render(DrawingContext context)
       {
           base.Render(context);
           foreach(IBrowserControl bc in Controls)
           {
               bc.Render(context);
           }
       }
    }
}