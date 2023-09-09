static class Seq {
  /// <remarks>O(L) where L is src's Length</remarks>
  [MI(256)]
  public static IEnumerable<(T Item, int Cnt)> RunLengthEncode<T>(
    IEnumerable<T> src, T defval)
    where T : IEquatable<T> {
    int c = 0; T x = defval;
    foreach (var a in src) {
      if (x.Equals(a)) { ++c; continue; }
      if (!x.Equals(defval)) yield return (x, c);
      x = a; c = 1;
    }
    if (!x.Equals(defval)) yield return (x, c);
  }

  /// <remarks>O(L log L) where L is src's Length</remarks>
  [MI(256)]
  public static bool TryFirstDuplicate<T>(
    IEnumerable<T> src, T defval, out int pos, out T res) {
    int i = 0; pos = -1; res = defval;
    var s = new HashSet<T>();
    foreach (var x in src) {
      if (s.Contains(x)) { pos = i; res = x; return true; }
      s.Add(x); ++i;
    }
    return false;
  }

  /// <remarks>O(L) where L is src's Length</remarks>
  [MI(256)]
  public static IEnumerable<TRes> Scan<TSrc, TRes>(
    IEnumerable<TSrc> src, TRes state, Func<TRes, TSrc, TRes> folder) {
    yield return state;
    foreach (var a in src) { state = folder(state, a); yield return state; }
  }

  /// <remarks>O(L log L) where L is src's Length</remarks>
  [MI(256)]
  public static Dictionary<T, int> Count<T>(IEnumerable<T> src) {
    var d = new Dictionary<T, int>();
    foreach (var x in src) if (d.TryGetValue(x, out var c)) d[x]++; else d[x] = 1;
    return d;
  }

  /// <remarks>O(L log L) where L is src's Length</remarks>
  [MI(256)]
  public static Dictionary<TVal, int> CountBy<TKey, TVal>(
    IEnumerable<TKey> src, Func<TKey, TVal> projection) {
    var d = new Dictionary<TVal, int>();
    foreach (var x in src) {
      var k = projection(x);
      if (d.TryGetValue(k, out var c)) d[k]++; else d[k] = 1;
    }
    return d;
  }
}
