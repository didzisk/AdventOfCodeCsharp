using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day17
{

	class Day17Calc
	{
		static void OutputConsole(string text, long position, long output)
		{
			//Console.WriteLine(String.Format(text, position, output));
		}

		public class Node
		{
			public int X { get; set; }
			public int Y { get; set; }
			public bool WallW { get; set; }
			public bool WallE { get; set; }
			public bool WallN { get; set; }
			public bool WallS { get; set; }
			public int NumWalls { get; set; }
			public bool IsOxygen { get; set; }
			public Node Parent { get; set; }
		}
		public static void Calc()
		{
			var arr = new char[43, 49];
			var mem = new long[10000];
			MachineStatus st = new MachineStatus { ProgramCounter = 0 };
			Day17Input.Day17Code.CopyTo(mem, 0);
			{
				var x = 0;
				var y = 0;
				do
				{
					st = Machine.RunToOutput(st, mem, OutputConsole);
					if (!st.RanToHalt)
					{
						switch ((char) st.Result)
						{
							case '\n':
								y++;
								x = 0;
								break;
							default:
								arr[x, y] = (char) st.Result;
								x++;
								break;
						}

						Console.Write((char) st.Result);
					}
				} while (!st.RanToHalt);
			}
			var checksum = 0;
			for (int y = 0; y < arr.GetLength(1); y++)
			{
				for (int x = 0; x < arr.GetLength(0); x++)
				{
					Console.SetCursorPosition(x,y);
					Console.Write(arr[x,y]);
					if (x == 0 || x == arr.GetLength(0) - 1 || y == 0 || y == arr.GetLength(1) - 1)
						continue;
					if (arr[x - 1, y] == '#' && arr[x + 1, y] == '#' && arr[x, y - 1] == '#' && arr[x, y + 1] == '#')
						checksum += x * y;
				}
			}
			Console.WriteLine($"Checksum: {checksum}");
			Console.ReadLine();
			//part2
			Day17Input.Day17Code.CopyTo(mem, 0);
			mem[0] = 2;
			bool wantCamera = false;
			//R,8,L,10,L,12,R,4,R,8,L,12,R,4,R,4,R,8,L,10,L,12,R,4,R,8,L,10,R,8,R,8,L,10,L,12,R,4,R,8,L,12,R,4,R,4,R,8,L,10,R,8,R,8,L,12,R,4,R,4,R,8,L,10,R,8,R,8,L,12,R,4,R,4
			// 
			string program = "A,B,A,C,A,B,C,B,C,B\nR,8,L,10,L,12,R,4\nR,8,L,12,R,4,R,4\nR,8,L,10,R,8\ny\n";
			st.ProgramCounter = 0;
			foreach (char c in program)
			{
				st = Machine.RunToInput(st, mem, c, OutputConsole);
			}
			do
			{
				st = Machine.RunToOutput(st, mem, OutputConsole);
				if (Console.CursorLeft>=43 && Console.CursorTop>=49)
					Console.Clear();

				if (st.Result<256)
				  Console.Write((char)st.Result);
				else
				{
					Console.WriteLine(st.Result);
				}
			} while (!st.RanToHalt);

			return;
		}

		public static void CalcPart2()
		{
			Console.Clear();
			var mem = new long[1000000];
			Day17Input.Day17Code.CopyTo(mem, 0);
			mem[0] = 2;
			bool wantCamera = false;
			string program = "A,B,A,C,A,B,C,B,C,B\nR,8,L,10,L,12,R,4\nR,8,L,12,R,4,R,4\nR,8,L,10,R,8\n";
			var st = new MachineStatus {ProgramCounter = 0};
			foreach (char c in program)
			{
				st = Machine.RunToInput(st, mem, c, OutputConsole);
			}
			st = new MachineStatus { ProgramCounter = 0 };
			do
			{
				st = Machine.RunToOutput(st, mem, OutputConsole);
			} while (!st.RanToHalt);

			if (wantCamera)
			{
				var x = 0;
				var y = 0;
				st = new MachineStatus { ProgramCounter = 0 };
				do
				{
					st = Machine.RunToOutput(st, mem, OutputConsole);
					if (!st.RanToHalt)
					{
						switch ((char)st.Result)
						{
							case '\n':
								y++;
								x = 0;
								break;
							default:
								x++;
								break;
						}

						Console.Write((char)st.Result);
					}
				} while (!st.RanToHalt);
			}

			st = Machine.RunToOutput(st, mem, OutputConsole);

			Console.WriteLine($"Final Num dust = {st.Result}");

		}


	}
}
