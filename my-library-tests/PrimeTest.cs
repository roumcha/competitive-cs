namespace my_library_tests;

using FluentAssertions;
using template;
using Xunit.Abstractions;
using static template.MyLib;

public class PrimeNumberTest(ITestOutputHelper _output) {
  [Theory]
  [InlineData(2, true)]
  [InlineData(4, false)]
  [InlineData(uint.MaxValue - 1, false)]
  [InlineData(uint.MaxValue, false)]
  [InlineData(10_000_000_000_000_061, true)]
  public void IsPrime_Valid(ulong n, bool expected) {
    PrimeNumber.IsPrime(n).Should().Be(expected);
  }

  [Theory]
  [InlineData(0)]
  public void IsPrime_OutOfRange(uint n) {
    var func = () => PrimeNumber.IsPrime(n);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void Sieve_Valid() {
    PrimeNumber.Sieve(8)
      .Should()
      .EndWith([true, true, false, true, false, true, false]);
  }

  [Fact(Timeout = 500)]
  public void Sieve_OutOfRange() {
    var func = () => PrimeNumber.Sieve(0);
    func.Should().Throw<ArgumentOutOfRangeException>();
    Assert.Throws<ArgumentOutOfRangeException>(() => PrimeNumber.Sieve((uint)Array.MaxLength));
  }

  [Fact(Timeout = 500)]
  public void Sieve_OutOfRange2() {
    var func = () => PrimeNumber.Sieve((uint)Array.MaxLength);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }

  [Theory]
  [InlineData(8, new uint[] { 0, 0, 2, 3, 2, 5, 2, 7, 2 })]
  [InlineData(0, new uint[] { 0 })]
  public void Spfs_Valid(uint maxIncl, uint[] expected) {
    PrimeNumber.Spfs(maxIncl).Should().Equal(expected);
  }

  [Fact]
  public void Spfs_OutOfRange() {
    var func = () => PrimeNumber.Spfs((uint)Array.MaxLength);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }
}

public class PrimeFactorizerTest {
  [Theory]
  [InlineData(8)]
  public void Pfer_Valid(uint maxIncl) {
    var func = () => new PrimeFactorizer(maxIncl);
    func.Should().NotThrow();
  }

  [Fact(Timeout = 500)]
  public void Pfer_OutOfRange() {
    var func = () => new PrimeFactorizer((uint)Array.MaxLength);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void FactorizeFast_Valid() {
    var pfer = new PrimeFactorizer(8);
    pfer.FactorizeFast(2).Should().Equal((2, 1));
    pfer.FactorizeFast(3).Should().Equal((3, 1));
    pfer.FactorizeFast(6).Should().Equal((2, 1), (3, 1));
    pfer.FactorizeFast(8).Should().Equal((2, 3));
  }

  [Fact]
  public void FactorizeFast_OutOfRange() {
    var pfer = new PrimeFactorizer(8);
    var func = () => pfer.FactorizeFast(9);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void FactorizeFast_OutOfRange2() {
    var pfer = new PrimeFactorizer(8);
    var func = () => pfer.FactorizeFast(1);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void Factorize_Valid() {
    PrimeFactorizer.Factorize(2).Should().Equal((2, 1));
    PrimeFactorizer.Factorize(3).Should().Equal((3, 1));
    PrimeFactorizer.Factorize(6).Should().Equal((2, 1), (3, 1));
    PrimeFactorizer.Factorize(8).Should().Equal((2, 3));
  }

  [Fact]
  public void Factorize_OutOfRange() {
    var func = () => PrimeFactorizer.Factorize(1);
    func.Should().Throw<ArgumentOutOfRangeException>();
  }
}
