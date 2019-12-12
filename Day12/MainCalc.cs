using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day12
{
	public class MainCalc
	{
		public static void CalcBatch(string inp, int numIterations)

		{
			var vectors = Day12Input.Satellites(inp);
			Console.WriteLine(vectors.Length);
			for (int i = 1; i < numIterations+1; i++)
			{
				UpdateAll(vectors);
			}

			Console.WriteLine($"After {numIterations} steps");
			var tot = vectors.Select(a => CalcKinOne(a) * CalcPotOne(a)).Sum();
			Console.WriteLine($"Tot = {tot}");
		}

		public static void Calc()
		{
			CalcBatch(Day12Input.Ex1, 10);
			CalcBatch(Day12Input.Ex2, 100);
			CalcBatch(Day12Input.official, 1000);
			Stopwatch watch = Stopwatch.StartNew();
			CalcBatch(Day12Input.official, 1000000);
			watch.Stop();
			Console.WriteLine(watch.Elapsed);
		}

		public static IntMove ApplyGravity(IntMove source, IntMove target)
		{
			if (source.X > target.X)
				target.VX++;
			if (source.X < target.X)
				target.VX--;
			if (source.Y > target.Y)
				target.VY++;
			if (source.Y < target.Y)
				target.VY--;
			if (source.Z > target.Z)
				target.VZ++;
			if (source.Z < target.Z)
				target.VZ--;
			return target;
		}

		public static IntMove UpdateWithGravity(IntMove a, IEnumerable<IntMove> l)
		{
			var influencers = l.Except(new[] {a}).ToArray();
			var target = a;
			foreach (var source in influencers)
			{
				target=ApplyGravity(source, target);
			}

			return target;
		}

		public static void UpdateAllWithGravity(IntMove[] l)
		{
			for (int i = 0; i < l.Length; i++)
			{
				l[i]=UpdateWithGravity(l[i], l);
			}
		}

		public static int CalcPotOne(IntMove a)
		{
			return Math.Abs(a.X) + Math.Abs(a.Y) + Math.Abs(a.Z);
		}
		public static int CalcKinOne(IntMove a)
		{
			return Math.Abs(a.VX) + Math.Abs(a.VY) + Math.Abs(a.VZ);
		}

		public static void UpdateAll(IntMove[] l)
		{
			UpdateAllWithGravity(l);
			for (int i = 0; i < l.Length; i++)
			{
				l[i].X += l[i].VX;
				l[i].Y += l[i].VY;
				l[i].Z += l[i].VZ;
			}
			foreach (var a in l)
			{
//				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");

			}
		}
	}
}
