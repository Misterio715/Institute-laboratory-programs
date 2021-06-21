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
using System.Windows.Media.Media3D;

namespace Comp_Graph_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pyramid MyPyramid { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyPyramid = new Pyramid(MyViewport);
            }
            catch { }
        }

        private void SliderY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyPyramid.RotationY((Slider)sender);
        }

        private void SliderX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyPyramid.RotationX((Slider)sender);
        }
    }
    class Pyramid
    {
        private Viewport3D MyViewport { get; set; }
        private ModelVisual3D Figure { get; set; } = new ModelVisual3D();
        private ModelVisual3D Light { get; set; } = new ModelVisual3D();
        private AxisAngleRotation3D rotateY = new AxisAngleRotation3D();
        private AxisAngleRotation3D rotateX = new AxisAngleRotation3D();

        private MatrixTransform3D rotatey = new MatrixTransform3D();
        private MatrixTransform3D rotatex = new MatrixTransform3D();
        private Matrix3D rotate = new Matrix3D();

        private RotateTransform3D MyRotateY { get; set; }
        private RotateTransform3D MyRotateX { get; set; }
        private double x { get; set; } = 5;
        private double centerZ { get; set; }
        private double angleY { get; set; } = 0;
        private double angleX { get; set; } = 0;
        public Pyramid(Viewport3D obj)
        {
            MyViewport = obj;
            centerZ = -x * Math.Sqrt(2) / Math.Sqrt(5 - Math.Sqrt(5));
            //rotatey.Matrix = rotate;
            SetCamera();
            SetLight();
            DrawFigure();
            MyRotateY = new RotateTransform3D(rotateY, 0, 0, 0);
            MyRotateX = new RotateTransform3D(rotateX, 0, 0, 0);
            Transform3DGroup MyTransform = new Transform3DGroup();
            MyTransform.Children.Add(MyRotateY);
            MyTransform.Children.Add(MyRotateX);
            //MyTransform.Children.Add(rotatey);
            //MyTransform.Children.Add(rotatex);
            Figure.Transform = MyTransform;
        }
        private void SetCamera()
        {
            MyViewport.Camera = new PerspectiveCamera() { Position = new Point3D(0, 5, 30), LookDirection = new Vector3D(0, 0, -10) };
        }
        private void SetLight()
        {
            Light.Content = new DirectionalLight() { Color = Colors.White, Direction = new Vector3D(0, 1, -1) };
            MyViewport.Children.Add(Light);
        }
        public void RotationY(Slider SliderY)
        {
            //angleY = SliderY.Value;
            //rotatey.Matrix = new Matrix3D(Math.Cos(angleY * Math.PI / 180), 0, -Math.Sin(angleY * Math.PI / 180), 0, 0, 1, 0, 0, Math.Sin(angleY * Math.PI / 180), 0, Math.Cos(angleY * Math.PI / 180), 0, 0, 0, 0, 1);
            rotateY.Axis = new Vector3D(0, Math.Cos(angleX * Math.PI / 180), -Math.Sin(angleX * Math.PI / 180));
            rotateY.Angle = SliderY.Value;
            angleY = SliderY.Value;
        }
        public void RotationX(Slider SliderX)
        {
            //angleX = SliderX.Value;
            //rotatex.Matrix = new Matrix3D(1, 0, 0, 0, 0, Math.Cos(angleX * Math.PI / 180), Math.Sin(angleX * Math.PI / 180), 0, 0, -Math.Sin(angleX * Math.PI / 180), Math.Cos(angleX * Math.PI / 180), 0, 0, 0, 0, 1);
            rotateX.Axis = new Vector3D(Math.Cos(angleY * Math.PI / 180), 0, 0);
            rotateX.Angle = SliderX.Value;
            angleX = SliderX.Value;
        }
        public void DrawFigure()
        {
            GeometryModel3D Pyramid = new GeometryModel3D();
            Point3DCollection Points = new Point3DCollection(6);
            Points.Add(new Point3D(0, 0, -centerZ));
            Points.Add(new Point3D(-x * Math.Cos(Math.PI / 5), 0, -x * Math.Sin(Math.PI / 5) - centerZ));
            Points.Add(new Point3D(x * Math.Sin(Math.PI / 10) - x * Math.Cos(Math.PI / 5), 0, -x * Math.Sin(Math.PI / 5) - x * Math.Cos(Math.PI / 10) - centerZ));
            Points.Add(new Point3D(x * Math.Cos(Math.PI / 5), 0, -x * Math.Sin(Math.PI / 5) - centerZ));
            Points.Add(new Point3D(-x * Math.Sin(Math.PI / 10) + x * Math.Cos(Math.PI / 5), 0, -x * Math.Sin(Math.PI / 5) - x * Math.Cos(Math.PI / 10) - centerZ));
            Points.Add(new Point3D(0, 10, 0));
            Pyramid.Geometry = new MeshGeometry3D() { Positions = Points, TriangleIndices = new Int32Collection() { 4, 3, 0, 0, 1, 2, 2, 4, 0, 0, 5, 1, 1, 5, 2, 2, 5, 4, 4, 5, 3, 3, 5, 0 } };
            Pyramid.Material = new DiffuseMaterial() { Brush =  Brushes.Blue };
            Figure.Content = Pyramid;
            MyViewport.Children.Add(Figure);
        }
    }
}
