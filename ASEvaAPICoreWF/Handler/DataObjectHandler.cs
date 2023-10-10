using Eto.Forms;
using System;
using sc = System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sw = System.Windows.Forms;
using Eto.Drawing;
using System.Collections.Specialized;
using System.IO;
using static System.Windows.Forms.SwfDataObjectExtensions;
using BitmapSource = System.Drawing.Image;
using Eto;
using Eto.WinForms;
using sd = System.Drawing;
using Eto.WinForms.Drawing;

namespace ASEva.UICoreWF
{
	class DataFormatsHandler : DataFormats.IHandler
	{
		public virtual string Text => sw.DataFormats.UnicodeText;
		public virtual string Html => sw.DataFormats.Html;
		public virtual string Color => "Color";
	}

	class DataObjectHandler : DataObjectHandler<DataObject, DataObject.ICallback>
	{
		public override string[] Types => Control.GetFormats();

		public override bool ContainsText => Control.ContainsText();

		public override bool ContainsHtml => Control.ContainsText(sw.TextDataFormat.Html);

		protected override bool InnerContainsImage => Control.ContainsImage();

		protected override bool InnerContainsFileDropList => Control.ContainsFileDropList();

		public DataObjectHandler()
		{
			Control = new sw.DataObject(new DragDropLib.DataObject());
			IsExtended = true;
		}
		public DataObjectHandler(sw.IDataObject data)
			: base(data)
		{
		}

		public DataObjectHandler(sw.DataObject data)
			: base(data)
		{
		}

		protected override object InnerGetData(string type) => Control.GetData(type);

		public override void Clear()
		{
			if (IsExtended)
				Control = new sw.DataObject(new DragDropLib.DataObject());
			else
				Control = new sw.DataObject();
			Update();
		}

		protected override BitmapSource InnerGetImage() => Control.GetImage();

		protected override StringCollection InnerGetFileDropList() => Control.GetFileDropList();

		public override bool Contains(string type) => Control.GetDataPresent(type);
	}

	abstract class DataObjectHandler<TWidget, TCallback> : WidgetHandler<sw.DataObject, TWidget, TCallback>, DataObject.IHandler
		where TWidget: Widget, IDataObject
	{
		protected bool IsExtended { get; set; }
		public const string UniformResourceLocatorW_Format = "UniformResourceLocatorW";
		public const string UniformResourceLocator_Format = "UniformResourceLocator";

		public DataObjectHandler()
		{
		}

		public DataObjectHandler(sw.IDataObject data)
		{
			IsExtended = data is DragDropLib.DataObject;
			Control = new sw.DataObject(data);
		}

		public DataObjectHandler(sw.DataObject data)
		{
			Control = data;
		}

		protected virtual void Update()
		{
		}

		public abstract string[] Types { get; }

		public abstract bool ContainsText { get; }

		public virtual string Text
		{
			get { return ContainsText ? Control.GetText() : null; }
			set
			{
				if (IsExtended)
					Control.SetDataEx(sw.DataFormats.UnicodeText, value);
				else
					Control.SetText(value);
				Update();
			}
		}

		public abstract bool ContainsHtml { get; }

		public virtual string Html
		{
			get { return ContainsHtml ? Control.GetText(sw.TextDataFormat.Html) : null; }
			set
			{
				if (IsExtended)
					Control.SetDataEx(sw.DataFormats.Html, value);
				else
					Control.SetText(value, sw.TextDataFormat.Html);
				Update();
			}
		}

		protected abstract bool InnerContainsImage { get; }
		protected abstract BitmapSource InnerGetImage();

		public bool ContainsImage => InnerContainsImage || Contains(sw.DataFormats.Dib);

		public Image Image
		{
			get
			{
				if (InnerContainsImage)
				{
					// clipboard returns true but the image returned is null sometimes.. hrmph.
					var img = InnerGetImage().ToEto();
					if (img != null)
						return img;
				}
				if (Contains(sw.DataFormats.Dib) && InnerGetData(sw.DataFormats.Dib) is Stream stream)
					return DataObjectWin32.FromDIB(stream);
				return null;
			}
			set
			{
				var dib = (value as Bitmap).ToDIB();
				if (dib != null)
				{
					// write a DIB here, so we can preserve transparency of the image
					if (IsExtended)
						Control.SetDataEx(sw.DataFormats.Dib, dib);
					else
						Control.SetData(sw.DataFormats.Dib, dib);
				}
				else if (IsExtended)
					Control.SetDataEx(sw.DataFormats.Bitmap, value.ToSD());
				else
					Control.SetImage(value.ToSD());

				Update();
			}
		}

		protected abstract bool InnerContainsFileDropList { get; }

		protected abstract StringCollection InnerGetFileDropList();

		public bool ContainsUris => InnerContainsFileDropList
			|| Contains(UniformResourceLocatorW_Format)
			|| Contains(UniformResourceLocator_Format);

		public Uri[] Uris
		{
			get
			{
				var list = InnerContainsFileDropList
					? InnerGetFileDropList().OfType<string>().Select(s => new Uri(s)) 
					: null;

				string urlString = null;
				if (Contains(UniformResourceLocatorW_Format))
					urlString = GetString(UniformResourceLocatorW_Format, Encoding.Unicode);
				else if (Contains(UniformResourceLocator_Format))
					urlString = GetString(UniformResourceLocator_Format);

				if (!string.IsNullOrEmpty(urlString) && Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out var uri))
				{
					var uris = new[] { uri };
					list = list?.Concat(uris) ?? uris;
				}
				return list?.ToArray();
			}
			set
			{
				if (value != null)
				{
					var coll = value as IList<Uri> ?? value.ToList();

					// file uris
					var files = new StringCollection();
					files.AddRange(coll.Where(r => r.IsFile).Select(r => r.LocalPath).ToArray());
					if (files.Count > 0)
					{
						if (IsExtended)
							Control.SetDataEx(sw.DataFormats.FileDrop, files.OfType<string>().ToArray());
						else
							Control.SetFileDropList(files);
					}

					// web uris (windows only supports one)
					var url = coll.Where(r => !r.IsFile).FirstOrDefault();
					if (url != null)
					{
						SetString(url.ToString(), UniformResourceLocator_Format);
						SetString(url.ToString(), UniformResourceLocatorW_Format);
					}
				}
				else
				{
					if (IsExtended)
					{
						Control.SetDataEx(sw.DataFormats.FileDrop, null);
						Control.SetDataEx(UniformResourceLocatorW_Format, null);
						Control.SetDataEx(UniformResourceLocator_Format, null);
					}
					else
					{
						Control.SetData(sw.DataFormats.FileDrop, null);
						Control.SetData(UniformResourceLocatorW_Format, null);
						Control.SetData(UniformResourceLocator_Format, null);
					}
				}
				Update();
			}
		}

		public abstract void Clear();

		public abstract bool Contains(string type);

		protected abstract object InnerGetData(string type);

		public byte[] GetData(string type)
		{
			if (Contains(type))
			{
				return GetAsData(InnerGetData(type));
			}
			return null;
		}

		protected byte[] GetAsData(object data)
		{
			if (data is byte[] bytes)
				return bytes;
			if (data is string str)
				return Encoding.UTF8.GetBytes(str);
			if (data is MemoryStream ms)
				return ms.ToArray();
			if (data is Stream stream)
			{
				ms = new MemoryStream();
				stream.CopyTo(ms);
				ms.Position = 0;
				return ms.ToArray();
			}
			if (data != null)
			{
				var converter = sc.TypeDescriptor.GetConverter(data.GetType());
				if (converter != null && converter.CanConvertTo(typeof(byte[])))
				{
					return converter.ConvertTo(data, typeof(byte[])) as byte[];
				}
#pragma warning disable 618
				var etoConverter = TypeDescriptor.GetConverter(data.GetType());
				if (etoConverter != null && etoConverter.CanConvertTo(typeof(byte[])))
				{
					return etoConverter.ConvertTo(data, typeof(byte[])) as byte[];
				}
#pragma warning restore 618
			}
			if (data is IConvertible)
				return Convert.ChangeType(data, typeof(byte[])) as byte[];
			return null;
		}

		public string GetString(string type) => GetString(type, Encoding.UTF8);

		protected string GetString(string type, Encoding encoding)
		{
			if (string.IsNullOrEmpty(type))
				return Text;
			if (!Contains(type))
				return null;
			return GetAsString(InnerGetData(type), encoding);
		}

		protected string GetAsString(object data, Encoding encoding)
		{
			if (data is string str)
				return str;
			if (data is MemoryStream ms)
				return encoding.GetString(ms.ToArray()).TrimEnd('\0'); // can contain a zero at the end, thanks windows.
			if (data != null)
			{
				var converter = sc.TypeDescriptor.GetConverter(data.GetType());
				if (converter != null && converter.CanConvertTo(typeof(string)))
				{
					return converter.ConvertTo(data, typeof(string)) as string;
				}
#pragma warning disable 618
				var etoConverter = TypeDescriptor.GetConverter(data.GetType());
				if (etoConverter != null && etoConverter.CanConvertTo(typeof(string)))
				{
					return etoConverter.ConvertTo(data, typeof(string)) as string;
				}
#pragma warning restore 618
			}
			if (data is IConvertible)
				return Convert.ChangeType(data, typeof(string)) as string;
			return null;
		}

		public void SetData(byte[] value, string type)
		{
			if (IsExtended)
				Control.SetDataEx(type, value);
			else
				Control.SetData(type, value);
			Update();
		}

		public void SetString(string value, string type)
		{
			if (string.IsNullOrEmpty(type))
				Text = value;
			else if (IsExtended)
				Control.SetDataEx(type, value);
			else
				Control.SetData(type, value);

			Update();
		}

		public void SetDragImage(Bitmap bitmap, PointF offset)
		{
			Control.SetDragImage(bitmap.ToSD(), offset.ToSDPoint());
		}

		public bool TrySetObject(object value, string type)
		{
			return false;
		}

		public bool TryGetObject(string type, out object value)
		{
			value = null;
			return false;
		}

		public void SetObject(object value, string type) => Widget.SetObject(value, type);

		public T GetObject<T>(string type) => Widget.GetObject<T>(type);
	}

	static class DataObjectWin32
	{
		public static Bitmap FromDIB(Stream ms)
		{
			var header = new byte[40];
			ms.Read(header, 0, header.Length);
			var size = BitConverter.ToInt32(header, 0);
			var width = BitConverter.ToInt32(header, 4);
			var height = BitConverter.ToInt32(header, 8);
			var bpp = BitConverter.ToInt16(header, 14);
			var compression = BitConverter.ToInt32(header, 16);
			if (size > header.Length)
				ms.Seek(size - header.Length, SeekOrigin.Current);

			if (bpp != 32)
				return null;
			if (compression == 3) // BI_BITFIELDS
			{
				// three dwords, each specifies the bits each RGB components takes
				// we require each takes one byte
				var segments = new byte[sizeof(int) * 3];
				ms.Read(segments, 0, segments.Length);
				var rcomp = BitConverter.ToInt32(segments, 0);
				var gcomp = BitConverter.ToInt32(segments, 4);
				var bcomp = BitConverter.ToInt32(segments, 8);
				if (rcomp != 0xFF0000 || gcomp != 0xFF00 || bcomp != 0xFF)
					return null;
			}
			else if (compression != 0) // BI_RGB
				return null;

			var bmp = new Bitmap(width, height, PixelFormat.Format32bppRgba);
			using (var bd = bmp.Lock())
			{
				for (int y = height - 1; y >= 0; y--)
					for (int x = 0; x < width; x++)
					{
						var b = ms.ReadByte();
						var g = ms.ReadByte();
						var r = ms.ReadByte();
						var a = ms.ReadByte();
						var af = a / 255f;
						if (af > 0)
							bd.SetPixel(x, y, new Color(r / af, g / af, b / af, af));
					}
			}
			return bmp;
		}

		static void Write(Stream stream, byte[] val) => stream.Write(val, 0, val.Length);

		public static MemoryStream ToDIB(this Bitmap bitmap, int dpi = 96)
		{
			if (bitmap == null)
				return null;
			using (var bd = bitmap.Lock())
			{
				if (bd.BytesPerPixel == 4 || bd.BytesPerPixel == 3) // only 32bpp or 24bpp supported
				{
					var size = bitmap.Size;
					var ms = new MemoryStream(size.Width * size.Height * bd.BytesPerPixel + 40);
					// write BITMAPINFOHEADER
					const float InchesPerMeter = 39.37f;
					var pelsPerMeter = Math.Round(dpi * InchesPerMeter); // convert dpi to ppm
					Write(ms, BitConverter.GetBytes((uint)40));  // biSize
					Write(ms, BitConverter.GetBytes((uint)size.Width)); // biWidth
					Write(ms, BitConverter.GetBytes((uint)size.Height));// biHeight
					Write(ms, BitConverter.GetBytes((ushort)1));  // biPlanes
					Write(ms, BitConverter.GetBytes((ushort)bd.BitsPerPixel)); // biBitCount
					Write(ms, BitConverter.GetBytes((uint)0));    //  biCompression (BI_RGB, uncompressed)
					Write(ms, BitConverter.GetBytes((uint)0));    //  biSizeImage
					Write(ms, BitConverter.GetBytes((uint)pelsPerMeter)); //  biXPelsPerMeter
					Write(ms, BitConverter.GetBytes((uint)pelsPerMeter)); //  biYPelsPerMeter
					Write(ms, BitConverter.GetBytes((uint)0));    //  biClrUsed
					Write(ms, BitConverter.GetBytes((uint)0));    //  biClrImportant

					var hasAlpha = bd.BytesPerPixel == 4;
					// write RGB data, dibs are flipped vertically
					for (int y = size.Height - 1; y >= 0; y--)
					{
						for (int x = 0; x < size.Width; x++)
						{
							var p = bd.GetPixel(x, y);
							// need to write RGB premultiplied by alpha (and round up)
							ms.WriteByte((byte)(p.B * p.A * 255f + .5f));
							ms.WriteByte((byte)(p.G * p.A * 255f + .5f));
							ms.WriteByte((byte)(p.R * p.A * 255f + .5f));
							if (hasAlpha)
								ms.WriteByte((byte)p.Ab);
						}
					}

					ms.Position = 0;
					return ms;
				}
			}
			return null;
		}

		public static Image ToEto(this sd.Image image)
		{
			if (image is sd.Bitmap bitmap)
				return new Bitmap(new BitmapHandler(bitmap));
			throw new NotSupportedException();
		}
	}
}
