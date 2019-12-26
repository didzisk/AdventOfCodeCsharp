using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day24
{
    public static class Day24Calc
    {
        public static void Calc1() 
        {
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


        }

        public static int ImportInput(string input)
        {
            var s=string.Join("",input.Split("\r\n"));
            var mask = 1;
            var rating = 0;
            foreach (char c in s)
            {
                if (c=='#')
                {
                    rating += mask;
                }
                mask = mask << 1;
            }
            return rating;
        }

        public static int HasOneOrTwo(int bitNr, int fullCurrent, int current)
        {
            var thisBit = 1 << bitNr;

            bool isEmpty=((fullCurrent & thisBit)==0);

            int numNeighbors = 0;
            for (int i = 0; i < 31; i++)
            {
                if ((current & 1)==1)
                    numNeighbors++;
                current=current >> 1;
                if (numNeighbors > 1 && !isEmpty)
                    return 0;
                if (numNeighbors > 2)
                    return 0;
            }
            if (numNeighbors > 0)
                return 1;
            return 0;
        }
        public static int NextRating(int current)
        {
            int bit0 =  HasOneOrTwo(0, current ,current & 0b00000_00000_00000_00001_00010);
            int bit1 =  HasOneOrTwo(1, current ,current & 0b00000_00000_00000_00010_00101) << 1;
            int bit2 =  HasOneOrTwo(2, current ,current & 0b00000_00000_00000_00100_01010) << 2;
            int bit3 =  HasOneOrTwo(3, current ,current & 0b00000_00000_00000_01000_10100) << 3;
            int bit4 =  HasOneOrTwo(4, current ,current & 0b00000_00000_00000_10000_01000) << 4;

            int bit5 =  HasOneOrTwo(5, current ,current & 0b00000_00000_00001_00010_00001) << 5;
            int bit6 =  HasOneOrTwo(6, current ,current & 0b00000_00000_00010_00101_00010) << 6;
            int bit7 =  HasOneOrTwo(7, current ,current & 0b00000_00000_00100_01010_00100) << 7;
            int bit8 =  HasOneOrTwo(8, current ,current & 0b00000_00000_01000_10100_01000) << 8;
            int bit9 =  HasOneOrTwo(9, current ,current & 0b00000_00000_10000_01000_10000) << 9;

            int bit10 = HasOneOrTwo(10, current ,current & 0b00000_00001_00010_00001_00000) << 10;
            int bit11 = HasOneOrTwo(11, current ,current & 0b00000_00010_00101_00010_00000) << 11;
            int bit12 = HasOneOrTwo(12, current ,current & 0b00000_00100_01010_00100_00000) << 12;
            int bit13 = HasOneOrTwo(13, current ,current & 0b00000_01000_10100_01000_00000) << 13;
            int bit14 = HasOneOrTwo(14, current ,current & 0b00000_10000_01000_10000_00000) << 14;

            int bit15 = HasOneOrTwo(15, current ,current & 0b00001_00010_00001_00000_00000) << 15;
            int bit16 = HasOneOrTwo(16, current ,current & 0b00010_00101_00010_00000_00000) << 16;
            int bit17 = HasOneOrTwo(17, current ,current & 0b00100_01010_00100_00000_00000) << 17;
            int bit18 = HasOneOrTwo(18, current ,current & 0b01000_10100_01000_00000_00000) << 18;
            int bit19 = HasOneOrTwo(19, current ,current & 0b10000_01000_10000_00000_00000) << 19;

            int bit20 = HasOneOrTwo(20, current ,current & 0b00010_00001_00000_00000_00000) << 20;
            int bit21 = HasOneOrTwo(21, current ,current & 0b00101_00010_00000_00000_00000) << 21;
            int bit22 = HasOneOrTwo(22, current ,current & 0b01010_00100_00000_00000_00000) << 22;
            int bit23 = HasOneOrTwo(23, current ,current & 0b10100_01000_00000_00000_00000) << 23;
            int bit24 = HasOneOrTwo(24, current ,current & 0b01000_10000_00000_00000_00000) << 24;
            return bit0 + bit1 + bit2 + bit3 + bit4 + bit5 + bit6 + bit7 + bit8 + bit9 + bit10 +
                bit11 + bit12 + bit13 + bit14 + bit15 + bit16 + bit17 + bit18 + bit19 + bit20 +
                bit21 + bit22 + bit23 + bit24;
        }

        public static void PrettyPrintRating(int rating)
        {
            var s = Convert.ToString(rating + 0b100000_00000_00000_00000_00000, 2).Substring(1,25);
            Console.WriteLine($"{s} - {rating}");
            for (int i = 0; i < 5; i++)
            {
                var ss=new string(s.Substring(5*(4-i),5).Reverse().Select(x=>(x=='1')?'#':'.').ToArray());
                Console.WriteLine(ss);

            }
        }
    }
}
