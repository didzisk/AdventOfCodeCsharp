﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Day21
{
	public enum CalcMode
	{
		Default = 0,
		RunToFirstInput = 1,
		RunToFirstOutput = 2
	}

	public struct MachineStatus
	{
		public long ProgramCounter;
		public long RelativeBase;
		public long Result;
		public bool RanToHalt;
	}

	public class Machine
	{
		public static long ArgMode(long code, long mask)
		{
			return (code % (mask * 10) - (code % mask)) / mask;
		}
		public static long CalcWithInput(int verb, int noun, long[] memory, Action<string, long, long> outputFunc)
		{

			memory[1] = verb;
			memory[2] = noun;
			var st = new MachineStatus{ProgramCounter = 0, RelativeBase = 0};
			return Calc(st, memory, new long[0], outputFunc).Result;
		}

		public static MachineStatus RunToInput(MachineStatus st, long[] memory, int singleInput)
		{
			return Calc(st, memory, new long[] { singleInput }, (s, l, arg3) => { }, CalcMode.RunToFirstInput);
		}

		public static MachineStatus RunToInput(MachineStatus st, long[] memory, int singleInput, Action<string, long, long> outputFunc)
		{
			return Calc(st, memory, new long[]{ singleInput }, outputFunc, CalcMode.RunToFirstInput);
		}
		public static MachineStatus RunToOutput(MachineStatus st, long[] memory)
		{
			return Calc(st, memory, new long[0], (s, l, arg3) => { }, CalcMode.RunToFirstOutput);
		}
		public static MachineStatus RunToOutput(MachineStatus st, long[] memory, Action<string, long, long> outputFunc)
		{
			return Calc(st, memory, new long[0], outputFunc, CalcMode.RunToFirstOutput);
		}

		public static MachineStatus Calc(MachineStatus st, long[] memory, long[] Inputs, Action<string, long, long> outputFunc, CalcMode calcMode=default)
		{
			var outputAcc="";
			int inputCounter = 0;
			long relativeBase = st.RelativeBase;
			long pc = st.ProgramCounter;
			while (pc < memory.Length)
			{
				var opcode = memory[pc] % 100;
				var arg1Mode = ArgMode(memory[pc], 100);
				var arg2Mode = ArgMode(memory[pc], 1000);
				var arg3Mode = ArgMode(memory[pc], 10000);
				long getArg1()
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
						case 9:
							var addr = memory[pc + 1];
							long arg = addr;
							if (arg1Mode == 0)
								arg = memory[addr];
							if (arg1Mode == 2)
								arg = memory[addr+relativeBase];
							return arg;
					}

					return 0;
				}
				long getArg2()
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
							long arg = addr;
							if (arg2Mode == 0)
								arg = memory[addr];
							if (arg2Mode==2)
								arg = memory[addr + relativeBase];
							return arg;
					}

					return 0;

				}
				long getArg3()
				{
					switch (opcode)
					{
						case 7:
						case 8:
							var addr = memory[pc + 3];
							long arg = addr;

							if (arg3Mode == 2)
								arg = memory[addr + relativeBase];

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
						if (arg3Mode == 2)
							addrt = addrt + relativeBase;
						memory[addrt] = arg1 + arg2;
						pc = pc + 4;
						break;
					case 2:
						addrt = memory[pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + relativeBase;
						memory[addrt] = arg1 * arg2;
						pc = pc + 4;
						break;
					case 3:
						var addr31 = memory[pc + 1];
						if (arg1Mode == 2)
						{
							addr31 = relativeBase + addr31;
						}
						memory[addr31] = Inputs[inputCounter++];
						pc = pc + 2;
						if (calcMode == CalcMode.RunToFirstInput)
							return new MachineStatus { Result = -240174, ProgramCounter = pc, RelativeBase =relativeBase, RanToHalt = false};
						break;
					case 4:
						arg1 = getArg1();
						outputFunc("Output {0} {1}", pc, arg1);
						pc = pc + 2;
						outputAcc += (char)arg1;
						/*
						if (outputAcc.Contains("Didn"))
							return new MachineStatus { Result = -1, ProgramCounter = pc, RelativeBase = relativeBase, RanToHalt = false };
							*/
						if (calcMode == CalcMode.RunToFirstOutput)
							return new MachineStatus { Result = arg1, ProgramCounter = pc, RelativeBase = relativeBase, RanToHalt = false };
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
						addrt = memory[pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + relativeBase;
						if (arg1 < arg2)
							memory[addrt] = 1;
						else
							memory[addrt] = 0;
						pc = pc + 4;
						break;
					case 8: //EQ
						addrt = memory[pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + relativeBase;
						if (arg1 == arg2)
							memory[addrt] = 1;
						else
							memory[addrt] = 0;
						pc = pc + 4;
						break;
					case 9: //Set Relative Base
						relativeBase += arg1;
						pc += 2;
						break;
					case 99:
						return new MachineStatus { Result = memory[0], ProgramCounter = pc, RelativeBase = relativeBase, RanToHalt = true };
				}
			}
			return new MachineStatus { Result = -230771, ProgramCounter = pc, RelativeBase = relativeBase, RanToHalt = false };
		}


	}
}
