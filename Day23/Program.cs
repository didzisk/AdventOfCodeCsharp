using System;
using System.Collections.Generic;
using System.Drawing;

namespace Day23
{
    class Program
    {
        public class ComputerItem
        {
            public MachineStatus st { get; set; }
            public List<long> inputQueue;
            public ComputerItem()
            {
                inputQueue = new List<long>();
            }
        }
        static List<(long, long, long)> ProcessTargetList(List<long> targetList)
        {
            int listIndex = 0;
            long newAddress = 0;
            long newX = 0;
            long newY = 0;
            var inputQueue = new List<(long, long, long)>();
            foreach (var c in targetList)
            {
                switch (listIndex)
                {
                    case 0:
                        newAddress = c;
                        break;
                    case 1:
                        newX = c;
                        break;
                    case 2:
                        newY = c;
                        inputQueue.Add((newAddress, newX, newY));
                        Console.WriteLine($"Adding {newAddress:00}, {newX}, {newY}");
                        break;
                }
                listIndex = (listIndex + 1) % 3;
            }
            return inputQueue;

        }
        static void Main(string[] args)
        {
            var computers = new Dictionary<int, ComputerItem>();
            var targetList = new List<long>();

            for (int i = 0; i < 50; i++)
            {
                //the initialization loop
                var arr = new long[2500];
                var st = new MachineStatus { memory = arr };
                Day23Input.Day23Code.CopyTo(arr, 0);
                var wi = new ComputerItem { st = st };
                computers.Add(i, wi);
                var m1input = new List<long>() { i, -1, -1 }; //address, X=unknown, Y=unknown

                wi.st = Machine.Calc(wi.st, m1input, (a, b, c) => { targetList.Add(c); Console.WriteLine(c); });
            }
            var inputQueue = ProcessTargetList(targetList);
            
            while (true)
            {
                targetList = new List<long>();
                var (a, x, y) = inputQueue[0];
                inputQueue.RemoveAt(0);
                var winput = new List<long>() { x, y };
                computers[(int)a].inputQueue.Add(x);
                computers[(int)a].inputQueue.Add(y);
                computers[(int)a].st = Machine.Calc(computers[(int)a].st, computers[(int)a].inputQueue,
                    (a, b, c) =>
                    {
                        targetList.Add(c); Console.WriteLine(c);
                    });
                var inputQueueNew = ProcessTargetList(targetList);
                inputQueue.AddRange(inputQueueNew);
            }
            //  

        }
    }
}
