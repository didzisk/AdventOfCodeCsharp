using System;

namespace Day9
{
	class Program
	{
		static void OutputConsole(string text, long position, long output)
		{
			Console.WriteLine(String.Format(text, position, output));
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			long[] largerExample2 =
			{
				3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
				1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
				999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
			};
			Console.WriteLine("asking for 8, input 9, expect 1001:");
			Machine.Calc(0, largerExample2, new long[] { 9 }, OutputConsole);

			long[] day9ex1 = {104, 1125899906842624, 99};
			Console.WriteLine("D9 asking for output, expect 1125899906842624:");
			Machine.RunToOutput(0, day9ex1, OutputConsole);

			long[] day9ex2 = {1102, 34915192, 34915192, 7, 4, 7, 99, 0};
			Console.WriteLine("D9Ex2, expect long:");
			Machine.RunToOutput(0, day9ex2, OutputConsole);


		}
	}
}
