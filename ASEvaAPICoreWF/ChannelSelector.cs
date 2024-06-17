using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Channel selector
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 通道选择器
    /// </summary>
    public partial class ChannelSelector : UserControl
    {
        public ChannelSelector()
        {
            InitializeComponent();

            var dpiRatio = (float)DeviceDpi / 96;
            gridSize = (int)(dpiRatio * 22);
        }

        /// \~English
        /// <summary>
        /// Set whether the data of each channel is available
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设置各通道数据是否有效
        /// </summary>
        public void SetChannelAvailables(bool[] availables)
        {
            for (int i = 0; i < availables.Length; i++) channelAvailables[i] = availables[i];
            for (int i = availables.Length; i < 12; i++) channelAvailables[i] = false;
            DrawBeat.CallerBegin(this);
            this.Refresh();
            DrawBeat.CallerEnd(this);
        }

        /// \~English
        /// <summary>
        /// Number of channels
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelCount
        {
            get { return channelCount; }
            set
            {
                channelCount = Math.Max(1, Math.Min(12, value));
                if (targetChannel >= channelCount) targetChannel = 0;
                DrawBeat.CallerBegin(this);
                this.Refresh();
                DrawBeat.CallerEnd(this);
            }
        }

        /// \~English
        /// <summary>
        /// The selected channel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 选中通道
        /// </summary>
        public int TargetChannel
        {
            get { return targetChannel; }
            set
            {
                if (value < 0 || value >= channelCount) return;
                targetChannel = value;
                DrawBeat.CallerBegin(this);
                this.Refresh();
                DrawBeat.CallerEnd(this);
            }
        }

        /// \~English
        /// <summary>
        /// Channel protocol, without '@x' 
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道协议，不需要加'@x' 
        /// </summary>
        public String ChannelProtocol { get; set; }

        public delegate void ChannelSelectorHander(ChannelSelector selector, int channel);
        public event ChannelSelectorHander ChannelClicked;

        private int channelCount = 4;
        private int targetChannel = 0;
        private bool[] channelAvailables = new bool[12];

        private int gridSize = 22;

        private ToolTip tooltip = new ToolTip();

        private void ChannelSelector_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < channelCount; i++)
            {
                if (e.X >= i * gridSize && e.X < (i + 1) * gridSize)
                {
                    TargetChannel = i;
                    if (ChannelClicked != null) ChannelClicked(this, i);
                    return;
                }
            }
        }

        private void ChannelSelector_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(this, "ASEva.UICoreWF.ChannelSelector");

            try
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                var silverBrush = new SolidBrush(Color.Silver);
                var grayBrush = new SolidBrush(Color.Gray);
                var paleGreenBrush = new SolidBrush(Color.PaleGreen);
                var limeGreenBrush = new SolidBrush(Color.LimeGreen);
                var fontBrush = new SolidBrush(Color.Black);
                var font = new Font("微软雅黑", 10.5f);

                var dpiRatio = (float)DeviceDpi / 96;

                for (int i = 0; i < channelCount; i++)
                {
                    Brush brush = null;
                    if (channelAvailables[i])
                    {
                        if (targetChannel == i) brush = paleGreenBrush;
                        else brush = limeGreenBrush;
                    }
                    else
                    {
                        if (targetChannel == i) brush = silverBrush;
                        else brush = grayBrush;
                    }

                    e.Graphics.FillRectangle(brush, gridSize * i, 0, gridSize, gridSize);

                    var text = ((char)('A' + i)).ToString();
                    e.Graphics.DrawString(text, font, fontBrush, gridSize * i + gridSize / 2 - 0.5f - e.Graphics.MeasureString(text, font).Width * 0.5f, dpiRatio * 2.0f);
                }
            }
            catch (Exception) { }

            DrawBeat.CallbackEnd(this);
        }

        private async void ChannelSelector_MouseMove(object sender, MouseEventArgs e)
        {
            var channel = this.PointToClient(Cursor.Position).X / gridSize;
            var text = "Channel " + (char)('A' + channel);
            if (!String.IsNullOrWhiteSpace(ChannelProtocol))
            {
                var name = await AgencyAsync.GetChannelAliasName(ChannelProtocol + "@" + channel);
                if (!String.IsNullOrWhiteSpace(name)) text = name;
            }
            tooltip.SetToolTip(this, text);
        }
    }
}
