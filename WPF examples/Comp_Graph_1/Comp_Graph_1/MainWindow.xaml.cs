using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;

namespace Comp_Graph_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlotArea Graph1;
        public MainWindow()
        {
            InitializeComponent();
            Graph1 = new PlotArea(MyChart);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyChart.Cursor = System.Windows.Forms.Cursors.Hand;
            Graph1.Plot(MyTextBox.Text);
        }
        private class ZoomFrame
        {
            public double XStart { get; set; }
            public double XFinish { get; set; }
            public double YStart { get; set; }
            public double YFinish { get; set; }
        }
        private Stack<ZoomFrame> _zoomFrames = new Stack<ZoomFrame>();
        private void MyChart_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    if (_zoomFrames.Count > 0)
                    {
                        ZoomFrame frame = _zoomFrames.Pop();
                        if (_zoomFrames.Count == 0)
                        {
                            MyChart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                            MyChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                        }
                        else
                        {
                            MyChart.ChartAreas[0].AxisX.ScaleView.Zoom(frame.XStart, frame.XFinish);
                            MyChart.ChartAreas[0].AxisY.ScaleView.Zoom(frame.YStart, frame.YFinish);
                        }
                    }
                }
                else if (e.Delta > 0)
                {
                    var xMin = MyChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    var xMax = MyChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    var yMin = MyChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    var yMax = MyChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    _zoomFrames.Push(new ZoomFrame { XStart = xMin, XFinish = xMax, YStart = yMin, YFinish = yMax });

                    var posXStart = MyChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = MyChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = MyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = MyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    MyChart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    MyChart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }
        private double PrevX;
        private double PrevY;
        private void MyChart_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) return;
                Axis ax = MyChart.ChartAreas[0].AxisX;
                Axis ay = MyChart.ChartAreas[0].AxisY;
                double RangeX = ax.Maximum - ax.Minimum;
                double RangeY = ay.Maximum - ay.Minimum;
                double NewX = ax.PixelPositionToValue(e.Location.X);
                double NewY = ay.PixelPositionToValue(e.Location.Y);
                ax.Minimum -= (NewX - PrevX);
                ax.Maximum = ax.Minimum + RangeX;
                ay.Minimum -= (NewY - PrevY);
                ay.Maximum = ay.Minimum + RangeY;
            }
            catch { }
        }
        private void MyChart_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Axis ax = MyChart.ChartAreas[0].AxisX;
            Axis ay = MyChart.ChartAreas[0].AxisY;
            PrevX = ax.PixelPositionToValue(e.Location.X);
            PrevY = ay.PixelPositionToValue(e.Location.Y);
        }
    }
    class PlotArea
    {
        private Chart FuncGraph { get; set; }
        private double a { get; set; } = 1;
        public PlotArea(Chart MyChart)
        {
            FuncGraph = MyChart;
        }
        public void Plot(string arg)
        {
            try
            {
                a = Convert.ToDouble(arg);
            }
            catch { }
            FuncGraph.ChartAreas.Clear();
            //FuncGraph.Series.Clear();
            FuncGraph.ChartAreas.Add(new ChartArea());
            Series FuncGraphPoints = new Series();
            FuncGraphPoints.ChartType = SeriesChartType.Line;
            FuncGraphPoints.Color = System.Drawing.Color.Blue;
            double r;
            for (double phi = -Math.PI; phi <= Math.PI; phi += 0.001)
            {
                r = a * Math.Cos(3 * phi);
                FuncGraphPoints.Points.AddXY(r * Math.Cos(phi), r * Math.Sin(phi));
            }
            SetSize();
            FuncGraph.Series.Add(FuncGraphPoints);
        }
        private void SetSize()
        {
            FuncGraph.ChartAreas[0].AxisX.Minimum = -a * 1.6;
            FuncGraph.ChartAreas[0].AxisX.Maximum = a * 1.6;
            FuncGraph.ChartAreas[0].AxisY.Minimum = -a * 0.9;
            FuncGraph.ChartAreas[0].AxisY.Maximum = a * 0.9;
        }
    }
}
