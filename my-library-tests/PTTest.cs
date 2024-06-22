namespace my_library_tests;

using System.Numerics;
using FluentAssertions;
using template;
using Xunit.Abstractions;
using static template.MyLib;

public class PTTest(ITestOutputHelper _output) {
  [Fact]
  public void Construct_Valid() {
    P<int> p = (3, 4);
    p.Should().Be(new P<int>(3, 4));
    p.X.Should().Be(3);
    p.Y.Should().Be(4);
  }

  [Fact]
  public void Construct_Invalid() {
    P<int> p = (3, 4);
    p.Should().NotBe(new P<int>(4, 3));
    p.X.Should().Be(3);
    p.Y.Should().Be(4);
  }

  [Fact]
  public void Deconstruct_Valid() {
    P<int> p = new(3, 4);
    var (px, py) = p;
    px.Should().Be(3);
    py.Should().Be(4);
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
    p.InInterval(ulIncl, drExcl).Should().Be(expected);
  }
}
