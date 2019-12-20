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
        public int InLevel { get; set; }
        public int ToLevel { get; set; }
        public bool IsOuter { get; set; }
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
            while (level < 100 && mindist > level)
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

        public static List<Node2> ParseLevel(List<Node2> nodes, string input, int level)
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
                        if (x > 1 && y > 1)
                        {
                            var label = "";
                            if (IsCapitalLetter(lines[y][x - 2]) && IsCapitalLetter(lines[y][x - 1]))
                                label = new string(new[] { lines[y][x - 2], lines[y][x - 1] });
                            if (IsCapitalLetter(lines[y][x + 1]) && IsCapitalLetter(lines[y][x + 2]))
                                label = new string(new[] { lines[y][x + 1], lines[y][x + 2] });
                            if (IsCapitalLetter(lines[y - 2][x]) && IsCapitalLetter(lines[y - 1][x]))
                                label = new string(new[] { lines[y - 2][x], lines[y - 1][x] });
                            if (IsCapitalLetter(lines[y + 2][x]) && IsCapitalLetter(lines[y + 1][x]))
                                label = new string(new[] { lines[y + 1][x], lines[y + 2][x] });
                            node.Label = label;
                            if ((node.Label == "AA" || node.Label == "ZZ"))
                            {
                                if (level > 0)
                                    continue;
                                else
                                {
                                    node.InLevel = 0;
                                    node.ToLevel = 0;
                                    nodes.Add(node);
                                    continue;
                                }
                            }
                            node.InLevel = level;
                            node.ToLevel = level;
                            if (!string.IsNullOrEmpty(node.Label))
                            {
                                if (isOuter)
                                    node.ToLevel = level - 1;
                                else
                                    node.ToLevel = level + 1;
                            }
                            node.IsOuter = isOuter;
                            nodes.Add(node);
                        }
                    }
                }
            }
            foreach (var node in nodes)
            {
                node.Neighbors = new List<Node2>();
                foreach (var candidate in nodes)
                {
                    if (candidate.Equals(node))
                        continue;
                    var wantThis = false;
                    if (!string.IsNullOrEmpty(candidate.Label) && !string.IsNullOrEmpty(node.Label))
                    {
                        if (candidate.Label == node.Label && candidate.IsOuter != node.IsOuter)
                        {
                            if (candidate.InLevel == node.ToLevel || candidate.ToLevel == node.InLevel)
                                wantThis = true;
                        }
                    }
                    if (candidate.InLevel == node.InLevel && candidate.X == node.X && (candidate.Y == node.Y - 1 || candidate.Y == node.Y + 1))
                        wantThis = true;
                    if (candidate.InLevel == node.InLevel && candidate.Y == node.Y && (candidate.X == node.X - 1 || candidate.Y == node.Y + 1))
                        wantThis = true;
                    if (wantThis && !node.Neighbors.Contains(candidate))
                        node.Neighbors.Add(candidate);

                }
                /*
    var p = nodes.Where(e=>
        e.Level==node.Level &&
    (
        (e.X==node.X && (e.Y==node.Y-1 || e.Y==node.Y+1)) ||
        (e.Y==node.Y && (e.X==node.X-1 || e.X==node.X+1)) ||
        (e.Label==node.Label && !string.IsNullOrEmpty(e.Label) && !e.Equals(node))
    ));
    node.Neighbors=p.ToList();*/
            }
            return nodes;
        }

        class QueueItem
        {
            public Dictionary<Node2, int> Path { get; set; }
            public Node2 CurrentNode { get; set; }
            public Node2 CameFrom { get; set; }
            public int dist { get; set; }
        }

        public static int Bfs(List<Node2> nodes)
        {
            var q = new Queue<QueueItem>();
            var root = nodes.First(e => e.Label == "AA");
            var seen = new Dictionary<Node2, int>();
            q.Enqueue(
                new QueueItem
                {
                    CurrentNode = root,
                    CameFrom = null,
                    dist = 0,
                    Path = new Dictionary<Node2, int>(new[] { new KeyValuePair<Node2, int>(root, 0) })
                }

                );
            seen.Add(root, 0);
            while (q.Count > 0)
            {
                var v = q.Dequeue();
                var lvl = v.dist + 1;
                var forwardNeighbors = v.CurrentNode.Neighbors;
                foreach (var item in forwardNeighbors)
                {
                    if (item.Label == "ZZ")
                        return lvl;
                    if (v.Path.ContainsKey(item))
                        continue;
                    if (seen.ContainsKey(item))
                        continue;
                    seen.Add(item, lvl);
                    var nextPath = new Dictionary<Node2, int>(v.Path);
                    nextPath.Add(item, v.dist + 1);
                    var nextItem = new QueueItem
                    {
                        CameFrom = v.CurrentNode,
                        CurrentNode = item,
                        Path = nextPath,
                        dist = v.dist + 1
                    };
                    q.Enqueue(nextItem);
                    ;
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
