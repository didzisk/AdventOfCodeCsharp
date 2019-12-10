using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;

namespace Day10
{
	public struct IntPoint
	{
		public int X;
		public int Y;
	}
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("◚");
			var t = new IntPoint { X = 3, Y = 0 };
			for (int i = 0; i < 20; i++)
			{
				Console.WriteLine($"Direction ({t.X}, {t.Y})");
				t = GetNextBorderPoint(t, Day10Input.Ex1);
			}

			Shoot200(new IntPoint { X = 11, Y = 13 }, Day10Input.Ex2);
			Shoot200(new IntPoint { X = 20, Y = 18 }, Day10Input.InputStrings);
//			Calc(Day10Input.InputStrings);
			return;
			PrintInput(Day10Input.Ex1);
			Calc(Day10Input.Ex1);
			PrintInput(Day10Input.InputStrings);
			Calc(Day10Input.InputStrings);
			PrintInput(Day10Input.Ex2);
			Calc(Day10Input.Ex2);
		}

		public static bool Blocks(IntPoint lookAt, IntPoint blocker, IntPoint basePoint, string[] lines)
		{
			if (blocker.Equals(lookAt))
				return false;
			if (blocker.Equals(basePoint))
				return false;
			IntPoint dv = DirectionVector(basePoint, lookAt);
			var distA = DistanceInDirectionVectors(basePoint, lookAt, dv);
			var distB = DistanceInDirectionVectors(basePoint, blocker, dv);
			if (distA <= distB)
				return false;
			for (int c = 1; c < 24; c++)
			{
				var currentX = basePoint.X + dv.X * c;
				var currentY = basePoint.Y + dv.Y * c;
				if (blocker.X == currentX && blocker.Y == currentY)
					if (lines[currentY][currentX] == '#')
						return true;
			}

			//is 2 blocking 1? 
			return false;
		}

		public static bool Visible(int x1i, int y1i, int xb, int yb, string[] lines)
		{
			if (lines[yb][xb] != '#') //intended base is not an asteroid
				return false;
			if (lines[y1i][x1i] != '#') //not looking at asteroid
				return false;
			if (x1i == xb && y1i == yb) //same asteroid
				return false;
			for (int y2 = 0; y2 < lines.Length; y2++)
			{
				for (int x2 = 0; x2 < lines[y2].Length; x2++)
				{
					if (Blocks(new IntPoint { X = x1i, Y = y1i }, new IntPoint { X = x2, Y = y2 }, new IntPoint { X = xb, Y = yb }, lines))
						return false;
				}
			}

			return true;

		}

		static void Calc(string[] lines)
		{
			var maxVisible = 0;
			for (int yb = 0; yb < lines.Length; yb++)
			{
				for (int xb = 0; xb < lines[yb].Length; xb++)
				{
					var currentVisible = 0;
					for (int y2 = 0; y2 < lines.Length; y2++)
					{
						for (int x2 = 0; x2 < lines[y2].Length; x2++)
						{
							if (Visible(x2, y2, xb, yb, lines))
								currentVisible++;
						}
					}

					if (currentVisible > maxVisible)
					{
						maxVisible = currentVisible;
						Console.WriteLine($"{xb}, {yb}, {maxVisible}");
					}

				}
			}
		}

		public static void PrintInput(string[] lines)
		{
			foreach (var line in lines)
			{
				Console.WriteLine(string.Join("", line.Select(x => x.ToString() + x.ToString())).ToArray());
			}
		}

		public static IntPoint BigDirectionVector(IntPoint b, IntPoint t)
		{
			var vx = t.X - b.X;
			var vy = t.Y - b.Y;
			return new IntPoint { X = vx, Y = vy };
		}
		public static IntPoint DirectionVector(IntPoint b, IntPoint t)
		{
			var vx = t.X - b.X;
			var vy = t.Y - b.Y;
			var len = Math.Max(Math.Abs(vx), Math.Abs(vy));
			var c = 1;
			for (int i = len; i >= 1; i--)
				if ((vx % i == 0) && (vy % i == 0))
				{
					c = i;
					break;
				}
			return new IntPoint { X = vx / c, Y = vy / c };
		}

		public static int DistanceInDirectionVectors(IntPoint b, IntPoint t, IntPoint dv)
		{
			if (dv.X == 0)
				return (t.Y - b.Y) / dv.Y;
			return (t.X - b.X) / dv.X;

		}

		public static IntPoint AddVector(IntPoint b, IntPoint dv)
		{
			return new IntPoint { X = b.X + dv.X, Y = b.Y + dv.Y };
		}

		public static bool IsPointValid(IntPoint b, string[] lines)
		{
			var maxvalue = lines.Length - 1;
			return !(b.X < 0 || b.Y < 0 || b.X > maxvalue || b.Y > maxvalue);
		}

		public static bool ContainsAsteroid(IntPoint b, string[] lines)
		{
			return lines[b.Y][b.X] == '#';
		}
		public static bool ShootAsteroid(IntPoint b, string[] lines, int num)
		{
			var str = lines[b.Y];
			if (lines[b.Y][b.X] == '#')
			{
				lines[b.Y] = str.Substring(0, b.X) + "." + str.Substring(b.X + 1);
				Console.WriteLine($"{num}: Shot ({b.X}, {b.Y})");
				return true;
			}

			return false;
		}


		public static IntPoint FirstInDirection(IntPoint b, IntPoint dv, string[] lines)
		{
			IntPoint current = b;
			IntPoint saved;
			do
			{
				saved = current;
				current = AddVector(current, dv);
				if (IsPointValid(current, lines) && ContainsAsteroid(current, lines))
					return current;
			} while (IsPointValid(current, lines));
			return saved;
		}

		public static IntPoint GetNextBorderPoint(IntPoint t, string[] lines)
		{
			var len = lines.Length - 1;
			if (t.X == len)
			{
				if (t.Y == len)
					return new IntPoint { X = t.X - 1, Y = len };
				return new IntPoint { X = t.X, Y = t.Y + 1 };
			}

			if (t.X == 0)
			{
				if (t.Y == 0)
					return new IntPoint { X = 1, Y = 0 };
				return new IntPoint { X = 0, Y = t.Y - 1 };
			}

			if (t.Y == 0)
			{
				if (t.X == 0)
					return new IntPoint { X = 1, Y = 0 };
				return new IntPoint { X = t.X + 1, Y = 0 };
			}

			if (t.Y == len)
			{
				if (t.X == 0)
					return new IntPoint { X = 0, Y = len - 1 };
				return new IntPoint { X = t.X - 1, Y = len };
			}
			return new IntPoint { X = 1, Y = 0 };//dummy, shouldn't happen'
		}

		public static void Shoot200(IntPoint b, string[] lines)
		{
			List<IntPoint> points = new List<IntPoint>();
			for (int x = 0; x < lines.Length; x++)
			{
				for (int y = 0; y < lines.Length; y++)
				{
					if (x==b.X && y==b.Y)
						continue;
					points.Add(new IntPoint{X=x, Y=y});
				}
			}

			var withAngles = points
				.Select(p => new { Point=new IntPoint { X = p.X, Y = p.Y }, Angle = Angle(b, p)})
				.OrderBy(elm=>elm.Angle)
				.ToList();
			var i = 2;
			foreach (var angle in withAngles)
			{
				Console.WriteLine($"({angle.Point.X},{angle.Point.Y}) {angle.Angle}");
			}

			double previousAngle = -10.0;
			for (int j = 0; j < 5; j++)
			{
				foreach (var angle in withAngles)
				{
					if (angle.Angle == previousAngle)
						continue;
					var dir = DirectionVector(b, angle.Point);
					var badPoint = FirstInDirection(b, dir, lines);
					if (ShootAsteroid(badPoint, lines, i))
					{
						i++;
						previousAngle = angle.Angle;
					}

				}

			}

			return;
			/*
			IntPoint currentPoint = new IntPoint { X = b.X, Y = 0 };

			for (int i = 0; i < 210; i++)
			{
				var dir = DirectionVector(b, currentPoint);
				var badPoint = FirstInDirection(b, dir, lines);
				Console.Write($"{i}:");
				ShootAsteroid(badPoint, lines);
				currentPoint = GetNextBorderPoint(currentPoint, lines);
				if (currentPoint.X == b.X && currentPoint.Y == b.Y)
					currentPoint = GetNextBorderPoint(currentPoint, lines);
			}
			*/
		}

		public static double Angle(IntPoint b, IntPoint t)
		{
			var dir = BigDirectionVector(b, t);
			var sin = (double)dir.X / Math.Sqrt((double)dir.Y * (double)dir.Y + (double)dir.X * (double)dir.X);
			if (dir.X > 0 && dir.Y < 0)
				return sin;
			if (dir.X > 0 && dir.Y > 0)
				return sin + 1;
			if (dir.X < 0 && dir.Y > 0)
				return sin + 2;
			return sin + 3;

		}
	}
}
