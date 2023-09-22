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
using WpfRhinoInsideExample.ViewModels;

namespace WpfRhinoInsideExample.Pages
{
	/// <summary>
	/// Render.xaml 的交互逻辑
	/// </summary>
	public partial class Render : UserControl
	{
		public Render()
		{
			InitializeComponent();
			viewport.ModelUpDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, 1);
			DataContext = new RenderViewModel();
		}
	}
}
