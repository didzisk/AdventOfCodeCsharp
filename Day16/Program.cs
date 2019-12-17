using System;
using System.Linq;
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

			calcPart2();
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

		public static string calcPart2()
		{
			var offset = 5973847;

			var originalInput = Day16Input.Official;
			var inputLength = originalInput.Length * 10000;
			var remainingLength = inputLength - offset;
			var builder=new StringBuilder(inputLength);
			for (int si = 0; si < 10000; si++)
			{
				builder.Append(Day16Input.Official);
			}

			var ch = new char[remainingLength];
			builder.CopyTo(offset, ch, 0, remainingLength);
			for (int i = 0; i < 100; i++)
			{
				var partialSum = ch.Sum(x => (int)(x - 48));
				for (int j = 0; j < remainingLength; j++)
				{
					var t = partialSum;
					partialSum -= (int)ch[j]-48;
					if (t >= 0)
					{
						ch[j] = (char)(t % 10 + 48);
					}
					else
					{
						ch[j] = (char)((-t) % 10 + 48);
					}
				}
			}
			var st = new string(ch.Take(8).ToArray());
			Console.WriteLine(st);

			return st;

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
