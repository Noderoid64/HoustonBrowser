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
            DefaultStyles();
            base.Render(context);
        }       

        public void DefaultStyles()
        {
            this.BackgroundBrush = new SolidColorBrush(new Color(145, 203,218,41));
            this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,30,30));

        }       
    }
}