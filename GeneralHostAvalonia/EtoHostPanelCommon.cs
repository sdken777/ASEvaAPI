using System;
using Avalonia.Headless;
using Avalonia.Platform;
using SkiaSharp;

namespace GeneralHostAvalonia
{
    class EtoHostPanelCommon(AvaloniaPanelContainer avaloniaContainer, Eto.Forms.Panel etoPanel)
    {
        public void Initialize()
        {
            if (containerShown) return;

            avaloniaContainer.Show();
            containerShown = true;

            if (isGtk)
            {
                overlayForGtk = ASEva.UIEto.SetContentAsControlExtension.SetContentAsControl(etoPanel, new ASEva.UIEto.OverlayLayout(), 0);
                focusForGtk = overlayForGtk.AddControl(new Eto.Forms.TextBox(), 0, null, 0, null) as Eto.Forms.TextBox;
                skiaView = overlayForGtk.AddControl(new ASEva.UIEto.SkiaView(), 0, 0, 0, 0) as ASEva.UIEto.SkiaView;
            }
            if (isMonoMac)
            {
                var layout = ASEva.UIEto.SetContentExtensions.SetContentAsColumnLayout(etoPanel, 0, 0);
                skiaView = ASEva.UIEto.StackLayoutAddControlExtension.AddControl(layout, new ASEva.UIEto.SkiaView(), true);
                focusForMonoMac = ASEva.UIEto.StackLayoutAddControlExtension.AddControl(layout, new Eto.Forms.Drawable{ CanFocus = true }, false, 0, 1);
            }

            skiaView.MouseDown += (o, e) =>
            {
                var button = convertMouseButton(e.Buttons);
                var modifiers = convertModifiers(e.Modifiers);
                avaloniaContainer.MouseDown(new Avalonia.Point(e.Location.X, e.Location.Y), button, modifiers);

                if (isGtk)
                {
                    overlayForGtk.UpdatePadding(focusForGtk, (int)e.Location.Y, null, (int)e.Location.X, null);
                    focusForGtk.Focus();
                }
                if (isMonoMac)
                {
                    focusForMonoMac.Focus();
                }
            };

            skiaView.MouseUp += (o, e) =>
            {
                var button = convertMouseButton(e.Buttons);
                var modifiers = convertModifiers(e.Modifiers);
                avaloniaContainer.MouseUp(new Avalonia.Point(e.Location.X, e.Location.Y), button, modifiers);
            };

            skiaView.MouseMove += (o, e) =>
            {
                var modifiers = convertModifiers(e.Modifiers);
                avaloniaContainer.MouseMove(new Avalonia.Point(e.Location.X, e.Location.Y), modifiers);
            };

            skiaView.MouseWheel += (o, e) =>
            {
                var modifiers = convertModifiers(e.Modifiers);
                var delta = new Avalonia.Vector(e.Delta.Width, e.Delta.Height);
                avaloniaContainer.MouseWheel(new Avalonia.Point(e.Location.X, e.Location.Y), delta, modifiers);
            };

            if (isGtk)
            {
                focusForGtk.KeyDown += (o, e) =>
                {
                    var physicalKey = convertToPhysicalKey(e.Key);
                    var modifiers = convertModifiers(e.Modifiers);
                    avaloniaContainer.KeyPressQwerty(physicalKey, modifiers);
                };

                focusForGtk.KeyUp += (o, e) =>
                {
                    var physicalKey = convertToPhysicalKey(e.Key);
                    var modifiers = convertModifiers(e.Modifiers);
                    avaloniaContainer.KeyReleaseQwerty(physicalKey, modifiers);
                };

                focusForGtk.TextChanging += (o, e) =>
                {
                    avaloniaContainer.KeyTextInput(e.Text);
                    lastFocusTextForGtk = e.NewText;
                };
            }
            if (isMonoMac)
            {
                focusForMonoMac.KeyDown += (o, e) =>
                {
                    var physicalKey = convertToPhysicalKey(e.Key);
                    var modifiers = convertModifiers(e.Modifiers);
                    avaloniaContainer.KeyPressQwerty(physicalKey, modifiers);
                };

                focusForMonoMac.KeyUp += (o, e) =>
                {
                    var physicalKey = convertToPhysicalKey(e.Key);
                    var modifiers = convertModifiers(e.Modifiers);
                    avaloniaContainer.KeyReleaseQwerty(physicalKey, modifiers);
                };

                focusForMonoMac.TextInput += (o, e) =>
                {
                    avaloniaContainer.KeyTextInput(e.Text);
                };
            }

            skiaView.Render += (o, e) =>
            {
                avaloniaContainer.InvalidateVisual();
                var bitmap = avaloniaContainer.CaptureRenderedFrame();

                bool drawn = false;
                if (bitmap != null)
                {
                    var commonImage = toCommonImage(bitmap);
                    if (commonImage != null)
                    {
                        var skiaImage = ASEva.UIEto.CommonImageSkiaExtensions.ToSKImage(commonImage);
                        if (skiaImage != null)
                        {
                            e.Canvas.DrawImage(skiaImage, 0, 0);
                            skiaImage.Dispose();
                            drawn = true;
                        }
                    }
                }

                if (!drawn) e.Canvas.Clear(SKColors.Black);
            };

            timer.Elapsed += delegate
            {
                if (isGtk && focusForGtk.Text != lastFocusTextForGtk)
                {
                    if (focusForGtk.Text.StartsWith(lastFocusTextForGtk))
                    {
                        avaloniaContainer.KeyTextInput(focusForGtk.Text.Substring(lastFocusTextForGtk.Length));
                    }
                    lastFocusTextForGtk = focusForGtk.Text = "";
                }

                skiaView.QueueRender();
            };
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void CloseContainer()
        {
            if (containerShown && !containerClosed)
            {
                skiaView.Close();
                avaloniaContainer.Close();
                containerClosed = true;
            }
        }

        public bool IsValid => containerShown && !containerClosed;

        private Avalonia.Input.MouseButton convertMouseButton(Eto.Forms.MouseButtons buttons)
        {
            if ((buttons & Eto.Forms.MouseButtons.Primary) != 0)
                return Avalonia.Input.MouseButton.Left;
            if ((buttons & Eto.Forms.MouseButtons.Alternate) != 0)
                return Avalonia.Input.MouseButton.Right;
            if ((buttons & Eto.Forms.MouseButtons.Middle) != 0)
                return Avalonia.Input.MouseButton.Middle;
            return Avalonia.Input.MouseButton.Left;
        }

        private Avalonia.Input.PhysicalKey convertToPhysicalKey(Eto.Forms.Keys key)
        {
            return key switch
            {
                Eto.Forms.Keys.A => Avalonia.Input.PhysicalKey.A,
                Eto.Forms.Keys.B => Avalonia.Input.PhysicalKey.B,
                Eto.Forms.Keys.C => Avalonia.Input.PhysicalKey.C,
                Eto.Forms.Keys.D => Avalonia.Input.PhysicalKey.D,
                Eto.Forms.Keys.E => Avalonia.Input.PhysicalKey.E,
                Eto.Forms.Keys.F => Avalonia.Input.PhysicalKey.F,
                Eto.Forms.Keys.G => Avalonia.Input.PhysicalKey.G,
                Eto.Forms.Keys.H => Avalonia.Input.PhysicalKey.H,
                Eto.Forms.Keys.I => Avalonia.Input.PhysicalKey.I,
                Eto.Forms.Keys.J => Avalonia.Input.PhysicalKey.J,
                Eto.Forms.Keys.K => Avalonia.Input.PhysicalKey.K,
                Eto.Forms.Keys.L => Avalonia.Input.PhysicalKey.L,
                Eto.Forms.Keys.M => Avalonia.Input.PhysicalKey.M,
                Eto.Forms.Keys.N => Avalonia.Input.PhysicalKey.N,
                Eto.Forms.Keys.O => Avalonia.Input.PhysicalKey.O,
                Eto.Forms.Keys.P => Avalonia.Input.PhysicalKey.P,
                Eto.Forms.Keys.Q => Avalonia.Input.PhysicalKey.Q,
                Eto.Forms.Keys.R => Avalonia.Input.PhysicalKey.R,
                Eto.Forms.Keys.S => Avalonia.Input.PhysicalKey.S,
                Eto.Forms.Keys.T => Avalonia.Input.PhysicalKey.T,
                Eto.Forms.Keys.U => Avalonia.Input.PhysicalKey.U,
                Eto.Forms.Keys.V => Avalonia.Input.PhysicalKey.V,
                Eto.Forms.Keys.W => Avalonia.Input.PhysicalKey.W,
                Eto.Forms.Keys.X => Avalonia.Input.PhysicalKey.X,
                Eto.Forms.Keys.Y => Avalonia.Input.PhysicalKey.Y,
                Eto.Forms.Keys.Z => Avalonia.Input.PhysicalKey.Z,
                Eto.Forms.Keys.D0 => Avalonia.Input.PhysicalKey.Digit0,
                Eto.Forms.Keys.D1 => Avalonia.Input.PhysicalKey.Digit1,
                Eto.Forms.Keys.D2 => Avalonia.Input.PhysicalKey.Digit2,
                Eto.Forms.Keys.D3 => Avalonia.Input.PhysicalKey.Digit3,
                Eto.Forms.Keys.D4 => Avalonia.Input.PhysicalKey.Digit4,
                Eto.Forms.Keys.D5 => Avalonia.Input.PhysicalKey.Digit5,
                Eto.Forms.Keys.D6 => Avalonia.Input.PhysicalKey.Digit6,
                Eto.Forms.Keys.D7 => Avalonia.Input.PhysicalKey.Digit7,
                Eto.Forms.Keys.D8 => Avalonia.Input.PhysicalKey.Digit8,
                Eto.Forms.Keys.D9 => Avalonia.Input.PhysicalKey.Digit9,
                Eto.Forms.Keys.Enter => Avalonia.Input.PhysicalKey.Enter,
                Eto.Forms.Keys.Escape => Avalonia.Input.PhysicalKey.Escape,
                Eto.Forms.Keys.Backspace => Avalonia.Input.PhysicalKey.Backspace,
                Eto.Forms.Keys.Tab => Avalonia.Input.PhysicalKey.Tab,
                Eto.Forms.Keys.Space => Avalonia.Input.PhysicalKey.Space,
                Eto.Forms.Keys.Delete => Avalonia.Input.PhysicalKey.Delete,
                Eto.Forms.Keys.Insert => Avalonia.Input.PhysicalKey.Insert,
                Eto.Forms.Keys.Home => Avalonia.Input.PhysicalKey.Home,
                Eto.Forms.Keys.End => Avalonia.Input.PhysicalKey.End,
                Eto.Forms.Keys.PageUp => Avalonia.Input.PhysicalKey.PageUp,
                Eto.Forms.Keys.PageDown => Avalonia.Input.PhysicalKey.PageDown,
                Eto.Forms.Keys.Up => Avalonia.Input.PhysicalKey.ArrowUp,
                Eto.Forms.Keys.Down => Avalonia.Input.PhysicalKey.ArrowDown,
                Eto.Forms.Keys.Left => Avalonia.Input.PhysicalKey.ArrowLeft,
                Eto.Forms.Keys.Right => Avalonia.Input.PhysicalKey.ArrowRight,
                Eto.Forms.Keys.F1 => Avalonia.Input.PhysicalKey.F1,
                Eto.Forms.Keys.F2 => Avalonia.Input.PhysicalKey.F2,
                Eto.Forms.Keys.F3 => Avalonia.Input.PhysicalKey.F3,
                Eto.Forms.Keys.F4 => Avalonia.Input.PhysicalKey.F4,
                Eto.Forms.Keys.F5 => Avalonia.Input.PhysicalKey.F5,
                Eto.Forms.Keys.F6 => Avalonia.Input.PhysicalKey.F6,
                Eto.Forms.Keys.F7 => Avalonia.Input.PhysicalKey.F7,
                Eto.Forms.Keys.F8 => Avalonia.Input.PhysicalKey.F8,
                Eto.Forms.Keys.F9 => Avalonia.Input.PhysicalKey.F9,
                Eto.Forms.Keys.F10 => Avalonia.Input.PhysicalKey.F10,
                Eto.Forms.Keys.F11 => Avalonia.Input.PhysicalKey.F11,
                Eto.Forms.Keys.F12 => Avalonia.Input.PhysicalKey.F12,
                _ => Avalonia.Input.PhysicalKey.None
            };
        }

        private Avalonia.Input.RawInputModifiers convertModifiers(Eto.Forms.Keys modifiers)
        {
            var result = Avalonia.Input.RawInputModifiers.None;
            
            if ((modifiers & Eto.Forms.Keys.Shift) != 0)
                result |= Avalonia.Input.RawInputModifiers.Shift;
            if ((modifiers & Eto.Forms.Keys.Control) != 0)
                result |= Avalonia.Input.RawInputModifiers.Control;
            if ((modifiers & Eto.Forms.Keys.Alt) != 0)
                result |= Avalonia.Input.RawInputModifiers.Alt;
            if ((modifiers & Eto.Forms.Keys.Application) != 0)
                result |= Avalonia.Input.RawInputModifiers.Meta;
            
            return result;
        }

        private static ASEva.Samples.CommonImage toCommonImage(Avalonia.Media.Imaging.Bitmap bitmap)
        {
            if (bitmap.Format != PixelFormat.Bgra8888 && bitmap.Format != PixelFormat.Rgba8888) return null;
            if (bitmap.AlphaFormat == AlphaFormat.Premul) return null;

            var image = ASEva.Samples.CommonImage.Create(bitmap.PixelSize.Width, bitmap.PixelSize.Height, true, bitmap.Format == PixelFormat.Rgba8888);
            unsafe
            {
                fixed (byte* dataPtr = &image.Data[0])
                {
                    bitmap.CopyPixels(new Avalonia.PixelRect(0, 0, image.Width, image.Height), (nint)dataPtr, image.RowBytes * image.Height, image.RowBytes);
                }
            }
            return image;
        }

        private bool isGtk => ASEva.UIEto.App.GetRunningUI() == "gtk";
        private bool isMonoMac => ASEva.UIEto.App.GetRunningUI() == "monomac";

        private bool containerShown = false;
        private bool containerClosed = false;
        private Eto.Forms.UITimer timer = new Eto.Forms.UITimer { Interval = 0.015 };
        private ASEva.UIEto.SkiaView skiaView;
        private ASEva.UIEto.OverlayLayout overlayForGtk;
        private Eto.Forms.TextBox focusForGtk;
        private String lastFocusTextForGtk = "";
        private Eto.Forms.Drawable focusForMonoMac;
    }
}