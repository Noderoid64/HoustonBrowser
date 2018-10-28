using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public interface IBrowserControl
    {
        string Name{get;set;}
        Geometry Form {get; set;}
        IBrush BackgroundBrush {get;set;}
        Pen StrokePen {get;set;}
        IBrush BorderBrush {get;set;}
        double BorderThickness {get;set;}
        bool IsEnabled {get;set;}
        double Left{get;set;}
        double Top{get;set;}
        double Height {get;set;}
        double Width {get;set;}
        string Text {get;set;}
        IBrush ForegroundBrush {get;set;}
        Typeface TextTypeface {get;set;}
        TextAlignment AlignText {get;set;}

        event EventHandler<KeyEventArgs> KeyDown;
        event EventHandler<RoutedEventArgs> Click;

        void Render(DrawingContext context);
        void OnClick (object sender, RoutedEventArgs e);
        void OnKeyDown(object sender, KeyEventArgs e);

        //mock
        string Render();
    }
}
