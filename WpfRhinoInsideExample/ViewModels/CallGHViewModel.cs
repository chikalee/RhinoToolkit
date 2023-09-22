using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grasshopper.Kernel;
using RhinoToolkit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfRhinoInsideExample.ViewModels
{
	public partial class CallGHViewModel : ObservableObject
	{

		[ObservableProperty]
		double num = 0;

		[ObservableProperty]
		double num2 = 0;

		[ObservableProperty]
		double num3 = 0;

		[RelayCommand]
		void Add() => Num3 = GHAdd(num, num2);

		[RelayCommand]
		void Sub() => Num3 = GHSub(num, num2);
		[RelayCommand]
		void Mul() => Num3 = GHMul(num, num2);
		[RelayCommand]
		void Div() => Num3 = GHDiv(num, num2);


		public double GHAdd(double num, double num1)
		{
			var result = DocServer.Instance.Doc.TryGetValue("A+B", out GH_Component core);
			if (result)
			{
				core.SetData(0, 0, num);
				core.SetData(1, 0, num1);
				core.Solution();
				return core.CastData<double>(0);
			}
			else
			{
				throw new KeyNotFoundException("no found in DocServer");
			}
		}

		public double GHSub(double num, double num1)
		{
			var result = DocServer.Instance.Doc.TryGetValue("A-B", out GH_Component core);
			if (result)
			{
				core.SetData(0, 0, num);
				core.SetData(1, 0, num1);
				core.Solution();
				return core.CastData<double>(0);
			}
			else
			{
				throw new KeyNotFoundException("no found in DocServer");
			}
		}

		public double GHMul(double num, double num1)
		{
			var result = DocServer.Instance.Doc.TryGetValue("A×B", out GH_Component core);
			if (result)
			{
				core.SetData(0, 0, num);
				core.SetData(1, 0, num1);
				core.Solution();
				return core.CastData<double>(0);
			}
			else
			{
				throw new KeyNotFoundException("no found in DocServer");
			}
		}

		public double GHDiv(double num, double num1)
		{
			var result = DocServer.Instance.Doc.TryGetValue("A/B", out GH_Component core);
			if (result)
			{
				core.SetData(0, 0, num);
				core.SetData(1, 0, num1);
				core.Solution();
				return core.CastData<double>(0);
			}
			else
			{
				throw new KeyNotFoundException("no found in DocServer");
			}
		}


	}
}
