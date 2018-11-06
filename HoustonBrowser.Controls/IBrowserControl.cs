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
        bool IsDefault {get;set;}
        bool IsPressed {get;set;}

        event EventHandler<KeyEventArgs> KeyDown;
        event EventHandler<PointerPressedEventArgs> PointerPressed;
        event EventHandler<PointerReleasedEventArgs> PointerReleased;

        void Render(DrawingContext context);
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnPointerPressed(object sender, PointerPressedEventArgs e);
        void OnPointerReleased(object sender, PointerReleasedEventArgs e);

        //mock
        string Render();
    }
}
