using System;

namespace Day10
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Calc(Day10Input.Ex1);
		}

		static bool Blocks(int x1i, int y1i, int x2i, int y2i, int xb, int yb, string[] lines)
		{
			//is 2 blocking 1? 
			if ((x2i - x1i == xb - x2i) && (y2i - y1i == yb - y2i))
			{
				if (lines[y2i][x2i] == '#')
					return true;
			}

			return false;
		}

		static bool Visible(int x1i, int y1i, int xb, int yb, string[] lines)
		{
			for (int y2 = 0; y2 < lines.Length; y2++)
			{
				for (int x2 = 0; x2 < lines[y2].Length; x2++)
				{
					if (Blocks(x1i, y1i, x2, y2, xb, yb, lines))
						return false;
				}
			}

			return true;

		}

		static void Calc(string[] lines)
		{
			var maxVisible = -1;
			for (int yb = 0; yb < lines.Length; yb++)
			{
				for (int xb = 0; xb < lines[yb].Length; xb++)
				{
					var currentVisible = 0;
					for (int y2 = 0; y2 < lines.Length; y2++)
					{
						for (int x2 = 0; x2 < lines[y2].Length; x2++)
						{
							if (Visible(x2, y2, xb, yb, lines))
								currentVisible++;
						}
					}

					if (currentVisible > maxVisible)
					{
						maxVisible = currentVisible;
						Console.WriteLine($"{xb}, {yb}, {maxVisible}");
					}

				}
			}
		}
	}
}
