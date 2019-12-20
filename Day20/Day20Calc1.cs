using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    public class Node1
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }

        public List<Node1> Neighbors { get; set; }
        public Node1(int x, int y)
        {
            X = x;
            Y = y;
            Neighbors = new List<Node1>();
        }
    }
    public static class Day20Calc1
    {
        public static void CalcPart1(string input)
        {
            var nodes = ParseLevel(input);
            var startNode = nodes.First(e => e.Label == "AA");
            var dist = Bfs(nodes);
            Console.WriteLine($"dist:{dist}");
        }
        
        public static List<Node1> ParseLevel(string input)
        {
            var nodes = new List<Node1>();
            var lines = input.Split("\r\n");
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '.')
                    {
                        var node = new Node1(x, y);
                        if (x>1 && y>1)
                        {
                            var label = "";
                            if (IsCapitalLetter(lines[y][x - 2]) && IsCapitalLetter(lines[y][x - 1]))
                                label = new string(new char[] { lines[y][x - 2], lines[y][x - 1] });
                            if (IsCapitalLetter(lines[y][x + 1]) && IsCapitalLetter(lines[y][x + 2]))
                                label = new string(new char[] { lines[y][x + 1], lines[y][x + 2] });
                            if (IsCapitalLetter(lines[y - 2][x]) && IsCapitalLetter(lines[y - 1][x]))
                                label = new string(new char[] { lines[y - 2][x], lines[y - 1][x] });
                            if (IsCapitalLetter(lines[y + 2][x]) && IsCapitalLetter(lines[y + 1][x]))
                                label = new string(new char[] { lines[y+1][x], lines[y+2][x] });
                            node.Label = label;
                            nodes.Add(node);
                        }
                    }
                }
            }
            foreach (var node in nodes)
            {
                var p = nodes.Where(e=>
                (
                    (e.X==node.X && (e.Y==node.Y-1 || e.Y==node.Y+1)) ||
                    (e.Y==node.Y && (e.X==node.X-1 || e.X==node.X+1)) ||
                    (e.Label==node.Label && !string.IsNullOrEmpty(e.Label) && !e.Equals(node))
                ));
                node.Neighbors.AddRange(p);
            }
            return nodes;
        }

        public static int Bfs(List<Node1> nodes)
        {
            var q = new Queue<(Node1,Node1, int)>();
            var root = nodes.First(e => e.Label == "AA");
            q.Enqueue((root, null, 0));
            while (q.Count>0)
            {
                var (v, cameFrom, dist) = q.Dequeue();
                dist++;
                var forwardNeighbors = v.Neighbors.Where(e => !e.Equals(cameFrom)).ToList();
                foreach (var item in forwardNeighbors)
                {
                    q.Enqueue((item, v, dist));
                    if (item.Label == "ZZ")
                        return dist;
                }
            }
            return 0;
        }

        public static bool IsCapitalLetter(char ch)
        {
            if (ch < 'A')
                return false;
            if (ch > 'Z')
                return false;
            return true;
        }
    }
}
