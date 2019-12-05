using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
	class Machine
	{
		public static int ArgMode(int code, int mask)
		{
			return (code % (mask * 10) - (code % mask))/mask;
		}
		public static int CalcWithInput(int verb, int noun, int[] memory)
		{

			memory[1] = verb;
			memory[2] = noun;
			return Calc(memory);
		}
			public static int Calc(int[] memory)
			{
				int pc = 0;
				while (pc < memory.Length)
			{
				var opcode = memory[pc] % 100;
				var arg1mode = ArgMode(memory[pc], 100);
				var arg2mode = ArgMode(memory[pc], 1000);
				var arg3mode = ArgMode(memory[pc], 10000);

				switch (opcode)
				{
					case 1:
						var addr1 = memory[pc + 1];
						int arg1=addr1;
						if (arg1mode == 0)
							arg1 = memory[addr1];
						var addr2 = memory[pc + 2];
						int arg2 = addr2;
						if (arg2mode == 0)
							arg2 = memory[addr2];
						var addrt = memory[pc + 3];
						memory[addrt] = arg1 + arg2;
						pc = pc + 4;
						break;
					case 2:
						var addr21 = memory[pc + 1];
						int arg21 = addr21;
						if (arg1mode == 0)
							arg21 = memory[addr21];
						var addr22 = memory[pc + 2];
						int arg22 = addr22;
						if (arg2mode == 0)
							arg22 = memory[addr22];
						var addrt2 = memory[pc + 3];
						memory[addrt2] = arg21 * arg22;
						pc = pc + 4;
						break;
					case 3:
						int singleInput = 1;
						var addr31 = memory[pc + 1];
						memory[addr31] = singleInput;
						pc = pc + 2;
						break;
					case 4:
						var addr41 = memory[pc + 1];
						int arg41 = addr41;
						if (arg1mode == 0)
							arg41 = memory[addr41];
						Console.WriteLine($"Output {pc} {arg41}");
						pc = pc + 2;
						break;
					case 99:
						return memory[0];
				}
			}
			return -1;

		}

	}
}
