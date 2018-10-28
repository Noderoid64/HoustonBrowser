using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class BrowserControl: IBrowserControl
    {
            public string Name{get;set;}
            public Geometry Form {get; set;}
            public IBrush BackgroundBrush {get;set;}
            public Pen StrokePen {get;set;}
            public IBrush BorderBrush {get;set;}
            public double BorderThickness {get;set;}
            public bool IsEnabled {get;set;}
            public double Left{get;set;}
            public double Top{get;set;}
            public double Height {get;set;}
            public double Width {get;set;}
            public string Text {get;set;}
            public IBrush ForegroundBrush {get;set;}
            public Typeface TextTypeface {get;set;}
            public TextAlignment AlignText {get;set;}

            public event EventHandler<KeyEventArgs> KeyDown;
            public event EventHandler<RoutedEventArgs> Click; 

            public BrowserControl(){}

            public virtual void Render(DrawingContext context)
            {
                context.DrawGeometry(BackgroundBrush, StrokePen, Form);

                if(!String.IsNullOrEmpty(Text))
                {Point origin = new Point(Left, Top);
                FormattedText controlText = new FormattedText();
                controlText.Text=Text;
                Size textConstraint = new Size(Width, Height);
                controlText.Constraint = textConstraint;
                controlText.Typeface=TextTypeface;
                controlText.TextAlignment=AlignText;    

                context.DrawText(ForegroundBrush, origin, controlText);
                }
            }

            public virtual void OnClick (object sender, RoutedEventArgs e)
            {
                Click?.Invoke(sender, e);
            }

            public virtual void OnKeyDown(object sender,KeyEventArgs e)
            {
                KeyDown?.Invoke(sender, e);
            }

            //mock
            public string Render()
            {
                return "Controls work.";
            }


    }
}