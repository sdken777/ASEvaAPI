using Eto.Forms;
using Eto.Wpf.Forms.Controls;

namespace ASEva.UIWpf
{
	class SearchBoxHandler : TextBoxHandler, SearchBox.IHandler
	{
        // CHECK: 增加文字为空时的水印显示
        public SearchBoxHandler()
        {
            var lang = Agency.GetAppLanguage();
            PlaceholderText = lang != null && lang == "ch" ? " 搜索" : " Search";
        }
    }
}
