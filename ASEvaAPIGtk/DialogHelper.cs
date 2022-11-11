using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASEva;
using Gtk;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) 对话框辅助类
    /// </summary>
    public class DialogHelper
    {
        public static void Notice(String message)
        {
            if (message == null) return;

            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";

            var msgbox = new MessageDialog(TopWindow, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "{0:s}", message);
            msgbox.Title = ch ? "消息" : "Notice";
            msgbox.Run();
            msgbox.Dispose();
        }

        public static void NoticeWithTitle(String message, String title)
        {
            if (message == null) return;

            var msgbox = new MessageDialog(TopWindow, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "{0:s}", message);
            msgbox.Title = title == null ? "" : title;
            msgbox.Run();
            msgbox.Dispose();
        }

        public static void Error(String message)
        {
            if (message == null) return;

            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";

            var msgbox = new MessageDialog(TopWindow, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "{0:s}", message);
            msgbox.Title = ch ? "错误" : "Error";
            msgbox.Run();
            msgbox.Dispose();
        }

        public static bool Confirm(String message)
        {
            if (message == null) return false;

            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";
            var res = false;

            var msgbox = new MessageDialog(TopWindow, DialogFlags.Modal, MessageType.Question, ButtonsType.YesNo, "{0:s}", message);
            msgbox.Title = ch ? "确认" : "Confirm";
            msgbox.Response += delegate(object o, ResponseArgs args)
            {
                res = args.ResponseId == ResponseType.Yes;
            };
            msgbox.Run();
            msgbox.Dispose();

            return res;
        }

        public static String[] OpenFile(String title, Dictionary<String, String> filters = null, String origin = null, bool multiple = false)
        {
            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";

            FileChooserDialog target = new FileChooserDialog(title == null ? "" : title, TopWindow, FileChooserAction.Open, ch ? "取消" : "Cancel", ResponseType.Cancel, ch ? "打开" : "Open", ResponseType.Ok);

            if (filters != null)
            {
                foreach (var pair in filters)
                {
                    var filter = new FileFilter();
                    filter.Name = pair.Value;
                    filter.AddPattern(pair.Key);
                    target.AddFilter(filter);
                }
            }

            if (origin != null && File.Exists(origin)) target.SelectFilename(origin);

            target.SelectMultiple = multiple;

            var result = target.Run();
            var output = result == (int)ResponseType.Ok ? (multiple ? target.Filenames : new String[] {target.Filename}) : null;

            target.Dispose();

            return output;
        }

        public static String SaveFile(String title, KeyValuePair<String, String>? filter = null, String origin = null)
        {
            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";

            FileChooserDialog target = new FileChooserDialog(title == null ? "" : title, TopWindow, FileChooserAction.Save, ch ? "取消" : "Cancel", ResponseType.Cancel, ch ? "保存" : "Save", ResponseType.Ok);

            target.DoOverwriteConfirmation = true;

            if (String.IsNullOrEmpty(origin)) origin = ch ? "未命名" : "Untitled";

            String extension = null;
            if (filter == null)
            {
                target.CurrentName = origin;
            }
            else
            {
                var ff = new FileFilter();
                ff.Name = filter.Value.Value;
                ff.AddPattern(filter.Value.Key);
                target.AddFilter(ff);

                extension = "." + filter.Value.Key.Split('.').Last();
                target.CurrentName = origin + extension;
            }

            String output = null;
            while (target.Run() == (int)ResponseType.Ok)
            {
                if (extension == null || target.Filename.EndsWith(extension))
                {
                    output = target.Filename;
                    break;
                }
                else
                {
                    target.CurrentName = System.IO.Path.GetFileNameWithoutExtension(target.Filename) + extension;
                }
            }

            target.Dispose();

            return output;
        }

        public static String OpenDir(String title, String origin = null)
        {
            var lang = Agency.GetAppLanguage();
            var ch = lang != null && lang == "ch";

            var dialog = new FileChooserDialog(title == null ? "" : title, TopWindow, FileChooserAction.SelectFolder, ch ? "取消" : "Cancel", ResponseType.Cancel, ch ? "打开" : "Open", ResponseType.Ok);
            dialog.CreateFolders = true;

            if (origin != null && Directory.Exists(origin)) dialog.SelectFilename(origin);

            var result = dialog.Run();
            var output = result == (int)ResponseType.Ok ? dialog.Filename : null;

            dialog.Dispose();

            return output;
        }

        /// <summary>
        /// 顶层窗口
        /// </summary>
        public static Window TopWindow
        {
            get
            {
                var windows = Window.ListToplevels();
                for (int i = 0; i < windows.Length; i++)
                {
                    if (windows[i].WindowType == WindowType.Popup) continue;
                    if (MainWindow != null && windows[i].Equals(MainWindow)) continue;
                    if (OtherMainWindows != null && OtherMainWindows.Contains(windows[i])) continue;
                    if (windows[i].IsActive) return windows[i];
                }
                return null;
            }
        }

        /// <summary>
        /// 主窗口
        /// </summary>
        public static Window MainWindow { get; set; }

        /// <summary>
        /// (api:gtk=2.4.3) 其他主窗口
        /// </summary>
        public static Window[] OtherMainWindows { get; set; }
    }
}
