// using System.Runtime.CompilerServices;

static class Prime
{
  /// <remarks>O(√N)</remarks>
  [MethodImpl(256)]
  public static bool IsPrime(uint n)
  {
    if ((n & 1) == 0 || n <= 2) return n == 2;
    for (uint i = 3; i * i <= n; i += 2)
      if (n % i == 0) return false;
    return true;
  }

  /// <remarks>
  /// <para>O(N log log N)</para>
  /// <para>1 &lt;= max &lt; int.MaxValue</para>
  /// </remarks>
  [MethodImpl(256)]
  public static bool[] Sieve(int max)
  {
    if (max == 1) return new bool[2];
    var sv = new bool[max + 1];
    sv[2] = true;
    for (int i = 3; i <= max; i += 2) sv[i] = true;
    for (int i = 3; i <= max; i += 2)
    {
      if (!sv[i]) continue;
      for (long j = (long)i * i; j <= max; j += i) sv[j] = false;
    }
    return sv;
  }

  // todo: PrimeFactorize
}
