using System.Runtime.CompilerServices;

static class Memo {
  [MethodImpl(256)]
  public static Func<T, R> Memoize<T, R>(Func<T, R> f) {
    var m = new Dictionary<T, R>();
    return (T x) => m.TryGetValue(x, out var p) ? p : m[x] = f(x);
  }

  [MethodImpl(256)]
  public static Func<T, U, R> Memoize<T, U, R>(Func<T, U, R> f) {
    var m = new Dictionary<(T, U), R>();
    return (T x, U y) =>
      m.TryGetValue((x, y), out var p) ? p : m[(x, y)] = f(x, y);
  }

  [MethodImpl(256)]
  public static Func<T, U, V, R> Memoize<T, U, V, R>(
    Func<T, U, V, R> f) {
    var m = new Dictionary<(T, U, V), R>();
    return (T x, U y, V z) => {
      return m.TryGetValue((x, y, z), out var p) ? p : m[(x, y, z)] = f(x, y, z);
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
