using System;

namespace Day19
{

	class Day19Calc
	{
		public static void Calc()
		{
			Console.WindowHeight = 60;
			var width = 50;
			var num = 0;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < width; y++)
				{
					var hasBeam = HasBeam(x, y);
					Console.SetCursorPosition(x, y);
					Console.Write(hasBeam ? 'X' : ' ');
					if (hasBeam)
						num++;
				}
			}
			Console.WriteLine($"Num cells {num}");
		}

		public static void CalcPart2()
		{
			Console.Clear();
			Console.WindowHeight = 60;
			int y = 10;
			int xStart = 0;
			while (true)
			{
				y++;
				Console.SetCursorPosition(55,30);
				if (y%10==0)
					Console.Write(y);
				while (!HasBeam(xStart, y))
				{
					xStart++;
				};
				int x = xStart;
				while (HasBeam(x + 99, y))
				{
					if (!HasBeam(x, y))
						throw new Exception("weird");
					if (HasBeam(x, y + 99))
					{
						Console.WriteLine($"{x * 10000 + y}");
						return;
					}
					x++;
				}
			}
		}

		public static bool HasBeam(int x, int y)
		{
			var mem = new long[10000];
			var st = new MachineStatus { ProgramCounter = 0 };
			Day19Input.Day19Code.CopyTo(mem, 0);
			st = Machine.RunToInput(st, mem, x);
			st = Machine.RunToInput(st, mem, y);
			st = Machine.RunToOutput(st, mem);
			return (st.Result == 1);
		}
	}
}
