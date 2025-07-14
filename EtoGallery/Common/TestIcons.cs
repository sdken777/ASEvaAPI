namespace Eto.Test
{
	public static class TestIcons
	{
		public static Icon TestIcon => Icon.FromResource("Common.Images.TestIcon.ico");

		public static Bitmap TestImage => Platform.Instance.Supports<Bitmap>() ? Bitmap.FromResource("Common.Images.TestImage.png") : null;

		public static Bitmap Textures => Bitmap.FromResource("Common.Images.Textures.png");

		public static Bitmap TexturesIndexed => Bitmap.FromResource("Common.Images.Textures.gif");

		public static Icon Logo => Icon.FromResource("Common.Images.Logo.png");

		public static Bitmap LogoBitmap => Bitmap.FromResource("Common.Images.Logo.png");

		public static Icon Logo288 => Icon.FromResource("Common.Images.LogoWith288DPI.png");

		public static Bitmap Logo288Bitmap => Bitmap.FromResource("Common.Images.LogoWith288DPI.png");

		public static Cursor TestCursor => Cursor.FromResource("Common.Images.Busy.cur");
	}
}
