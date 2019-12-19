using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day19
{

    class Day19Calc
    {
        static void OutputConsole(string text, long position, long output)
        {
            //Console.WriteLine(String.Format(text, position, output));
        }

        public class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool WallW { get; set; }
            public bool WallE { get; set; }
            public bool WallN { get; set; }
            public bool WallS { get; set; }
            public int NumWalls { get; set; }
            public bool IsOxygen { get; set; }
            public Node Parent { get; set; }
        }
        public static void Calc()
        {
            var width = 50;
            var arr = new char[width, width];
            var li = new List<int>();
            var mem = new long[10000];
            MachineStatus st = new MachineStatus { ProgramCounter = 0 };
            Day19Input.Day19Code.CopyTo(mem, 0);
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        st = new MachineStatus { ProgramCounter = 0 };
                        Day19Input.Day19Code.CopyTo(mem, 0);
                        st = Machine.RunToInput(st, mem, x, OutputConsole);
                        st = Machine.RunToInput(st, mem, y, OutputConsole);
                        st = Machine.RunToOutput(st, mem, OutputConsole);
                        arr[x, y] = (char)(st.Result + 48);
                        Console.SetCursorPosition(x, y);
                        Console.Write(arr[x, y]);

                        li.Add((int)st.Result);
                    }
                }
                var num = li.Count(e => e == 1);
                Console.WriteLine($"Num cells {num}");
            }
            return;
        }

        public static void CalcPart2()
        {
            Console.Clear();
            int y = 10;
            int x = 0;
            int xStart = 0;
            var squareFits = false;
            do
            {
                y++;
                Console.WriteLine(y);
                while (!HasBeam(xStart,y))
                {
                    xStart++;
                };
                x = xStart;
                while (HasBeam(x+99,y))
                {
                    if (!HasBeam(x, y))
                        throw new Exception("weird");
                    if (HasBeam(x, y + 99))
                    {
                        Console.WriteLine($"{x * 10000 + y}");
                        return;
                    }
                    x++;
                }
            } while (!squareFits); 
        }

        public static bool HasBeam(int x, int y)
        {
            var mem = new long[10000];
            var st = new MachineStatus { ProgramCounter = 0 };
            Day19Input.Day19Code.CopyTo(mem, 0);
            st = Machine.RunToInput(st, mem, x, OutputConsole);
            st = Machine.RunToInput(st, mem, y, OutputConsole);
            st = Machine.RunToOutput(st, mem, OutputConsole);
            return (st.Result == 1);
        }
    }
<<<<<<< HEAD
=======

>>>>>>> day19
}
