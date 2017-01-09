using System;
using System.Collections.Generic;
using System.Linq;
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
            var stringPropertyClass = new StringPropertyClass
            {
                StringProperty = propertyValue
            };

            StringPropertyClass clone = _cloner.Clone(stringPropertyClass);            

            Assert.That(clone, Is.Not.EqualTo(stringPropertyClass));
            Assert.That(clone.StringProperty, Is.EqualTo(stringPropertyClass.StringProperty));
        }

        [Test]
        public void Should_clone_int_property()
        {
            var intPropertyClass = new IntPropertyClass()
            {
                IntProperty = 1
            };

            IntPropertyClass clone = _cloner.Clone(intPropertyClass);

            Assert.That(clone, Is.Not.EqualTo(intPropertyClass));
            Assert.That(clone.IntProperty, Is.EqualTo(intPropertyClass.IntProperty));
        }

        [TestCase(1)]
        [TestCase(null)]
        public void Should_clone_nullable_int_property(int? propertyValue)
        {
            var nullableIntPropertyClass = new NullableIntPropertyClass
            {
                NullableIntProperty = 1
            };

            NullableIntPropertyClass clone = _cloner.Clone(nullableIntPropertyClass);

            Assert.That(clone, Is.Not.EqualTo(nullableIntPropertyClass));
            Assert.That(clone.NullableIntProperty, Is.EqualTo(nullableIntPropertyClass.NullableIntProperty));
        }

        [Test]
        public void Should_clone_parent_child_object()
        {
            var parentChildClass = new ParentChildClass
            {
                StringPropertyClass = new StringPropertyClass
                {
                    StringProperty = "a"
                }
            };

            ParentChildClass clone = _cloner.Clone(parentChildClass);

            Assert.That(clone, Is.Not.EqualTo(parentChildClass));
            Assert.That(clone.StringPropertyClass, Is.Not.EqualTo(parentChildClass.StringPropertyClass));
            Assert.That(clone.StringPropertyClass.StringProperty, Is.EqualTo(parentChildClass.StringPropertyClass.StringProperty));
        }

        [Test]
        public void Should_clone_parent_child_object_with_null_child()
        {
            var parentChildClass = new ParentChildClass();            

            ParentChildClass clone = _cloner.Clone(parentChildClass);

            Assert.That(clone, Is.Not.EqualTo(parentChildClass));
            Assert.That(clone.StringPropertyClass, Is.EqualTo(parentChildClass.StringPropertyClass));            
        }

        [Test]
        public void Should_clone_parent_child_child_object()
        {
            var parentChildChildClass = new ParentChildChildClass
            {
                ParentChildClass = new ParentChildClass
                {
                    StringPropertyClass = new StringPropertyClass
                    {
                        StringProperty = "a"
                    }
                }
            };

            ParentChildChildClass clone = _cloner.Clone(parentChildChildClass);

            Assert.That(clone, Is.Not.EqualTo(parentChildChildClass));
            Assert.That(clone.ParentChildClass, Is.Not.EqualTo(parentChildChildClass.ParentChildClass));
            Assert.That(clone.ParentChildClass.StringPropertyClass, Is.Not.EqualTo(parentChildChildClass.ParentChildClass.StringPropertyClass));
            Assert.That(clone.ParentChildClass.StringPropertyClass.StringProperty, Is.EqualTo(parentChildChildClass.ParentChildClass.StringPropertyClass.StringProperty));
        }

        [Test]
        public void Should_clone_generic_list_of_string()
        {
            const string listItem1 = "string 1";
            const string listItem2 = "string 2";

            var genericList = new List<string>
            {
                listItem1,
                listItem2
            };

            List<string> clone = _cloner.Clone(genericList);
            
            Assert.That(clone.Count, Is.EqualTo(genericList.Count));            
            Assert.That(clone.Any(x => x == listItem1));
            Assert.That(clone.Any(x => x == listItem2));
        }

        [Test]
        public void Should_clone_generic_list_of_int()
        {
            const int listItem1 = 1;
            const int listItem2 = 2;

            var genericList = new List<int>
            {
                listItem1,
                listItem2
            };

            List<int> clone = _cloner.Clone(genericList);

            Assert.That(clone.Count, Is.EqualTo(genericList.Count));
            Assert.That(clone.Any(x => x == listItem1));
            Assert.That(clone.Any(x => x == listItem2));
        }

        [Test]
        public void Should_clone_generic_list_of_non_primitives()
        {
            var listItem1 = new StringPropertyClass { StringProperty = "1" };
            var listItem2 = new StringPropertyClass { StringProperty = "2" };

            var genericList = new List<StringPropertyClass>
            {
                listItem1,
                listItem2
            };

            List<StringPropertyClass> clone = _cloner.Clone(genericList);

            Assert.That(clone.Count, Is.EqualTo(genericList.Count));
            Assert.That(clone[0], Is.Not.EqualTo(genericList[0]));
            Assert.That(clone.Any(x => x.StringProperty == listItem1.StringProperty));
            Assert.That(clone[1], Is.Not.EqualTo(genericList[1]));
            Assert.That(clone.Any(x => x.StringProperty == listItem2.StringProperty));
        }

        [Test]
        public void Should_ignore_read_only_int_property()
        {
            var intPropertyClass = new ReadOnlyIntPropertyClass();            

            ReadOnlyIntPropertyClass clone = _cloner.Clone(intPropertyClass);

            Assert.That(clone, Is.Not.EqualTo(intPropertyClass));            
        }

        [Test]
        public void Should_ignore_read_only_child_property()
        {
            var parentReadOnlyChildClass = new ParentReadOnlyChildClass();

            ParentReadOnlyChildClass clone = _cloner.Clone(parentReadOnlyChildClass);

            Assert.That(clone, Is.Not.EqualTo(parentReadOnlyChildClass));
        }

        [Test]
        public void Should_throw_for_when_the_object_types_without_a_parameterless_constructor()
        {
            var noParameterlessConstructorClass = new NoParameterlessConstructorClass("parameter value");

            var exception = Assert.Throws<CloneRangerException>(() => _cloner.Clone(noParameterlessConstructorClass));
            Assert.That(exception.Message, Is.EqualTo($"The class {typeof(NoParameterlessConstructorClass).Name} has no parameterless constructor and no clone construction function has been provided."));
        }

        [Test]
        public void Should_clone_a_uri()
        {
            var uri = new Uri("https://www.google.com");

            Uri clone = _cloner.Clone(uri, () => new Uri("https://www.google.com"));

            //Assert.That(clone, Is.Not.EqualTo(uri));
            Assert.That(clone.AbsoluteUri, Is.EqualTo(uri.AbsoluteUri));            
        }
    }
}
