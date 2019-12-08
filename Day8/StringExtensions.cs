using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day8
{
	public static class StringExtensions
	{
		public static IEnumerable<string> Split(string str, int chunkSize)
		{
			return Enumerable.Range(0, str.Length / chunkSize)
				.Select(i => str.Substring(i * chunkSize, chunkSize));
		}
	}
}
