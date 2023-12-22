namespace template;
#pragma warning disable format, CS8981
using static MyLib; using static AlgoLib; using static HeuLib; using AtCoder; using AtCoder.Extension; using MathNet.Numerics; using System.Collections; using System.Collections.ObjectModel; using System.Diagnostics; using System.Globalization; using System.Numerics; using System.Runtime.CompilerServices; using System.Runtime.InteropServices; using System.Runtime.Intrinsics; using System.Text; using System.Threading; using static System.Math; using MI = System.Runtime.CompilerServices.MethodImplAttribute; using bigint = System.Numerics.BigInteger;
#pragma warning restore format


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
  static readonly Stopwatch s_clock = new();
  public static readonly Random Rng = new();

  [MI(R256)] public static int GetTime() => (int)s_clock.ElapsedMilliseconds;

  [MI(R256)] public static int RandRange(this Random Rng, int maxExclusive) => (int)(((long)Rng.Next() * maxExclusive) >> 31);

  [MI(R256)] public static void Log(this COut @out, string s) => @out.WriteLine($"[{GetTime():D4}ms] " + s);

  public static void Main() {
    s_clock.Start();

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
    cerr.Flush();
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
  public static readonly COut cerr = new(Console.OpenStandardError()) { AutoFlush = DEBUG ? true : false };
  public static readonly ReadOnlyCollection<P> Dir4 = new P[] { (-1, 0), (0, -1), (1, 0), (0, 1) }.AsReadOnly();
  public static readonly ReadOnlyCollection<P> Dir8 = new P[] { (-1, 0), (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1) }.AsReadOnly();
  [MI(R256)] public static T Identity<T>(T x) => x;
  [MI(R256)] public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
  [MI(R256)] public static bool Change<T>(this ref T a, T b) where T : struct, IEquatable<T> { if (a.Equals(b)) return false; else { a = b; return true; } }
  [MI(R256)] public static bool ChMax<T>(this ref T a, T b) where T : struct, IComparable<T> { if (a.CompareTo(b) < 0) { a = b; return true; } return false; }
  [MI(R256)] public static int ChMax<T>(this ref T a, params T[] others) where T : struct, IComparable<T> { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMax(others[i])) idx = i; return idx; }
  [MI(R256)] public static bool ChMin<T>(this ref T a, T b) where T : struct, IComparable<T> { if (a.CompareTo(b) > 0) { a = b; return true; } return false; }
  [MI(R256)] public static int ChMin<T>(this ref T a, params T[] others) where T : struct, IComparable<T> { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMin(others[i])) idx = i; return idx; }
  [MI(R256)] public static long nCr(int n, int r) { if (r < 0 || n < r) return 0; if (n - r < r) r = n - r; long x = 1; for (int i = 1; i <= r; i++) x = x * (n - i + 1) / i; return x; }
  [MI(R256)] public static long nPr(long n, int r) { if (r < 0 || n < r) return 0; long x = 1; while (n > r) x *= n--; return x; }
  [MI(R256)] public static T Pown<T>(T @base, uint exp) where T : IBinaryInteger<T> { T res = T.One; while (exp > 0) { if ((exp & 1) == 1) res *= @base; @base *= @base; exp >>>= 1; } return res; }
  [MI(R256)] public static bigint Pown(bigint @base, uint exp, bigint mod) { bigint res = 1; @base = (@base % mod + mod) % mod; while (exp > 0) { if ((exp & 1) == 1) res = res * @base % mod; @base = @base * @base % mod; exp >>>= 1; } return res; }
  [MI(R256)] public static int DivCeil(int dividend, int divisor) => (dividend + divisor - 1) / divisor;
  [MI(R256)] public static long DivCeil(long dividend, long divisor) => (dividend + divisor - 1) / divisor;
  [MI(R256)] public static int RangeSum(int min, int max) => (max - min + 1) * (min + max) / 2;
  [MI(R256)] public static long RangeSum(long min, long max) => (max - min + 1) * (min + max) / 2;
  [MI(R256)] public static long Gcd(long a, long b) { while (b != 0) (a, b) = (b, a % b); return a; }
  [MI(R256)] public static long Lcm(long a, long b) => a / Gcd(a, b) * b;
  [MI(R256)] public static Span<T> AsSpan<T>(this T[,] array) => MemoryMarshal.CreateSpan(ref array[0, 0], array.Length);
  [MI(R256)] public static Span<T> AsSpan<T>(this T[,,] array) => MemoryMarshal.CreateSpan(ref array[0, 0, 0], array.Length);
  [MI(R256)] public static Span<T> AsSpan<T>(this List<T> list) => CollectionsMarshal.AsSpan(list);
  [MI(R256)] public static void Fill<T>(this T[,] array, T value) => array.AsSpan().Fill(value);
  [MI(R256)] public static void Fill<T>(this T[,,] array, T value) => array.AsSpan().Fill(value);
  [MI(R256)] public static void Fill<T>(this IList<IList<T>> list, int length0, int length1, T v) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) list[i][j] = v; }
  [MI(R256)] public static void Fill<T>(this IList<IList<IList<T>>> list, int length0, int length1, int length2, T value) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) list[i][j][k] = value; }
  [MI(R256)] public static void Init<T>(this T[,] array, Func<int, int, T> generate) { int x = array.GetLength(0), y = array.GetLength(1); for (int i = 0; i < x; ++i) for (int j = 0; j < y; ++j) array[i, j] = generate(i, j); }
  [MI(R256)] public static void Init<T>(this IList<IList<T>> list, int length0, int length1, Func<int, int, T> generate) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) list[i][j] = generate(i, j); }
  [MI(R256)] public static void Init<T>(this T[,,] array, int length0, int length1, int length2, Func<int, int, int, T> generate) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) array[i, j, k] = generate(i, j, k); }
  [MI(R256)] public static void Init<T>(this IList<IList<IList<T>>> list, int length0, int length1, int length2, Func<int, int, int, T> generate) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) list[i][j][k] = generate(i, j, k); }
  [MI(R256)] public static T[,] Transpose<T>(this T[,] array) { var res = new T[array.GetLength(1), array.GetLength(0)]; for (int i = 0; i < array.GetLength(1); i++) for (int j = 0; j < array.GetLength(0); j++) res[i, j] = array[j, i]; return res; }
  [MI(R256)] public static T[,] Transpose<T>(this IList<IList<T>> list) { var res = new T[list[0].Count, list.Count]; for (int i = 0; i < list[0].Count; i++) for (int j = 0; j < list.Count; j++) res[i, j] = list[j][i]; return res; }
  [MI(R256)] public static Dictionary<TVal, int> CountBy<TKey, TVal>(this IEnumerable<TKey> seq, Func<TKey, TVal> func) where TVal : notnull { var dict = new Dictionary<TVal, int>(); foreach (var item in seq) { var key = func(item); dict[key] = dict.TryGetValue(key, out int count) ? count + 1 : 1; } return dict; }
  [MI(R256)] public static IEnumerable<U> Scan<T, U>(this IEnumerable<T> seq, U def, Func<U, T, U> func) { yield return def; foreach (var x in seq) { def = func(def, x); yield return def; } }
  [MI(R256)] public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> seq) where T : struct { var e = seq.GetEnumerator(); e.MoveNext(); T prev = e.Current; while (e.MoveNext()) yield return (prev, prev = e.Current); }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, Predicate<T> condition) where T : IBinaryInteger<T>, ISignedNumber<T> { while (good - bad < -T.One || T.One < good - bad) { T mid = (good + bad) >> 1; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, Func<T, T, T, bool> condition) where T : IBinaryInteger<T>, ISignedNumber<T> { while (good - bad < -T.One || T.One < good - bad) { T mid = (good + bad) >> 1; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, T precision, Predicate<T> condition) where T : INumber<T>, ISignedNumber<T> { T two = T.One + T.One; while (good - bad < -precision || precision < good - bad) { var mid = (good + bad) / two; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T BinarySearch<T>(T good, T bad, T precision, Func<T, T, T, bool> condition) where T : INumber<T>, ISignedNumber<T> { T two = T.One + T.One; while (good - bad < -precision || precision < good - bad) { var mid = (good + bad) / two; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(R256)] public static T PopLast<T>(this List<T> list) { T res = list[^1]; list.RemoveAt(list.Count - 1); return res; }
  [MI(R256)] public static (bool Success, T Item) TryPopLast<T>(this List<T> list) => list.Count == 0 ? (false, default) : (true, list.PopLast());
  [MI(R256)] public static IEnumerable<T> PopRange<T>(this List<T> list, Range rng) { var (off, len) = rng.GetOffsetAndLength(list.Count); var res = list.GetRange(off, len); list.RemoveRange(off, len); return res; }
#if DEBUG
  public const bool DEBUG = true;
#else
  public const bool DEBUG = false;
#endif
#if ATCODER
  public const bool ATCODER = true;
#else
  public const bool ATCODER = false;
#endif
  public const short R256 = DEBUG ? 0 : 256, R512 = DEBUG ? 0 : 512;
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
  [MI(R256)] public void Print2D(IEnumerable<IEnumerable<char>> seq) { foreach (var x in seq) this.WriteLine(string.Concat(x)); }
  [MI(R256)] public void Print2D<T>(IEnumerable<IEnumerable<T>> seq) { foreach (var x in seq) this.WriteLine(string.Join(' ', x)); }
  [MI(R256)] public void Print2D(char[,] array) { var span = MemoryMarshal.CreateReadOnlySpan(ref array[0, 0], array.Length); int h = array.GetLength(0), w = array.GetLength(1); for (int i = 0; i < h; i++) this.WriteLine(new string(span[(i * w)..((i + 1) * w)])); }
  [MI(R256)] public void Print2D<T>(T[,] array) { int h = array.GetLength(0), w = array.GetLength(1); for (int i = 0; i < h; i++) { var sb = new StringBuilder(2 * w); for (int j = 0; j < w; j++) { sb.Append(array[i, j]); if (j < w - 1) sb.Append(' '); } this.WriteLine(sb); } }
}

public readonly record struct P(int X, int Y) : IEquatable<P> {
  [MI(R256)] public static implicit operator P((int X, int Y) t) => new(t.X, t.Y);
  [MI(R256)] public static P operator +(P a, P b) => (a.X + b.X, a.Y + b.Y);
  [MI(R256)] public static P operator -(P a, P b) => (a.X - b.X, a.Y - b.Y);
  [MI(R256)] public static P operator *(P a, int b) => (a.X * b, a.Y * b);
  [MI(R256)] public static P operator /(P a, int b) => (a.X / b, a.Y / b);
  [MI(R256)] public readonly bool InClosed(int min_x, int max_x, int min_y, int max_y) => min_x <= this.X && this.X <= max_x && min_y <= this.Y && this.Y <= max_y;
  [MI(R256)] public readonly bool InClosed(P a, P b) => Min(a.X, b.X) <= this.X && this.X <= Max(a.X, b.X) && Min(a.Y, b.Y) <= this.Y && this.Y <= Max(a.Y, b.Y);
  [MI(R256)] public readonly double DistE(P p) { double dx = (double)this.X - p.X, dy = (double)this.Y - p.Y; return Math.Sqrt(dx * dx + dy * dy); }
  [MI(R256)] public readonly long DistE2(P p) { long dx = (long)this.X - p.X, dy = (long)this.Y - p.Y; return dx * dx + dy * dy; }
  [MI(R256)] public readonly long DistM(P p) => Math.Abs((long)this.X - p.X) + Math.Abs((long)this.Y - p.Y);
  [MI(R256)] public override readonly string ToString() => this.X.ToString() + " " + this.Y.ToString();
  [MI(R256)] public readonly string ToString(string pre, string sep, string post) => pre + this.X.ToString() + sep + this.Y.ToString() + post;
  [MI(R256)] public override readonly int GetHashCode() => base.GetHashCode();
}

#endregion
