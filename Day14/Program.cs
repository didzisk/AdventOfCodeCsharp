using System;
using System.Collections.Generic;
using System.Linq;

using INT = System.Int64;

namespace Day14
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			ParseInput(Day14Input.Ex1, 1);//31
			//ParseInput(Day14Input.Ex2);//165
			ParseInput(Day14Input.Ex3, 1);//13312 
			ParseInput(Day14Input.Ex3, 82892753);//13312 
			var fuelMax=FindMax(Day14Input.official);
			Console.WriteLine($"{fuelMax}");
			//ParseInput(Day14Input.Ex4);
			//ParseInput(Day14Input.official);
		}

		public struct ComponentContents
		{
			public string Name { get; set; }
			public INT QuantityInBatch { get; set; }
			public List<ComponentContents> Recipe { get; set; }
			public INT QuantityInRecipe { get; set; }
			public INT QuantityProduced { get; set; }
			public INT QuantityUsed { get; set; }
		}

		public static (INT qty, string name) ParseOneComponent(string inp)
		{
			var s = inp.Trim().Split(' ');
			var qty = INT.Parse(s[0]);
			return (qty, s[1]);
		}
		public static INT ParseInput(string input, INT finalCount)
		{
			var inputStrings = input.Split("\r\n").Select(x=>x.Split("=>")).ToList();
			var recipes = new Dictionary<string,ComponentContents>();
			foreach (var inputString in inputStrings)
			{
				var (qty,s) = ParseOneComponent(inputString[1]);
				var comp = new ComponentContents() {Name = s, QuantityInBatch = qty, Recipe = new List<ComponentContents>()};

				var recipeStrings=inputString[0].Split(',');

				foreach (var recipeString in recipeStrings)
				{
					var (rqty, rs) = ParseOneComponent(recipeString);
					var rcomp=new ComponentContents{Name = rs, QuantityInRecipe = rqty};
					comp.Recipe.Add(rcomp);
				}
				recipes.Add(comp.Name,comp);
			}
//			Console.WriteLine($"Input count {inputStrings.Count()}");
//			Console.WriteLine($"Recipe count {recipes.Count}");
			var fuelLine = recipes.First(r => r.Key == "FUEL");
//			Console.WriteLine($"Fuel {fuelLine.Value.QuantityInBatch}");
			var oreComp = new ComponentContents { Name = "ORE", QuantityProduced = 0, QuantityInBatch = 1};
			recipes.Add("ORE", oreComp);
			//TraverseComponents(fuelLine.Value,1, recipes);

			NeedMoreOf(fuelLine.Value, finalCount, recipes);
			var oreRes = recipes["ORE"].QuantityProduced;
			Console.WriteLine($"Ore needed {oreRes} - fuel produced {finalCount}");
			return oreRes;
		}

		public static void NeedMoreOf(ComponentContents componentLine, INT qtyNeeded, Dictionary<string, ComponentContents> recipes)
		{
			var recipeLine = recipes[componentLine.Name];
			recipeLine.QuantityUsed += qtyNeeded;
			while (recipeLine.QuantityProduced < recipeLine.QuantityUsed)
			{
				var numBatches = (recipeLine.QuantityUsed - recipeLine.QuantityProduced) / recipeLine.QuantityInBatch;
				if (numBatches == 0)
					numBatches = 1;
				recipeLine.QuantityProduced += recipeLine.QuantityInBatch*numBatches;
				//produce one batch
				if (recipeLine.Recipe == null)
				{

				}
				else
				{
					foreach (var rComp in recipeLine.Recipe)
					{
						var subcomponentRecipeLine = recipes[rComp.Name];
						NeedMoreOf(subcomponentRecipeLine, rComp.QuantityInRecipe*numBatches, recipes);

					}
				}

			}
			recipes[componentLine.Name] = recipeLine;
//			Console.WriteLine($"Produced {qtyNeeded} {componentLine.Name}");
		}

		public static INT FindMax(string input)
		{
			INT fuelMax= 4366000;
			INT oreReq = 1;
			while (oreReq < 1000000000000)
			{
				oreReq = ParseInput(input, fuelMax);
				if (oreReq > 1000000000000)
					return fuelMax;
				fuelMax+=1;
			}

			return fuelMax;
		}
	}
}
