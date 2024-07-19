// ログあり: dotnet test --logger "console;verbosity=detailed"

namespace my_library_tests;

using System.Numerics;
using FluentAssertions;
using template;
using Xunit.Abstractions;
using static template.MyLib;

public class TemplateTest_Math(ITestOutputHelper _output) {
  [Theory]
  [InlineData(-2, 0, 1)]
  [InlineData(-2, 1, -2)]
  [InlineData(-2, 2, 4)]
  [InlineData(-2, 30, 1073741824)]
  [InlineData(-2, 31, -2147483648)]
  [InlineData(-1, 0, 1)]
  [InlineData(-1, 1, -1)]
  [InlineData(-1, 2, 1)]
  [InlineData(-1, 4294967294, 1)]
  [InlineData(-1, 4294967295, -1)]
  [InlineData(0, 0, 1)]
  [InlineData(0, 1, 0)]
  [InlineData(0, 2, 0)]
  [InlineData(0, 4294967295, 0)]
  [InlineData(1, 0, 1)]
  [InlineData(1, 1, 1)]
  [InlineData(1, 2, 1)]
  [InlineData(1, 4294967294, 1)]
  [InlineData(1, 4294967295, 1)]
  [InlineData(2, 0, 1)]
  [InlineData(2, 1, 2)]
  [InlineData(2, 2, 4)]
  [InlineData(2, 30, 1073741824)]
  [InlineData(24, 6, 191102976)]
  public void Pown_Int32_Valid(int @base, uint exp, int expected) {
    Pown(@base, exp).Should().Be(expected);
  }

  [Theory]
  [InlineData(-2, 32)]
  [InlineData(2, 31)]
  [InlineData(24, 7)]
  public void Pown_Int32_Overflow(int @base, uint exp) {
    var func = () => Pown(@base, exp);
    func.Should().Throw<OverflowException>();
  }

  [Theory]
  [InlineData(0, 1, 0)]
  [InlineData(1, 1, 1)]
  [InlineData(5, 2, 10)]
  [InlineData(61, 30, 232714176627630544)]
  public void nCr_Valid(int n, int r, long expected) {
    Assert.Equal(expected, nCr(n, r));
  }

  [Theory]
  [InlineData(62, 30)]
  public void nCr_Overflow(int n, int r) {
    Assert.Throws<OverflowException>(() => nCr(n, r));
  }

  [Theory]
  [InlineData(0, 1, 0)]
  [InlineData(1, 1, 1)]
  [InlineData(5, 2, 20)]
  [InlineData(29, 14, 6761440164390912000)]
  public void nPr_Valid(int n, int r, long expected) {
    Assert.Equal(nPr(n, r), expected);
  }

  [Theory]
  [InlineData(30, 14)]
  public void nPr_Overflow(int n, int r) {
    Assert.Throws<OverflowException>(() => nPr(n, r));
  }
}
