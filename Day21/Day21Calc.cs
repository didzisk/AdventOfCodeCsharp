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
            var onescript =
            "OR C J\n" + //c wall-j
            "AND A J\n" +//a wall, too-j
            "NOT J J\n" +//reverse, meaning if either is empty
            "AND D J\n"//d must be intact
            ;
            var twoscript =
                "NOT A J\n" + //a hole-j
                "NOT B T\n" + //b hole-t
                "OR T J\n" +  //if either is hole-j
                "NOT C T\n" +  //c hole-t
                "OR T J\n" +   //if either is hole-j
                "NOT D T\n" +  //d hole-t
                "NOT T T\n"+
                "AND T J\n" +   //if either is hole-j
                "NOT E T\n"+   //e must be wall, too
                "NOT T T\n"+   //so we store the reverse of is hole
                "OR H T\n"+    //or H
                "AND T J\n"
;
            if (CalcTwoScript(
                    twoscript))
                Console.WriteLine("Success");
            return;
        }
            public static void CalcPart1()
        {
        //    if (CalcOneScript("OR C J\nAND A J\nNOT J J\nAND D J"))
                if (CalcOneScript(
                "NOT A J\n" + //a hole-j
                "NOT B T\n" + //b hole-t
                "OR T J\n" +  //if either is hole-j
                "NOT C T\n" +  //c hole-t
                "OR T J\n" +   //if either is hole-j
                "NOT D T\n" +  //d hole-t
                "NOT T T\n" +  //reverse - d must be wall
                "AND T J\n"      //put together
                    ))
                    Console.WriteLine("Success");
            return;
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
