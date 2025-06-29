namespace my_library_tests;

using System.Numerics;
using System.Runtime.InteropServices;
using template;
using static template.MyLib;

public class TemplateTest_Cout(ITestOutputHelper _output) {
  [Fact]
  public void Print2D_CharArray_Speed() {
    var cout = new COut(Stream.Null);
    var data = new char[10000, 10000];
    MemoryMarshal.CreateSpan(ref data[0, 0], data.Length).Fill('*');

    Should.CompleteIn(
      () => cout.Print2D(data),
      TimeSpan.FromMilliseconds(200)
    );
  }

  [Fact]
  public void Print2D_IntArray_Speed() {
    var cout = new COut(Stream.Null);
    var data = new int[3000, 3000];
    MemoryMarshal.CreateSpan(ref data[0, 0], data.Length).Fill(100);

    Should.CompleteIn(
      () => cout.Print2D(data),
      TimeSpan.FromMilliseconds(600)
    );
  }
}
