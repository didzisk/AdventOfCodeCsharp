using System;
using System.Collections.Generic;
using System.Text;

namespace Day25
{
    public static class Gray
    {
        public static int[] IntToBinary(int n)
        {
            int[] binary = new int[8];
            int i = 0;
            do
            {
                binary[i++] = n % 2;
            } while ((n /= 2) > 0);
            return binary;
        }

        public static int BinaryToInt(int[] binary)
        {
            int ans = 0;
            for (int i = 0; i <= 7; ++i)
            {
                ans = ans + (int)Math.Pow(2, i) * binary[i];
            }
            return ans;
        }

        public static int[] BinaryToGray(int[] binary)
        {
            int[] gray = new int[8];
            gray[7] = binary[7]; // copy high-order bit
            for (int i = 6; i >= 0; --i)
            { // remaining bits
                if (binary[i] == 0 && binary[i + 1] == 0)
                    gray[i] = 0;
                else if (binary[i] == 1 && binary[i + 1] == 1)
                    gray[i] = 0;
                else if (binary[i] == 0 && binary[i + 1] == 1)
                    gray[i] = 1;
                else if (binary[i] == 1 && binary[i + 1] == 0)
                    gray[i] = 1;
            }
            return gray;
        }

        public static int[] GrayToBinary(int[] gray)
        {
            int[] binary = new int[8];
            binary[7] = gray[7]; // copy high-order bit
            for (int i = 6; i >= 0; --i)
            { // remaining bits
                if (gray[i] == 0 && binary[i + 1] == 0)
                    binary[i] = 0;
                else if (gray[i] == 1 && binary[i + 1] == 1)
                    binary[i] = 0;
                else if (gray[i] == 0 && binary[i + 1] == 1)
                    binary[i] = 1;
                else if (gray[i] == 1 && binary[i + 1] == 0)
                    binary[i] = 1;
            }
            return binary;
        }

        public static int[] IntToGray(int n)
        {
            int[] binary = IntToBinary(n);
            int[] gray = BinaryToGray(binary);
            return gray;
        }

        public static int GrayToInt(int[] gray)
        {
            int[] binary = GrayToBinary(gray);
            int n = BinaryToInt(binary);
            return n;
        }

        public static void showBitsInMemory(int[] bits)
        {
            Console.Write("[");
            for (int i = 0; i < bits.Length; ++i)
            {
                Console.Write(bits[i] + " ");
            }
            Console.WriteLine("]");
        }

        public static void showBitsStandard(int[] bits)
        {
            Console.Write("[ ");
            for (int i = bits.Length - 1; i >= 0; --i) 
            {
                Console.Write(bits[i] + " ");
            }
            Console.WriteLine("]");
        }
    }
}
