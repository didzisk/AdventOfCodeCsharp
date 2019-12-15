using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day15
{

	class Day15Calc
	{
		static void OutputConsole(string text, long position, long output)
		{
			//Console.WriteLine(String.Format(text, position, output));
		}
		//north (1), south (2), west (3), and east (4)
		private const int N = 1;
		private const int S = 2;
		private const int W = 3;
		private const int E = 4;

		public class Node
		{
			public int X { get; set; }
			public int Y { get; set; }
			public bool WallW { get; set; }
			public bool WallE { get; set; }
			public bool WallN { get; set; }
			public bool WallS { get; set; }
			public bool IsOxygen { get; set; }
			public Node Parent { get; set; }
		}
		public static void Calc()
		{

			var rootNode = new Node { X=0, Y = 0};
			var mem = new long[10000];
			Day15Input.Day15Code.CopyTo(mem, 0);
			var nodeList = new Dictionary<Point, Node>();
			nodeList.Add(new Point(0,0), rootNode);
			var currentNode = rootNode;
			Console.SetCursorPosition(currentNode.X + 30, currentNode.Y + 30);
			Console.Write("x");
			TryAllDirections(rootNode, 0, mem, nodeList);
			var OxygenNode = nodeList.First(e => e.Value.IsOxygen);
			var current = OxygenNode.Value.Parent;
			int i = 0;
			while (current.Parent != null)
			{
				i++;
				current = current.Parent;
			}
			Console.WriteLine($"Num steps = {i}");
		}

		public static int AdvanceToNode(int pc, long[] mem, Node currentNode, Point nodeAt, Dictionary<Point, Node> nodeList)
		{
			if (currentNode.X != nodeAt.X && currentNode.Y != nodeAt.Y)
				throw new Exception("tried an illegal move");
			var localCounter = pc;
			int dir = N;
			if (currentNode.X < nodeAt.X)
				dir = E;
			if (currentNode.X > nodeAt.X)
				dir = W;
			if (currentNode.Y < nodeAt.Y)
				dir = S;
			if (currentNode.Y > nodeAt.Y)
				dir = N;
			var st = Machine.RunToInput(pc, mem, dir, OutputConsole);
			st = Machine.RunToOutput(st.ProgramCounter, mem, OutputConsole);
			/*	0: Wall. Position not changed.
				1: Moved
				2: Moved. Oxygen. */
			localCounter = (int)st.ProgramCounter;
			Node newNode;
			if (!nodeList.ContainsKey(nodeAt))
			{
				if (st.Result != 0)
				{
					newNode = new Node
					{
						X = nodeAt.X, Y = nodeAt.Y, IsOxygen = st.Result == 2, Parent = currentNode
					};
					nodeList.Add(nodeAt, newNode);
					Console.SetCursorPosition(nodeAt.X+30, nodeAt.Y+30);
					if (newNode.IsOxygen)
					{
						Console.Write("o");
					}
					else
					{
						Console.Write(".");

					}
					localCounter = TryAllDirections(newNode, localCounter, mem, nodeList);
					localCounter = RetreatToParent(localCounter, mem, newNode, nodeList);
				}
			}

			return localCounter;


		}

		public static int RetreatToParent(int pc, long[] mem, Node currentNode, Dictionary<Point, Node> nodeList)
		{
			var parent = currentNode.Parent;
			if (parent == null)
				return pc;
			if (currentNode.X != parent.X && currentNode.Y != parent.Y)
				throw new Exception("tried an illegal move");
			//Console.SetCursorPosition(currentNode.X + 30, currentNode.Y + 30);
			//Console.Write("<");

			var localCounter = pc;
			int dir = N;
			if (currentNode.X < parent.X)
				dir = E;
			if (currentNode.X > parent.X)
				dir = W;
			if (currentNode.Y < parent.Y)
				dir = S;
			if (currentNode.Y > parent.Y)
				dir = N;
			//Console.SetCursorPosition(parent.X + 30, parent.Y + 30);
			//Console.Write("p");
			var st = Machine.RunToInput(pc, mem, dir, OutputConsole);
			st = Machine.RunToOutput(st.ProgramCounter, mem, OutputConsole);
			if (st.Result==0)
				throw new Exception("Tried to go back and failed, wtf?");
			/*	0: Wall. Position not changed.
				1: Moved
				2: Moved. Oxygen. */
			pc = (int)st.ProgramCounter;

			return localCounter;


		}

		public static int TryAllDirections(Node node, int pc, long[] mem, Dictionary<Point, Node> nodeList)
		{
			Point nodeAt= new Point(node.X, node.Y - 1);
			var localCounter = pc;
			for (int i = 1; i < 5; i++)
			{
				switch (i)
				{
					case N:
						nodeAt = new Point(node.X, node.Y-1);
						break;
					case E:
						nodeAt = new Point(node.X+1, node.Y);
						break;
					case S:
						nodeAt = new Point(node.X, node.Y+1);
						break;
					case W:
						nodeAt = new Point(node.X-1, node.Y);
						break;
				}

				if (node.Parent == null || !(node.Parent.X == nodeAt.X && node.Parent.Y == nodeAt.Y))
					localCounter=AdvanceToNode(localCounter, mem, node, nodeAt, nodeList);
			}

			return localCounter;
		}
	}
}
