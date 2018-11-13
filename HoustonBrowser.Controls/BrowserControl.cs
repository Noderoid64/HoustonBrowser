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
            public TextWrapping WrapText {get;set;}
            public bool IsDefault {get;set;}
            public bool IsPressed {get;set;}

            private FormattedText _formattedText;
            
            public FormattedText FormattedText
            {
                get
            {
                if (_formattedText == null)
                {
                    _formattedText = CreateFormattedText();
                }

                return _formattedText;
            }
            }

            public event EventHandler<KeyEventArgs> KeyDown;
            public event EventHandler<PointerPressedEventArgs> PointerPressed;
            public event EventHandler<PointerReleasedEventArgs> PointerReleased;

            public BrowserControl(){}

            public virtual void Render(DrawingContext context)
            {
                context.DrawGeometry(BackgroundBrush, StrokePen, Form);

                if(!String.IsNullOrEmpty(Text))
                {
                    Point origin = new Point(Left, Top+Height/2-this.FormattedText.Measure().Height/2);  
                    context.DrawText(ForegroundBrush, origin, this.FormattedText);
                }
            }

            private FormattedText CreateFormattedText()
            {
                return new FormattedText
            {
                Constraint = new Size(Width, Height),
                Typeface = TextTypeface,
                Text = this.Text ?? string.Empty,
                TextAlignment = AlignText,
                Wrapping = WrapText
            };
            }

            public virtual void OnKeyDown(object sender,KeyEventArgs e)
            {
                KeyDown?.Invoke(sender, e);
            }

            public virtual void OnPointerPressed(object sender, PointerPressedEventArgs e)
            {
                IsPressed=true;
                PointerPressed?.Invoke(sender, e);
            }

            public void OnPointerReleased(object sender, PointerReleasedEventArgs e)
            {
                IsPressed=false;
                PointerReleased?.Invoke(sender, e);
            }

            //mock
            public string Render()
            {
                return "Controls work.";
            }


    }
}