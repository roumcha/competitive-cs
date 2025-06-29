namespace my_library_tests;

using System.Numerics;
using template;
using static template.MyLib;

public class TemplateTest_Seq(ITestOutputHelper _output) {
  [Theory]
  [InlineData("", "", true)]
  [InlineData("", "a", true)]
  [InlineData("a", "", false)]
  [InlineData("a", "b", false)]
  [InlineData("abc", "abc", true)]
  [InlineData("ab", "abc", true)]
  [InlineData("abc", "ab", false)]
  [InlineData("abc", "acb", false)]
  [InlineData("abc", "abdc", true)]
  public static void IsSubsequenceOf_Vaild(string str, string sub, bool expected) {
    str.IsSubsequenceOf(sub).ShouldBe(expected);
  }

  [Theory]
  [InlineData("", true)]
  [InlineData("A", true)]
  [InlineData("AA", true)]
  [InlineData("Aa", false)]
  [InlineData("AAA", true)]
  [InlineData("AaA", true)]
  [InlineData("Aaa", false)]
  public static void IsPalindrome_Valid(string input, bool expected) {
    IsPalindrome(input.AsSpan()).ShouldBe(expected);
  }
}
