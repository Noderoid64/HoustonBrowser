using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Media;

namespace HoustonBrowser.Render
{
    public class Style
    {
        Rectangle distanceBetweenControl;
        Rectangle distanceBetweenBlock;
        public Rectangle DistanceBetweenControl { get => distanceBetweenControl; }
        public Rectangle DistanceBetweenBlock { get => distanceBetweenBlock; }

        public FontStyle FontStyle { get; set; } = FontStyle.Normal;
        public string Font { get; set; } = "Century";
        public double SizeFont { get; set; } = 14;
        public bool Bold { get; set; } = false;

        double marginBlockStart;
        double marginBlockEnd;
        double marginInlineStart;
        double marginInlineEnd;

        double paddingTop;
        double paddingBotton;
        double paddingLeft;
        double paddingRight;

        public Style
            (
            double marginBlockStart,
            double marginBlockEnd,
            double marginInlineStart,
            double marginInlineEnd
            )
        {
            paddingTop = 0;
            paddingBotton = 0;
            paddingLeft = 0;
            paddingRight = 0;

            distanceBetweenControl = new Rectangle(
                marginInlineStart,
                marginBlockStart,
                (marginInlineStart + marginInlineEnd),
                (marginBlockStart + marginBlockEnd)
                );

            distanceBetweenBlock = new Rectangle(paddingLeft, paddingTop, (paddingLeft + paddingRight), (paddingTop + paddingBotton));
        }
    }
}
