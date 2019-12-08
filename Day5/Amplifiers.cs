using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day5
{
	public class Amplifiers
	{
		public static int Calc5AmpsPhase1(int[] memory, int[] phases, Action<string, int, int> outerOutputFunc)
		{
			var OutputList = new List<int>();

			void OutputFunc(string msg, int pos, int outp)
			{
				OutputList.Add(outp);
				outerOutputFunc(msg, pos, outp);
			}

			var mem = memory.ToArray();
			var inputs = new int[2]
			{
				phases[0],//phase
				0//signal
			};
			//Machine.Calc(0, mem, inputs, OutputFunc);//A
			var resA=Machine.RunToInput(0, mem, phases[0], OutputFunc);//"init"
			resA=Machine.RunToInput(resA.ProgramCounter, mem, 0, OutputFunc);//input signal
			resA = Machine.RunToOutput(resA.ProgramCounter, mem, OutputFunc);
			inputs[0] = phases[1];
			inputs[1] = resA.Result;//was OutputList.Last();
			OutputList = new List<int>();
			mem = memory.ToArray();
			Machine.Calc(0, mem, inputs, OutputFunc);//B
			inputs[0] = phases[2];
			inputs[1] = OutputList.Last();
			OutputList = new List<int>();
			mem = memory.ToArray();
			Machine.Calc(0, mem, inputs, OutputFunc);//C
			inputs[0] = phases[3];
			inputs[1] = OutputList.Last();
			OutputList = new List<int>();
			mem = memory.ToArray();
			Machine.Calc(0, mem, inputs, OutputFunc);//D
			inputs[0] = phases[4];
			inputs[1] = OutputList.Last();
			OutputList = new List<int>();
			mem = memory.ToArray();
			Machine.Calc(0, mem, inputs, OutputFunc);//E
			return OutputList.Last();
		}

		public static int calcMax(int[] memory, Action<string, int, int> outerOutputFunc)
		{
			var res = 0;
			var phases = new int[5];
			for (int a = 0; a < 5; a++)
			{
				for (int b = 0; b < 5; b++)
				{
					if (a==b)
						continue;
					for (int c = 0; c < 5; c++)
					{
						if (c==a||c==b)
							continue;
						for (int d = 0; d < 5; d++)
						{
							if (d==a||d==b||d==c)
								continue;
							for (int e = 0; e < 5; e++)
							{
								if (e==a||e==b||e==c||e==d)
									continue;
								phases[0] = a;
								phases[1] = b;
								phases[2] = c;
								phases[3] = d;
								phases[4] = e;
								var signal = Calc5AmpsPhase1(memory, phases, outerOutputFunc);
								if (signal > res)
									res = signal;
							}
						}
					}

				}
			}

			return res;
		}

		public static int Calc5AmpsPhase2(int[] memory, int[] phases, Action<string, int, int> outerOutputFunc)
		{
			var OutputList = new List<int>();

			void OutputFunc(string msg, int pos, int outp)
			{
				OutputList.Add(outp);
				outerOutputFunc(msg, pos, outp);
			}

			var memA = memory.ToArray();
			var resA = Machine.RunToInput(0, memA, phases[0], OutputFunc);//"init"
			var memB = memory.ToArray();
			var resB = Machine.RunToInput(0, memB, phases[1], OutputFunc);//"init"
			var memC = memory.ToArray();
			var resC = Machine.RunToInput(0, memC, phases[2], OutputFunc);//"init"
			var memD = memory.ToArray();
			var resD = Machine.RunToInput(0, memD, phases[3], OutputFunc);//"init"
			var memE = memory.ToArray();
			var resE = Machine.RunToInput(0, memE, phases[4], OutputFunc);//"init"

			var inputA = 0;
			while (true)
			{
				resA = Machine.RunToInput(resA.ProgramCounter, memA, inputA, OutputFunc);//input signal
				resA = Machine.RunToOutput(resA.ProgramCounter, memA, OutputFunc);
				resB = Machine.RunToInput(resB.ProgramCounter, memB, resA.Result, OutputFunc);//input signal
				resB = Machine.RunToOutput(resB.ProgramCounter, memB, OutputFunc);
				resC = Machine.RunToInput(resC.ProgramCounter, memC, resB.Result, OutputFunc);//input signal
				resC = Machine.RunToOutput(resC.ProgramCounter, memC, OutputFunc);
				resD = Machine.RunToInput(resD.ProgramCounter, memD, resC.Result, OutputFunc);//input signal
				resD = Machine.RunToOutput(resD.ProgramCounter, memD, OutputFunc);
				resE = Machine.RunToInput(resE.ProgramCounter, memE, resD.Result, OutputFunc);//input signal
				resE = Machine.RunToOutput(resE.ProgramCounter, memE, OutputFunc);
				if (resE.RanToHalt)
					return inputA;
				inputA = resE.Result;
			}
		}

		public static int calcMaxPhase2(int[] memory, Action<string, int, int> outerOutputFunc)
		{
			var res = 0;
			var phases = new int[5];
			for (int a = 5; a < 10; a++)
			{
				for (int b = 5; b < 10; b++)
				{
					if (a == b)
						continue;
					for (int c = 5; c < 10; c++)
					{
						if (c == a || c == b)
							continue;
						for (int d = 5; d < 10; d++)
						{
							if (d == a || d == b || d == c)
								continue;
							for (int e = 5; e < 10; e++)
							{
								if (e == a || e == b || e == c || e == d)
									continue;
								phases[0] = a;
								phases[1] = b;
								phases[2] = c;
								phases[3] = d;
								phases[4] = e;
								var signal = Calc5AmpsPhase2(memory, phases, outerOutputFunc);
								if (signal > res)
									res = signal;
							}
						}
					}

				}
			}

			return res;
		}

	}
}
