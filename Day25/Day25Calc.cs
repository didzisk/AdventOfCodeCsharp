using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day25
{
    public static class Day25Calc
    {
        static void OutputConsole(string text, long position, long output)
        {
            // Console.WriteLine(String.Format(text, position, output));
            Console.Write((char)output);
        }

        public static MachineStatus Perform(string cmd, MachineStatus st)
        {
            var scr = new List<long>();
            scr = (cmd+"\n").ToCharArray().Select(c => (long)c).ToList();
            st = Machine.Calc(st, scr, (a, b, c) => { Console.Write((char)c); });
            return st;
        }
        public static void CalcPart1()
        {
            var asm = new long[5500];
            Day25Input.Day25Code.CopyTo(asm, 0);
            var st = new MachineStatus();
            st.memory = asm;
            var scr = new List<long>();
            st = Machine.Calc(st, scr, (a, b, c) => { Console.Write((char)c); });
            st = Perform("west", st);
            st = Perform("take fixed point", st);
            st = Perform("north", st);
            st = Perform("take sand", st);
            st = Perform("south", st);
            st = Perform("east", st);
            st = Perform("east", st); //observ
            st = Perform("take asterisk", st);
            st = Perform("north", st); //arcade
            st = Perform("north", st); //Passages
            st = Perform("take hypercube", st);
            st = Perform("north", st); //Storage
            st = Perform("take coin", st);
            st = Perform("north", st); //navigation
            st = Perform("take easter egg", st);
                        //st = Perform("west", st); //hot choc
            st = Perform("south", st); //Storage
            st = Perform("south", st); //Passages
                                       //st = Perform("west", st); //Holodeck, empty
            st = Perform("south", st); //Arcade
            st = Perform("west", st); //Crew q
            st = Perform("north", st); //corridor
            st = Perform("take spool of cat6", st);
            st = Perform("north", st); //kitchen
            st = Perform("take shell", st);
            st = Perform("west", st); //Security checkpoint
            var things = new Dictionary<int, string> {
                { 0,"easter egg" },
                { 1,"sand"},
                { 2,"fixed point" },
                { 3,"coin" },
                { 4,"spool of cat6" },
                { 5, "shell" },
                { 6, "hypercube" },
                { 7, "asterisk" }
            };
            var oldGrayCode= Gray.IntToGray(170);
            for (int i = 0; i < 256; i++)
            {
                var currCode = (i + 19) % 256;
                Console.Write(currCode);
                var grayCode = Gray.IntToGray(currCode);
                Gray.showBitsInMemory(grayCode);
                for (int j = 0; j < 8; j++)
                {
                    
                    if (oldGrayCode[j] == 1 && grayCode[j] == 0)
                        st = Perform($"drop {things[j]}", st); ;
                    if (oldGrayCode[j] == 0 && grayCode[j] == 1)
                        st = Perform($"take {things[j]}", st);
                }
                st = Perform("north", st); //Pressure sensitive, failure, back in SEC
                st = Perform("inv", st); //corridor
                oldGrayCode = grayCode;
                Console.ReadLine();
                Console.Clear();
            }



        }
        
    }
}
