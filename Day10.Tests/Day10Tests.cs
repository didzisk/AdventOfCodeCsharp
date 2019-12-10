using System;
using System.Linq;
using NUnit.Framework;
using Day10;

namespace Day10.Tests
{
	public class Day10Tests
	{
		[Test]
		public void CanBlockOne()
		{
			IntPoint b = new IntPoint { X = 1, Y = 0 };
			IntPoint t = new IntPoint { X = 4, Y = 3 };
			IntPoint blocker = new IntPoint { X = 3, Y = 2 };
			var isBlocking = Program.Blocks(t, blocker, b, Day10Input.Ex1);
			Assert.That(isBlocking, Is.True);
		}
		[Test]
		public void CanBlockOther()
		{
			IntPoint b = new IntPoint { X = 1, Y = 0 };
			IntPoint blocker = new IntPoint { X = 2, Y = 2 };
			IntPoint t = new IntPoint { X = 3, Y = 4 };
			var isBlocking = Program.Blocks(t, blocker, b, Day10Input.Ex1);
			Assert.That(isBlocking, Is.True);
		}
		[Test]
		public void CanSeeOne()
		{
			var isVisible = Program.Visible(4, 4, 1, 0, Day10Input.Ex1);
			Assert.That(isVisible, Is.True);
		}
		[Test]
		[TestCase(4, 0)]
		[TestCase(0, 2)]
		[TestCase(1, 2)]
		[TestCase(2, 2)]
		[TestCase(3, 2)]
		[TestCase(4, 2)]
		[TestCase(4, 3)]
		[TestCase(4, 4)]
		public void CanSeeFromBiggest(int targetX, int targetY)
		{
			var isVisible = Program.Visible(targetX, targetY, 3, 4, Day10Input.Ex1);
			Assert.That(isVisible, Is.True);
		}
		[Test]
		public void CanShootOne()
		{
			var arr = Day10Input.Ex1.ToArray();
			var dir = new IntPoint {X = 0, Y = -1};
			var b = new IntPoint { X = 3, Y = 4 };
			var badPoint = Program.FirstInDirection(b, dir, arr);
			Assert.That(badPoint.X, Is.EqualTo(3));
			Assert.That(badPoint.Y, Is.EqualTo(2));
			Assert.That(arr[badPoint.Y][badPoint.X], Is.EqualTo('#'));
			Program.ShootAsteroid(badPoint, arr, 1);
			Assert.That(arr[badPoint.Y][badPoint.X], Is.EqualTo('.'));
		}
	}
}
