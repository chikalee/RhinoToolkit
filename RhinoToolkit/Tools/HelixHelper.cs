using HelixToolkit.Wpf.SharpDX;
using Rhino.Geometry;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Rhino.Render;
using Plane = Rhino.Geometry.Plane;
using Transform = Rhino.Geometry.Transform;

namespace RhinoToolkit.Tools
{
	public static class HelixHelper
	{

		public static Vector3 ToVector3(this Point3d pt) => new Vector3((float)pt.X, (float)pt.Y, (float)pt.Z);

		public static LineGeometry3D ToHelixPolyline(this Curve crv)
		{
			var pl = crv.ToPolyline(0.1, 1, 0.1, 1000).ToPolyline();
			var lb = new LineBuilder();
			for (int i = 0; i < pl.Count - 1; i++)
			{
				lb.AddLine(pl[i].ToVector3(), pl[i + 1].ToVector3());
			}
			return lb.ToLineGeometry3D();
		}

		public static HelixToolkit.Wpf.SharpDX.MeshGeometry3D ToHelixMesh(this Mesh mesh)
		{
			mesh.SetTextureCoordinates(TextureMapping.CreateBoxMapping(Plane.WorldXY, new Interval(0, 100), new Interval(0, 100), new Interval(0, 100), false), Transform.Identity, false);
			var mb = new MeshBuilder(true);
			mesh.Faces.ConvertQuadsToTriangles();
			var pts = mesh.Vertices.Select(v => new Vector3(v.X, v.Y, v.Z)).ToList();
			var indices = new List<int>();
			mesh.Faces.Select(f =>
			{
				indices.AddRange(new int[3] { f.A, f.B, f.C });
				return 0;
			}).ToList();
			double minX = double.MaxValue;
			double minY = double.MaxValue;
			double maxX = double.MinValue;
			double maxY = double.MinValue;
			var nor = mesh.Normals.Select(v => new Vector3(v.X, v.Y, v.Z)).ToList();
			var texCoords = new List<Vector2>();
			foreach (Point2f texPt in mesh.TextureCoordinates.ToList())
			{
				if (texPt.X > maxX) maxX = texPt.X;
				if (texPt.X < minX) minX = texPt.X;
				if (texPt.Y > maxY) maxY = texPt.Y;
				if (texPt.Y < minY) minY = texPt.Y;
				texCoords.Add(new Vector2(texPt.X, 1.0f - texPt.Y));
			}
			mb.Append(pts, indices, nor, texCoords);
			var helixmesh = mb.ToMesh();

			return mb.ToMesh();
		}

	}
}
