using System;
using System.Windows.Forms;

namespace ASEva.CoreWF
{
    /// <summary>
    /// (api:corewf=1.0.0) 双缓存ListView
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
