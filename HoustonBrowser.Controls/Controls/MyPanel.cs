using System;
using System.Collections.Generic;
using System.Linq;
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
            if (Controls != null)
            {
                foreach (BrowserControl bc in Controls)
                {
                    if(bc!=null)
                    {
                        bc.Render(context);
                    }
                }
                OnRender();
            }
       }

       protected override void OnPointerPressed(PointerPressedEventArgs e)
       {
           if(Controls!=null)
           {
                foreach(var cntrl in Controls)
                {
                    Point location = e.GetPosition(this);
                    if (location.X>=cntrl.Left && location.X<=cntrl.Width+cntrl.Left && location.Y>=cntrl.Top && location.Y<=cntrl.Top+cntrl.Height)
                    { 
                        cntrl.IsPressed=true;
                    }
                }
           }
            base.OnPointerPressed(e);
            this.InvalidateVisual();
       }

       protected override void OnPointerReleased(PointerReleasedEventArgs e)
       {
           if(Controls!=null)
           {
                foreach(var cntrl in Controls)
                {
                    Point location = e.GetPosition(this);
                    if (location.X>=cntrl.Left && location.X<=cntrl.Width+cntrl.Left && location.Y>=cntrl.Top && location.Y<=cntrl.Top+cntrl.Height)
                    { 
                        cntrl.IsPressed=false;
                    }
                }
           }
           base.OnPointerReleased(e);
            this.InvalidateVisual();
        }

        protected void OnRender()
        {
                var maxHeight=Controls.Max(c=>(c.Top+c.Height));
                this.Height=maxHeight;
        }
    }
}