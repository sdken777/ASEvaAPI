using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 双缓存ListView
    /// </summary>
    public class DoubleBufferListView : ListView
    {
        public DoubleBufferListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }
}
