using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Day13
{
	class Program
	{
		static void OutputConsole(string text, long position, long output)
		{
			//Console.WriteLine(String.Format(text, position, output));
		}


		static void Main(string[] args)
		{
			var memory = new long[10000];
			Day13Input.Day13Code.CopyTo(memory, 0);
			var grid = new Dictionary<Point, long>();
			var res = new MachineStatus();
			do
			{
				res = Machine.RunToOutput(res.ProgramCounter, memory, OutputConsole);
				var x = res.Result;
				res = Machine.RunToOutput(res.ProgramCounter, memory, OutputConsole);
				var y = res.Result;
				res = Machine.RunToOutput(res.ProgramCounter, memory, OutputConsole);
				var item = res.Result;
				var pt = new Point {X = (int) x, Y = (int) y};
				grid[pt] = item;
			} while (!res.RanToHalt);
			//var res=Machine.Calc(0, memory.ToArray(), new long[]{} , OutputConsole);
			var pt1 = grid.Count(e => e.Value == 2);

			Console.WriteLine($"Num blocks part 1 = {pt1}");
		}
	}
}
