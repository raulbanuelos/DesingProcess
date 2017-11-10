using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace View.Services
{
    public class Utility
    {

        public Utility()
        {
        }
        public static void CreateRectangleFace(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Color surfaceColor, Viewport3D viewport)
        {

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = surfaceColor;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material);
            ModelVisual3D model = new ModelVisual3D();
            model.Content = geometry;
            viewport.Children.Add(model);
        }

        public static void CreateWireframe(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Color lineColor, Viewport3D viewport)
        {

            ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();
            ssl.Points.Add(p0);
            ssl.Points.Add(p1);
            ssl.Points.Add(p1);
            ssl.Points.Add(p2);
            ssl.Points.Add(p2);
            ssl.Points.Add(p3);
            ssl.Points.Add(p3);
            ssl.Points.Add(p0);
            ssl.Color = lineColor;
            ssl.Thickness = 2;
            viewport.Children.Add(ssl);
        }

        public static Point3D GetNormalize(Point3D pt, double xmin, double xmax, double ymin, double ymax, double zmin, double zmax)
        {

            pt.X = -1 + 2 * (pt.X - xmin) / (xmax - xmin);
            pt.Y = -1 + 2 * (pt.Y - ymin) / (ymax - ymin);
            pt.Z = -1 + 2 * (pt.Z - zmin) / (zmax - zmin);
            return pt;
        }

        public static void CreateTriangleFace(Point3D p0, Point3D p1, Point3D p2, Color color, bool isWireframe, Viewport3D viewport)
        {

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material);
            ModelUIElement3D model = new ModelUIElement3D();
            model.Model = geometry;
            viewport.Children.Add(model);

            if (isWireframe == true)
            {

                ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();
                ssl.Points.Add(p0);
                ssl.Points.Add(p1);
                ssl.Points.Add(p1);
                ssl.Points.Add(p2);
                ssl.Points.Add(p2);
                ssl.Points.Add(p0);
                ssl.Color = Colors.White;
                ssl.Thickness = 2;
                viewport.Children.Add(ssl);
            }
        }

        public static Matrix3D SetViewMatrix(Point3D cameraPosition,Vector3D lookDirection, Vector3D upDirection)
        {
            // Normalize vectors:
            lookDirection.Normalize();
            upDirection.Normalize();
            // Define vectors, XScale, YScale, and ZScale:
            double denom = Math.Sqrt(1 - Math.Pow(Vector3D.DotProduct(lookDirection,
            upDirection), 2));
            Vector3D XScale = Vector3D.CrossProduct(lookDirection, upDirection) / denom;
            Vector3D YScale = (upDirection - (Vector3D.DotProduct(upDirection, lookDirection)) * lookDirection) / denom;
            Vector3D ZScale = lookDirection;
            // Construct M matrix:
            Matrix3D M = new Matrix3D();
            M.M11 = XScale.X;
            M.M21 = XScale.Y;
            M.M31 = XScale.Z;
            M.M12 = YScale.X;
            M.M22 = YScale.Y;
            M.M32 = YScale.Z;
            M.M13 = ZScale.X;
            M.M23 = ZScale.Y;
            M.M33 = ZScale.Z;
            // Translate the camera position to the origin:
            Matrix3D translateMatrix = new Matrix3D();
            translateMatrix.Translate(new Vector3D(-cameraPosition.X,
            -cameraPosition.Y, -cameraPosition.Z));
            // Define reflect matrix about the Z axis:
            Matrix3D reflectMatrix = new Matrix3D();
            reflectMatrix.M33 = -1;
            // Construct the View matrix:
            Matrix3D viewMatrix = translateMatrix * M * reflectMatrix;
            return viewMatrix;
        }

        public static Matrix3D SetOrthographic(double width, double height, double near, double far)
        {
            Matrix3D orthographicMatrix = new Matrix3D();
            orthographicMatrix.M11 = 2 / width;
            orthographicMatrix.M22 = 2 / height;
            orthographicMatrix.M33 = 1 / (near - far);
            orthographicMatrix.OffsetZ = near / (near - far);
            return orthographicMatrix;
        }

        public static Matrix3D SetPerspectiveFov(double fov, double aspectRatio, double near, double far)
        {
            Matrix3D perspectiveMatrix = new Matrix3D();
            double yscale = 1.0 / Math.Tan(fov * Math.PI / 180 / 2);
            double xscale = yscale / aspectRatio;
            perspectiveMatrix.M11 = xscale;
            perspectiveMatrix.M22 = yscale;
            perspectiveMatrix.M33 = far / (near - far);
            perspectiveMatrix.M34 = -1.0;
            perspectiveMatrix.OffsetZ = near * far / (near - far);
            perspectiveMatrix.M44 = 0;
            return perspectiveMatrix;
        }
    }
}
