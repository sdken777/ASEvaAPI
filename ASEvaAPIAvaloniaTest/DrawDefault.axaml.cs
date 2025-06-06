using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Controls.Shapes;
using Avalonia.Input;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawDefault : Panel
    {
        public DrawDefault()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language);

            Loaded += delegate
            {
                labelBound.Width = label.Bounds.Width;
                labelBound.Height = label.Bounds.Height;
                var pos = label.TranslatePoint(new Point(0, 0), this);
                labelBound.Margin = new Thickness(pos.Value.X, pos.Value.Y, 0, 0);
            };

            canvas.PointerPressed += canvas_PointerPressed;
            buttonA.Click += buttonA_Click;
            buttonB.Click += buttonB_Click;
            buttonC.Click += buttonC_Click;
            buttonD.Click += buttonD_Click;
        }

        public void OnLoop()
        {
            var pieAngle = (DateTime.Now - startTime).TotalMilliseconds * 0.1;
            pieAngle -= Math.Floor(pieAngle / 360) * 360;
            var pieAngleRad = pieAngle * Math.PI / 180 - Math.PI / 2;

            var figure = new PathFigure{ IsClosed = true };
            figure.Segments.Add(new LineSegment{ Point = new Point(0, -90) });
            figure.Segments.Add(new ArcSegment{ Point = new Point(90.0 * Math.Cos(pieAngleRad), 90.0 * Math.Sin(pieAngleRad)), Size = new Size(90, 90), IsLargeArc = pieAngle > 180 });
            pie.Data = new PathGeometry{ Figures = { figure } };
        }

        private void canvas_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var circle = new Ellipse{ Width = 2, Height = 2, Fill = new SolidColorBrush(Colors.DarkBlue) };
            Canvas.SetLeft(circle, e.GetPosition(canvas).X - 1);
            Canvas.SetTop(circle, e.GetPosition(canvas).Y - 1);
            canvas.Children.Add(circle);
        }

        private void buttonA_Click(object sender, RoutedEventArgs e)
        {
            buttonB.IsVisible = buttonC.IsVisible = buttonD.IsVisible = true;
        }

        private void buttonB_Click(object sender, RoutedEventArgs e)
        {
            link.IsVisible = true;
        }

        private void buttonC_Click(object sender, RoutedEventArgs e)
        {
            link.IsVisible = false;
        }

        private void buttonD_Click(object sender, RoutedEventArgs e)
        {
            buttonB.IsVisible = buttonC.IsVisible = buttonD.IsVisible = false;
        }

        private DateTime startTime = DateTime.Now;
        private LanguageSwitch language;
    }
}