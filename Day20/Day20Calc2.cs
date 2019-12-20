using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    public class Node2
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }
        public int Level { get; set; }

        public List<Node2> Neighbors { get; set; }
        public Node2(int x, int y)
        {
            X = x;
            Y = y;
            Neighbors = new List<Node2>();
        }
    }
    public static class Day20Calc2
    {
        public static void CalcPart2(string input)
        {
	        var nodes = new List<Node2>();
	        var level = 0;
	        var mindist = 100000000;
	        while (level < 100 && mindist>level)
	        {
		        ParseLevel(nodes, input, level);
		        var startNode = nodes.First(e => e.Label == "AA");
		        var dist = Bfs(nodes);
		        if (dist < mindist)
			        mindist = dist;
		        Console.WriteLine($"dist:{dist} level:{level}");
		        level++;
	        }
        }
        
        public static List<Node2> ParseLevel(List<Node2>nodes, string input, int level)
        {
            var lines = input.Split("\r\n");
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
	                bool isOuter = IsOuter(x, y, lines[y].Length, lines.Length);
                    if (lines[y][x] == '.')
                    {
                        var node = new Node2(x, y);
                        if (x>1 && y>1)
                        {
                            var label = "";
                            if (IsCapitalLetter(lines[y][x - 2]) && IsCapitalLetter(lines[y][x - 1]))
                                label = new string(new [] { lines[y][x - 2], lines[y][x - 1] });
                            if (IsCapitalLetter(lines[y][x + 1]) && IsCapitalLetter(lines[y][x + 2]))
                                label = new string(new [] { lines[y][x + 1], lines[y][x + 2] });
                            if (IsCapitalLetter(lines[y - 2][x]) && IsCapitalLetter(lines[y - 1][x]))
                                label = new string(new [] { lines[y - 2][x], lines[y - 1][x] });
                            if (IsCapitalLetter(lines[y + 2][x]) && IsCapitalLetter(lines[y + 1][x]))
                                label = new string(new [] { lines[y+1][x], lines[y+2][x] });
                            node.Label = label;
                            if ((node.Label == "AA" || node.Label == "ZZ"))
                            {
	                            if (level > 0)
		                            continue;
	                            else
	                            {
		                            node.Level = 0;
                                    nodes.Add(node);
                                    continue;
	                            }
                            }
                            if (isOuter && !string.IsNullOrEmpty(node.Label))
                                node.Level = level + 1;
                            else
                                node.Level = level;
                            nodes.Add(node);
                        }
                    }
                }
            }
            foreach (var node in nodes)
            {
                var p = nodes.Where(e=>
                    e.Level==node.Level &&
                (
                    (e.X==node.X && (e.Y==node.Y-1 || e.Y==node.Y+1)) ||
                    (e.Y==node.Y && (e.X==node.X-1 || e.X==node.X+1)) ||
                    (e.Label==node.Label && !string.IsNullOrEmpty(e.Label) && !e.Equals(node))
                ));
                node.Neighbors.AddRange(p);
            }
            return nodes;
        }

        public static int Bfs(List<Node2> nodes)
        {
            var q = new Queue<(Node2,Node2, int)>();
            var root = nodes.First(e => e.Label == "AA");
            q.Enqueue((root, null, 0));
            while (q.Count>0)
            {
                var (v, cameFrom, lvl) = q.Dequeue();
                lvl++;
                var forwardNeighbors = v.Neighbors.Where(e => !e.Equals(cameFrom)).ToList();
                foreach (var item in forwardNeighbors)
                {
                    q.Enqueue((item, v, lvl));
                    if (item.Label == "ZZ")
                        return lvl;
                }
            }
            return 100000000;
        }

        public static bool IsCapitalLetter(char ch)
        {
            if (ch < 'A')
                return false;
            if (ch > 'Z')
                return false;
            return true;
        }

        public static bool IsOuter(int x, int y, int width, int height)
        {
	        if (x < 3)
		        return true;
	        if (y < 3)
		        return true;
	        if (x > width - 3)
		        return true;
	        if (y > height - 3)
		        return true;
	        return false;
        }
    }
}
