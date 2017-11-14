using MahApps.Metro.Controls;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using View.Services;

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
            CreateCylinder(new Point3D(0.2, 0.1, 0), 1.1, 1.5, 0.29, 100, Colors.SlateGray, false);
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
   
    }
}
