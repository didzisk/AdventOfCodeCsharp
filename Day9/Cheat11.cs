using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Day9;

namespace Day11
{
	using Coords = Tuple<int, int>;
	using INT = Int64;
	class Intcode
	{
		private List<INT> inputQueue = new List<INT>();

		public INT output = 0;
		public Intcode outputDest = null;

		private Dictionary<INT, INT> memory;
		private INT ip = 0;
		private INT rb = 0;

		public bool waiting = false;
		public bool halted = false;
		public bool sentOutput = false;

		public int id = 0;
		private static int nextID = 0;

		public override string ToString()
		{
			return ("Intcode" + id);
		}

		public Intcode(List<INT> initialMemory)
		{
			id = nextID;
			nextID++;
			memory = new Dictionary<INT, INT>();
			INT address = 0;
			foreach (INT n in initialMemory)
			{
				memory[address] = n;
				address = address + 1;
			}
		}

		public void SendInput(INT input)
		{
			inputQueue.Add(input);
			waiting = false;
		}

		private void Output(INT i)
		{
			output = i;
			if (outputDest != null)
			{
				outputDest.SendInput(i);
			}
			else
			{
				// Console.WriteLine(i);
			}
			sentOutput = true;
		}

		private INT memget(INT address)
		{
			if (memory.ContainsKey(address))
				return memory[address];
			else
				return 0;
		}

		private void memset(INT address, INT value)
		{
			memory[address] = value;

		}

		private int accessMode(int idx)
		{
			int mode = (int)memget(ip) / 100;
			for (int i = 1; i < idx; i++)
				mode = mode / 10;
			return mode % 10;
		}

		private void SetParam(int idx, INT value)
		{
			INT param = memget(ip + idx);
			switch (accessMode(idx))
			{
				case 0: // position mode
					memset(param, value);
					break;
				case 1: // immediate mode -- should never occur
					throw new System.Exception("Intcode immediate mode not allowed in setting memory");
				case 2: // relative mode
					memset(rb + param, value);
					break;
				default:
					throw new System.Exception("Invalid Intcode parameter mode");
			}
		}

		private INT GetParam(int idx)
		{
			INT param = memget(ip + idx);
			switch (accessMode(idx))
			{
				case 0: // position mode
					return memget(param);
				case 1: // immediate mode
					return param;
				case 2: // relative mode
					return memget(rb + param);
				default:
					throw new System.Exception("Invalid Intcode parameter mode");
			}

		}

		public void Step()
		{
			int opcode = (int)(memget(ip) % 100);
			switch (opcode)
			{
				case 1: // add
					SetParam(3, GetParam(1) + GetParam(2));
					ip += 4;
					break;
				case 2: // multiply
					SetParam(3, GetParam(1) * GetParam(2));
					ip += 4;
					break;
				case 3: // input
					if (inputQueue.Count > 0)
					{
						SetParam(1, inputQueue[0]);
						inputQueue.RemoveAt(0);
						ip += 2;
					}
					else
					{
						waiting = true;
					}
					break;
				case 4: // output
					Output(GetParam(1));
					ip += 2;
					break;
				case 5: // jump-if-true
					if (GetParam(1) == 0)
						ip += 3;
					else
						ip = GetParam(2);
					break;
				case 6: // jump-if-false
					if (GetParam(1) == 0)
						ip = GetParam(2);
					else
						ip += 3;
					break;
				case 7: // less-than
					if (GetParam(1) < GetParam(2))
						SetParam(3, 1);
					else
						SetParam(3, 0);
					ip += 4;
					break;
				case 8: // equals
					if (GetParam(1) == GetParam(2))
						SetParam(3, 1);
					else
						SetParam(3, 0);
					ip += 4;
					break;
				case 9: // relative base offset
					rb += GetParam(1);
					ip += 2;
					break;
				case 99:
					halted = true;
					break;
				default:
					throw new System.Exception("Unknown Intcode opcode " + opcode + " at position " + ip);
			}

		}

		public void RunToOutput()
		{
			sentOutput = false;
			while (!halted && !waiting && !sentOutput)
				Step();
		}

		public void Run()
		{
			while (!halted && !waiting)
				Step();
			if (waiting)
			{
			}
		}

	}

	class Paintbot
	{
		Dictionary<Coords, int> grid;
		Intcode brain;
		Coords cursor;
		int facing = 0;

		public Paintbot(List<INT> input)
		{
			grid = new Dictionary<Coords, int>();
			brain = new Intcode(input);
			cursor = new Coords(0, 0);
		}

		public int GetColor(Coords coords)
		{
			return (grid.ContainsKey(coords)) ? grid[coords] : 0;
		}

		public int GetColor()
		{
			return GetColor(cursor);
		}

		public void SetColor(int color)
		{
			grid[cursor] = color;
		}

		public void TurnLeft()
		{
			facing = (facing + 3) % 4;
		}

		public void TurnRight()
		{
			facing = (facing + 1) % 4;
		}

		public void StepForward()
		{
			switch (facing)
			{
				case 0:
					cursor = new Coords(cursor.Item1, cursor.Item2 + 1);
					Console.Write('v');
					break;
				case 1:
					cursor = new Coords(cursor.Item1 + 1, cursor.Item2);
					Console.Write('>');
					break;
				case 2:
					cursor = new Coords(cursor.Item1, cursor.Item2 - 1);
					Console.Write('^');
					break;
				case 3:
					cursor = new Coords(cursor.Item1 - 1, cursor.Item2);
					Console.Write('<');
					break;
			}
		}

		public void Run()
		{
			brain.RunToOutput();
			Console.WriteLine($"({cursor.Item1},{cursor.Item2})={GetColor()}");
			int i = 0;
			while (i<100 && !brain.halted)
			{
				if (brain.waiting)
				{
					brain.SendInput(GetColor());
				}
				else
				{
					SetColor((int)brain.output);
					brain.RunToOutput();
					if (brain.output == 0)
						TurnLeft();
					else
						TurnRight();
					StepForward();
					Console.WriteLine($"({cursor.Item1},{cursor.Item2})={GetColor()}");

				}
				brain.RunToOutput();
				i++;
			}
		}

		public int countPaintedPanels()
		{
			return grid.Count;
		}

		public void Display()
		{
			int minx = 0;
			int maxx = 0;
			int miny = 0;
			int maxy = 0;
			foreach (Coords coords in grid.Keys)
			{
				if (coords.Item1 < minx) minx = coords.Item1;
				if (coords.Item1 > maxx) maxx = coords.Item1;
				if (coords.Item2 < miny) miny = coords.Item2;
				if (coords.Item2 > maxy) maxy = coords.Item2;
			}

			for (int y = maxy; y >= miny; y--)
			{
				for (int x = minx; x <= maxx; x++)
				{
					Console.Write(GetColor(new Coords(x, y)) > 0 ? '#' : ' ');
				}
				Console.WriteLine();
			}


		}

	}

	public class MainClass
	{

		public static void MainCalc()
		{
			List<INT> input = new List<INT>();
			string line;
			line = Day9Input.Day11CodeAsString;
			foreach (string token in line.Split(','))
			{
				input.Add(INT.Parse(token));
			}

			Paintbot p = new Paintbot(input);
			p.Run();
			Console.WriteLine(p.countPaintedPanels());

			p = new Paintbot(input);
			p.SetColor(1);
			p.Run();
			p.Display();
		}

	}

	public static class Day13Main
	{
		public static void MainCalc()
		{
			List<INT> input = new List<INT>();
			string line;
			line = Day9Input.Day13CodeAsString;
			foreach (string token in line.Split(','))
			{
				input.Add(INT.Parse(token));
			}
			var grid = new Dictionary<Point, INT>();
			var brain = new Intcode(input);
			while (!brain.halted)
			{
				brain.RunToOutput();
				var x =brain.output;
				brain.RunToOutput();
				var y = brain.output;
				brain.RunToOutput();
				var item = brain.output;
				var pt = new Point {X = (int)x, Y = (int)y};
				grid[pt] = item;
			}

			var pt1=grid.Count(e=>e.Value==2);

			Console.WriteLine($"Num blocks part 1 = {pt1}");
		}

		public static void WaitMicroseconds(int d)
		{
			//1 tick=100ns
			var t = DateTime.Now;
			var next = t.Add(new TimeSpan(10 * d));
			while (DateTime.Now < next)
			{ }
		}

		public static void Part2Calc()
		{
			Console.ReadLine();
			List<INT> input = new List<INT>();
			string line;
			line = Day9Input.Day13CodeAsString;
			foreach (string token in line.Split(','))
			{
				input.Add(INT.Parse(token));
			}

			input[0] = 2;
			var grid = new Dictionary<Point, INT>();
			var brain = new Intcode(input);
			INT joystick = 0;
			INT paddleX = 0;
			bool badBallSeen=false;
			INT maxDisplay = 0;
			int delayTimer = 100;
			Console.CursorVisible = false;
			while (!brain.halted)
			{
				if (brain.waiting)
				{
					brain.SendInput(joystick);
					WaitMicroseconds(delayTimer);
				}
				else
				{
					brain.RunToOutput();
					var x =(int)brain.output;
					brain.RunToOutput();
					var y = (int)brain.output;
					brain.RunToOutput();
					var item = (int)brain.output;
					if (x == -1 && y == 0)
					{
						INT display = item;
						if (display > maxDisplay)
							maxDisplay = display;
						var numBlocks = grid.Count(e => e.Value == 2);
						delayTimer = numBlocks / 4 + 1;
						Console.SetCursorPosition(1, 1);
						Console.Write($"SCORE: {maxDisplay:#####}   Remaining: {numBlocks:000}");
					}
					else
					{
						if (item<10)
							Console.SetCursorPosition(x, y+2);
						if (item == 0)
						{
							if (!(x==0&&y==0))
								Console.Write(" ");
						}
						if (item == 1)
						{
							Console.ForegroundColor = ConsoleColor.Green;
							if (x==0 || x==42)
								switch (y)
								{
									case 0:
										Console.Write("+");
										break;
									default:
										Console.Write("|");
										break;
								}
							else
							  Console.Write("-");
							Console.ForegroundColor = ConsoleColor.White;
						}
						if (item == 2)
						{
							Console.Write("o");
						}

						if (item == 3)
						{
							Console.ForegroundColor = ConsoleColor.DarkRed;
							Console.Write("=");
							Console.ForegroundColor = ConsoleColor.White;
							paddleX = x;
							WaitMicroseconds(delayTimer*6);
							//Console.SetCursorPosition(1, 2);
							//Console.Write($"({x:0000}, {y:0000}) Paddle");
						}

						if (item == 4) //4=ball
						{
							if (!badBallSeen && x == 4 && y == 4)
							{
								badBallSeen = true;
								continue;
							}

							badBallSeen = false;
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.Write("o");
							Console.ForegroundColor = ConsoleColor.White;
							WaitMicroseconds(delayTimer * 60);
							//Console.SetCursorPosition(1,3);
							//Console.Write($"({x:0000}, {y:0000}) Ball");
							joystick = 0;
							if (x < paddleX)
								joystick = -1;
							if (x > paddleX)
								joystick = 1;
							WaitMicroseconds(delayTimer);
						}
					}
					var pt = new Point {X = (int) x, Y = (int) y};
					grid[pt] = item;
				}
			}

			Console.ReadLine();
		}

	}
}

