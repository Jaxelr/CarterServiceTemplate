using System;
using System.Collections.Generic;
using CarterService.Cache;
using CarterServiceTests.Fakes;
using Xunit;

namespace CarterService.Tests.Unit
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
            Assert.Contains(type.Name, result);
            Assert.Contains(field, result);
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
            Assert.Contains(type.Name, result);
            Assert.Contains(elementType.Name, result);
            Assert.Contains(field, result);
        }

        [Theory]
        [InlineData("bool3")]
        public void Key_create_single_element_generic_invokation(string field)
        {
            //Arrange & Act
            string result = Key.Create<bool>(field);

            //Assert
            Assert.Contains(typeof(bool).Name, result);
            Assert.Contains(field, result);
        }

        [Theory]
        [InlineData("int4")]
        public void Key_create_collection_element_generic_invocation(string field)
        {
            //Arrange & Act
            string result = Key.Create<List<int>>(field);

            //Assert
            Assert.Contains(typeof(List<int>).Name, result);
            Assert.Contains(typeof(int).Name, result);
            Assert.Contains(field, result);
        }

        [Theory]
        [InlineData(typeof(bool), "bool5", "bool6")]
        [InlineData(typeof(int), "int5", "int6")]
        [InlineData(typeof(FakeRequest), "fakeRequest5", "fakeRequest6")]
        public void Key_create_single_element_multiple_fields(Type type, params string[] fields)
        {
            //Arrange & Act
            string result = Key.Create(type, fields);

            //Assert
            Assert.Contains(type.Name, result);
            Assert.All(fields, item => result.Contains(item));
        }

        [Theory]
        [InlineData("int7", "int8")]
        public void Key_create_collection_element_generic_invocation_multiple_fields(params string[] fields)
        {
            //Arrange & Act
            string result = Key.Create<List<int>>(fields);

            //Assert
            Assert.Contains(typeof(List<int>).Name, result);
            Assert.Contains(typeof(int).Name, result);
            Assert.All(fields, item => result.Contains(item));
        }


        [Theory]
        [InlineData(typeof(bool[]), typeof(bool), "bool9", "bool10")]
        [InlineData(typeof(List<int>), typeof(int), "int9", "int10")]
        [InlineData(typeof(string), typeof(char), "string9", "string10")]
        public void Key_create_collection_element_multiple_fields(Type type, Type elementType, params string[] fields)
        {
            //Arrange & Act
            string result = Key.Create(type, fields);

            //Assert
            Assert.Contains(type.Name, result);
            Assert.Contains(elementType.Name, result);
            Assert.All(fields, item => result.Contains(item));
        }
    }
}
