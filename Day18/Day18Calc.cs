using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day18
{
    public class Day18Calc
    {
        public static int width;
        public static int height;
        public static void CalcMain()
        {

            var starts = new List<Point>();
            var grid = Day18Input.Day18Maze.Split("\r\n");
            width = grid[0].Length;
            height = grid.Length;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i][j] == '@')
                        starts.Add(new Point(i, j));

                }

            }
        }
    }
}
