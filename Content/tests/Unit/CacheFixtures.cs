using System;
using System.Collections.Generic;
using CarterService.Cache;
using CarterServiceTests.Fakes;
using Xunit;

namespace CarterServiceTests.Unit
{
    public class CacheFixtures
    {
        public string FieldSeparator => Key.FieldSeparator;

        [Theory]
        [InlineData(typeof(bool), "bool1")]
        [InlineData(typeof(int), "int1")]
        [InlineData(typeof(FakeRequest), "fakeRequest1")]
        public void Key_create_single_element(Type type, string field)
        {
            //Arrange & Act
            string result = Key.Create(type, field);

            //Assert
            Assert.Equal($"{type.Name}{FieldSeparator}{field}", result);
        }

        [Theory]
        [InlineData(typeof(bool[]), typeof(bool), "bool2")]
        [InlineData(typeof(List<int>), typeof(int), "int2")]
        [InlineData(typeof(string), typeof(char), "string2")]
        public void Key_create_collection_element(Type type, Type elementType, string field)
        {
            //Arrange & Act
            string result = Key.Create(type, field);

            //Assert
            Assert.Equal($"{type.Name}{FieldSeparator}{elementType.Name}{FieldSeparator}{field}", result);
        }

        [Theory]
        [InlineData("bool3")]
        public void Key_create_single_element_generic_invokation(string field)
        {
            //Arrange & Act
            string result = Key.Create<bool>(field);

            //Assert
            Assert.Equal($"{typeof(bool).Name}{FieldSeparator}{field}", result);
        }

        [Theory]
        [InlineData("int4")]
        public void Key_create_collection_element_generic_invocation(string field)
        {
            //Arrange & Act
            string result = Key.Create<List<int>>(field);

            //Assert
            Assert.Equal($"{typeof(List<int>).Name}{FieldSeparator}{typeof(int).Name}{FieldSeparator}{field}", result);
        }
    }
}
