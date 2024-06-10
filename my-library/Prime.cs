static class PrimeNumber {
  /// <summary>素数か否か</summary>
  /// <remarks>O(√N)</remarks>
  [MI(R256)]
  public static bool IsPrime(uint n) {
    if (n == 0) throw new ArgumentOutOfRangeException(nameof(n), "input must not be 0");
    if ((n & 1) == 0 || n <= 2) return n == 2;
    for (ulong i = 3; i * i <= n; i += 2)
      if (n % i == 0) return false;
    return true;
  }

  /// <summary>素数か否か一覧</summary>
  /// <remarks>O(N log log N)</remarks>
  public static bool[] Sieve(uint max) {
    if (max <= 1) throw new ArgumentOutOfRangeException(nameof(max), "input must be >= 2");
    var s = new bool[(ulong)max + 1];
    Array.Fill(s, true); s[1] = false;
    for (uint i = 2; i <= max; i++) {
      if (!s[i]) continue;
      for (ulong j = (ulong)i * i; j <= max; j += i) s[j] = false;
    }
    return s;
  }

  /// <summary>最小の素因数一覧 (Smallest prime factor)</summary>
  /// <remarks>O(N log log N)</remarks>
  public static uint[] Spfs(uint max) {
    if (max <= 1 || max == uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(max), "input must be >= 2 and < uint.MaxValue");
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

  /// <summary>素数列挙</summary>
  /// <remarks>
  ///   <para>O(N log log N)</para>
  ///   <para><c>maxIncl</c> > 200000 は厳しい</para>
  /// </remarks>
  public static List<T> PrimeList<T>(T maxIncl) where T : INumber<T> {
    T t2 = T.One + T.One;
    if (maxIncl < t2) return new();
    else if (maxIncl == t2) return new() { t2 };

    var res = new List<T>() { t2 };
    for (T i = t2 + T.One; i <= maxIncl; i += t2) {
      if (res.All(x => i % x != T.Zero)) res.Add(i);
    }
    return res;
  }
}

/// <summary>前計算あり・なしの素因数分解</summary>
class PrimeFactorizer {
  readonly uint[] _spfs;

  /// <remarks>O(N log log N)</remarks>
  public PrimeFactorizer(uint maxIncl) {
    if (maxIncl <= 1 || maxIncl == uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(maxIncl), "input must be >= 2 and < uint.MaxValue");
    _spfs = PrimeNumber.Spfs(maxIncl);
  }

  public PrimeFactorizer(uint[] spfs) => _spfs = spfs;

  /// <remarks>O(log N)</remarks>
  public List<(uint Prime, uint Cnt)> FactorizeFast(uint n) {
    if (n <= 1 || n == uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(n), "input must be >= 2 and < uint.MaxValue");
    if (n == uint.MaxValue) return new() { (3, 1), (5, 1), (17, 1), (257, 1), (65537, 1) };
    var res = new List<(uint Prime, uint Cnt)>();

    // 2はビットで処理
    int two_cnt = BitOperations.TrailingZeroCount(n);
    n >>= two_cnt;
    if (two_cnt > 0) res.Add((2, (uint)two_cnt));

    // 3以上
    while (n > 1) {
      uint prime = _spfs[n], e = 0;
      while (_spfs[n] == prime) { n /= prime; e++; }
      res.Add((prime, e));
    }
    return res;
  }

  /// <remarks>O(sqrt(n))</remarks>
  public static List<(ulong Prime, uint Cnt)> Factorize(ulong n) {
    if (n <= 1) throw new ArgumentOutOfRangeException(nameof(n), "input must be >= 2");
    var res = new List<(ulong Prime, uint Cnt)>();

    // 2はビットで処理
    int two_cnt = BitOperations.TrailingZeroCount(n);
    n >>= two_cnt;
    if (two_cnt > 0) res.Add((2, (uint)two_cnt));

    // 3以上
    for (ulong i = 3; i * i < n; i += 2) {
      uint cnt = 0;
      while (DivRem(n, i) is (var div, 0)) { n = div; cnt++; }
      if (cnt > 0) res.Add((i, cnt));
    }

    if (n != 1) res.Add((n, 1));
    return res;
  }
}
