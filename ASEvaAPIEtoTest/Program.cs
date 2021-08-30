using System;
using ASEva.Utility;

namespace ASEvaAPIEtoTest
{
    partial class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var t = TextResource.Load("test.xml", "en");
            Console.WriteLine(t["plain-text"]);
            Console.WriteLine(t.Format("format-text", 777));

            t = TextResource.Load("test.xml", "ch");
            Console.WriteLine(t["plain-text"]);
            Console.WriteLine(t.Format("format-text", 777));
        }
    }
}