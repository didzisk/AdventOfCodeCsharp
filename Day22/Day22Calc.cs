using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day22
{
    class Day22Calc
    {
        public static void CalcPart1()
        {
            var exarr = new int[10];
            for (int i = 0; i < exarr.Length; i++)
            {
                exarr[i] = i;
            }
            var ex1arr = new int[10];
            exarr.CopyTo(ex1arr, 0);
            PerformShuffle(ex1arr, Day22Input.Ex1);
            var ex2arr = new int[10];
            exarr.CopyTo(ex2arr, 0);
            PerformShuffle(ex2arr, Day22Input.Ex2);
            exarr.CopyTo(ex2arr, 0);
            PerformShuffle(ex2arr, Day22Input.Ex3);
            exarr.CopyTo(ex2arr, 0);
            PerformShuffle(ex2arr, Day22Input.Ex4);
            var offarr = new int[10007];
            for (int i = 0; i < offarr.Length; i++)
            {
                offarr[i] = i;
            }
            PerformShuffle(offarr, Day22Input.Official);

        }

        public static void PerformShuffle(int[] stack, string shuffleScript)
        {
            var commands = shuffleScript.Split("\r\n");
            foreach (var command in commands)
            {
                if (command.StartsWith("deal into new stack"))
                    stack = stack.Reverse().ToArray();
                var lastPart = command.Split(' ').Last();
                var intArg = 0;
                if (int.TryParse(lastPart, out intArg))
                {
                    if (command.StartsWith("cut"))
                    {
                        var end = intArg;
                        IEnumerable<int> src; 
                        if (intArg<0)
                        {
                            var remainingLen = stack.Length + end;
                            src = stack.Skip(remainingLen).Take(-end).Concat(stack.Take(remainingLen));

                        }
                        else
                        {
                            var remainingLen = stack.Length-end;
                            src = stack.Skip(end).Take(remainingLen).Concat(stack.Take(end));
                        }
                        stack = src.ToArray();
                    }
                    if (command.StartsWith("deal with"))
                    {
                        int i= 0;
                        foreach (var item in stack.ToArray())
                        {
                            stack[i] = item;
                            i = (i + intArg)%stack.Length;

                        }
                    }
                }
                    ;
            }
            Console.WriteLine();
            if (stack.Length < 100)
            {
                foreach (var item in stack)
                    Console.Write($"{item}, ");
            }
            if (stack.Contains(2019))
            {
                for (int i = 0; i < stack.Length; i++)
                {
                    if (stack[i]==2019)
                    {
                        Console.WriteLine($"Pos{i}");
                        break;
                    }
                }
            }

        }

    }

    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
    }
}
