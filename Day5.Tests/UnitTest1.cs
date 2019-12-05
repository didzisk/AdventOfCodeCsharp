using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Day5.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		private List<int> OutputList;
		void CollectOutput(string text, int position, int output)
		{
			Console.WriteLine(text, position, output);
			OutputList.Add(output);
		}

		[Test]
		public void Day2Test1()
		{
			int[] memory1 =
			{
				1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 6, 19, 23, 2, 6, 23, 27, 1, 5, 27, 31,
				2, 31, 9, 35, 1, 35, 5, 39, 1, 39, 5, 43, 1, 43, 10, 47, 2, 6, 47, 51, 1, 51, 5, 55, 2, 55, 6, 59, 1, 5,
				59, 63, 2, 63, 6, 67, 1, 5, 67, 71, 1, 71, 6, 75, 2, 75, 10, 79, 1, 79, 5, 83, 2, 83, 6, 87, 1, 87, 5,
				91, 2, 9, 91, 95, 1, 95, 6, 99, 2, 9, 99, 103, 2, 9, 103, 107, 1, 5, 107, 111, 1, 111, 5, 115, 1, 115,
				13, 119, 1, 13, 119, 123, 2, 6, 123, 127, 1, 5, 127, 131, 1, 9, 131, 135, 1, 135, 9, 139, 2, 139, 6,
				143, 1, 143, 5, 147, 2, 147, 6, 151, 1, 5, 151, 155, 2, 6, 155, 159, 1, 159, 2, 163, 1, 9, 163, 0, 99,
				2, 0, 14, 0
			};
			OutputList = new List<int>();
			var res = Machine.CalcWithInput(12, 2, memory1, CollectOutput);

			Assert.That(res.Equals(10566835));
		}

		[Test]
		public void Day2Test2()
		{
			var result = 0;
			for (int i = 0; i < 100; i++)
				for (int j = 0; j < 100; j++)
				{
					int[] memory2 =
					{
					1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 6, 19, 23, 2, 6, 23, 27, 1, 5, 27,
					31, 2, 31, 9, 35, 1, 35, 5, 39, 1, 39, 5, 43, 1, 43, 10, 47, 2, 6, 47, 51, 1, 51, 5, 55, 2, 55, 6,
					59, 1, 5, 59, 63, 2, 63, 6, 67, 1, 5, 67, 71, 1, 71, 6, 75, 2, 75, 10, 79, 1, 79, 5, 83, 2, 83, 6,
					87, 1, 87, 5, 91, 2, 9, 91, 95, 1, 95, 6, 99, 2, 9, 99, 103, 2, 9, 103, 107, 1, 5, 107, 111, 1, 111,
					5, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 6, 123, 127, 1, 5, 127, 131, 1, 9, 131, 135, 1, 135, 9,
					139, 2, 139, 6, 143, 1, 143, 5, 147, 2, 147, 6, 151, 1, 5, 151, 155, 2, 6, 155, 159, 1, 159, 2, 163,
					1, 9, 163, 0, 99, 2, 0, 14, 0
				};
					OutputList = new List<int>();
					var outp = Machine.CalcWithInput(i, j, memory2, CollectOutput);
					if (outp == 19690720)
					{
						result = i * 100 + j;
						Console.WriteLine($"result {i * 100 + j}"); //2347
						break;
					}
				}
			Assert.That(result, Is.EqualTo(2347));

		}

		[Test]
		public void Day5Test1()
		{
			int[] memory3 =
			{
				3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 2, 106, 196, 224, 101, -1157, 224, 224, 4, 224, 102, 8,
				223, 223, 1001, 224, 7, 224, 1, 224, 223, 223, 1002, 144, 30, 224, 1001, 224, -1710, 224, 4, 224, 1002,
				223, 8, 223, 101, 1, 224, 224, 1, 224, 223, 223, 101, 82, 109, 224, 1001, 224, -111, 224, 4, 224, 102,
				8, 223, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 1102, 10, 50, 225, 1102, 48, 24, 224, 1001, 224,
				-1152, 224, 4, 224, 1002, 223, 8, 223, 101, 5, 224, 224, 1, 223, 224, 223, 1102, 44, 89, 225, 1101, 29,
				74, 225, 1101, 13, 59, 225, 1101, 49, 60, 225, 1101, 89, 71, 224, 1001, 224, -160, 224, 4, 224, 1002,
				223, 8, 223, 1001, 224, 6, 224, 1, 223, 224, 223, 1101, 27, 57, 225, 102, 23, 114, 224, 1001, 224,
				-1357, 224, 4, 224, 102, 8, 223, 223, 101, 5, 224, 224, 1, 224, 223, 223, 1001, 192, 49, 224, 1001, 224,
				-121, 224, 4, 224, 1002, 223, 8, 223, 101, 3, 224, 224, 1, 223, 224, 223, 1102, 81, 72, 225, 1102, 12,
				13, 225, 1, 80, 118, 224, 1001, 224, -110, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 224, 223,
				223, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1,
				99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999,
				1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101,
				294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0,
				106, 0, 0, 1105, 1, 99999, 7, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 329, 101, 1, 223, 223, 108,
				226, 226, 224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 1108, 226, 677, 224, 102, 2, 223,
				223, 1006, 224, 359, 1001, 223, 1, 223, 107, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 374, 1001,
				223, 1, 223, 1107, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 389, 1001, 223, 1, 223, 107, 677, 226,
				224, 1002, 223, 2, 223, 1005, 224, 404, 101, 1, 223, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224,
				419, 101, 1, 223, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 434, 101, 1, 223, 223, 1007, 677,
				677, 224, 102, 2, 223, 223, 1006, 224, 449, 1001, 223, 1, 223, 107, 226, 226, 224, 1002, 223, 2, 223,
				1006, 224, 464, 1001, 223, 1, 223, 1007, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 479, 1001, 223, 1,
				223, 1008, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 494, 101, 1, 223, 223, 7, 677, 677, 224, 102, 2,
				223, 223, 1005, 224, 509, 1001, 223, 1, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 524, 101,
				1, 223, 223, 1108, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 539, 101, 1, 223, 223, 1108, 677, 677,
				224, 102, 2, 223, 223, 1005, 224, 554, 101, 1, 223, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1005, 224,
				569, 101, 1, 223, 223, 8, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 584, 101, 1, 223, 223, 1107, 226,
				226, 224, 102, 2, 223, 223, 1006, 224, 599, 101, 1, 223, 223, 108, 677, 677, 224, 102, 2, 223, 223,
				1006, 224, 614, 101, 1, 223, 223, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 629, 1001, 223, 1,
				223, 1107, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 1008, 677, 677, 224, 1002,
				223, 2, 223, 1005, 224, 659, 101, 1, 223, 223, 1007, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 674,
				1001, 223, 1, 223, 4, 223, 99, 226
			};
			OutputList = new List<int>();
			Machine.Calc(memory3, 1, CollectOutput);
			var res = OutputList.Last();
			Assert.That(res, Is.EqualTo(15097178));

		}

		[Test]
		public void Day5Test2()
		{
			Console.WriteLine("running the whole, input 5");
			int[] memory3 =
			{
				3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 2, 106, 196, 224, 101, -1157, 224, 224, 4, 224, 102, 8,
				223, 223, 1001, 224, 7, 224, 1, 224, 223, 223, 1002, 144, 30, 224, 1001, 224, -1710, 224, 4, 224, 1002,
				223, 8, 223, 101, 1, 224, 224, 1, 224, 223, 223, 101, 82, 109, 224, 1001, 224, -111, 224, 4, 224, 102,
				8, 223, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 1102, 10, 50, 225, 1102, 48, 24, 224, 1001, 224,
				-1152, 224, 4, 224, 1002, 223, 8, 223, 101, 5, 224, 224, 1, 223, 224, 223, 1102, 44, 89, 225, 1101, 29,
				74, 225, 1101, 13, 59, 225, 1101, 49, 60, 225, 1101, 89, 71, 224, 1001, 224, -160, 224, 4, 224, 1002,
				223, 8, 223, 1001, 224, 6, 224, 1, 223, 224, 223, 1101, 27, 57, 225, 102, 23, 114, 224, 1001, 224,
				-1357, 224, 4, 224, 102, 8, 223, 223, 101, 5, 224, 224, 1, 224, 223, 223, 1001, 192, 49, 224, 1001, 224,
				-121, 224, 4, 224, 1002, 223, 8, 223, 101, 3, 224, 224, 1, 223, 224, 223, 1102, 81, 72, 225, 1102, 12,
				13, 225, 1, 80, 118, 224, 1001, 224, -110, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 224, 223,
				223, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1,
				99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999,
				1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101,
				294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0,
				106, 0, 0, 1105, 1, 99999, 7, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 329, 101, 1, 223, 223, 108,
				226, 226, 224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 1108, 226, 677, 224, 102, 2, 223,
				223, 1006, 224, 359, 1001, 223, 1, 223, 107, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 374, 1001,
				223, 1, 223, 1107, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 389, 1001, 223, 1, 223, 107, 677, 226,
				224, 1002, 223, 2, 223, 1005, 224, 404, 101, 1, 223, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224,
				419, 101, 1, 223, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 434, 101, 1, 223, 223, 1007, 677,
				677, 224, 102, 2, 223, 223, 1006, 224, 449, 1001, 223, 1, 223, 107, 226, 226, 224, 1002, 223, 2, 223,
				1006, 224, 464, 1001, 223, 1, 223, 1007, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 479, 1001, 223, 1,
				223, 1008, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 494, 101, 1, 223, 223, 7, 677, 677, 224, 102, 2,
				223, 223, 1005, 224, 509, 1001, 223, 1, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 524, 101,
				1, 223, 223, 1108, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 539, 101, 1, 223, 223, 1108, 677, 677,
				224, 102, 2, 223, 223, 1005, 224, 554, 101, 1, 223, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1005, 224,
				569, 101, 1, 223, 223, 8, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 584, 101, 1, 223, 223, 1107, 226,
				226, 224, 102, 2, 223, 223, 1006, 224, 599, 101, 1, 223, 223, 108, 677, 677, 224, 102, 2, 223, 223,
				1006, 224, 614, 101, 1, 223, 223, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 629, 1001, 223, 1,
				223, 1107, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 1008, 677, 677, 224, 1002,
				223, 2, 223, 1005, 224, 659, 101, 1, 223, 223, 1007, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 674,
				1001, 223, 1, 223, 4, 223, 99, 226
			};

			OutputList = new List<int>();
			Machine.Calc(memory3, 5, CollectOutput);
			var res = OutputList.Last();
			Assert.That(res, Is.EqualTo(1558663));

		}
	}
}