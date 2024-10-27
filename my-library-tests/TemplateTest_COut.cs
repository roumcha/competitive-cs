namespace my_library_tests;

using System.Numerics;
using System.Runtime.InteropServices;
using FluentAssertions;
using FluentAssertions.Extensions;
using template;
using Xunit.Abstractions;
using static template.MyLib;

public class TemplateTest_Cout(ITestOutputHelper _output) {
  [Fact]
  public void Print2D_CharArray_Speed() {
    var cout = new COut(Stream.Null);
    var data = new char[10000, 10000];
    MemoryMarshal.CreateSpan(ref data[0, 0], data.Length).Fill('*');
    Action action = () => cout.Print2D(data);
    action.ExecutionTime().Should().BeLessThan(200.Milliseconds());
  }

  [Fact]
  public void Print2D_IntArray_Speed() {
    var cout = new COut(Stream.Null);
    var data = new int[3000, 3000];
    MemoryMarshal.CreateSpan(ref data[0, 0], data.Length).Fill(100);
    Action action = () => cout.Print2D(data);
    action.ExecutionTime().Should().BeLessThan(600.Milliseconds());
  }
}
