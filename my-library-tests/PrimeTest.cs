namespace MyLibrary.Tests;

using System.Diagnostics;
using template;
using static template.MyLib;

public class PrimeNumberTest(ITestOutputHelper _output) {

  [Theory]
  [InlineData(2, true)]
  [InlineData(4, false)]
  [InlineData(uint.MaxValue - 1, false)]
  [InlineData(uint.MaxValue, false)]
  [InlineData(10_000_000_000_000_061, true)]
  public void IsPrime_Valid(ulong n, bool expected) {
    PrimeNumber.IsPrime(n).ShouldBe(expected);
  }

  [Theory]
  [InlineData(0)]
  public void IsPrime_OutOfRange(uint n) {
    Should.Throw<ArgumentOutOfRangeException>(() => PrimeNumber.IsPrime(n));
  }

  [Fact]
  public void Sieve_Valid() {
    PrimeNumber.Sieve(8).Skip(1)
      .ShouldBe([false, true, true, false, true, false, true, false]);
  }

  [Fact(Timeout = 500)]
  public void Sieve_OutOfRange() {
    Should.Throw<ArgumentOutOfRangeException>(
      () => PrimeNumber.Sieve(0)
    );

    Should.Throw<ArgumentOutOfRangeException>(
      () => PrimeNumber.Sieve((uint)Array.MaxLength)
    );
  }

  [Fact(Timeout = 500)]
  public void Sieve_OutOfRange2() {
    Should.Throw<ArgumentOutOfRangeException>(
      () => PrimeNumber.Sieve((uint)Array.MaxLength)
    );
  }

  [Theory]
  [InlineData(8, new uint[] { 0, 0, 2, 3, 2, 5, 2, 7, 2 })]
  [InlineData(0, new uint[] { 0 })]
  public void Spfs_Valid(uint maxIncl, uint[] expected) {
    PrimeNumber.Spfs(maxIncl).ShouldBe(expected);
  }

  [Fact]
  public void Spfs_OutOfRange() {
    Should.Throw<ArgumentOutOfRangeException>(
      () => PrimeNumber.Spfs((uint)Array.MaxLength)
    );
  }

}

public class PrimeFactorizerTest(ITestOutputHelper _output) {

  [Theory]
  [InlineData(8)]
  public void Pfer_Valid(uint maxIncl) {
    Should.NotThrow(() => new PrimeFactorizer(maxIncl));
  }

  [Fact(Timeout = 500)]
  public void Pfer_OutOfRange() {
    Should.Throw<ArgumentOutOfRangeException>(
      () => new PrimeFactorizer((uint)Array.MaxLength)
    );
  }

  [Fact]
  public void FactorizeFast_Valid() {
    var pfer = new PrimeFactorizer(8);
    pfer.FactorizeFast(2).ShouldBe([(2, 1)]);
    pfer.FactorizeFast(3).ShouldBe([(3, 1)]);
    pfer.FactorizeFast(6).ShouldBe([(2, 1), (3, 1)]);
    pfer.FactorizeFast(8).ShouldBe([(2, 3)]);
  }

  [Fact]
  public void FactorizeFast_OutOfRange() {
    var pfer = new PrimeFactorizer(8);
    Should.Throw<ArgumentOutOfRangeException>(() => pfer.FactorizeFast(9));
  }

  [Fact]
  public void FactorizeFast_OutOfRange2() {
    var pfer = new PrimeFactorizer(8);
    Should.Throw<ArgumentOutOfRangeException>(() => pfer.FactorizeFast(1));
  }

  [Fact]
  public void Factorize_Valid() {
    PrimeFactorizer.Factorize(2).ShouldBe([(2, 1)]);
    PrimeFactorizer.Factorize(3).ShouldBe([(3, 1)]);
    PrimeFactorizer.Factorize(6).ShouldBe([(2, 1), (3, 1)]);
    PrimeFactorizer.Factorize(8).ShouldBe([(2, 3)]);
  }

  [Fact]
  public void Factorize_OutOfRange() {
    Should.Throw<ArgumentOutOfRangeException>(
      () => PrimeFactorizer.Factorize(1)
    );
  }

}
