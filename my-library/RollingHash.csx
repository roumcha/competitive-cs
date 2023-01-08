// 作成中・未検証。
// RH1 に https://qiita.com/keymoon/items/11fac5627672a6d6a9f6 をやる。
// ulong 最大値を mod にした RHFast と、二重チェックの RH2　も。

// using System.Runtime.CompilerServices;

// 参考: drken1215/algorithm

class RollingHashFast {

}

class RollingHash1<T> where T : RollingHashConsts {
  int N; long[] Hash, Pow; static T C = default(T);

  [MethodImpl(512)]
  public RollingHash1(string s) {
    N = s.Length;
    Hash = new long[N + 1]; Hash[0] = 0;
    Pow = new long[N + 1]; Pow[0] = 1;
    for (int i = 0; i < N; i++) {
      Hash[i + 1] = (Hash[i] * C.Base + s[i]) % C.Mod;
      Pow[i + 1] = Pow[i] * C.Base % C.Mod;
    }
  }

  [MethodImpl(256)]
  public long Get(int l, int r) {
    Debug.Assert(l <= r);
    var res = Hash[r] - Hash[l] * Pow[r - l] % C.Mod;
    return res < 0 ? res + C.Mod : res;
  }

  public long this[Range a] {
    [MethodImpl(256)]
    get {
      int l = a.Start.IsFromEnd ? N - a.Start.Value : a.Start.Value;
      int r = a.End.IsFromEnd ? N - a.End.Value : a.End.Value;
      return this.Get(l, r);
    }
  }

  [MethodImpl(256)]
  public int Lcp(int l1, int l2) {
    int len = Math.Min(N - l1 + 1, N - l2 + 1);
    Debug.Assert(len >= 0);
    int low = 0, high = len;
    while (high - low > 1) {
      int mid = (low + high) >> 1;
      if (this.Get(l1, l1 + mid) != this.Get(l2, l2 + mid)) high = mid;
      else low = mid;
    }
    return low;
  }

  [MethodImpl(256)]
  public static int Lcp(
    RollingHash1<T> h1, int l1, RollingHash1<T> h2, int l2
  ) {
    int len = Math.Min(h1.N - l1 + 1, h2.N - l2 + 1);
    Debug.Assert(len >= 0);
    int low = 0, high = len;
    while (high - low > 1) {
      int mid = (low + high) >> 1;
      if (h1.Get(l1, l1 + mid) != h2.Get(l2, l2 + mid)) high = mid;
      else low = mid;
    }
    return low;
  }
}

class RollingHash2<T, U>
  where T : RollingHashConsts where U : RollingHashConsts {
  int N; long[] Hash1, Hash2, Pow1, Pow2; static T C = default(T);

}

public interface RollingHashConsts {
  public uint Base { get; }
  public uint Mod { get; }
}

public struct RollingHashDefaults : RollingHashConsts {
  public uint Base { [MethodImpl(256)] get { return 1007; } }
  public uint Mod { [MethodImpl(256)] get { return 1000000007; } }
}
