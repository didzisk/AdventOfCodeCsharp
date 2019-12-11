using System;
using System.Collections.Generic;
using System.Numerics;
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
}

