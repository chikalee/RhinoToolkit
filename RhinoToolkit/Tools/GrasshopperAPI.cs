using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoToolkit.Tools
{
	public static class GrasshopperAPI
	{
		public static object lockObject = new object();
		public static void SetData<T>(this GH_Component component, int paramIndex, int index, T data)
		{
			var param = component.Params.Input[paramIndex];
			param.ExpireSolution(false);
			param.AddVolatileData(new GH_Path(0), index, data);
		}
		public static void SetData<T>(this GH_Component component, int paramIndex, IEnumerable<T> data)
		{
			var param = component.Params.Input[paramIndex];
			param.ExpireSolution(false);
			param.AddVolatileDataList(new GH_Path(0), data);
		}

		public static void SetData<T>(this GH_Component component, int paramIndex, GH_Structure<T> data) where T : IGH_Goo
		{
			var param = component.Params.Input[paramIndex];
			param.ExpireSolution(false);
			param.AddVolatileDataTree(data);
		}

		public static void Solution(this GH_Component component)
		{
			lock (lockObject)
			{
				component.OnPingDocument().NewSolution(false);
			}
		}

		public static T CastData<T>(this GH_Component component, int paramIndex)
		{
			T data = default(T);
			var param = component.Params.Output[paramIndex];
			param.VolatileData.AllData(true).FirstOrDefault()?.CastTo<T>(out data);
			return data;
		}
		public static List<T> CastDataList<T>(this GH_Component component, int paramIndex)
		{
			var param = component.Params.Output[paramIndex];
			return param.VolatileData.AllData(true).Select(g => { g.CastTo<T>(out T data); return data; }).ToList();
		}
	}
}
