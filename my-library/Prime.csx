static class Prime {
  /// <summary素数か否か</summary>
  /// <remarks>O(√N)</remarks>
  [MI(256)]
  public static bool IsPrime(uint n) {
    if ((n & 1) == 0 || n <= 2) return n == 2;
    for (ulong i = 3; i * i <= n; i += 2)
      if (n % i == 0) return false;
    return true;
  }

  /// <summary素数か否か一覧</summary>
  /// <remarks>O(N log log N)</remarks>
  public static bool[] Sieve(uint max) {
    if (max <= 1) return new bool[max];
    var s = new bool[(ulong)max + 1];
    Array.Fill(s, true); s[1] = false;
    for (uint i = 2; i <= max; i++) {
      if (!s[i]) continue;
      for (var j = (ulong)i * i; j <= max; j += i) s[j] = false;
    }
    return s;
  }

  /// <summary>最小の素因数一覧 (Smallest prime factor)</summary>
  /// <remarks>O(N log log N)</remarks>
  public static uint[] Spfs(uint max) {
    if (max <= 1) return new uint[0];
    var s = new uint[(ulong)max + 1];
    for (uint i = 2; i <= max; i++) {
      if (s[i] != 0) continue;
      s[i] = i;
      for (var j = (ulong)i * i; j <= max; j += i) {
        if (s[j] == 0) s[j] = i;
      }
    }
    return s;
  }
}

class PrimeFactorizer {
  uint[] s;
  /// <remarks>O(N log log N)</remarks>
  public PrimeFactorizer(uint max) { s = Prime.Spfs(max); }
  /// <summary>O(log N)</summary>
  public List<Tuple<uint, uint>> Factorize(uint n) {
    var r = new List<Tuple<uint, uint>>();
    while (n > 1) {
      uint p = s[n], e = 0;
      while (s[n] == p) { n /= p; e++; }
      r.Add(Tuple.Create(p, e));
    }
    return r;
  }
}
