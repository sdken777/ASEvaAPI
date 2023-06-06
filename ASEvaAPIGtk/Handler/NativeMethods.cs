
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace ASEva.UIGtk
{
	static class NativeMethods
	{
		static class NM4
		{
			public const string libwebkit = "libwebkit2gtk-4.0.so.37";

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_new();

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_new_with_settings(IntPtr settings);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_settings_new();

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_settings_set_enable_accelerated_2d_canvas(IntPtr settings, bool enable);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_settings_set_enable_webgl(IntPtr settings, bool enable);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_settings_set_hardware_acceleration_policy(IntPtr settings, int policy);

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

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static bool webkit_response_policy_decision_is_mime_type_supported(IntPtr decision);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_policy_decision_download(IntPtr decision);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_response_policy_decision_get_request(IntPtr decision);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_javascript_result_unref(IntPtr result);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static IntPtr webkit_web_view_get_inspector(IntPtr web_view);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_inspector_show(IntPtr inspector);

			[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
			public extern static void webkit_web_inspector_detach(IntPtr inspector);
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
			return NM4.webkit_web_view_new();
		}

		public static IntPtr webkit_web_view_new_with_settings(IntPtr settings)
		{
			return NM4.webkit_web_view_new_with_settings(settings);
		}

		public static IntPtr webkit_settings_new()
		{
			return NM4.webkit_settings_new();
		}

		public static void webkit_settings_set_enable_accelerated_2d_canvas(IntPtr settings, bool enable)
		{
			NM4.webkit_settings_set_enable_accelerated_2d_canvas(settings, enable);
		}

		public static void webkit_settings_set_enable_webgl(IntPtr settings, bool enable)
		{
			NM4.webkit_settings_set_enable_webgl(settings, enable);
		}

		public static void webkit_settings_set_hardware_acceleration_policy(IntPtr settings, int policy)
		{
			NM4.webkit_settings_set_hardware_acceleration_policy(settings, policy);
		}

		public static void webkit_web_view_load_uri(IntPtr web_view, string uri)
		{
			NM4.webkit_web_view_load_uri(web_view, uri);
		}

		public static string webkit_web_view_get_uri(IntPtr web_view)
		{
			return GetString(NM4.webkit_web_view_get_uri(web_view));
		}

		public static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri)
		{
			NM4.webkit_web_view_load_html(web_view, content, base_uri);
		}

		public static string webkit_web_view_get_title(IntPtr web_view)
		{
			return GetString(NM4.webkit_web_view_get_title(web_view));
		}

		public static void webkit_web_view_reload(IntPtr web_view)
		{
			NM4.webkit_web_view_reload(web_view);
		}

		public static void webkit_web_view_stop_loading(IntPtr web_view)
		{
			NM4.webkit_web_view_stop_loading(web_view);
		}

		public static bool webkit_web_view_can_go_back(IntPtr web_view)
		{
			return NM4.webkit_web_view_can_go_back(web_view);
		}

		public static void webkit_web_view_go_back(IntPtr web_view)
		{
			NM4.webkit_web_view_go_back(web_view);
		}

		public static bool webkit_web_view_can_go_forward(IntPtr web_view)
		{
			return NM4.webkit_web_view_can_go_forward(web_view);
		}

		public static void webkit_web_view_go_forward(IntPtr web_view)
		{
			NM4.webkit_web_view_go_forward(web_view);
		}

		public static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data)
		{
			NM4.webkit_web_view_run_javascript(web_view, script, cancellable, callback, user_data);
		}

		public static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error)
		{
			return NM4.webkit_web_view_run_javascript_finish(web_view, result, error);
		}

		public static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result)
		{
			return NM4.webkit_javascript_result_get_global_context(js_result);
		}

		public static IntPtr webkit_javascript_result_get_value(IntPtr js_result)
		{
			return NM4.webkit_javascript_result_get_value(js_result);
		}

		public static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk)
		{
			return NM4.JSValueToStringCopy(context, value, idk);
		}

		public static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value)
		{
			return NM4.JSStringGetMaximumUTF8CStringSize(js_str_value);
		}

		public static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length)
		{
			NM4.JSStringGetUTF8CString(js_str_value, str_value, str_length);
		}

		public static void JSStringRelease(IntPtr js_str_value)
		{
			NM4.JSStringRelease(js_str_value);
		}

		public static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision)
		{
			return NM4.webkit_navigation_policy_decision_get_request(decision);
		}

		public static string webkit_uri_request_get_uri(IntPtr request)
		{
			return GetString(NM4.webkit_uri_request_get_uri(request));
		}

		public static bool webkit_response_policy_decision_is_mime_type_supported(IntPtr decision)
		{
			return NM4.webkit_response_policy_decision_is_mime_type_supported(decision);
		}

		public static void webkit_policy_decision_download(IntPtr decision)
		{
			NM4.webkit_policy_decision_download(decision);
		}

		public static IntPtr webkit_response_policy_decision_get_request(IntPtr decision)
		{
			return NM4.webkit_response_policy_decision_get_request(decision);
		}

		public static void webkit_javascript_result_unref(IntPtr result)
		{
			NM4.webkit_javascript_result_unref(result);
		}

		public static IntPtr webkit_web_view_get_inspector(IntPtr web_view)
		{
			return NM4.webkit_web_view_get_inspector(web_view);
		}

		public static void webkit_web_inspector_show(IntPtr inspector)
		{
			NM4.webkit_web_inspector_show(inspector);
		}

		public static void webkit_web_inspector_detach(IntPtr inspector)
		{
			NM4.webkit_web_inspector_detach(inspector);
		}
	}
}
