using System;
using System.Collections.Generic;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var lifeStart = Day24Calc.ImportInput(Day24Input.Official);
            Day24Calc.PrettyPrintRating(lifeStart);
            var rating = Day24Calc.NextRating(lifeStart);
            var history = new List<int>();
            history.Add(lifeStart);
            while (!history.Contains(rating))
            {
                Day24Calc.PrettyPrintRating(rating);
                history.Add(rating);
                rating = Day24Calc.NextRating(rating);
            }
            Console.WriteLine(rating);
            Console.WriteLine();
            foreach (var item in history)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(rating);

                lifeStart = Day24Calc.ImportInput(Day24Input.Official);
        }
    }
}
