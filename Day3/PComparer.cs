using System;
using System.Collections.Generic;
using System.Text;

namespace Day3
{
	class PComparer : IEqualityComparer<Point>
	{
		// Products are equal if their names and product numbers are equal.
		public bool Equals(Point x, Point y)
		{

			//Check whether the compared objects reference the same data.
			if (Object.ReferenceEquals(x, y)) return true;

			//Check whether any of the compared objects is null.
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			//Check whether the products' properties are equal.
			return x.X == y.X && x.Y == y.Y;
		}

		// If Equals() returns true for a pair of objects 
		// then GetHashCode() must return the same value for these objects.

		public int GetHashCode(Point product)
		{
			//Check whether the object is null
			if (Object.ReferenceEquals(product, null)) return 0;

			//Get hash code for the Name field if it is not null.
			int hashPointX =  product.X.ToString().GetHashCode();

			//Get hash code for the Code field.
			int hashPointY = product.Y.ToString().GetHashCode();

			//Calculate the hash code for the product.
			return hashPointX ^ hashPointY;
		}

	}
}
