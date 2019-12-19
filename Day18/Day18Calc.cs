using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Nito.Collections;

namespace Day18
{
    public class Day18Calc
    {
        public static int Width;
        public static int Height;
        public static Dictionary<string, int> Seen;

        /*
def reachablekeys(grid, start, havekeys):
    bfs = collections.deque([start])
    distance = {start: 0}
    keys = {}
    while bfs:
        h = bfs.popleft()
        for pt in [
            (h[0] + 1, h[1]),
            (h[0] - 1, h[1]),
            (h[0], h[1] + 1),
            (h[0], h[1] - 1),
        ]:
            if not (0 <= pt[0] < len(grid) and 0 <= pt[1] < len(grid[0])):
                continue
            ch = grid[pt[0]][pt[1]]
            if ch == '#':
                continue
            if pt in distance:
                continue
            distance[pt] = distance[h] + 1
            if 'A' <= ch <= 'Z' and ch.lower() not in havekeys:
                continue
            if 'a' <= ch <= 'z' and ch not in havekeys:
                keys[ch] = distance[pt], pt
            else:
                bfs.append(pt)
    return keys
*/

        public static Dictionary<char, (int, Point)> ReachableKeys(string[] grid, Point start, string haveKeys)
        {
	        var bfs = new Deque<Point>(new[] {start});
	        var distance = new Dictionary<Point, int> {{start, 0}};
            var keys=new Dictionary<char, (int,Point)>();
	        while (bfs.Count > 0)
	        {
		        var h = bfs.RemoveFromFront();
		        List<Point> points = new List<Point>
		        {
			        new Point {X = h.X + 1, Y = h.Y},
			        new Point {X = h.X - 1, Y = h.Y},
			        new Point {X = h.X, Y = h.Y-1},
			        new Point {X = h.X, Y = h.Y+1}
		        };
		        foreach (var point in points)
		        {
			        if (!(0<=point.X&&point.X<Height&&0<=point.Y&&point.Y<Width))
                        continue;
			        var ch = grid[point.X][point.Y];
                    if (ch=='#')
                        continue;
                    if (distance.ContainsKey(point))
                        continue;
                    distance.Add(point, distance[h]+1);
                    if ('A'<=ch && ch<='Z' && haveKeys.All(c => c != ch + 32))
                        continue;
                    if ('a' <= ch && ch <= 'z' && haveKeys.All(c => c != ch))
                        keys.Add(ch, (distance[point], point));
                    else
                    {
	                    bfs.AddToBack(point);
                    }

		        }

            }

	        return keys;
        }
      /*

def reachable4(grid, starts, havekeys):
    keys = {}
    for i, start in enumerate(starts):
        for ch, (dist, pt) in reachablekeys(grid, start, havekeys).items():
            keys[ch] = dist, pt, i
    return keys
*/
      public static Dictionary<char, (int, Point, int)> Reachable4(string[] grid, List<Point> starts, string haveKeys)
      {
	      var keys = new Dictionary<char, (int, Point, int)>();

          for (int i = 0; i < starts.Count; i++)
	      {
		      var reachableKeys = ReachableKeys(grid, starts[i], haveKeys);
		      foreach (var reachableKey in reachableKeys)
		      {
			      keys.Add(reachableKey.Key, (reachableKey.Value.Item1, reachableKey.Value.Item2, i));

              }

	      }

          return keys;
      }
        /*

  seen = {}
  def minwalk(grid, starts, havekeys):
      hks = ''.join(sorted(havekeys))
      if (starts, hks) in seen:
          return seen[starts, hks]
      if len(seen) % 10 == 0:
          print(hks)
      keys = reachable4(grid, starts, havekeys)
      if len(keys) == 0:
          # done!
          ans = 0
      else:
          poss = []
          for ch, (dist, pt, roi) in keys.items():
              nstarts = tuple(pt if i == roi else p for i, p in enumerate(starts))
              poss.append(dist + minwalk(grid, nstarts, havekeys + ch))
          ans = min(poss)
      seen[starts, hks] = ans
      return ans

           */
        public static int MinWalk(string[] grid, List<Point> starts, string haveKeys)
        {
	        var hks = Sorted(haveKeys);
	        var seenKey = StartKey(starts, hks);
	        if (Seen.ContainsKey(seenKey))
		        return Seen[seenKey];
            if (Seen.Count % 100 ==0)
                Console.WriteLine(hks);
            var keys = Reachable4(grid, starts, haveKeys);
            var ans = 0;
            if (keys.Count == 0)
	            ans = 0;//done
            else
            {
	            var poss = new List<int>();
	            foreach (var (ch, (dist, pt, roi)) in keys)
	            {
		            var nstarts = new List<Point>();
		            for (int i = 0; i < starts.Count; i++)
		            {
			            if (i==roi)
                            nstarts.Add(pt);
			            else
			            {
				            nstarts.Add(starts[i]);
			            }
		            }

		            poss.Add(dist + MinWalk(grid, nstarts, haveKeys + ch));
	            }

	            ans = poss.Min();
            }
            Seen.Add(seenKey, ans);
	        return ans;
        }
        public static void CalcMain()
        {
            Seen=new Dictionary<string, int>();
            var starts = new List<Point>();
            var grid = Day18Input.Day18Maze0.Split("\r\n");
            Width = grid[0].Length;
            Height = grid.Length;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (grid[i][j] == '@')
                        starts.Add(new Point(i, j));

                }

            }
            Console.WriteLine(MinWalk(grid, starts, ""));
            //print(minwalk(grid, tuple(starts), ''))
        }

        public static string Sorted(string input)
        {
	        return new string(input.ToCharArray().OrderBy(c => c).ToArray());

        }

        public static string StartKey(List<Point> starts, string hks)
        {
            StringBuilder b = new StringBuilder();
            foreach (var start in starts)
            {
	            b.Append($"{start.X:00}{start.Y:00}");
            }
            b.Append(hks);


            return b.ToString();
        }
    }
}
