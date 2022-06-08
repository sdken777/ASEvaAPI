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
                if (String.IsNullOrEmpty(text.text)) continue;

                var defaultFont = App.DefaultFont();
                var defaultFontName = "Noto Sans CJK SC";
                
                cairo.SetFontSize((text.sizeScale == 0 ? 1.0 : text.sizeScale) * defaultFont.Size);
                cairo.SelectFontFace(String.IsNullOrEmpty(text.fontName) ? defaultFontName : text.fontName, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
                cairo.SetSourceColor(new Cairo.Color((double)text.red / 255, (double)text.green / 255, (double)text.blue / 255, text.alpha == 0 ? 1.0 : ((double)text.alpha / 255)));
                
                var posX = text.posX;
                var posY = text.posY;
                if (text.isRealPos)
                {
                    posX = (int)(posX / size.RealPixelScale);
                    posY = (int)(posY / size.RealPixelScale);
                }

                var textSize = cairo.TextExtents(text.text);
                int fullWidth = (int)textSize.Width, fullHeight = (int)textSize.Height;
                int halfWidth = (int)(textSize.Width / 2), halfHeight = (int)(textSize.Height / 2);
                switch (text.anchor)
                {
                    case TextAnchor.Center:
                        cairo.MoveTo(posX - halfWidth, posY + halfHeight);
                        break;
                    case TextAnchor.TopLeft:
                        cairo.MoveTo(posX, posY + fullHeight);
                        break;
                    case TextAnchor.LeftCenter:
                        cairo.MoveTo(posX, posY + halfHeight);
                        break;
                    case TextAnchor.BottomLeft:
                        cairo.MoveTo(posX, posY);
                        break;
                    case TextAnchor.TopRight:
                        cairo.MoveTo(posX - fullWidth, posY + fullHeight);
                        break;
                    case TextAnchor.RightCenter:
                        cairo.MoveTo(posX - fullWidth, posY + halfHeight);
                        break;
                    case TextAnchor.BottomRight:
                        cairo.MoveTo(posX - fullWidth, posY);
                        break;
                    case TextAnchor.TopCenter:
                        cairo.MoveTo(posX - halfWidth, posY + fullHeight);
                        break;
                    case TextAnchor.BottomCenter:
                        cairo.MoveTo(posX - halfWidth, posY);
                        break;
                }
                cairo.ShowText(text.text);
            }
        }
    }
}