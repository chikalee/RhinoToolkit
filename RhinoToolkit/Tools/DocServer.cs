using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoToolkit.Tools
{
	public class DocServer
	{
		public Dictionary<string, GH_Component> Doc = new Dictionary<string, GH_Component>();
		public static DocServer Instance => instance.Value;

		private static Lazy<DocServer> instance = new Lazy<DocServer>(() => new DocServer());

		public void PreOpenFile(IEnumerable<string> files)
		{
			foreach (var file in files) PreOpenFile(file);
		}
		public void PreOpenFile(string path)
		{
			if (!File.Exists(path)) throw new FileNotFoundException("", path);
			var io = new GH_DocumentIO();
			if (!io.Open(path))
			{
				Console.WriteLine("File loading failed.");
			}
			else
			{
				io.Document.Enabled = true;
				try
				{
					io.Document.NewSolution(true);
				}
				catch (Exception e)
				{

				}
				foreach (var obj in io.Document.Objects)
				{
					Doc[obj.NickName] = obj as GH_Component;
				}
			}
		}

	}
}
