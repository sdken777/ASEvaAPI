using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// (api:corewf=2.0.0) 传感器坐标系及位置等的可视化控件
    /// </summary>
    public partial class DeviceCoordView : UserControl
    {
        public DeviceCoordView()
        {
            InitializeComponent();

            VehicleWidth = 2;
            VehicleLength = 5;
            OriginX = 0;
            OriginY = 0;
        }

        public double VehicleWidth { get; set; }
        public double VehicleLength { get; set; }
        public double OriginX { get; set; }
        public double OriginY { get; set; }
        public double? YawAngle { get; set; }
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public double? Orientation { get; set; }
        public double? FOV { get; set; }
        public bool? Symmetry { get; set; }
        public double? LeftYawOffset { get; set; }
        public double? RightYawOffset { get; set; }

        private bool largeScale;

        private void drawFOV(Graphics g, Pen pen, PointF px, double fov, double orientation)
        {
            var dpiRatio = (float)DeviceDpi / 96;

            var circleBox = new RectangleF(px.X - dpiRatio * 10, px.Y - dpiRatio * 10, dpiRatio * 20, dpiRatio * 20);
            if (fov == 360)
            {
                g.DrawEllipse(pen, circleBox);
                g.DrawLine(pen, new PointF(px.X - dpiRatio * 10, px.Y), new PointF(px.X + dpiRatio * 10, px.Y));
                g.DrawLine(pen, new PointF(px.X, px.Y - dpiRatio * 10), new PointF(px.X, px.Y + dpiRatio * 10));
            }
            else
            {
                var angle1 = -(orientation - fov * 0.5 + 90) * Math.PI / 180;
                var angle2 = -(orientation + fov * 0.5 + 90) * Math.PI / 180;
                var p1 = new PointF((float)(px.X + dpiRatio * 300 * Math.Cos(angle1)), (float)(px.Y + dpiRatio * 300 * Math.Sin(angle1)));
                var p2 = new PointF((float)(px.X + dpiRatio * 300 * Math.Cos(angle2)), (float)(px.Y + dpiRatio * 300 * Math.Sin(angle2)));
                g.DrawLine(pen, px, p1);
                g.DrawLine(pen, px, p2);
                g.DrawArc(pen, circleBox, (float)(angle2 * 180 / Math.PI), (float)((angle1 - angle2) * 180 / Math.PI));
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.DeviceCoordView");

            try
            {
                double viewWidth = this.Width;
                double viewHeight = this.Height;
                double zeroX = viewWidth * 0.5;
                double zeroY = viewHeight * 0.25;

                var dpiRatio = (float)DeviceDpi / 96;

                var blackPen = new Pen(Color.Black);
                var grayPen = new Pen(Color.LightGray);
                var coordPen = new Pen(Color.LimeGreen) { Width = dpiRatio * 2 };
                var fovPen = new Pen(Color.MediumPurple);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                e.Graphics.DrawLine(grayPen, new PointF(0, (float)zeroY), new PointF((float)viewWidth, (float)zeroY));
                e.Graphics.DrawLine(grayPen, new PointF((float)zeroX, 0), new PointF((float)zeroX, (float)viewHeight));

                double k = dpiRatio * (largeScale ? 5 : 15);
                double triangleLength = Math.Min(2.0, VehicleLength / 3) * k;
                var pa = new PointF((float)(zeroX - VehicleWidth * k / 2), (float)zeroY);
                var pb = new PointF((float)(zeroX + VehicleWidth * k / 2), (float)zeroY);
                var pc = new PointF((float)(zeroX - VehicleWidth * k / 2), (float)(zeroY + VehicleLength * k));
                var pd = new PointF((float)(zeroX + VehicleWidth * k / 2), (float)(zeroY + VehicleLength * k));
                var pe = new PointF((pa.X + pb.X) * 0.5f, (float)zeroY);
                var pf = new PointF(pa.X, (float)(zeroY + triangleLength));
                var pg = new PointF(pb.X, (float)(zeroY + triangleLength));
                e.Graphics.DrawPolygon(blackPen, new PointF[] { pa, pb, pd, pc });
                e.Graphics.DrawPolygon(blackPen, new PointF[] { pe, pf, pg });

                var yaw = (YawAngle == null ? 0 : YawAngle.Value) * Math.PI / 180;
                var px = new PointF((float)(zeroX - OriginY * k), (float)(zeroY - OriginX * k));
                var py = new PointF((float)(px.X - k * Math.Cos(yaw)), (float)(px.Y + k * Math.Sin(yaw)));
                var pz = new PointF((float)(px.X - 1.5 * k * Math.Sin(yaw)), (float)(px.Y - 1.5 * k * Math.Cos(yaw)));
                e.Graphics.DrawLine(coordPen, px, py);
                e.Graphics.DrawLine(coordPen, px, pz);

                var symmetry = Symmetry != null && Symmetry.Value == true;
                if (Orientation != null && FOV != null)
                {
                    var fpx = px;
                    if (PositionX != null && PositionY != null) fpx = new PointF((float)(zeroX - PositionY.Value * k), (float)(zeroY - PositionX.Value * k));
                    drawFOV(e.Graphics, fovPen, fpx, FOV.Value, Orientation.Value + (symmetry && LeftYawOffset != null ? LeftYawOffset.Value : 0));

                    if (symmetry)
                    {
                        var pxs = new PointF((float)(zeroX + OriginY * k), (float)(zeroY - OriginX * k));
                        if (PositionX != null && PositionY != null) pxs = new PointF((float)(zeroX + PositionY.Value * k), (float)(zeroY - PositionX.Value * k));
                        var pys = new PointF((float)(pxs.X - k * Math.Cos(-yaw)), (float)(pxs.Y + k * Math.Sin(-yaw)));
                        var pzs = new PointF((float)(pxs.X - 1.5 * k * Math.Sin(-yaw)), (float)(pxs.Y - 1.5 * k * Math.Cos(-yaw)));
                        e.Graphics.DrawLine(coordPen, pxs, pys);
                        e.Graphics.DrawLine(coordPen, pxs, pzs);
                        drawFOV(e.Graphics, fovPen, pxs, FOV.Value, -Orientation.Value + (RightYawOffset != null ? RightYawOffset.Value : 0));
                    }
                }
            }
            catch (Exception) { }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            linkLabelScale.Text = "Scale: " + (largeScale ? "Large" : "Small");
            if (DrawBeat.CallerBegin(pictureBox1))
            {
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        private void linkLabelScale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            largeScale = !largeScale;
        }

    }
}
