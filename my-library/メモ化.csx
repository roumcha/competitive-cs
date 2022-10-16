// using System.Runtime.CompilerServices;

static class Memo
{
  [MethodImpl(256)]
  public static Func<T, R> Memoize<T, R>(Func<T, R> f)
  {
    var m = new Dictionary<T, R>();
    return (T x) =>
    {
      if (m.TryGetValue(x, out var p)) return p;
      else { var r = f(x); m.Add(x, r); return r; }
    };
  }

  [MethodImpl(256)]
  public static Func<T, U, R> Memoize<T, U, R>(Func<T, U, R> f)
  {
    var m = new Dictionary<(T, U), R>();
    return (T x, U y) =>
    {
      if (m.TryGetValue((x, y), out var p)) return p;
      else { var r = f(x, y); m.Add((x, y), r); return r; }
    };
  }

  [MethodImpl(256)]
  public static Func<T, U, V, R> Memoize<T, U, V, R>(
    Func<T, U, V, R> f)
  {
    var m = new Dictionary<(T, U, V), R>();
    return (T x, U y, V z) =>
    {
      if (m.TryGetValue((x, y, z), out var p)) return p;
      else { var r = f(x, y, z); m.Add((x, y, z), r); return r; }
    };
  }
}

/* Usage:

static Func<int,int> Fib_Memo = Memo.Memoize<int, int>(Fib);
static int Fib(int x)
{
  if (x <= 2) return 1;
  // Use 'Fib_Memo' instead of 'Fib' when recursing.
  return Fib_Memo(x - 1) + Fib_Memo(x - 2);
}

*/