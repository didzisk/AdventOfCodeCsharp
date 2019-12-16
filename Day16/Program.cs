using System;
using System.Text;

namespace Day16
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.WriteLine();
			var s = CalcNextPhase("12345678");
			Console.WriteLine(s);
			s = CalcNextPhase(s);
			Console.WriteLine(s);
			s = CalcNextPhase(s);
			Console.WriteLine(s);
			s = CalcNextPhase(s);
			Console.WriteLine(s);

			calcPart1(Day16Input.Ex2);
			calcPart1(Day16Input.Ex3);
			calcPart1(Day16Input.Official);
		}

		public static string calcPart1(string input)
		{
			var s = input;
			for (int i = 0; i < 100; i++)
			{
				s = CalcNextPhase(s);
			}
			Console.WriteLine(s.Substring(0,8));
			return s.Substring(0, 8);

		}

		public static string CalcNextPhase(string input)
		{
			var pattern = new int[]{0, 1, 0, -1};
			var shift = 0;
			var outputBuilder = new StringBuilder(input.Length);
			foreach (var outp in input)
			{
				var sum = 0;
				for (int i = shift; i < input.Length; i++)
				{
					var ip = ( i + 1) / (shift + 1) % 4;
					var m = input[i]-48;
					var p = pattern[ip];
					//Console.Write($"{m}*{p}={m*p};");
					sum += m * p;
				}

				char theChar = (char)(Math.Abs(sum) % 10 + 48);
				outputBuilder.Append(theChar);
				shift++;
			}

			return outputBuilder.ToString();
		}
	}
}
