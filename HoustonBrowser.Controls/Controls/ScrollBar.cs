using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class ScrollBar: BrowserControl
    {
       public Button ButtonUp {get;set;}
       public Button ButtonDown {get;set;}
       public Button ScrollButton {get;set;}
    }
}