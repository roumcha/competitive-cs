namespace my_library_tests;

using template;
using Xunit.Abstractions;
using static template.MyLib;

public class TemplateTest1(ITestOutputHelper _output) {
  [Theory]
  [InlineData(0, 1, 0)]
  [InlineData(1, 1, 1)]
  [InlineData(5, 2, 10)]
  [InlineData(61, 30, 232714176627630544)]
  public void nCr_Valid(int n, int r, long expected)
  => Assert.Equal(expected, nCr(n, r));

  [Theory]
  [InlineData(62, 30)]
  public void nCr_Overflow(int n, int r)
  => Assert.Throws<OverflowException>(() => nCr(n, r));

  [Theory]
  [InlineData(0, 1, 0)]
  [InlineData(1, 1, 1)]
  [InlineData(5, 2, 20)]
  [InlineData(29, 14, 6761440164390912000)]
  public void nPr_Valid(int n, int r, long expected)
    => Assert.Equal(nPr(n, r), expected);

  [Theory]
  [InlineData(30, 14)]
  public void nPr_Overflow(int n, int r)
  => Assert.Throws<OverflowException>(() => nPr(n, r));
}
