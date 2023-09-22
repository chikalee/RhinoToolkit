using Microsoft.Xaml.Behaviors;
using Rhino;
using Rhino.Runtime.InProcess;
using RhinoInside;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RhinoToolkit.Behaviors
{
	public class RhinoAttachBehavior : Behavior<Window>
	{
		protected RhinoCore _rhinoCore;

		static RhinoAttachBehavior()
		{
			Resolver.Initialize();
		}
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.Loaded += AssociatedObject_Loaded;
			AssociatedObject.Initialized += AssociatedObject_Initialized;
			AssociatedObject.Closed += AssociatedObject_Closed;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.Loaded -= AssociatedObject_Loaded;
			AssociatedObject.Initialized -= AssociatedObject_Initialized;
			AssociatedObject.Closed -= AssociatedObject_Closed;
		}

		private void AssociatedObject_Closed(object sender, EventArgs e)
		{
			_rhinoCore?.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		private void AssociatedObject_Initialized(object sender, EventArgs e)
		{
			_rhinoCore = new RhinoCore(new string[] { "/nosplash" }, Rhino.Runtime.InProcess.WindowStyle.Hidden);
			RhinoDoc.ActiveDoc.Modified = false;
		}



		private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
		{
		}


	}
}
