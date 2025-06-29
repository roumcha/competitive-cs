namespace my_library_tests;

using template;
using static template.MyLib;

public class TemplateTest_PT(ITestOutputHelper _output) {
  [Fact]
  public void Construct_Valid() {
    P<int> p = (3, 4);
    p.ShouldBe(new P<int>(3, 4));
    p.x.ShouldBe(3);
    p.y.ShouldBe(4);
  }

  [Fact]
  public void Construct_Invalid() {
    P<int> p = (3, 4);
    p.ShouldNotBe(new P<int>(4, 3));
    p.x.ShouldBe(3);
    p.y.ShouldBe(4);
  }

  [Fact]
  public void Deconstruct_Valid() {
    P<int> p = new(3, 4);
    var (px, py) = p;
    px.ShouldBe(3);
    py.ShouldBe(4);
  }

  public static IEnumerable<object[]> Data_InInterval_Valid() {
    yield return new object[] { new P<int>(0, 0), new P<int>(0, 0), new P<int>(0, 0), false };
    yield return new object[] { new P<int>(0, 0), new P<int>(-1, 0), new P<int>(0, 0), false };
    yield return new object[] { new P<int>(0, 0), new P<int>(0, -1), new P<int>(0, 0), false };
    yield return new object[] { new P<int>(0, 0), new P<int>(0, 0), new P<int>(1, 0), false };
    yield return new object[] { new P<int>(0, 0), new P<int>(0, 0), new P<int>(0, 1), false };
    yield return new object[] { new P<int>(0, 0), new P<int>(0, 0), new P<int>(1, 1), true };
  }

  [Theory]
  [MemberData(nameof(Data_InInterval_Valid))]
  public void InInterval_Valid(P<int> p, P<int> ulIncl, P<int> drExcl, bool expected) {
    p.InInterval(ulIncl, drExcl).ShouldBe(expected);
  }

  [Theory]
  [InlineData(0, 0, 0, 0, 0)]
  [InlineData(0, 5, 0, 10, 5)]
  [InlineData(10, 20, 30, 45, 25)]
  [InlineData(-10, 2, 10, 3, 20)]
  [InlineData(0, 0, int.MaxValue, int.MaxValue, int.MaxValue)]
  public void DistC_Valid(int px, int py, int qx, int qy, int expected) {
    new P<int>(px, py).DistC(new P<int>(qx, qy)).ShouldBe(expected);
  }
}
