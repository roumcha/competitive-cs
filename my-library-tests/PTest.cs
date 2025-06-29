namespace my_library_tests;

using System.Numerics;
using template;
using static template.MyLib;

public class PTest(ITestOutputHelper _output) {

  [Fact]
  public void Construct_Valid() {
    P p = (3, 4);
    p.ShouldBe(new P(3, 4));
    p.X.ShouldBe(3);
    p.Y.ShouldBe(4);
  }

  [Fact]
  public void Construct_Invalid() {
    P p = (3, 4);
    p.ShouldNotBe(new P(4, 3));
    p.X.ShouldBe(3);
    p.Y.ShouldBe(4);
  }

  [Fact]
  public void Deconstruct_Valid() {
    P p = new(3, 4);
    var (px, py) = p;
    px.ShouldBe(3);
    py.ShouldBe(4);
  }

  public static IEnumerable<object[]> Data_InInterval_Valid() {
    yield return new object[] { new P(0, 0), new P(0, 0), new P(0, 0), false };
    yield return new object[] { new P(0, 0), new P(-1, 0), new P(0, 0), false };
    yield return new object[] { new P(0, 0), new P(0, -1), new P(0, 0), false };
    yield return new object[] { new P(0, 0), new P(0, 0), new P(1, 0), false };
    yield return new object[] { new P(0, 0), new P(0, 0), new P(0, 1), false };
    yield return new object[] { new P(0, 0), new P(0, 0), new P(1, 1), true };
  }

  [Theory]
  [MemberData(nameof(Data_InInterval_Valid))]
  public void InInterval_Valid(P p, P ulIncl, P drExcl, bool expected) {
    p.InInterval(ulIncl, drExcl).ShouldBe(expected);
  }

}
