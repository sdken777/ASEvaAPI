using System;
using Eto.Forms;
using Eto.Drawing;
using Eto.Mac.Drawing;
using System.Text.RegularExpressions;
using System.Linq;
using MonoMac.AppKit;


namespace ASEva.UIMonoMac
{
	class LabelHandler : MacLabel<NSTextField, Label, Label.ICallback>, Label.IHandler
	{
	}
}
