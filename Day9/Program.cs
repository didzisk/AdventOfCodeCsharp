using System;
using System.Collections.Generic;
using System.Drawing;
using Day11;

namespace Day9
{
	class Program
	{
		static void OutputConsole(string text, long position, long output)
		{
			//Console.WriteLine(String.Format(text, position, output));
		}

		static int GetColor(Dictionary<Point, int> wall, Point pos)
		{
			return (wall.ContainsKey(pos)) ? wall[pos] : 0;

		}

		static void Main(string[] args)
		{
			//Day13Main.MainCalc();
			Day13Main.Part2Calc();
			return;
			Day11.MainClass.MainCalc();
			Console.WriteLine("Hello World!");
			long[] largerExample2 =
			{
				3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
				1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
				999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
			};
			Console.WriteLine("asking for 8, input 9, expect 1001:");
			Machine.Calc(0, largerExample2, new long[] { 9 }, OutputConsole);

			long[] day9ex1 = { 104, 1125899906842624, 99 };
			Console.WriteLine("D9 asking for output, expect 1125899906842624:");
			Machine.RunToOutput(0, day9ex1, OutputConsole);

			long[] day9ex2 = { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
			Console.WriteLine("D9Ex2, expect long:");
			Machine.RunToOutput(0, day9ex2, OutputConsole);

			long[] day9ex3code =
				{109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99};
			long[] day9ex3 = new long[1000];
			day9ex3code.CopyTo(day9ex3, 0);
			/*
			 00	109		IncreaseRB
			 01	  1	 		by value 1
			 02 204		Output from relative pos 1-1=0
			 03 1001	ADD
			 04	 100		from address 100
			 05    1		and value 1
			 06	 100		Place into 100
			 07 1008	EQ?
			 08	 100		from address 100
			 09   16		value 16
			 10	 101		place 1 or 0 into 101
			 11	1006	JZ
			 12  101		if this is 0
			 13    0		goto 0
			 14   99	HALT

			 */
			/*
			Console.WriteLine("D9Ex3, expect prog:");
			Machine.Calc(0, day9ex3, new long[0], OutputConsole);
			long[] day9mem1 = new long[10000];
			Console.WriteLine("D9Part1, expect 1 line");
			Day9Input.Day9Code.CopyTo(day9mem1, 0);
			Machine.Calc(0, day9mem1, new long[]{1}, OutputConsole);
			day9mem1 = new long[10000];
			Console.WriteLine("D9Part2, expect 1 line");
			Day9Input.Day9Code.CopyTo(day9mem1, 0);
			Machine.Calc(0, day9mem1, new long[] { 2 }, OutputConsole);
			*/
			long[] day11mem = new long[10000];
			var wall = new Dictionary<Point, int>();
			var pos = new Point { X = 0, Y = 0 };
			wall[pos] = 0;
			var dir = '^';
			Console.WriteLine("D11part1");
			Day9Input.Day11Code.CopyTo(day11mem, 0);
			var pc = 0;
			var halted = false;
			var i = 0;
			do
			{

				var st = Machine.RunToInput(pc, day11mem, GetColor(wall, pos), OutputConsole);
				st = Machine.RunToOutput(st.ProgramCounter, day11mem, OutputConsole);
				wall[pos] = (int)st.Result;
				st = Machine.RunToOutput(st.ProgramCounter, day11mem, OutputConsole);
				var turn = st.Result;
				var newDir = dir;
				Console.WriteLine($"({pos.X},{pos.Y})={wall[pos]} {dir}");
				switch (dir)
				{
					case '^':
						newDir = turn == 0 ? '<' : '>';
						break;
					case 'v':
						newDir = turn == 0 ? '>' : '<';
						break;
					case '<':
						newDir = turn == 0 ? 'v' : '^';
						break;
					case '>':
						newDir = turn == 0 ? '^' : 'v';
						break;
				}
				dir = newDir;
				switch (dir)
				{
					case '^':
						pos.Y--;
						break;
					case 'v':
						pos.Y++;
						break;
					case '<':
						pos.X--;
						break;
					case '>':
						pos.X++;
						break;
				}
				halted = st.RanToHalt;
				i++;
			} while (i < 100 && !halted );

		}
	}
}
