using System;
using NUnit.Framework;
using Titan.ECS.Components;

namespace Titan.Tests
{
    [TestFixture]
    internal class ComponentIdTests
    {
        [Test]
        public void Id_Always_ReturnGreaterThanZero()
        {
            var result = ComponentId<short>.Id;

            Assert.That(result, Is.GreaterThan(0));
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(short))]
        [TestCase(typeof(ushort))]
        [TestCase(typeof(sbyte))]
        [TestCase(typeof(byte))]
        [TestCase(typeof(long))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void Id_Always_ReturnPowerOfTwo(Type type)
        {
            var id = (ulong)typeof(ComponentId<>).MakeGenericType(type).GetProperty("Id").GetValue(null);

            var result = (id - 1ul) & id;

            Assert.That(result, Is.Zero);
        }
    }
}
