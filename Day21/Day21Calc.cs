using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    class Day21Calc
    {
        static void OutputConsole(string text, long position, long output)
        {
            // Console.WriteLine(String.Format(text, position, output));
            Console.Write((char)output);
        }

        public static void CalcPart2()
        {
            if (CalcTwoScript("OR C J\nAND A J\nNOT J J\nAND D J\n"))
                Console.WriteLine("Success");
            return;
        }
            public static void CalcPart1()
        {
        //    if (CalcOneScript("OR C J\nAND A J\nNOT J J\nAND D J"))
                if (CalcOneScript("OR C J\nAND A J\nNOT J J\nAND D J\n"))
                    Console.WriteLine("Success");
            return;
            var commands = new[] { "NOT", "OR", "AND" };
            var fromRegisters = new[] { "A", "B", "C", "D", "J", "T" };
            var toRegisters = new[] { "J", "T" };
            var normalCommands =
                from c in commands
                from r in fromRegisters
                from t in toRegisters
                select $"{c} {r} {t}\n";
            var finalCommands =
                from c in commands
                from r in fromRegisters
                select $"{c} {r} J\n";
            var fourCommandScripts =
                from a in normalCommands
                from b in normalCommands
                from c in normalCommands
                from d in finalCommands
                select a + b + c + d;
            var i = 0;
            foreach (string c in fourCommandScripts)
            {
                CalcOneScript(c);
                if (i % 32 == 0)
                {
                    Console.SetCursorPosition(1, 1);
                    Console.Write(i);
                    Console.Write(c);
                }
                i++;
            }
            //CalcOneScript("");
        }

        public static bool CalcOneScript(string script)
        {
            var scr = (script + "WALK\n").ToCharArray().Select(c => (long)c).ToArray();
            var asm = new long[2500];
            Day21Input.Day21Code.CopyTo(asm, 0);
            var st = new MachineStatus();
            var outp = new List<long>();
            Machine.Calc(st, asm, scr, (a, b, c) => outp.Add(c));
            var str = new string(outp.Where(x => x < 256).Select(x => (char)x).ToArray());
            Console.WriteLine(str);
            if (str.Contains("Didn"))
                return false;
            else
            {
                Console.WriteLine(outp.First(c => c > 256));
                return true;
            }
        }
        public static bool CalcTwoScript(string script)
        {
            var scr = (script + "RUN\n").ToCharArray().Select(c => (long)c).ToArray();
            var asm = new long[2500];
            Day21Input.Day21Code.CopyTo(asm, 0);
            var st = new MachineStatus();
            var outp = new List<long>();
            Machine.Calc(st, asm, scr, (a, b, c) => outp.Add(c));
            var str = new string(outp.Where(x => x < 256).Select(x => (char)x).ToArray());
            Console.WriteLine(str);
            if (str.Contains("Didn"))
                return false;
            else
            {
                Console.WriteLine(outp.First(c => c > 256));
                return true;
            }
        }
    }
}
