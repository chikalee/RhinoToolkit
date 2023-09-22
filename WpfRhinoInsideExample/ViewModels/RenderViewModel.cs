using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelixToolkit.Wpf.SharpDX;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MeshGeometry3D = HelixToolkit.Wpf.SharpDX.MeshGeometry3D;
using RhinoToolkit.Tools;

namespace WpfRhinoInsideExample.ViewModels
{
	public partial class RenderViewModel : ObservableObject
	{
		[ObservableProperty]
		EffectsManager effectsManager;
		[ObservableProperty]
		HelixToolkit.Wpf.SharpDX.Camera camera;
		[ObservableProperty]
		HelixToolkit.Wpf.SharpDX.Material material;
		[ObservableProperty]
		LineGeometry3D lineGeometry;
		[ObservableProperty]
		MeshGeometry3D meshGeometry;
		[ObservableProperty]
		PointGeometry3D pointGeometry;
		[ObservableProperty]
		double p1X;
		[ObservableProperty]
		double p1Y;
		[ObservableProperty]
		double p1Z;
		[ObservableProperty]
		double p2X=3;
		[ObservableProperty]
		double p2Y=3;
		[ObservableProperty]
		double p2Z=3;
		[ObservableProperty]
		double radius = 20;

		[RelayCommand]
		void DrawPoint()
		{
			var p = new PointGeometry3D();
			p.Positions = new Vector3Collection() { new Point3d(p1X, p1Y, p1Z).ToVector3(), new Point3d(p2X, p2Y, p2Z).ToVector3() };
			PointGeometry = p;
		}

		[RelayCommand]
		void DrawLine()
		{
			var l = new Line(new Point3d(p1X, p1Y, p1Z), new Point3d(p2X, p2Y, p2Z)).ToNurbsCurve();
			LineGeometry = l.ToHelixPolyline();
		}

		[RelayCommand]
		void DrawMesh()
		{
			var brp = new Sphere(new Point3d(0, 0, 0), radius).ToBrep();
			var msh = Mesh.CreateFromBrep(brp, MeshingParameters.Smooth);
			var m = new Mesh();
			m.Append(msh);
			MeshGeometry = m.ToHelixMesh();
		}


		public RenderViewModel()
		{
			EffectsManager = new DefaultEffectsManager();
			Camera = new HelixToolkit.Wpf.SharpDX.PerspectiveCamera
			{
				FarPlaneDistance = double.PositiveInfinity,
				LookDirection = new Vector3D(-3, -3, -5),
				UpDirection = new Vector3D(0, 0, 1),
			};
			Material = new PBRMaterial()
			{
				AlbedoColor = Colors.Silver.ToColor4(),
				RenderEnvironmentMap = true,
				RoughnessFactor = 0.5,
				MetallicFactor = 0.3,
				EnableAutoTangent = true,
				RenderShadowMap = true,
			};
		}
	}
}
