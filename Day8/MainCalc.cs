using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day8
{
	public class MainCalc
	{
		public static int CalcPart1()
		{
			var layerSize = 25 * 6;
			var layers = StringExtensions.Split(Day8Input.Text, layerSize);
			var minNumZeros = 2000000;
			string minLayer=string.Empty;
			foreach (string layer in layers)
			{
				var numZeros = layer.Count(c => c == '0');
				if (numZeros < minNumZeros)
				{
					minLayer = layer;
					minNumZeros=numZeros;
				}
			}

			var numOnes = minLayer.Count(c => c == '1');
			var numTwos = minLayer.Count(c => c == '2');
			return numOnes * numTwos;
		}

		static char GetColorAtIndex(List<string> layers, int ind)
		{
			var color = '2';
			foreach (var layer in layers)
			{
				switch (layer[ind])
				{
					case '0':
						return ' ';
					case '1':
						return '1';
					default:
						continue;
				}
			}
			return color;
		}
		public static void CalcPart2()
		{
			var layerSize = 25 * 6;
			var layers = StringExtensions.Split(Day8Input.Text, layerSize).ToList();
			var x = 0;
			var outp = string.Empty;
			for (int i = 0; i < layers[0].Length; i++)
			{
				x++;
				outp += GetColorAtIndex(layers,i);
				if (x == 25)
				{
					Console.WriteLine(outp);
					x = 0;
					outp = string.Empty;
				}
			}
		}
	}
}
