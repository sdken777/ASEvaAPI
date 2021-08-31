using System;
using ASEva.Eto;

namespace ASEvaAPIEtoTest
{
    partial class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App.Init();
            App.Run(new TestWindow("ch"/* 改为"en"显示英文 */));
        }
    }
}