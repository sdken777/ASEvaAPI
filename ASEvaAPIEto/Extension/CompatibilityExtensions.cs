using System;
using Eto.Forms;

namespace ASEva.Eto
{
    /// <summary>
    /// (api:eto=1.0.0) 解决兼容性问题的扩展
    /// </summary>
    public static class CompatibilityExtensions
    {
        /// <summary>
        /// 修正组合框内部字体问题
        /// </summary>
        /// <param name="groupBox">组合框</param>
        public static void SetTitleFontToContent(this GroupBox groupBox)
        {
            if (solver != null) solver.SetGroupBoxTitleFontToContent(groupBox);
        }

        public static CompatibilitySolver Solver
        {
            set { solver = value; }
        }
        private static CompatibilitySolver solver = null;
    }

    public interface CompatibilitySolver
    {
        void SetGroupBoxTitleFontToContent(GroupBox groupBox);
    }
}
