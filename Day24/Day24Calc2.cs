using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day24
{
    class Day24Calc2
    {
        public static void Calc1()
        {
            var ratings = new List<int>();

            var field = ImportInput(Day24Input.Official);
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

        public static void Calc2()
        {
            var ex1bugs = Calc2Outer(Day24Input.Ex1, 10);
            Console.WriteLine($"Bugs {ex1bugs}");
            var offbugs = Calc2Outer(Day24Input.Official, 200);
            Console.WriteLine($"Bugs {offbugs}");

        }
        public static int Calc2Outer(string input, int minutes)
        {
            var space = new Dictionary<int, bool[,]>();
            var field = ImportInput(input);
            space.Add(0, field);
            for (int i = 0; i < minutes; i++)
            {
                var spreadOut = i / 2 + 1;
                if (i % 2 == 0)
                {
                    var outer = new bool[5, 5];
                    space.Add(-spreadOut, outer);
                    var inner = new bool[5, 5];
                    space.Add(spreadOut, inner);
                }
                var newSpace = new Dictionary<int, bool[,]>();
                foreach (var (level, curr) in space)
                {
                    var res = DoLife2(space, level);
                    newSpace.Add(level, res);
                    var currRating = GetRating(res);
                    //Console.WriteLine($"Level:{level}");
                    //PrettyPrintRating(currRating);
                }
                space = newSpace;
                //Console.ReadLine();

            }
            var numBugs=space.Sum((kvp) =>
            {
                var bugs = 0;
                foreach (bool k in kvp.Value)
                { bugs += k ? 1 : 0; }
                return bugs;
            });
            var minusLevels = space.Keys.Min();
            var plusLevels = space.Keys.Max();
            for (int i = minusLevels; i <=plusLevels; i++)
            {
                var rat = GetRating(space[i]);
                //PrettyPrintRating(rat);
            }
            return numBugs;
        }

        public static bool[,] DoLife2(Dictionary<int, bool[,]> space, int level)
        {
            var res = new bool[5, 5];
            var curr = space[level];
            bool[,] outer = null;
            bool[,] inner = null;
            if (space.ContainsKey(level - 1))
                outer = space[level - 1];
            if (space.ContainsKey(level + 1))
                inner = space[level + 1];
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var nn = NLeft(row, col, curr, inner, outer)
                        + NUp(row, col, curr, inner, outer)
                        + NRight(row, col, curr, inner, outer)
                        + NDown(row, col, curr, inner, outer);
                    res[row, col] = nn == 1;
                    if (!curr[row, col] && nn == 2)
                        res[row, col] = true;

                }
            }
            res[2, 2] = false;

            return res;
        }

        public static int NLeft(int row, int col, bool[,] curr, bool[,] inner, bool[,] outer)
        {
            int res = 0;
            bool allowOut = outer != null;
            bool allowIn = inner != null;
            switch (col)
            {
                case 0://one level out and left, it's row 2, col 1

                    res = allowOut && outer[2, 1] ? 1 : 0;
                    break;

                case 3 when row == 2://one level in, all 5 in col 4 (last col)
                    if (allowIn)
                        res = (inner[0, 4] ? 1 : 0) +
                            (inner[1, 4] ? 1 : 0) +
                            (inner[2, 4] ? 1 : 0) +
                            (inner[3, 4] ? 1 : 0) +
                            (inner[4, 4] ? 1 : 0);
                    break;
                case 1:
                case 2:
                case 3:
                case 4://regular
                    {
                        res = curr[row, col - 1] ? 1 : 0;
                        break;
                    }
            }
            return res;
        }
        public static int NRight(int row, int col, bool[,] curr, bool[,] inner, bool[,] outer)
        {
            int res = 0;
            bool allowOut = outer != null;
            bool allowIn = inner != null;
            switch (col)
            {
                case 4://one level out and right, it's row 2, col 3
                    if (allowOut)
                        res = outer[2, 3] ? 1 : 0;
                    break;

                case 1 when row == 2://one level in, all 5 in col 0
                    if (allowIn)
                        res = (inner[0, 0] ? 1 : 0) +
                            (inner[1, 0] ? 1 : 0) +
                            (inner[2, 0] ? 1 : 0) +
                            (inner[3, 0] ? 1 : 0) +
                            (inner[4, 0] ? 1 : 0);
                    break;
                case 0:
                case 1:
                case 2:
                case 3://regular
                    res = curr[row, col + 1] ? 1 : 0;
                    break;
            }
            return res;
        }
        public static int NUp(int row, int col, bool[,] curr, bool[,] inner, bool[,] outer)
        {
            int res = 0;
            bool allowOut = outer != null;
            bool allowIn = inner != null;
            switch (row)
            {
                case 0://one level out and up, it's row 1, col 2
                    if (allowOut)
                        res = outer[1, 2] ? 1 : 0;
                    break;

                case 3 when col == 2://one level in, all 5 in row 4 (last row)
                    if (allowIn)
                        res = (inner[4, 0] ? 1 : 0) +
                            (inner[4, 1] ? 1 : 0) +
                            (inner[4, 2] ? 1 : 0) +
                            (inner[4, 3] ? 1 : 0) +
                            (inner[4, 4] ? 1 : 0);
                    break;
                case 1:
                case 2:
                case 3:
                case 4://regular
                    {
                        res = curr[row - 1, col] ? 1 : 0;
                        break;
                    }
            }
            return res;
        }
        public static int NDown(int row, int col, bool[,] curr, bool[,] inner, bool[,] outer)
        {
            int res = 0;
            bool allowOut = outer != null;
            bool allowIn = inner != null;
            switch (row)
            {
                case 4://one level out and down, it's row 3, col 2
                    if (allowOut)
                        res = outer[3, 2] ? 1 : 0;
                    break;

                case 1 when col == 2://one level in, all 5 in row 0 (first row)
                    if (allowIn)
                        res = (inner[0, 0] ? 1 : 0) +
                            (inner[0, 1] ? 1 : 0) +
                            (inner[0, 2] ? 1 : 0) +
                            (inner[0, 3] ? 1 : 0) +
                            (inner[0, 4] ? 1 : 0);
                    break;
                case 0:
                case 1:
                case 2:
                case 3://regular
                    {
                        res = curr[row + 1, col] ? 1 : 0;
                        break;
                    }
            }
            return res;
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
        public static void PrettyPrintRating(int rating)
        {
            var s = Convert.ToString(rating + 0b100000_00000_00000_00000_00000, 2).Substring(1, 25);
            for (int i = 0; i < 5; i++)
            {
                var ss = new string(s.Substring(5 * (4 - i), 5).Reverse().Select(x => (x == '1') ? '#' : '.').ToArray());
                Console.WriteLine(ss);

            }
        }

    }
}
