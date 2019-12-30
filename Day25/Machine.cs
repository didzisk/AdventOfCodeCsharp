using System;
using System.Collections.Generic;
using System.Text;

namespace Day25
{
	public enum CalcMode
	{
		Default = 0,
		RunToFirstInput = 1,
		RunToFirstOutput = 2
	}

	public enum ReturnMode
	{
		RanToHalt = 0,
		WaitingForInput = 1,
		ProcessedInput = 2,
		ProducedOutput = 3
	}

	public class MachineStatus
	{
		public long pc;
		public long RelativeBase;
		public long Result;
		public ReturnMode ReturnMode;
		public long[] memory;
	}

	public class Machine
	{
		public static long ArgMode(long code, long mask)
		{
			return (code % (mask * 10) - (code % mask)) / mask;
		}

		public static MachineStatus RunToInput(MachineStatus st, int singleInput)
		{
			return Calc(st, new List<long>() { singleInput }, (s, l, arg3) => { }, CalcMode.RunToFirstInput);
		}

		public static MachineStatus RunToInput(MachineStatus st, int singleInput, Action<string, long, long> outputFunc)
		{
			return Calc(st, new List<long>() { singleInput }, outputFunc, CalcMode.RunToFirstInput);
		}
		public static MachineStatus RunToOutput(MachineStatus st, long[] memory)
		{
			return Calc(st, new List<long>(), (s, l, arg3) => { }, CalcMode.RunToFirstOutput);
		}
		public static MachineStatus RunToOutput(MachineStatus st, long[] memory, Action<string, long, long> outputFunc)
		{
			return Calc(st, new List<long>(), outputFunc, CalcMode.RunToFirstOutput);
		}

		public static MachineStatus Calc(MachineStatus st, List<long> Inputs, Action<string, long, long> outputFunc, CalcMode calcMode=default)
		{
			while (st.pc < st.memory.Length)
			{
				var opcode = st.memory[st.pc] % 100;
				var arg1Mode = ArgMode(st.memory[st.pc], 100);
				var arg2Mode = ArgMode(st.memory[st.pc], 1000);
				var arg3Mode = ArgMode(st.memory[st.pc], 10000);
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
							var addr = st.memory[st.pc + 1];
							long arg = addr;
							if (arg1Mode == 0)
								arg = st.memory[addr];
							if (arg1Mode == 2)
								arg = st.memory[addr+st.RelativeBase];
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
							var addr = st.memory[st.pc + 2];
							long arg = addr;
							if (arg2Mode == 0)
								arg = st.memory[addr];
							if (arg2Mode==2)
								arg = st.memory[addr + st.RelativeBase];
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
							var addr = st.memory[st.pc + 3];
							long arg = addr;

							if (arg3Mode == 2)
								arg = st.memory[addr + st.RelativeBase];

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
						var addrt = st.memory[st.pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + st.RelativeBase;
						st.memory[addrt] = arg1 + arg2;
						st.pc = st.pc + 4;
						break;
					case 2:
						addrt = st.memory[st.pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + st.RelativeBase;
						st.memory[addrt] = arg1 * arg2;
						st.pc = st.pc + 4;
						break;
					case 3:
						var addr31 = st.memory[st.pc + 1];
						if (arg1Mode == 2)
						{
							addr31 = st.RelativeBase + addr31;
						}
						if (Inputs.Count > 0)
						{
							st.memory[addr31] = Inputs[0];
							Inputs.RemoveAt(0);
							st.pc = st.pc + 2;
							if (calcMode == CalcMode.RunToFirstInput)
							{
								st.ReturnMode = ReturnMode.ProcessedInput;
								return st;
							}
						}
						else
						{
							st.ReturnMode = ReturnMode.WaitingForInput;
							return st;
						}
						break;
					case 4:
						arg1 = getArg1();
						outputFunc("Output {0} {1}", st.pc, arg1);
						st.pc = st.pc + 2;
						if (calcMode == CalcMode.RunToFirstOutput)
						{
							st.Result = arg1;
							st.ReturnMode = ReturnMode.ProducedOutput;
						}
						break;
					case 5: //JNZ
						if (arg1 != 0)
							st.pc = arg2;
						else
							st.pc = st.pc + 3;
						break;
					case 6: //JZ
						if (arg1 == 0)
							st.pc = arg2;
						else
							st.pc = st.pc + 3;
						break;
					case 7: //LT
						addrt = st.memory[st.pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + st.RelativeBase;
						if (arg1 < arg2)
							st.memory[addrt] = 1;
						else
							st.memory[addrt] = 0;
						st.pc = st.pc + 4;
						break;
					case 8: //EQ
						addrt = st.memory[st.pc + 3];
						if (arg3Mode == 2)
							addrt = addrt + st.RelativeBase;
						if (arg1 == arg2)
							st.memory[addrt] = 1;
						else
							st.memory[addrt] = 0;
						st.pc = st.pc + 4;
						break;
					case 9: //Set Relative Base
						st.RelativeBase += arg1;
						st.pc += 2;
						break;
					case 99:
						{
							st.ReturnMode = ReturnMode.RanToHalt;
							st.Result = st.memory[0];
							return st;
						}
				}
			}
			return st;
		}


	}
}
