using Eto.Forms;
using Eto.Wpf.Forms.Controls;

namespace ASEva.UIWpf
{
	class SearchBoxHandler : TextBoxHandler, SearchBox.IHandler
	{
        public SearchBoxHandler()
        {
            var lang = Agency.GetAppLanguage();
            PlaceholderText = lang != null && lang == "ch" ? " ËÑË÷" : " Search";
        }
    }
}
