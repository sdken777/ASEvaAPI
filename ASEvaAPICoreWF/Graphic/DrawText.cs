using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
    class DrawText
    {
        public static void Draw(OpenGL gl, GLTextTask task, GLSizeInfo size, IntPtr hdc, ref IntPtr? oldFont)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            gl.Color(task.red, task.green, task.blue, task.alpha == 0 ? (byte)255 : task.alpha);

            int posX = task.posX;
            int posY = task.posY;
            if (!task.isRealPos)
            {
                posX = (int)(size.RealPixelScale * posX);
                posY = (int)(size.RealPixelScale * posY);
            }

            float fontSizeCoef = 1.5f;
            float textSizeCoefW = 1.15f;
            float textSizeCoefH = 1.4f;

            var defaultFont = App.DefaultFont();

            var fontName = String.IsNullOrEmpty(task.fontName) ? defaultFont.FamilyName : task.fontName;
            var fontSize = (int)Math.Round((task.sizeScale <= 0 ? 1.0f : task.sizeScale) * defaultFont.Size * fontSizeCoef * size.RealPixelScale);

            lock (ctxs)
            {
                if (!ctxs.ContainsKey(fontName)) ctxs[fontName] = new Dictionary<int, FontContext>();
                var subTable = ctxs[fontName];

                if (!subTable.ContainsKey(fontSize))
                {
                    var hFont = Win32.CreateFont(fontSize, 0, 0, 0, 0, 0, 0, 0, 1, 8, 0, 5, 2, fontName);
                    if (hFont == IntPtr.Zero) return;
                    subTable[fontSize] = new FontContext(hFont);
                }
                var fontContext = subTable[fontSize];

                var hOldFont = Win32.SelectObject(hdc, fontContext.FontHandle);
                if (oldFont == null) oldFont = hOldFont;

                var chars = task.text.ToCharArray();
                Size textSize = new Size();
                Win32.GetTextExtentPointW(hdc, task.text, chars.Length, ref textSize);
                textSize = new Size((int)(textSize.Width / textSizeCoefW), (int)(textSize.Height / textSizeCoefH));

                int fullWidth = textSize.Width;
                int fullHeight = textSize.Height;
                int halfWidth = textSize.Width / 2;
                int halfHeight = textSize.Height / 2;
                switch (task.anchor)
                {
                    case TextAnchor.TopLeft:
                        posY += fullHeight;
                        break;
                    case TextAnchor.LeftCenter:
                        posY += halfHeight;
                        break;
                    case TextAnchor.TopCenter:
                        posX -= halfWidth;
                        posY += fullHeight;
                        break;
                    case TextAnchor.Center:
                        posX -= halfWidth;
                        posY += halfHeight;
                        break;
                    case TextAnchor.BottomCenter:
                        posX -= halfWidth;
                        break;
                    case TextAnchor.TopRight:
                        posX -= fullWidth;
                        posY += fullHeight;
                        break;
                    case TextAnchor.RightCenter:
                        posX -= fullWidth;
                        posY += halfHeight;
                        break;
                    case TextAnchor.BottomRight:
                        posX -= fullWidth;
                        break;
                    default:
                        break;
                }

                if (posX < 0 && chars.Length > 0)
                {
                    var buffer = chars.ToList();
                    while (true)
                    {
                        var c = buffer[0];
                        buffer.RemoveAt(0);
                        if (buffer.Count == 0) return;

                        Win32.GetTextExtentPointW(hdc, c.ToString(), 1, ref textSize);
                        posX += (int)(textSize.Width / textSizeCoefW);
                        if (posX >= 0)
                        {
                            chars = buffer.ToArray();
                            break;
                        }
                    }
                }

                gl.RasterPos(posX, size.RealHeight - Math.Max(0, posY));

                for (int i = 0; i < chars.Length; i++)
                {
                    ushort charCode = (ushort)chars[i];
                    var groupIndex = charCode / 256;
                    var elemIndex = charCode % 256;

                    if (fontContext.ListBases[groupIndex] == null)
                    {
                        var newListBase = gl.GenLists(256);
                        Win32.wglUseFontBitmapsW(hdc, (uint)(groupIndex * 256), 256, newListBase);
                        fontContext.ListBases[groupIndex] = newListBase;
                    }

                    var listBase = fontContext.ListBases[groupIndex].Value;
                    gl.ListBase(listBase);
                    gl.CallLists(1, new uint[] { (uint)elemIndex });
                }
            }
        }

        class FontContext
        {
            public IntPtr FontHandle { get; set; }
            public uint?[] ListBases { get; set; }
            public FontContext(IntPtr fontHandle)
            {
                FontHandle = fontHandle;
                ListBases = new uint?[256];
            }
        }

        private static Dictionary<String, Dictionary<int, FontContext>> ctxs = new Dictionary<String, Dictionary<int, FontContext>>();
    }
}
