using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace HoustonBrowser.Controls
{
    public class Image: BrowserControl
    {   
        public Bitmap ImageBitmap{get;set;}
        public override void Render(DrawingContext context)
        {
            if(this.Form==null)
            {
                this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            }
            
            base.Render(context);

            if(ImageBitmap!=null)
                {
                    Rect imageRect = new Rect(0,0, ImageBitmap.PixelWidth, ImageBitmap.PixelHeight);
                    Rect canvasRect = new Rect(this.Left,this.Top,this.Width,this.Height);
                    context.DrawImage(ImageBitmap, 1, imageRect, canvasRect);
                }
        }   
    }
}