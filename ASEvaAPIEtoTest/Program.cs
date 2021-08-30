using System;
using ASEva.Utility;

namespace ASEvaAPIEtoTest
{
    partial class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var t = TextResource.Load("test.xml");
            Console.WriteLine(t["plain-text"]);
            Console.WriteLine(t.Format("format-text", 777));
        }
    }
}