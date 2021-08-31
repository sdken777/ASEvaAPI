/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.Eto \n
 * 本文档对应API版本：1.1.1
 */

using System;

namespace ASEva.Eto
{
    /// <summary>
    /// version=1.1.1
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 1, 1, 0); // 新增SizerExtensions.SetLocalcalWidth/Height/Size。新增Control.Sizer。新增App。新增AddExtensions.AddButton/LinkButton/CheckBox/RadioButtonList/ComboBox/GroupBox/Row/RowStackLayout/ColumnStackLayout/TableLayout。新增SetContentExtensions。AddExtensions.AddControl接口变更。移除Panel/Dialog.Sizer。使用PixelScale前自动初始化
        }
    }
}
