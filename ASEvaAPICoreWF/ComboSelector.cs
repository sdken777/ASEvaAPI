using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 基于组合框的通道选择器，集成输入通道别名和有效性显示
    /// </summary>
    public partial class ComboSelector : UserControl
    {
        public ComboSelector()
        {
            InitializeComponent();
        }

        public void Init()
        {
            if (SelectedChannel < 0)
            {
                if (CanBeDisabled) SelectedChannel = -1;
                else SelectedChannel = 0;
            }
            ChannelCount = Math.Max(ChannelCount, 2);
            SelectedChannel = Math.Min(SelectedChannel, ChannelCount - 1);
            if (Protocol == null || Protocol.Length == 0) Protocol = "unknown";

            comboBox1.Items.Clear();

            if (CanBeDisabled) comboBox1.Items.Add("(Disabled)");

            for (int i = 0; i < ChannelCount; i++)
            {
                var channelChar = (char)((char)'A' + i);

                var alias = Agency.GetChannelAliasName(Protocol + "@" + i);
                if (alias == null || alias.Length == 0) comboBox1.Items.Add(channelChar + ": (Not available)");
                else comboBox1.Items.Add(channelChar + ": " + alias);
            }

            if (CanBeDisabled) comboBox1.SelectedIndex = SelectedChannel + 1;
            else comboBox1.SelectedIndex = SelectedChannel;
        }

        public int SelectedChannel { get; set; }

        public bool CanBeDisabled { get; set; }

        public String Protocol { get; set; }

        public int ChannelCount { get; set; }

        public event EventHandler SelectedChannelChanged;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanBeDisabled) SelectedChannel = comboBox1.SelectedIndex - 1;
            else SelectedChannel = comboBox1.SelectedIndex;
            if (SelectedChannelChanged != null) SelectedChannelChanged(this, e);
        }
    }
}
