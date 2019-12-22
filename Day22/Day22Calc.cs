using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public static void CalcPart2()
        {
            var cards = 119315717514047;
            var repeats = 101741582076661;
            long incrementMul = 1;
            long offsetDiff = 0;
            long inv(long n)
            {
                return (long)BigInteger.ModPow(n, cards - 2, cards);
            }

            (long, long) GetSequence(long iterations)
            {
                var increment = (long)BigInteger.ModPow(incrementMul, iterations, cards);
                var offset = offsetDiff * (1 - increment) * inv((1 - incrementMul) % cards);
                return (increment, offset);
            }

            long Get(long offset, long increment, long i)
            {
                //gets the ith number in a given sequence
                return (offset + i * increment) % cards;
            }

            var commands = Day22Input.Official.Split("\r\n");
            foreach (var command in commands)
            {
                if (command.StartsWith("deal into new stack"))
                {
                    incrementMul = -1 * incrementMul;
                    incrementMul %= cards;
                    offsetDiff += incrementMul;
                    offsetDiff %= cards;
                }
                var lastPart = command.Split(' ').Last();
                var intArg = 0;
                if (int.TryParse(lastPart, out intArg))
                {
                    if (command.StartsWith("cut"))
                    {
                        offsetDiff += intArg * incrementMul;
                        offsetDiff %= cards;
                    }
                    if (command.StartsWith("deal with"))
                    {
                        incrementMul *= inv(intArg);
                        incrementMul %= cards;

                    }
                }

            }
            var (increment, offset) = GetSequence(repeats);
            var place = Get(offset, increment, 2020);
            Console.WriteLine(place);
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
                        if (intArg < 0)
                        {
                            var remainingLen = stack.Length + end;
                            src = stack.Skip(remainingLen).Take(-end).Concat(stack.Take(remainingLen));

                        }
                        else
                        {
                            var remainingLen = stack.Length - end;
                            src = stack.Skip(end).Take(remainingLen).Concat(stack.Take(end));
                        }
                        stack = src.ToArray();
                    }
                    if (command.StartsWith("deal with"))
                    {
                        int i = 0;
                        foreach (var item in stack.ToArray())
                        {
                            stack[i] = item;
                            i = (i + intArg) % stack.Length;

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
                    if (stack[i] == 2019)
                    {
                        Console.WriteLine($"Pos{i}");
                        break;
                    }
                }
            }

        }

    }
}
