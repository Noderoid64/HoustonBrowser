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
    }
}