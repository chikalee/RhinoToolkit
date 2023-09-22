// See https://aka.ms/new-console-template for more information
using Rhino.Compute;
using Rhino.Geometry;

var crv = new Circle().ToNurbsCurve();
Console.WriteLine(crv.GetLength());