using System.Numerics;
using NUnit.Framework;
using Titan.Components;
using Titan.Core.Math;

namespace Titan.Tests
{
    [TestFixture]
    internal class BoundingBox2DTests
    {
        [TestCase(1,1)]
        [TestCase(5,5)]
        [TestCase(15,5)]
        [TestCase(5, 15)]
        [TestCase(21, 21)]
        [TestCase(21, 10)]
        [TestCase(10, 21)]
        public void Intersect_NoIntersect_ReturnFalse(int x, int y)
        {
            var box = new BoundingBox2D(new Vector2(10,10), new Size(10, 10));

            var result = box.Intersects(new Vector2(x, y));
            Assert.That(result, Is.False);
        }

        [TestCase(10, 10)]
        [TestCase(15, 15)]
        [TestCase(20, 20)]
        [TestCase(15, 20)]
        [TestCase(10, 20)]
        public void Intersect_Intersects_ReturnTrue(int x, int y)
        {
            var box = new BoundingBox2D(new Vector2(10, 10), new Size(10, 10));

            var result = box.Intersects(new Vector2(x, y));
            Assert.That(result, Is.True);
        }
    }
}
