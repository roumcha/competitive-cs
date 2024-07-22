namespace my_library_tests;

using System.Numerics;
using FluentAssertions;
using template;
using Xunit.Abstractions;
using static template.MyLib;

public class TemplateTest_String(ITestOutputHelper _output) {
  [Theory]
  [InlineData("", true)]
  [InlineData("A", true)]
  [InlineData("AA", true)]
  [InlineData("Aa", false)]
  [InlineData("AAA", true)]
  [InlineData("AaA", true)]
  [InlineData("Aaa", false)]
  public static void IsPalindrome_Char_Valid(string input, bool expected) {
    IsPalindrome(input.AsSpan()).Should().Be(expected);
  }

  [Fact]
  public static void IsPalindrome_Int32_Valid() {
    IsPalindrome([1, 2, 1]).Should().Be(true);
    IsPalindrome([1, 2, 2]).Should().Be(false);
  }
}
