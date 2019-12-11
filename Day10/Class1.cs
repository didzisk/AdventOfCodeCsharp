using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
	class Class1
	{
		public static void RunMain()
		{
			var lines = Day10Input.InputStrings;

			var asteroids = new List<Tuple<int, int>>();
			for (var y = 0; y < lines.Length; y++)
			{
				var line = lines[y];
				for (var x = 0; x < line.Length; x++)
				{
					if (line[x] == '#')
					{
						asteroids.Add(Tuple.Create(x, y));
					}
				}
			}

			int bestCanSee = 0;
			var bestBase = Tuple.Create(-1, -1);
			IGrouping<double, Tuple<int, int>>[] bestGroups = null;
			foreach (var asteroid in asteroids)
			{
				var groups = asteroids.Except(new[] { asteroid })
					.Select(a => Tuple.Create(a.Item1 - asteroid.Item1, a.Item2 - asteroid.Item2))
					.GroupBy(a => Math.Atan2(-a.Item2, a.Item1)) // y coordinate is reversed
					.ToArray();
				var canSee = groups.Count();

				if (canSee > bestCanSee)
				{
					bestBase = asteroid;
					bestCanSee = canSee;
					bestGroups = groups;
				}
			}

			//Output.Part1 = bestCanSee;

			var removals = bestGroups
				.Select(g => new { Angle = g.Key, Targets = new Queue<Tuple<int, int>>(g.OrderBy(a => Math.Sqrt(a.Item1 * a.Item1 + a.Item2 * a.Item2))) })
				// could just be Abs of Item1...
				.OrderBy(g => g.Angle > Math.PI / 2) // start at straight up
				.ThenByDescending(g => g.Angle)      // then work anticlockise
				.ToArray();

			var removed = 0;
			while (true)
			{
				for (var i = 0; i < removals.Length; i++)
				{
					if (removals[i].Targets.Count > 0)
					{
						var toRemove = removals[i].Targets.Dequeue();
						removed++;
						if (removed == 200)
						{
							var y = toRemove.Item2 + bestBase.Item2;
							var x = toRemove.Item1 + bestBase.Item1;

							Console.WriteLine($"Part2 = {x * 100 + y}");

							Console.ReadLine();
							return;
						}
					}
				}
			}
		}
	}
}