using CloneRanger.Tests.test.TestClasses;
using NUnit.Framework;

namespace CloneRanger.Tests.test
{
    [TestFixture]
    public class Cloner_Clone
    {
        private Cloner _cloner;

        [SetUp]
        public void TestSetup()
        {            
            _cloner = new Cloner();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("this is a string")]
        public void Should_clone_string_property(string propertyValue)
        {
            var simple = new StringPropertyClass
            {
                StringProperty = propertyValue
            };

            StringPropertyClass clone = _cloner.Clone(simple);            

            Assert.That(clone, Is.Not.EqualTo(simple));
            Assert.That(clone.StringProperty, Is.EqualTo(simple.StringProperty));
        }

        [Test]
        public void Should_clone_int_property()
        {
            var simple = new IntPropertyClass()
            {
                IntProperty = 1
            };

            IntPropertyClass clone = _cloner.Clone(simple);

            Assert.That(clone, Is.Not.EqualTo(simple));
            Assert.That(clone.IntProperty, Is.EqualTo(simple.IntProperty));
        }

        [TestCase(1)]
        [TestCase(null)]
        public void Should_clone_nullable_int_property(int? propertyValue)
        {
            var simple = new NullableIntPropertyClass
            {
                NullableIntProperty = 1
            };

            NullableIntPropertyClass clone = _cloner.Clone(simple);

            Assert.That(clone, Is.Not.EqualTo(simple));
            Assert.That(clone.NullableIntProperty, Is.EqualTo(simple.NullableIntProperty));
        }

        [Test]
        public void Should_clone_parent_child_object()
        {
            var parentChild = new ParentChildClass
            {
                StringPropertyClass = new StringPropertyClass
                {
                    StringProperty = "a"
                }
            };

            ParentChildClass clone = _cloner.Clone(parentChild);

            Assert.That(clone, Is.Not.EqualTo(parentChild));
            Assert.That(clone.StringPropertyClass, Is.Not.EqualTo(parentChild.StringPropertyClass));
            Assert.That(clone.StringPropertyClass.StringProperty, Is.EqualTo(parentChild.StringPropertyClass.StringProperty));
        }
    }
}
