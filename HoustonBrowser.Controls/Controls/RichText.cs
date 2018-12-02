using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HoustonBrowser.Controls
{
    public class RichText: Label
    {
        public IBrush LinkBrush {get;set;} = new SolidColorBrush(new Color(255,0,0,102));

        public IBrush PressedBrush {get;set;} = new SolidColorBrush(new Color(255, 51, 0, 102));

        public List<LinkParameters> Links {get;set;}

        public event EventHandler<string> LinkPressed;
        public override void Render(DrawingContext context)
        {
            if(this.Form==null)
            {
                this.Form=new RectangleGeometry(new Rect(this.Left,this.Top,this.Width,this.Height));
            }
            context.DrawGeometry(BackgroundBrush, StrokePen, Form);

            Pen underlinePen=new Pen(this.LinkBrush);
            Point origin=new Point(Left, Top);

            var lines = FormattedText.GetLines().ToList();
            int start = 0;
            List<int> indx = new List<int>(){start};
            for(int i=0; i<lines.Count; i++)
            {
                origin = FormattedText.HitTestTextPosition(start).TopLeft;
                int endLineIndex=start+lines[i].Length-1;
                
                string line=Text.Substring(start, lines[i].Length);
                if(Links!=null)
                {
                    foreach (var param in Links)
                    {
                        string link = Text.Substring(param.StartIndex, param.EndIndex+1-param.StartIndex);
                        
                        if(param.StartIndex>=start&&param.StartIndex<=endLineIndex)
                        {
                            Point linkTextPoint = FormattedText.HitTestTextPosition(param.StartIndex).TopLeft;
                            FormattedText linkText=null;
                            var bounds1 = this.FormattedText.HitTestTextPosition(param.StartIndex);
                            Rect bounds2=new Rect();

                            if(param.EndIndex<=endLineIndex)
                            {
                                linkText=FormatText(link);
                                bounds2 = this.FormattedText.HitTestTextPosition(param.EndIndex);
                                indx.Add(param.StartIndex);
                                indx.Add(param.EndIndex+1);
                            }
                            else
                            {
                                linkText=FormatText(Text.Substring(param.StartIndex, endLineIndex+1-param.StartIndex));
                                bounds2 = this.FormattedText.HitTestTextPosition(endLineIndex);
                                indx.Add(param.StartIndex);
                                indx.Add(endLineIndex);
                                
                            }                            
                            
                            if(param.IsPressed)
                            {
                                Pen pressPen=new Pen(PressedBrush);
                                context.DrawText(PressedBrush, linkTextPoint+new Point(Left,Top), linkText);
                                context.DrawLine(pressPen, bounds1.BottomLeft+new Point(Left,Top), bounds2.BottomRight+new Point(Left, Top));
                            }
                            else
                            {
                                context.DrawText(LinkBrush, linkTextPoint+new Point(Left,Top), linkText);
                                context.DrawLine(underlinePen, bounds1.BottomLeft+new Point(Left,Top), bounds2.BottomRight+new Point(Left, Top));
                            }
                            
                        }
                        else if(param.StartIndex<start && param.EndIndex<=endLineIndex && start<=param.EndIndex)
                        {
                            var linkText = FormatText(Text.Substring(start, param.EndIndex+1-start));
                            var bounds1=this.FormattedText.HitTestTextPosition(start);
                            var bounds2 = this.FormattedText.HitTestTextPosition(param.EndIndex);
                            if (param.IsPressed)
                            {
                                Pen pressPen = new Pen(PressedBrush);
                                context.DrawText(PressedBrush, origin, linkText);
                                context.DrawLine(pressPen, bounds1.BottomLeft + new Point(Left, Top), bounds2.BottomRight + new Point(Left, Top));

                            }
                            else
                            {
                                context.DrawText(LinkBrush, origin, linkText);
                                context.DrawLine(underlinePen, bounds1.BottomLeft + new Point(Left, Top), bounds2.BottomRight + new Point(Left, Top));
                            }                            
                            indx.Add(start);
                            indx.Add(param.EndIndex+1);
                        }
                        else if(param.StartIndex<start && param.EndIndex>endLineIndex)
                        {
                            var lineText=FormatText(line);
                            var bounds1 = this.FormattedText.HitTestTextPosition(start);
                            var bounds2 = this.FormattedText.HitTestTextPosition(endLineIndex);
                            if (param.IsPressed)
                            {
                                Pen pressPen = new Pen(PressedBrush);
                                context.DrawText(PressedBrush, origin, lineText);
                                context.DrawLine(pressPen, bounds1.BottomLeft + new Point(Left, Top), bounds2.BottomRight + new Point(Left, Top));
                            }
                            else
                            {
                                context.DrawText(LinkBrush, origin, lineText);
                                context.DrawLine(underlinePen, bounds1.BottomLeft + new Point(Left, Top), bounds2.BottomRight + new Point(Left, Top));
                            }
                            
                            indx.Add(start);
                            indx.Add(endLineIndex);
                        }
                        else
                        {
                            if(!indx.Contains(start)) indx.Add(start);
                            if(!indx.Contains(endLineIndex)) indx.Add(endLineIndex);
                        }
                    }
                    if (!indx.Contains(endLineIndex)) { indx.Add(endLineIndex); }
                    
                }
                else
                {
                    var lineText=FormatText(line);
                    context.DrawText(ForegroundBrush, origin, lineText);
                }
                
                start+=lines[i].Length;
            }

            for(int j=0; j<indx.Count-1;j+=2)
                    {
                        var subText=Text.Substring(indx[j], indx[j+1]-indx[j]);
                        var bounds1=this.FormattedText.HitTestTextPosition(indx[j]);
                        context.DrawText(ForegroundBrush, bounds1.TopLeft+new Point(Left,Top), FormatText(subText));
                    }
        }

        private FormattedText FormatText(string text)
        {
            return new FormattedText
            {
                Constraint = FormattedText.Constraint,
                Typeface = TextTypeface,
                Text = text ?? string.Empty,
                TextAlignment = AlignText,
                Wrapping = WrapText
            };
        }

        protected override void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (Links != null)
            {
                var textPoint = FormattedText.HitTestPoint(PressedLocation);
                var textIndex = textPoint.TextPosition;
                for (int i = 0; i < Links.Count(); i++)
                {
                    var param = Links[i];
                    if (param.StartIndex <= textIndex && param.EndIndex >= textIndex)
                    {
                        param.IsPressed = true;
                        LinkPressed?.Invoke(sender, param.URL);
                        break;
                    }
                }
            }
            base.OnPointerPressed(sender, e);
        }
    }
}