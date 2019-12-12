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
			//CalcBatch(Day12Input.Ex1, 10);
			//CalcBatch(Day12Input.Ex2, 100);
			//CalcBatch(Day12Input.official, 1000);
			Stopwatch watch = Stopwatch.StartNew();
			CalcBatch(Day12Input.Test1, 100);
			watch.Stop();
			Console.WriteLine(watch.Elapsed);

			CalcAllPeriods(Day12Input.Satellites(Day12Input.Ex1));
			CalcAllPeriods(Day12Input.Satellites(Day12Input.Ex2));
			CalcAllPeriods(Day12Input.Satellites(Day12Input.official));

			//Console.WriteLine($"{GCD(6,4)}");
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
				//Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");

			}
		}

		public static int CalcPeriod(int idx, IntMove[] l)
		{
			var a = l[idx];
			var counter = 0;
			//if (a.Equals())
			do
			{
				UpdateAll(l);
				counter++;
			} while (!a.Equals(l[idx]));
			return counter;
		}

		public static int CalcPeriodX(IntMove[] l)
		{
			var initialState = l.ToArray();
			Console.WriteLine("Initial X");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");

			var counter = 0;
			var eq = true;
			do
			{
				UpdateAll(l);
				counter++;
				eq = true;
				for (int i = 0; i < l.Length; i++)
				{
					if (l[i].X != initialState[i].X)
						eq = false;
					if (l[i].VX != initialState[i].VX)
						eq = false;
				}

			} while (!eq);
			Console.WriteLine("Final X");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");
			return counter;
		}
		public static int CalcPeriodY(IntMove[] l)
		{
			Console.WriteLine("Initial Y");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");
			var initialState = l.ToArray();

			var counter = 0;
			var eq = true;
			do
			{
				UpdateAll(l);
				counter++;
				eq = true;
				for (int i = 0; i < l.Length; i++)
				{
					if (l[i].Y != initialState[i].Y)
						eq = false;
					if (l[i].VY != initialState[i].VY)
						eq = false;
				}

			} while (!eq);
			Console.WriteLine("Final Y");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");
			return counter;
		}
		public static int CalcPeriodZ(IntMove[] l)
		{
			var initialState = l.ToArray();
			Console.WriteLine("Initial Z");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");

			var counter = 0;
			var eq = true;
			do
			{
				UpdateAll(l);
				counter++;
				eq = true;
				for (int i = 0; i < l.Length; i++)
				{
					if (l[i].Z != initialState[i].Z)
						eq = false;
					if (l[i].VZ != initialState[i].VZ)
						eq = false;
				}

			} while (!eq);
			Console.WriteLine("Final Z");
			foreach (var a in l)
				Console.WriteLine($"x={a.X}, y={a.Y}, z={a.Z};  vx={a.VX}, vy={a.VY}, vz={a.VZ}");
			return counter;
		}

		static Int64 GCD(Int64 a, Int64 b)
		{
			return b == 0 ? Math.Abs(a) : GCD(b, a % b);
		}

		public static Int64 LCM(Int64 a, Int64 b)
		{
			return a * b / GCD(a, b);
		}


		public static long CalcAllPeriods(IntMove[] l)
		{
			var px = CalcPeriodX(l.ToArray());
			var py = CalcPeriodY(l.ToArray());
			var pz = CalcPeriodZ(l.ToArray());

			var gcd = GCD(px, GCD(py, pz));
			var per = LCM(px, py);
			per = LCM(pz, per);
			Console.WriteLine($"px={px}, py={py}, pz={pz}, gcd={gcd}, period={per}");
			return per;
		}

	}
}
