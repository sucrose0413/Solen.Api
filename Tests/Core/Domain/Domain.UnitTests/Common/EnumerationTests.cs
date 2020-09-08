using System;
using NUnit.Framework;
using Solen.Core.Domain.Common;

namespace Domain.UnitTests.Common
{
    [TestFixture]
    public class EnumerationTests
    {
        [Test]
        public void GetAll_WhenCalled_ReturnAllValues()
        {
            var result = Enumeration.GetAll<EnumTest>();

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void FromValue_ValidValue_ReturnTheItemBasedOnItsValue()
        {
            var result = Enumeration.FromValue<EnumTest>(1);

            Assert.That(result, Is.TypeOf<Value1>());
        }

        [Test]
        public void FromValue_InvalidValue_ThrowApplicationException()
        {
            Assert.That(() => Enumeration.FromValue<EnumTest>(10),
                Throws.Exception.TypeOf<ApplicationException>());
        }

        [Test]
        public void FromName_ValidName_ReturnTheItemBasedOnItsName()
        {
            var result = Enumeration.FromName<EnumTest>("Value 2");

            Assert.That(result, Is.TypeOf<Value2>());
        }

        [Test]
        public void FromName_InvalidName_ThrowApplicationException()
        {
            Assert.That(() => Enumeration.FromName<EnumTest>("invalid Name"),
                Throws.Exception.TypeOf<ApplicationException>());
        }

        abstract class EnumTest : Enumeration
        {
            protected EnumTest(int value, string name) : base(value, name)
            {
            }
        }

        class Value1 : EnumTest
        {
            public Value1() : base(1, "Value 1")
            {
            }
        }
        
        class Value2 : EnumTest
        {
            public Value2() : base(2, "Value 2")
            {
            }
        }
    }
}