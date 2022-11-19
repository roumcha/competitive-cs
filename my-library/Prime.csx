static class Prime
{
  /// <remarks>O(√N)</remarks>
  public static bool IsPrime(uint n)
  {
    if ((n & 1) == 0 || n <= 2) return n == 2;
    for (ulong i = 3; i * i <= n; i += 2)
      if (n % i == 0) return false;
    return true;
  }

  /// <remarks>O(N log log N)</remarks>
  public static bool[] Sieve(uint max)
  {
    if (max <= 1) return new bool[max];
    var sv = new bool[(ulong)max + 1];
    Array.Fill(sv, true); sv[1] = false;
    for (uint i = 2; i <= max; i++)
    {
      if (!sv[i]) continue;
      for (var j = (ulong)i * i; j <= max; j += i) sv[j] = false;
    }
    return sv;
  }

  /// <summary>最小の素因数 (Smallest prime factor)</summary>
  /// <remarks>O(N log log N)</remarks>
  public static int[] Spfs(uint max)
  {
    if (max <= 1) return new int[0];
    var sv = new int[(ulong)max + 1];
    for (uint i = 2; i <= max; i++)
    {
      if (sv[i] != 0) continue;
      sv[i] = (int)i;
      for (var j = (ulong)i * i; j <= max; j += i) sv[j] = (int)i;
    }
    return sv;
  }
}

/// <remarks>
/// <para>SPF: 前処理 O(N log log N)、クエリ O(log N)</para>
/// <para>篩: 前処理 O(√N log log N)、クエリ O(√N / log N)</para>
/// </remarks>
class PrimeFactorizer
{
  uint _max; bool _mode; int[] _pre;

  public PrimeFactorizer(uint max, bool spfOrSieve)
  {
    _max = max; _mode = spfOrSieve;
    if (spfOrSieve) _pre = Prime.Spfs(max);
    else
    {
      var sieve = Prime.Sieve((uint)Math.Sqrt(max) + 10);
      // var primes = new List<uint>((int)(max / Math.Log(max) * 1.1) + 100);
      for (ulong i = 2; i * i <= max; i++)
    }
  }
}