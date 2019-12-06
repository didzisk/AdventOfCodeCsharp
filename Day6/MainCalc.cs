using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day6
{
	public class MyOrbitData
	{
		public String Code;
		public int NumParents;
		public bool HasSan;
		public bool HasYou;
		public MyOrbitData ParentNode;
	}

	public class MainCalc
	{
		public static string ChildCode(string fullCode)
		{
			var a = fullCode.Split(')');
			return a.Last();
		}
		public static string ParentCode(string fullCode)
		{
			var a = fullCode.Split(')');
			return a.First();
		}

		public static int CalcWithRootCode(MyOrbitData parentNode, int ParentCount, List<MyOrbitData> Day6Output)
		{
			foreach (string code in Day6Input.Text.Where(s => ParentCode(s) == parentNode.Code).Select(ChildCode))
			{
				var hasSan = code == "SAN";
				var hasYou = code == "YOU";
				MyOrbitData NewData = new MyOrbitData
				{
					Code = code,
					NumParents = ParentCount,
					HasSan = code == "SAN",
					HasYou = code == "YOU",
					ParentNode = parentNode
				};

				Day6Output.Add(NewData);
				CalcWithRootCode(NewData, ParentCount + 1, Day6Output);
			}
			return ParentCount + 1;
		}

		public static void GoBack(MyOrbitData node)
		{
			var parentNode = node.ParentNode;
			if (parentNode != null)
			{
				if (node.HasSan)
					parentNode.HasSan = true;
				if (node.HasYou)
					parentNode.HasYou = true;
				GoBack(parentNode);
			}
		}

		public static int Calc()
		{
			List<MyOrbitData> Day6Output = new List<MyOrbitData>();
			MyOrbitData RootData = new MyOrbitData
			{
				Code = "COM",
				NumParents = 0,
				HasSan = false,
				HasYou = false,
				ParentNode = null
			};
			CalcWithRootCode(RootData, 1, Day6Output);
			var sum = Day6Output.Sum(x => x.NumParents);

			foreach (MyOrbitData youNode in Day6Output.Where(x => x.HasYou))
				GoBack(youNode);
			foreach (MyOrbitData sanNode in Day6Output.Where(x => x.HasSan))
				GoBack(sanNode);

			var listHasBoth = Day6Output.Where(x => x.HasYou && x.HasSan).ToList();
			var numHasBoth = Day6Output.Where(x => x.HasYou && x.HasSan).Count();
			var numHasSan = Day6Output.Where(x => x.HasSan).Count();
			var numHasYou = Day6Output.Where(x => x.HasYou).Count();
			var numTransfers = numHasYou + numHasSan - 2 - 2 * numHasBoth;
			return sum;
		}
	}
}
