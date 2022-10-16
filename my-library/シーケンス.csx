// using System.Runtime.CompilerServices;

class Seq
{
  // 連長圧縮 - O(L) where L = src.Length
  [MethodImpl(256)]
  public static IEnumerable<(T Item, int Cnt)> RunLengthEncode<T>(
    IEnumerable<T> src, T defval)
    where T : IEquatable<T>
  {
    int c = 0;
    T x = defval;
    foreach (var a in src)
    {
      if (x.Equals(a)) ++c;
      else
      {
        if (!x.Equals(defval)) yield return (x, c);
        x = a;
        c = 1;
      }
    }
    if (!x.Equals(defval)) yield return (x, c);
  }

  // 最初の重複 - O(L log L) where L = src.Length
  [MethodImpl(256)]
  public static bool TryFirstDuplicate<T>(
    IEnumerable<T> src, T defval, out int pos, out T res)
  {
    pos = -1; res = defval;
    var s = new HashSet<T>();
    int i = 0;
    foreach (var x in src)
    { if (s.Contains(x)) { pos = i; res = x; return true; } else s.Add(x); ++i; }
    return false;
  }

  [MethodImpl(256)]
  public static IEnumerable<TRes> Scan<TSrc, TRes>(
    IEnumerable<TSrc> src, TRes state, Func<TRes, TSrc, TRes> folder)
  {
    yield return state;
    foreach (var a in src)
    {
      state = folder(state, a);
      yield return state;
    }
  }
}