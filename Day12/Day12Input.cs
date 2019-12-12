using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day12
{
	public class Day12Input
	{
		public static string Ex1 =
			@"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

		public static string Ex2 =
			@"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";
		public static string Test1 =
			@"<x=1, y=1, z=1>
<x=3, y=3, z=3>";

		public static string official = @"<x=15, y=-2, z=-6>
<x=-5, y=-4, z=-11>
<x=0, y=-6, z=0>
<x=5, y=9, z=6>";

		public static string StripBraces(string vectorInput)
		{
			return vectorInput.Trim('<', '>');
		}
		public static int StripName(string vectorInput)
		{
			return int.Parse(vectorInput.Split('=').Last());
		}

		public static IntMove ParseVector(string vectorInput)
		{
			var vectorArray=vectorInput.Split(',').Select(StripBraces).Select(StripName).ToArray();
			var a = new IntMove {X = vectorArray[0], Y = vectorArray[1], Z = vectorArray[2]};
			return a;
		}

		public static IntMove[] Satellites(string satelliteInput)
		{
			var lines = satelliteInput.Split("\r\n");
			return lines.Select(ParseVector).ToArray();
		}
	}
}
