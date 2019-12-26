using System;
using System.Collections.Generic;
using System.Text;

namespace Day24
{
    class Day24Calc2
    {
        public static void Calc1()
        {
            var ratings = new List<int>();

            var field=ImportInput(Day24Input.Official);
            var rating = Day24Calc2.GetRating(field);
            while (!ratings.Contains(rating))
            {
                ratings.Add(rating);
                Day24Calc.PrettyPrintRating(rating);
                field = DoLife(field);
                rating = Day24Calc2.GetRating(field);
            }
            Day24Calc.PrettyPrintRating(rating);
        }

        public static bool[,] DoLife(bool[,] field)
        {
            var res = new bool[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var nLeft = (j > 0 && field[i, j - 1]) ? 1 : 0;
                    var nUp = (i > 0 && field[i - 1, j]) ? 1 : 0;
                    var nRight = (j < 4 && field[i, j + 1]) ? 1 : 0;
                    var nDown = (i < 4 && field[i + 1, j]) ? 1 : 0;
                    var nn = nLeft + nUp + nRight + nDown;
                    res[i, j] = nn == 1;
                    if (!field[i, j] && nn == 2)
                        res[i, j] = true;

                }
            }
            return res;
        }
        public static bool[,] ImportInput(string input)
        {
            var res = new bool[5, 5];
            var lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    res[i, j] = lines[i][j] == '#';
                }
            }
            return res;
        }

        public static int GetRating(bool[,] field)
        {
            var res = 0;
            var mask = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (field[i, j])
                        res += mask;
                    mask = mask << 1;
                }
            }
            return res;
        }

    }
}
