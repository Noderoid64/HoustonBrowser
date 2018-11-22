using System;
using System.Linq;
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
            public virtual IBrush BackgroundBrush {get;set;} 
            public Pen StrokePen {get;set;}
            public IBrush BorderBrush {get;set;}
            public double BorderThickness {get;set;}
            public bool IsEnabled {get;set;}
            public double Left{get;set;}
            public double Top{get;set;}
            public double Height 
            {
                get
                {
                    if(_height==0)
                    {
                        if(!String.IsNullOrEmpty(Text))
                        {
                            _height=FormattedText.Measure().Height;
                        }
                        else 
                        {
                            _height=30;
                        }
                    }
                    return _height;
                }
                set
                {
                    _height=value;
                }
            }
            public double Width 
            {
                get
                {
                    if(_width==0)
                    {
                        if(!String.IsNullOrEmpty(Text))
                        {
                            var textWidth = FormattedText.Measure().Width;
                            if(textWidth>=920-Left)
                            {
                                _width=920-Left;
                                WrapText=TextWrapping.Wrap;
                            }
                            else
                            {
                                _width = textWidth;
                            }
                        }
                        else 
                        {
                            _width=60;
                        }
                    }
                    return _width;
                }
                set
                {
                    _width=value;
                }
            }
            public string Text {get;set;}
            public virtual IBrush ForegroundBrush {get;set;} = new SolidColorBrush(new Color(255,0,0,0));
            public Typeface TextTypeface {get;set;} = new Typeface("Arial", 10);
            public TextAlignment AlignText {get;set;} = TextAlignment.Left;
            public TextWrapping WrapText {get;set;} = TextWrapping.NoWrap;
            public bool IsPressed 
            {
                get
                {
                    return _isPressed;
                }
                set
                {
                    _isPressed=value;
                    if(_isPressed)
                    {
                        OnPointerPressed(this, new PointerPressedEventArgs());
                    }
                    else
                    {
                        OnPointerReleased(this, new PointerReleasedEventArgs());
                    }

                }
            }

            private bool _isPressed;
            private FormattedText _formattedText;
            private Size _constraint;
            private double _width;
            private double _height;

            private Size Constraint 
            {
                get 
                {
                    if(_width!=0 && _height == 0 && WrapText == TextWrapping.Wrap)
                    {
                        _constraint = new Size(_width, 0);
                    }
                    else if (_width == 0 && _height != 0 && WrapText == TextWrapping.Wrap)
                    {
                        _constraint = new Size(0, _height);
                    }
                    else if(_width!=0 && _height!=0 && WrapText==TextWrapping.Wrap)
                    {
                        _constraint=new Size(_width, _height);
                    }
                    else
                    {
                        _constraint=Size.Infinity;
                    }
                    return _constraint;
                }
            }
            
            public FormattedText FormattedText
            {
                get
                {
                    _formattedText = CreateFormattedText();
                    return _formattedText;
                }
            }

            public event EventHandler<KeyEventArgs> KeyDown;
            public event EventHandler<PointerPressedEventArgs> PointerPressed;
            public event EventHandler<PointerReleasedEventArgs> PointerReleased;

            public BrowserControl()
            {
            }

            public virtual void Render(DrawingContext context)
            {
                context.DrawGeometry(BackgroundBrush, StrokePen, Form);

                if(!String.IsNullOrEmpty(Text))
                {
                    Point origin = new Point(Left, Top);  
                    context.DrawText(ForegroundBrush, origin, this.FormattedText);
                }
            }

            private FormattedText CreateFormattedText()
            {
                return new FormattedText
            {
                Constraint = Constraint,
                Typeface = TextTypeface,
                Text = this.Text ?? string.Empty,
                TextAlignment = AlignText,
                Wrapping = WrapText
            };
            }

            protected virtual void OnKeyDown(object sender, KeyEventArgs e)
            {
                KeyDown?.Invoke(sender, e);
            }

            protected virtual void OnPointerPressed(object sender, PointerPressedEventArgs e)
            {
                PointerPressed?.Invoke(sender, e);
            }

            protected void OnPointerReleased(object sender, PointerReleasedEventArgs e)
            {
                PointerReleased?.Invoke(sender, e);
            }

            //mock
            public string Render()
            {
                return "Controls work.";
            }


    }
}