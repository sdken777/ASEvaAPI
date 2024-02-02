using Eto.Forms;
using Eto.Wpf.Forms.Controls;

namespace ASEva.UIWpf
{
	class SearchBoxHandler : TextBoxHandler, SearchBox.IHandler
	{
        // CHECK: 增加文字为空时的水印显示 (eto-2.7.5新增的官方版本风格与wpf默认风格不一致)
        public SearchBoxHandler()
        {
            var lang = Agency.GetAppLanguage();
            PlaceholderText = lang != null && lang == "ch" ? " 搜索" : " Search";

            var textBox = Control as EtoWatermarkTextBox;
            textBox.MouseRightButtonUp += (o, e) =>
            {
                textBox.Text = "";
                e.Handled = true;
            };
        }
    }
}
