using MahApps.Metro.Controls;
using System;
using System.Windows.Media.Media3D;
using View.Services;
using System.Windows.Input;
using System.Windows.Media;

namespace View.Forms.Routing
{
    /// <summary>
    /// Lógica de interacción para WPattern.xaml
    /// </summary>
    public partial class WPattern : MetroWindow
    {
        public WPattern()
        {
            InitializeComponent();
            
            CreateCylinder(new Point3D(0.2, 0.1, 0), 1.1, 1.5, .40, 100, Colors.SlateGray, false);
            CreateCylinder2(new Point3D(0, 0,0), 1.1,1.72, 0.17, 100, Colors.SlateGray, false);
        }

        
        /// <summary>
        ///  //Método para dibujar el anillo
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="theta"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Point3D GetPosition(double radius, double theta, double y)
        {
            Point3D pt = new Point3D();
            double sn = Math.Sin(theta * Math.PI / 180);
            double cn = Math.Cos(theta * Math.PI / 180);
            pt.X = radius * cn;
            pt.Y = y;
            pt.Z = -radius * sn;
            return pt;
        }

        /// <summary>
        /// Método para dibujar el anillo, rin = radius in, rout= radius out
        /// </summary>
        /// <param name="center"></param>
        /// <param name="rin"></param>
        /// <param name="rout"></param>
        /// <param name="height"></param>
        /// <param name="n"></param>
        /// <param name="color"></param>
        /// <param name="isWireframe"></param>
        private void CreateCylinder(Point3D center, double rin, double rout, double height, int n, Color color, bool isWireframe)
        {
            if (n < 2 || rin == rout)
                return;
            double radius = rin;
            if (rin > rout)
            {
                rin = rout;
                rout = radius;
            }
            double h = height / 2;
            Model3DGroup cylinder = new Model3DGroup();
            Point3D[,] pts = new Point3D[n, 4];
            for (int i = 0; i < n; i++)
            {
                pts[i, 0] = GetPosition(rout, i * 360 / (n - 1), h);
                pts[i, 1] = GetPosition(rout, i * 360 / (n - 1), -h);
                pts[i, 2] = GetPosition(rin, i * 360 / (n - 1), -h);
                pts[i, 3] = GetPosition(rin, i * 360 / (n - 1), h);
            }

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < 4; j++)
                    pts[i, j] += (Vector3D)center;
            }
            Point3D[] p = new Point3D[8];

            for (int i = 0; i < n - 1; i++)
            {

                p[0] = pts[i, 0];
                p[1] = pts[i, 1];
                p[2] = pts[i, 2];
                p[3] = pts[i, 3];
                p[4] = pts[i + 1, 0];
                p[5] = pts[i + 1, 1];
                p[6] = pts[i + 1, 2];
                p[7] = pts[i + 1, 3];
                // Top surface:
                Utility.CreateTriangleFace(p[0], p[4], p[3], color,
                isWireframe, myViewport);
                Utility.CreateTriangleFace(p[4], p[7], p[3], color,
                isWireframe, myViewport);
                // Bottom surface:
                Utility.CreateTriangleFace(p[1], p[5], p[2], color,
                isWireframe, myViewport);
                Utility.CreateTriangleFace(p[5], p[6], p[2], color,
                isWireframe, myViewport);
                // Outer surface:
                Utility.CreateTriangleFace(p[0], p[1], p[4], color,
                isWireframe, myViewport);
                Utility.CreateTriangleFace(p[1], p[5], p[4], color,
                isWireframe, myViewport);
                // Inner surface:
                Utility.CreateTriangleFace(p[2], p[7], p[6], color,
                isWireframe, myViewport);
                Utility.CreateTriangleFace(p[2], p[3], p[7], color, isWireframe, myViewport);
            }
        }

        /// <summary>
        /// Método para dibujar el segundo anillo, con diferente vista.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="rin"></param>
        /// <param name="rout"></param>
        /// <param name="height"></param>
        /// <param name="n"></param>
        /// <param name="color"></param>
        /// <param name="isWireframe"></param>
        private void CreateCylinder2(Point3D center, double rin, double rout, double height, int n, Color color, bool isWireframe)
        {
            if (n < 2 || rin == rout)
                return;
            double radius = rin;
            if (rin > rout)
            {
                rin = rout;
                rout = radius;
            }
            double h = height / 2;
            Model3DGroup cylinder = new Model3DGroup();
            Point3D[,] pts = new Point3D[n, 4];
            for (int i = 0; i < n; i++)
            {
                pts[i, 0] = GetPosition(rout, i * 360 / (n - 1), h);
                pts[i, 1] = GetPosition(rout, i * 360 / (n - 1), -h);
                pts[i, 2] = GetPosition(rin, i * 360 / (n - 1), -h);
                pts[i, 3] = GetPosition(rin, i * 360 / (n - 1), h);
            }

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < 4; j++)
                    pts[i, j] += (Vector3D)center;
            }
            Point3D[] p = new Point3D[8];

            for (int i = 0; i < n - 1; i++)
            {

                p[0] = pts[i, 0];
                p[1] = pts[i, 1];
                p[2] = pts[i, 2];
                p[3] = pts[i, 3];
                p[4] = pts[i + 1, 0];
                p[5] = pts[i + 1, 1];
                p[6] = pts[i + 1, 2];
                p[7] = pts[i + 1, 3];

                // Top surface:
                Utility.CreateTriangleFace(p[0], p[4], p[3], color, isWireframe, viewport2);
                Utility.CreateTriangleFace(p[4], p[7], p[3], color, isWireframe, viewport2);

                // Bottom surface:
                Utility.CreateTriangleFace(p[1], p[5], p[2], color, isWireframe, viewport2);
                Utility.CreateTriangleFace(p[5], p[6], p[2], color, isWireframe, viewport2);

                // Outer surface:
                Utility.CreateTriangleFace(p[0], p[1], p[4], color, isWireframe, viewport2);
                Utility.CreateTriangleFace(p[1], p[5], p[4], color, isWireframe, viewport2);

                // Inner surface:
                Utility.CreateTriangleFace(p[2], p[7], p[6], color, isWireframe, viewport2);
                Utility.CreateTriangleFace(p[2], p[3], p[7], color, isWireframe, viewport2);
            }
        }

        private void btnSalir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        public void Rotate(double d)
        {
            double u = 0.05;
            double angleD = u * d;
            Vector3D lookDirection = camera.LookDirection;

            var m = new Matrix3D();
            m.Rotate(new Quaternion(camera.UpDirection, -angleD)); // Rotate about the camera's up direction to look left/right
            camera.LookDirection = m.Transform(camera.LookDirection);
        }

        public void RotateVertical(double d)
        {
            double u = 0.05;
            double angleD = u * d;
           
            Vector3D lookDirection = camera.LookDirection;

            // Cross Product gets a vector that is perpendicular to the passed in vectors (order does matter, reverse the order and the vector will point in the reverse direction)
            var cp = Vector3D.CrossProduct(camera.UpDirection, lookDirection);
            cp.Normalize();

            var m = new Matrix3D();
            m.Rotate(new Quaternion(cp, -angleD)); // Rotate about the vector from the cross product
            camera.LookDirection = m.Transform(camera.LookDirection);
        }

        public void Move(double d)
        {
            double u = 0.05;
            //PerspectiveCamera camera = (PerspectiveCamera)Viewport3D.Camera;
            Vector3D lookDirection = camera.LookDirection;
            Point3D position = camera.Position;

            lookDirection.Normalize();
            position = position + u * lookDirection * d;

            camera.Position = position;
        }

        private void MetroWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.NumPad6)
            {
                Rotate(10);
            }
            else if (e.Key == Key.NumPad4)
            {
                Rotate(-10);
            }
            else if (e.Key == Key.NumPad8)
            {
                Move(-10);
            }
            else if (e.Key == Key.NumPad2)
            {
                Move(10);
            }
            else if (e.Key == Key.PageUp)
            {
                RotateVertical(10);
            }
            else if (e.Key == Key.PageDown)
            {
                RotateVertical(-10);
            }

        }
    }
}
