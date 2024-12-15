namespace template;
#pragma warning disable format, CS8981
using static MyLib; using static AlgoLib; using static HeuLib; using AtCoder; using AtCoder.Extension; using MathNet.Numerics; using System.Collections; using System.Collections.ObjectModel; using System.Diagnostics; using System.Globalization; using System.Numerics; using System.Runtime.CompilerServices; using System.Runtime.InteropServices; using System.Runtime.Intrinsics; using System.Text; using static System.Math; using MI = System.Runtime.CompilerServices.MethodImplAttribute; using bigint = System.Numerics.BigInteger;
#pragma warning restore format
// using mint = AtCoder.StaticModInt<AtCoder.Mod>;


public static class Program {

  public static void main() {

  }

}



#region library
#pragma warning disable
public static class AlgoLib {
  public static void Main() { Console.SetOut(cout); Console.SetError(cerr); if (MANY_RECURSIONS) { Thread t = new(Program.main, 134217728); t.Start(); t.Join(); } else Program.main(); cout.Flush(); cerr.Flush(); }
}


public static class HeuLib {
  public const long TL = 1950;
  public static readonly long StartupTime = Stopwatch.GetTimestamp();
  public static readonly Random Rng = new();

  [MI(R256)] public static int GetTime() => (int)((Stopwatch.GetTimestamp() - StartupTime) * 1000 / Stopwatch.Frequency);

  [MI(R256)] public static int RandRange(this Random Rng, int maxExclusive) => (int)(((long)Rng.Next() * maxExclusive) >> 31);

  [MI(R256)] public static void Log(this COut @out, string s) => @out.WriteLine($"[{GetTime():D4}ms] " + s);

  public static void Main() {
    cerr.Log($"Start.");

    Console.SetOut(cout);
    Console.SetError(cerr);

    if (MANY_RECURSIONS) {
      var t = new Thread(Program.main, 134217728);
      t.Start();
      t.Join();
    } else {
      Program.main();
    }

    cout.Flush();
    cerr.Log($"Complete.");
    cerr.Flush();
  }

}


public static partial class MyLib {
  public const bool MANY_RECURSIONS = false;
  public const int INF32 = (1 << 30) - 1;
  public const long INF64 = 1L << 60;
  public static readonly CIn cin = new(Console.OpenStandardInput());
  public static readonly COut cout = new(Console.OpenStandardOutput()) { AutoFlush = DEBUG ? false : false };
  public static readonly COut cerr = new(Console.OpenStandardError()) { AutoFlush = DEBUG ? true : true };
  [MI(R256)] public static T Identity<T>(T x) => x;
  [MI(R256)] public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
  [MI(R256)] public static bool Change<T>(this ref T a, T b) where T : struct, IEquatable<T> { if (a.Equals(b)) return false; else { a = b; return true; } }
  [MI(R256)] public static bool ChMax<T>(this ref T a, T b) where T : struct, IComparable<T> { if (a.CompareTo(b) < 0) { a = b; return true; } return false; }
  [MI(R256)] public static int ChMax<T>(this ref T a, params T[] others) where T : struct, IComparable<T> { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMax(others[i])) idx = i; return idx; }
  [MI(R256)] public static bool ChMax<T, U>(this Dictionary<T, U> d, T k, U v) where U : struct, IComparable<U> { if (d.TryGetValue(k, out var x) && x.CompareTo(v) > 0) return false; d[k] = v; return true; }
  [MI(R256)] public static bool ChMin<T>(this ref T a, T b) where T : struct, IComparable<T> { if (a.CompareTo(b) > 0) { a = b; return true; } return false; }
  [MI(R256)] public static int ChMin<T>(this ref T a, params T[] others) where T : struct, IComparable<T> { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMin(others[i])) idx = i; return idx; }
  [MI(R256)] public static bool ChMin<T, U>(this Dictionary<T, U> d, T k, U v) where U : struct, IComparable<U> { if (d.TryGetValue(k, out var x) && x.CompareTo(v) < 0) return false; d[k] = v; return true; }
  [MI(R256)] public static long nCr(int n, int r) { if (r < 0 || n < r) return 0; if (n - r < r) r = n - r; long x = 1; for (int i = 1; i <= r; i++) x = x * (n - i + 1) / i; return x; }
  [MI(R256)] public static long nPr(long n, int r) { if (r < 0 || n < r) return 0; long x = 1; while (r-- > 0) x *= n--; return x; }
  [MI(R256)] public static T Pown<T, U>(T @base, U exp) where T : INumberBase<T> where U : IBinaryInteger<U>, IUnsignedNumber<U> { T res = T.One; if (exp == U.Zero) return res; while (true) { if ((exp & U.One) == U.One) res *= @base; if ((exp >>>= 1) > U.Zero) @base *= @base; else break; } return res; }
  [MI(R256)] public static bigint Pown<U>(bigint @base, U exp, bigint mod) where U : IBinaryInteger<U>, IUnsignedNumber<U> { bigint res = 1; @base = (@base % mod + mod) % mod; while (exp > U.Zero) { if ((exp & U.One) == U.One) res = res * @base % mod; @base = @base * @base % mod; exp >>>= 1; } return res; }
  [MI(R256)] public static T DivFloor<T>(T x, T y) where T : INumberBase<T> => T.IsPositive(x) == T.IsPositive(y) ? T.Abs(x) / T.Abs(y) : -(T.Abs(x) + T.Abs(y) - T.One) / T.Abs(y);
  [MI(R256)] public static T DivCeil<T>(T x, T y) where T : INumberBase<T> => T.IsPositive(x) == T.IsPositive(y) ? (T.Abs(x) + T.Abs(y) - T.One) / T.Abs(y) : -(T.Abs(x) / T.Abs(y));
  [MI(R256)] public static T RangeSum<T>(T minIncl, T maxIncl) where T : IBinaryInteger<T> { T l = maxIncl - minIncl + T.One, r = minIncl + maxIncl; if (T.IsEvenInteger(l)) l >>= 1; else r >>= 1; return l * r; }
  [MI(R256)] public static T Gcd<T>(T a, T b) where T : IBinaryInteger<T>, IUnsignedNumber<T> { while (!T.IsZero(b)) (a, b) = (b, a % b); return a; }
  [MI(R256)] public static T Lcm<T>(T a, T b) where T : IBinaryInteger<T>, IUnsignedNumber<T> { if (T.IsZero(a)) throw new ArgumentOutOfRangeException($"{nameof(a)} must not be 0"); if (T.IsZero(b)) throw new ArgumentOutOfRangeException($"{nameof(b)} must not be 0"); return a / Gcd(a, b) * b; }
  [MI(R256)] public static Span<T> AsSpan<T>(this T[,] array) => MemoryMarshal.CreateSpan(ref array[0, 0], array.Length);
  [MI(R256)] public static Span<T> AsSpan<T>(this T[,,] array) => MemoryMarshal.CreateSpan(ref array[0, 0, 0], array.Length);
  [MI(R256)] public static Span<T> AsSpan<T>(this List<T> list) => CollectionsMarshal.AsSpan(list);
  [MI(R256)] public static void Fill<T>(this IList<List<T>> list, int length0, int length1, T v) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) list[i][j] = v; }
  [MI(R256)] public static void Fill<T>(this IList<List<List<T>>> list, int length0, int length1, int length2, T value) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) list[i][j][k] = value; }
  [MI(R256)] public static void NewAll<T>(this IList<T> list) where T : new() { for (int i = 0; i < list.Count; i++) list[i] = new(); }
  [MI(R256)] public static T PopLast<T>(this List<T> list) { T res = list[^1]; list.RemoveAt(list.Count - 1); return res; }
  [MI(R256)] public static (bool Success, T Item) TryPopLast<T>(this List<T> list) => list.Count == 0 ? (false, default) : (true, list.PopLast());
  [MI(R256)] public static IEnumerable<T> PopRange<T>(this List<T> list, Range rng) { var (off, len) = rng.GetOffsetAndLength(list.Count); var res = list.GetRange(off, len); list.RemoveRange(off, len); return res; }
  [MI(R256)] public static Dictionary<V, int> CountBy<K, V>(this IEnumerable<K> seq, Func<K, V> func) where V : notnull { var dict = new Dictionary<V, int>(); foreach (var item in seq) { var key = func(item); dict[key] = dict.TryGetValue(key, out int count) ? count + 1 : 1; } return dict; }
  [MI(R256)] public static IEnumerable<U> Scan<T, U>(this IEnumerable<T> seq, U def, Func<U, T, U> func) { yield return def; foreach (var x in seq) { def = func(def, x); yield return def; } }
  [MI(R256)] public static IEnumerable<U> ScanBack<T, U>(this IEnumerable<T> seq, U def, Func<U, T, U> func) { yield return def; foreach (var x in seq.Reverse()) { def = func(def, x); yield return def; } }
  [MI(R256)] public static IEnumerable<(T Fst, T Snd)> Pairwise<T>(this IEnumerable<T> seq) where T : struct { var e = seq.GetEnumerator(); e.MoveNext(); T prev = e.Current; while (e.MoveNext()) yield return (prev, prev = e.Current); }
  [MI(R256)] public static bool IsSubsequenceOf<T>(this IEnumerable<T> seq, IEnumerable<T> sub) where T : IEquatable<T> { using var e = seq.GetEnumerator(); using var f = sub.GetEnumerator(); if (!e.MoveNext()) return true; while (f.MoveNext()) if (e.Current.Equals(f.Current) && !e.MoveNext()) return true; return false; }
  [MI(R256)] public static string CollectStr(this IEnumerable<char> seq) => string.Concat(seq);
  [MI(R256)] public static string ReverseStr(this string s) { var t = s.ToCharArray(); t.AsSpan().Reverse(); return new(t); }
  [MI(R256)] public static bool IsPalindrome<T>(ReadOnlySpan<T> span) where T : IEquatable<T> { int h = span.Length / 2; for (int i = 0; i < h; i++) { if (!span[i].Equals(span[^(i + 1)])) return false; } return true; }
  [MI(R256)] public static int HammingDist<T>(ReadOnlySpan<T> s, ReadOnlySpan<T> t) where T : IEquatable<T> { if (s.Length != t.Length) throw new ArgumentException($"different lengths: {s.Length}, {t.Length}"); int res = 0; for (int i = 0; i < s.Length; i++) if (!s[i].Equals(t[i])) res++; return res; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, Predicate<T> condition) where T : IBinaryInteger<T>, ISignedNumber<T> { while (good - bad < -T.One || T.One < good - bad) { T mid = (good + bad) >> 1; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, Func<T, T, T, bool> condition) where T : IBinaryInteger<T>, ISignedNumber<T> { while (good - bad < -T.One || T.One < good - bad) { T mid = (good + bad) >> 1; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, T precision, Predicate<T> condition) where T : INumber<T>, ISignedNumber<T> { T two = T.One + T.One; while (good - bad < -precision || precision < good - bad) { var mid = (good + bad) / two; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, T precision, Func<T, T, T, bool> condition) where T : INumber<T>, ISignedNumber<T> { T two = T.One + T.One; while (good - bad < -precision || precision < good - bad) { var mid = (good + bad) / two; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static Func<T, R> Memoize<T, R>(Func<T, R> f) { var m = new Dictionary<T, R>(); return (T x) => m.TryGetValue(x, out R p) ? p : m[x] = f(x); }
#if DEBUG
  public const bool DEBUG = true;
  public const short R256 = 0, R512 = 0;
#else
  public const bool DEBUG = false;
  public const short R256 = 256, R512 = 512;
#endif
#if ATCODER
  public const bool ATCODER = true;
#else
  public const bool ATCODER = false;
#endif
}


public class CIn {
  public CIn(Stream stream) { _stream = stream; }
  readonly Stream _stream; readonly byte[] _buffer = new byte[1024]; int _len, _ptr; bool _end; public bool End { [MI(R256)] get => _end; }
  [MI(R256)] byte R() { if (_end) throw new EndOfStreamException(); if (_ptr >= _len) { _ptr = 0; if ((_len = _stream.Read(_buffer, 0, 1024)) <= 0) { _end = true; return 0; } } return _buffer[_ptr++]; }
  public char c { [MI(R256)] get { byte b; do b = this.R(); while (b < 33 || 126 < b); return (char)b; } }
  public string s { [MI(R256)] get { var sb = new StringBuilder(); for (char b = this.c; 33 <= b && b <= 126; b = (char)this.R()) sb.Append(b); return sb.ToString(); } }
  public long l { [MI(R256)] get { long res = 0; byte b; var negative = false; do b = this.R(); while (b != '-' && (b < '0' || '9' < b)); if (b == '-') { negative = true; b = this.R(); } for (; true; b = this.R()) { if (b < '0' || '9' < b) return negative ? -res : res; else res = res * 10 + b - '0'; } } }
  public ulong ul { [MI(R256)] get { ulong res = 0; byte b; do b = this.R(); while (b < '0' || '9' < b); for (; true; b = this.R()) { if (b < '0' || '9' < b) return res; else res = res * 10 + b - '0'; } } }
  public int i { [MI(R256)] get => (int)this.l; }
  public uint u { [MI(R256)] get => (uint)this.ul; }
  public float f { [MI(R256)] get => float.Parse(this.s, CultureInfo.InvariantCulture); }
  public double d { [MI(R256)] get => double.Parse(this.s, CultureInfo.InvariantCulture); }
  public decimal m { [MI(R256)] get => decimal.Parse(this.s, CultureInfo.InvariantCulture); }
  public bigint b { [MI(R256)] get => bigint.Parse(this.s, CultureInfo.InvariantCulture); }
}


public class COut : StreamWriter {
  public override IFormatProvider FormatProvider { [MI(R256)] get => CultureInfo.InvariantCulture; }
  public COut(Stream stream) : base(stream, new UTF8Encoding(false, true)) { }
  public COut(Stream stream, Encoding encoding) : base(stream, encoding) { }
  [MI(R256)] public void Print(IEnumerable<char> seq) => this.WriteLine(string.Concat(seq));
  [MI(R256)] public void Print2D(IEnumerable<IEnumerable<char>> seq) { foreach (var x in seq) this.WriteLine(string.Concat(x)); }
  [MI(R256)] public void Print2D<T>(IEnumerable<IEnumerable<T>> seq) { foreach (var x in seq) this.WriteLine(string.Join(' ', x)); }
  [MI(R256)] public void Print2D(char[,] array) { var span = MemoryMarshal.CreateReadOnlySpan(ref array[0, 0], array.Length); int h = array.GetLength(0), w = array.GetLength(1); for (int i = 0; i < h; i++) this.WriteLine(span[(i * w)..((i + 1) * w)]); }
  [MI(R256)] public void Print2D<T>(T[,] array) { int h = array.GetLength(0), w = array.GetLength(1); var s = MemoryMarshal.CreateReadOnlySpan(ref array[0, 0], array.Length); for (int i = 0; i < h; i++) { var r = s.Slice(i * w, w); this.WriteLine(string.Join(' ', r.ToArray())); } }
}


public readonly record struct P<T>(T x, T y) : IEquatable<P<T>> where T : INumber<T> {
  [MI(R256)] public static implicit operator P<T>((T X, T Y) t) => new(t.X, t.Y);
  [MI(R256)] public void Deconstruct(out T x, out T y) { x = this.x; y = this.y; }
  public static P<T> Zero { [MI(R256)] get => (T.Zero, T.Zero); }
  [MI(R256)] public static P<T> operator +(P<T> a, P<T> b) => (a.x + b.x, a.y + b.y);
  [MI(R256)] public static P<T> operator -(P<T> a, P<T> b) => (a.x - b.x, a.y - b.y);
  [MI(R256)] public static P<T> operator *(P<T> a, T b) => (a.x * b, a.y * b);
  [MI(R256)] public static P<T> operator /(P<T> a, T b) => (a.x / b, a.y / b);
  [MI(R256)] public P<int> Signs() => (T.Sign(this.x), T.Sign(this.y));
  [MI(R256)] public bool InInterval(P<T> ulIncl, P<T> drExcl) => ulIncl.x <= this.x && this.x < drExcl.x && ulIncl.y <= this.y && this.y < drExcl.y;
  [MI(R256)] public bool InClosedInterval(P<T> a, P<T> b) => T.Min(a.x, b.x) <= this.x && this.x <= T.Max(a.x, b.x) && T.Min(a.y, b.y) <= this.y && this.y <= T.Max(a.y, b.y);
  [MI(R256)] public static IEnumerable<P<T>> Range(P<T> drExcl) { for (T i = T.Zero; i < drExcl.x; i++) { for (T j = T.Zero; j < drExcl.y; j++) yield return new(i, j); } }
  public static IEnumerable<P<T>> Range(P<T> incl, P<T> excl) { T sx = excl.x > incl.x ? T.One : -T.One, sy = excl.y > incl.y ? T.One : -T.One; for (T i = incl.x; sx > T.Zero ? i < excl.x : i > excl.x; i += sx) for (T j = incl.y; sy > T.Zero ? j < excl.y : j > excl.y; j += sy) yield return new(i, j); }
  [MI(R256)] public T DistE2(P<T> p) { T dx = this.x - p.x, dy = this.y - p.y; return dx * dx + dy * dy; }
  [MI(R256)] public T DistM(P<T> p) => T.Abs(this.x - p.x) + T.Abs(this.y - p.y);
  [MI(R256)] public T DistC(P<T> p) => T.Max(T.Abs(this.x - p.x), T.Abs(this.y - p.y));
  [MI(R256)] public override string ToString() => this.x.ToString() + " " + this.y.ToString();
  [MI(R256)] public string ToString(string pre, string sep, string post) => pre + this.x.ToString() + sep + this.y.ToString() + post;
  /// <summary>R, D, L, U</summary>
  public static readonly ReadOnlyCollection<P<T>> Dir4 = new P<T>[] { (T.Zero, T.One), (T.One, T.Zero), (T.Zero, -T.One), (-T.One, T.Zero), }.AsReadOnly();
  /// <summary>R, RD, D, DL, L, LU, U, UR</summary>
  public static readonly ReadOnlyCollection<P<T>> Dir8 = new P<T>[] { (T.Zero, T.One), (T.One, T.One), (T.One, T.Zero), (T.One, -T.One), (T.Zero, -T.One), (-T.One, -T.One), (-T.One, T.Zero), (-T.One, T.One), }.AsReadOnly();
}

#endregion
