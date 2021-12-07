using System;
using System.Collections.Generic;
using CarterService.Extensions;
using Xunit;

namespace CarterService.Tests.Unit;

public class ExtensionFixtures
{
    [Theory]
    [InlineData(typeof(string[]), typeof(string))]
    [InlineData(typeof(List<string>), typeof(string))]
    [InlineData(typeof(string), typeof(char))]
    public void Get_element_type(Type inputType, Type expectedType)
    {
        //Arrange & Act
        var element = inputType.GetAnyElementType();

        //Assert
        Assert.Equal(expectedType, element);
    }

    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(List<string>))]
    [InlineData(typeof(string[]))]
    public void Validate_if_is_iEnumerable(Type input)
    {
        //Arrange & Act
        bool valid = input.IsIEnumerable();

        //Assert
        Assert.True(valid);
    }

    [Fact]
    public void Validate_if_is_not_iEnumerable()
    {
        //Arrange
        var input = typeof(int);

        //Act
        bool valid = input.IsIEnumerable();

        //Assert
        Assert.False(valid);
    }
}
