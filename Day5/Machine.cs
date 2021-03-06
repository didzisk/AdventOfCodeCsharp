﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
	public enum CalcMode
	{
		Default = 0,
		RunToFirstInput = 1,
		RunToFirstOutput = 2
	}

	public struct MachineStatus
	{
		public int ProgramCounter;
		public int Result;
		public bool RanToHalt;
	}

	public class Machine
	{
		public static int ArgMode(int code, int mask)
		{
			return (code % (mask * 10) - (code % mask)) / mask;
		}
		public static int CalcWithInput(int verb, int noun, int[] memory, Action<string, int, int> outputFunc)
		{

			memory[1] = verb;
			memory[2] = noun;
			return Calc(0, memory, new int[0], outputFunc).Result;
		}

		public static MachineStatus RunToInput(int pc, int[] memory, int singleInput, Action<string, int, int> outputFunc)
		{
			return Calc(pc, memory, new[] { singleInput }, outputFunc, CalcMode.RunToFirstInput);
		}
		public static MachineStatus RunToOutput(int pc, int[] memory, Action<string, int, int> outputFunc)
		{
			return Calc(pc, memory, new int[0], outputFunc, CalcMode.RunToFirstOutput);
		}

		public static MachineStatus Calc(int pc, int[] memory, int[] Inputs, Action<string, int, int> outputFunc, CalcMode calcMode=default)
		{
			int inputCounter = 0;
			while (pc < memory.Length)
			{
				var opcode = memory[pc] % 100;
				var arg1Mode = ArgMode(memory[pc], 100);
				var arg2Mode = ArgMode(memory[pc], 1000);
				var arg3Mode = ArgMode(memory[pc], 10000);
				int getArg1()
				{
					switch (opcode)
					{
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
						case 8:
							var addr = memory[pc + 1];
							int arg = addr;
							if (arg1Mode == 0)
								arg = memory[addr];
							return arg;
					}

					return 0;
				}
				int getArg2()
				{
					switch (opcode)
					{
						case 1:
						case 2:
						case 5:
						case 6:
						case 7:
						case 8:
							var addr = memory[pc + 2];
							int arg = addr;
							if (arg2Mode == 0)
								arg = memory[addr];
							return arg;
					}

					return 0;

				}
				int getArg3()
				{
					switch (opcode)
					{
						case 7:
						case 8:
							var addr = memory[pc + 3];
							int arg = addr;
							return arg;
					}

					return 0;
				}
				var arg1 = getArg1();
				var arg2 = getArg2();
				var arg3 = getArg3();
				switch (opcode)
				{
					case 1:
						var addrt = memory[pc + 3];
						memory[addrt] = arg1 + arg2;
						pc = pc + 4;
						break;
					case 2:
						addrt = memory[pc + 3];
						memory[addrt] = arg1 * arg2;
						pc = pc + 4;
						break;
					case 3:
						var addr31 = memory[pc + 1];
						memory[addr31] = Inputs[inputCounter++];
						pc = pc + 2;
						if (calcMode == CalcMode.RunToFirstInput)
							return new MachineStatus { Result = -240174, ProgramCounter = pc, RanToHalt = false};
						break;
					case 4:
						arg1 = getArg1();
						outputFunc("Output {0} {1}", pc, arg1);
						pc = pc + 2;
						if (calcMode == CalcMode.RunToFirstOutput)
							return new MachineStatus { Result = arg1, ProgramCounter = pc, RanToHalt = false };
						break;
					case 5: //JNZ
						if (arg1 != 0)
							pc = arg2;
						else
							pc = pc + 3;
						break;
					case 6: //JZ
						if (arg1 == 0)
							pc = arg2;
						else
							pc = pc + 3;
						break;
					case 7: //LT
						if (arg1 < arg2)
							memory[arg3] = 1;
						else
							memory[arg3] = 0;
						pc = pc + 4;
						break;
					case 8: //EQ
						if (arg1 == arg2)
							memory[arg3] = 1;
						else
							memory[arg3] = 0;
						pc = pc + 4;
						break;
					case 99:
						return new MachineStatus { Result = memory[0], ProgramCounter = pc, RanToHalt = true };
				}
			}
			return new MachineStatus { Result = -230771, ProgramCounter = pc, RanToHalt = false };
		}


	}
}
