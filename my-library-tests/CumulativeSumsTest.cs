namespace MyLibrary.Tests;

using template;
using static template.MyLib;

public class CSumTest(ITestOutputHelper _output) {

  [Fact]
  public void Construct_Valid() {
    var func = () => new CSum<int>(new[] { 1, 4, 5, 2, 3 }.AsEnumerable());
    func.ShouldNotThrow();
  }

  [Theory]
  [InlineData(0, 0, 0)]
  [InlineData(0, 1, 1)]
  [InlineData(0, 2, 5)]
  [InlineData(0, 5, 15)]
  [InlineData(1, 1, 0)]
  [InlineData(1, 4, 11)]
  [InlineData(5, 5, 0)]
  public void Get_Valid(int fromIncl, int toExcl, int expected) {
    var csum = new CSum<int>([1, 4, 5, 2, 3]);
    csum.Get(fromIncl, toExcl).ShouldBe(expected);
  }

  [Fact]
  public void Add_Valid() {
    var csum = new CSum<int>([1, 4, 5, 2, 3]);
    csum.Add(1);
    csum.Get(0, 6).ShouldBe(16);
    csum.Get(5, 6).ShouldBe(1);
  }

}


public class CSum2DTest(ITestOutputHelper _output) {
  // TODO
}


public class CSum3DTest(ITestOutputHelper _output) {
  // TODO
}
