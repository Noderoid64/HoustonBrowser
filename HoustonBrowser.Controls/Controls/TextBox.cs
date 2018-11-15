using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class TextBox: BrowserControl
    {
        public override IBrush BackgroundBrush {get;set;} = new SolidColorBrush(new Color(0,203,218,41));
         public void Clear()
        {
            
        }
        public void SelectAll()
        {

        }

        public void Select (int start, int end)
        {

        }      

        public event EventHandler<KeyEventArgs> KeyUp; 

        public void OnKeyUp(object sender,KeyEventArgs e)
        {
                KeyUp?.Invoke(sender, e);
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