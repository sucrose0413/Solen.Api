using System.Collections.Generic;
using NUnit.Framework;
using Solen.Core.Domain.Common;

namespace Domain.UnitTests.Common
{
    [TestFixture]
    public class ValueObjectTests
    {
        [Test]
        public void Equals_DifferentValues_ShouldReturnFalse()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 1);

            Assert.That(point1.Equals(point2), Is.False);
        }

        [Test]
        public void Equals_GivenMatchingValues_ShouldReturnTrue()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(1, 2);

            Assert.That(point1.Equals(point2), Is.True);
        }

        private class Point : ValueObject
        {
            private int X { get; set; }
            private int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return X;
                yield return Y;
            }
        }
    }
}