
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace ASEva.UIGtk
{
	static class NativeMethods
	{
		static bool useNM3;

		public static void DetectNM3()
		{
			useNM3 = false;

			String libRoot = null;
			switch (ASEva.APIInfo.GetRunningOS())
			{
			case "linux":
				libRoot = "/usr/lib/x86_64-linux-gnu";
				break;
			case "linuxarm":
				libRoot = "/usr/lib/aarch64-linux-gnu";
				break;
			default:
				return;
			}
			
			if (!Directory.Exists(libRoot)) return;

			foreach (var filePath in Directory.GetFiles(libRoot))
			{
				if (Path.GetFileName(filePath).StartsWith(NM3.libwebkit))
				{
					useNM3 = true;
					return;
				}
			}
		}

		static class NM3
		{
			public const string libwebkit = "libwebkit2gtk-3.0.so.25";

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_new();

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_load_uri(IntPtr web_view, string uri);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_get_uri(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_get_title(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_reload(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_stop_loading(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static bool webkit_web_view_can_go_back(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_go_back(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static bool webkit_web_view_can_go_forward(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_go_forward(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_javascript_result_get_value(IntPtr js_result);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void JSStringRelease(IntPtr js_str_value);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_uri_request_get_uri(IntPtr request);
		}

		static class NM4
		{
			public const string libwebkit = "libwebkit2gtk-4.0.so.37";

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_new();

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_load_uri(IntPtr web_view, string uri);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_get_uri(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_get_title(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_reload(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_stop_loading(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static bool webkit_web_view_can_go_back(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_go_back(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static bool webkit_web_view_can_go_forward(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_go_forward(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_javascript_result_get_value(IntPtr js_result);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void JSStringRelease(IntPtr js_str_value);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_uri_request_get_uri(IntPtr request);
		}

		public static string GetString(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				return "";

			int len = 0;
			while (Marshal.ReadByte(handle, len) != 0)
				len++;

			var bytes = new byte[len];
			Marshal.Copy(handle, bytes, 0, bytes.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		public static IntPtr webkit_web_view_new()
		{
			if (useNM3) return NM3.webkit_web_view_new();
			else return NM4.webkit_web_view_new();
		}

		public static void webkit_web_view_load_uri(IntPtr web_view, string uri)
		{
			if (useNM3) NM3.webkit_web_view_load_uri(web_view, uri);
			else NM4.webkit_web_view_load_uri(web_view, uri);
		}

		public static string webkit_web_view_get_uri(IntPtr web_view)
		{
			if (useNM3) return GetString(NM3.webkit_web_view_get_uri(web_view));
			else return GetString(NM4.webkit_web_view_get_uri(web_view));
		}

		public static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri)
		{
			if (useNM3) NM3.webkit_web_view_load_html(web_view, content, base_uri);
			else NM4.webkit_web_view_load_html(web_view, content, base_uri);
		}

		public static string webkit_web_view_get_title(IntPtr web_view)
		{
			if (useNM3) return null;
			else return GetString(NM4.webkit_web_view_get_title(web_view));
		}

		public static void webkit_web_view_reload(IntPtr web_view)
		{
			if (useNM3) {}
			else NM4.webkit_web_view_reload(web_view);
		}

		public static void webkit_web_view_stop_loading(IntPtr web_view)
		{
			if (useNM3) {}
			else NM4.webkit_web_view_stop_loading(web_view);
		}

		public static bool webkit_web_view_can_go_back(IntPtr web_view)
		{
			if (useNM3) return false;
			else return NM4.webkit_web_view_can_go_back(web_view);
		}

		public static void webkit_web_view_go_back(IntPtr web_view)
		{
			if (useNM3) {}
			else NM4.webkit_web_view_go_back(web_view);
		}

		public static bool webkit_web_view_can_go_forward(IntPtr web_view)
		{
			if (useNM3) return false;
			else return NM4.webkit_web_view_can_go_forward(web_view);
		}

		public static void webkit_web_view_go_forward(IntPtr web_view)
		{
			if (useNM3) {}
			else NM4.webkit_web_view_go_forward(web_view);
		}

		public static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data)
		{
			if (useNM3) {}
			else NM4.webkit_web_view_run_javascript(web_view, script, cancellable, callback, user_data);
		}

		public static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error)
		{
			if (useNM3) return IntPtr.Zero;
			else return NM4.webkit_web_view_run_javascript_finish(web_view, result, error);
		}

		public static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result)
		{
			if (useNM3) return IntPtr.Zero;
			else return NM4.webkit_javascript_result_get_global_context(js_result);
		}

		public static IntPtr webkit_javascript_result_get_value(IntPtr js_result)
		{
			if (useNM3) return IntPtr.Zero;
			else return NM4.webkit_javascript_result_get_value(js_result);
		}

		public static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk)
		{
			if (useNM3) return IntPtr.Zero;
			else return NM4.JSValueToStringCopy(context, value, idk);
		}

		public static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value)
		{
			if (useNM3) return 0;
			else return NM4.JSStringGetMaximumUTF8CStringSize(js_str_value);
		}

		public static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length)
		{
			if (useNM3) {}
			else NM4.JSStringGetUTF8CString(js_str_value, str_value, str_length);
		}

		public static void JSStringRelease(IntPtr js_str_value)
		{
			if (useNM3) {}
			else NM4.JSStringRelease(js_str_value);
		}

		public static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision)
		{
			if (useNM3) return IntPtr.Zero;
			else return NM4.webkit_navigation_policy_decision_get_request(decision);
		}

		public static string webkit_uri_request_get_uri(IntPtr request)
		{
			if (useNM3) return null;
			else return GetString(NM4.webkit_uri_request_get_uri(request));
		}
	}
}
