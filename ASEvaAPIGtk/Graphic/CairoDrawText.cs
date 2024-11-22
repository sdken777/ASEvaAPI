using System;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    class CairoDrawText
    {
        public static void Draw(Cairo.Context cairo, GLTextTask[] textTasks, GLSizeInfo size)
        {
            foreach (var text in textTasks)
            {
                if (String.IsNullOrEmpty(text.Text)) continue;

                var defaultFontName = "Noto Sans CJK SC";
                var defaultFontSize = 12.0f;
                
                cairo.SetFontSize((text.SizeScale == 0 ? 1.0 : text.SizeScale) * defaultFontSize);
                cairo.SelectFontFace(String.IsNullOrEmpty(text.FontName) ? defaultFontName : text.FontName, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
                cairo.SetSourceColor(new Cairo.Color((double)text.Red / 255, (double)text.Green / 255, (double)text.Blue / 255, text.Alpha == 0 ? 1.0 : ((double)text.Alpha / 255)));
                
                var posX = text.PosX;
                var posY = text.PosY;
                if (text.IsRealPos)
                {
                    posX = (int)(posX / size.RealPixelScale);
                    posY = (int)(posY / size.RealPixelScale);
                }

                var textSize = cairo.TextExtents(text.Text);
                int fullWidth = (int)textSize.Width;
                int halfWidth = (int)(textSize.Width / 2);
                int topHeightOffset = (int)(0.95 * textSize.Height);
                int middleHeightOffset = (int)(0.45 * textSize.Height);
                int bottomHeightOffset = (int)(-0.05 * textSize.Height);
                switch (text.Anchor)
                {
                    case TextAnchor.Center:
                        cairo.MoveTo(posX - halfWidth, posY + middleHeightOffset);
                        break;
                    case TextAnchor.TopLeft:
                        cairo.MoveTo(posX, posY + topHeightOffset);
                        break;
                    case TextAnchor.LeftCenter:
                        cairo.MoveTo(posX, posY + middleHeightOffset);
                        break;
                    case TextAnchor.BottomLeft:
                        cairo.MoveTo(posX, posY + bottomHeightOffset);
                        break;
                    case TextAnchor.TopRight:
                        cairo.MoveTo(posX - fullWidth, posY + topHeightOffset);
                        break;
                    case TextAnchor.RightCenter:
                        cairo.MoveTo(posX - fullWidth, posY + middleHeightOffset);
                        break;
                    case TextAnchor.BottomRight:
                        cairo.MoveTo(posX - fullWidth, posY + bottomHeightOffset);
                        break;
                    case TextAnchor.TopCenter:
                        cairo.MoveTo(posX - halfWidth, posY + topHeightOffset);
                        break;
                    case TextAnchor.BottomCenter:
                        cairo.MoveTo(posX - halfWidth, posY + bottomHeightOffset);
                        break;
                }
                cairo.ShowText(text.Text);
            }
        }
    }
}