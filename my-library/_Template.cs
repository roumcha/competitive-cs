#pragma warning disable format, CS8981
global using static StaticLib; global using AtCoder; global using MathNet.Numerics; global using System.Collections; global using System.Diagnostics; global using System.Globalization; global using System.Numerics; global using System.Runtime.InteropServices; global using System.Text; global using System.Threading; global using static System.Math; global using MI = System.Runtime.CompilerServices.MethodImplAttribute; global using bigint = System.Numerics.BigInteger;
#pragma warning restore format


public static class Program {

  public static void main() {

  }

}



#region library
#pragma warning disable
public static class StaticLib {
#if DEBUG
  public const bool DEBUG = true;
#else
  public const bool DEBUG = false;
#endif
  public const bool MANY_RECURSIONS = false;
  public const int INF32 = (1 << 30) - 1;
  public const long INF64 = 1L << 60;
  public static readonly CIn cin = new(Console.OpenStandardInput());
  public static readonly COut cout = new(Console.OpenStandardOutput()) { AutoFlush = DEBUG ? false : false };
  public static readonly COut cerr = new(Console.OpenStandardError()) { AutoFlush = DEBUG ? true : false };
  public static P[] Dir4 { [MI(256)] get => new[] { new P(-1, 0), new(0, -1), new(1, 0), new(0, 1) }; }
  public static P[] Dir8 { [MI(256)] get => new[] { new P(-1, 0), new(-1, -1), new(0, -1), new(1, -1), new(1, 0), new(1, 1), new(0, 1), new(-1, 1) }; }
  public static void Main() { Console.SetOut(cout); Console.SetError(cerr); if (MANY_RECURSIONS) { var t = new Thread(Program.main, 134217728); t.Start(); t.Join(); } else Program.main(); cout.Flush(); }
  [MI(256)] public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
  [MI(256)] public static bool Change<T>(this ref T a, T b) where T : struct, IEquatable<T> { if (a.Equals(b)) return false; else { a = b; return true; } }
  [MI(256)] public static bool ChMax<T>(this ref T a, T b) where T : struct, IComparable { if (a.CompareTo(b) < 0) { a = b; return true; } return false; }
  [MI(256)] public static int ChMax<T>(this ref T a, params T[] others) where T : struct, IComparable { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMax(others[i])) idx = i; return idx; }
  [MI(256)] public static bool ChMin<T>(this ref T a, T b) where T : struct, IComparable { if (a.CompareTo(b) > 0) { a = b; return true; } return false; }
  [MI(256)] public static int ChMin<T>(this ref T a, params T[] others) where T : struct, IComparable { int idx = -1; for (int i = 0; i < others.Length; i++) if (a.ChMin(others[i])) idx = i; return idx; }
  [MI(256)] public static long nCr(this long n, int r) { if (r < 0 || n < r) return 0; long x = 1; for (int i = 0; i < r; i++) x *= n - i; for (int i = 1; i <= r; i++) x /= i; return x; }
  [MI(256)] public static long nPr(this long n, int r) { if (r < 0 || n < r) return 0; long x = 1; while (n > r) x *= n--; return x; }
  [MI(256)] public static long PowN(this long x, long exponent) { long res = 1; while (exponent > 0) { if ((exponent & 1) == 1) res *= x; x *= x; exponent >>= 1; } return res; }
  [MI(256)] public static int DivCeil(this int dividend, int divisor) => (dividend + divisor - 1) / divisor;
  [MI(256)] public static long DivCeil(this long dividend, long divisor) => (dividend + divisor - 1) / divisor;
  [MI(256)] public static int RangeSum(int min, int max) => (max - min + 1) * (min + max) / 2;
  [MI(256)] public static long RangeSum(long min, long max) => (max - min + 1) * (min + max) / 2;
  [MI(256)] public static long Gcd(long a, long b) { while (b != 0) (a, b) = (b, a % b); return a; }
  [MI(256)] public static long Lcm(long a, long b) => a / Gcd(a, b) * b;
  [MI(256)] public static Span<T> AsSpan<T>(this T[,] array) => MemoryMarshal.CreateSpan(ref array[0, 0], array.Length);
  [MI(256)] public static Span<T> AsSpan<T>(this T[,,] array) => MemoryMarshal.CreateSpan(ref array[0, 0, 0], array.Length);
  [MI(256)] public static void Fill<T>(this T[,] array, T value) => array.AsSpan().Fill(value);
  [MI(256)] public static void Fill<T>(this T[,,] array, T value) => array.AsSpan().Fill(value);
  [MI(256)] public static void Fill<T>(this IList<IList<T>> list, int length0, int length1, T v) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) list[i][j] = v; }
  [MI(256)] public static void Fill<T>(this IList<IList<IList<T>>> list, int length0, int length1, int length2, T value) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) list[i][j][k] = value; }
  [MI(256)] public static void Init<T>(this T[,] array, Func<int, int, T> generator) { int x = array.GetLength(0), y = array.GetLength(1); for (int i = 0; i < x; ++i) for (int j = 0; j < y; ++j) array[i, j] = generator(i, j); }
  [MI(256)] public static void Init<T>(this IList<IList<T>> list, int length0, int length1, Func<int, int, T> generator) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) list[i][j] = generator(i, j); }
  [MI(256)] public static void Init<T>(this T[,,] array, int length0, int length1, int length2, Func<int, int, int, T> generator) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) array[i, j, k] = generator(i, j, k); }
  [MI(256)] public static void Init<T>(this IList<IList<IList<T>>> list, int length0, int length1, int length2, Func<int, int, int, T> generator) { for (int i = 0; i < length0; ++i) for (int j = 0; j < length1; ++j) for (int k = 0; k < length2; ++k) list[i][j][k] = generator(i, j, k); }
  [MI(256)] public static T[,] Transpose<T>(this T[,] array) { var res = new T[array.GetLength(1), array.GetLength(0)]; for (int i = 0; i < array.GetLength(1); i++) for (int j = 0; j < array.GetLength(0); j++) res[i, j] = array[j, i]; return res; }
  [MI(256)] public static T[,] Transpose<T>(this IList<IList<T>> list) { var res = new T[list[0].Count, list.Count]; for (int i = 0; i < list[0].Count; i++) for (int j = 0; j < list.Count; j++) res[i, j] = list[j][i]; return res; }
  [MI(256)] public static Dictionary<V, int> CountBy<K, V>(this IEnumerable<K> seq, Func<K, V> func) where V : notnull { var dict = new Dictionary<V, int>(); foreach (var item in seq) { var key = func(item); dict[key] = dict.TryGetValue(key, out int count) ? count + 1 : 1; } return dict; }
  [MI(256)] public static IEnumerable<R> Scan<S, R>(this IEnumerable<S> seq, R defaultValue, Func<R, S, R> func) { yield return defaultValue; foreach (var item in seq) { defaultValue = func(defaultValue, item); yield return defaultValue; } }
  [MI(256)] public static int BinarySearch(int good, int bad, Predicate<int> condition) { while (Abs(bad - good) > 1) { var mid = (good + bad) >> 1; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static int BinarySearch(int good, int bad, Func<int, int, int, bool> condition) { while (Abs(bad - good) > 1) { var mid = (good + bad) >> 1; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static long BinarySearch(long good, long bad, Predicate<long> condition) { while (Abs(bad - good) > 1) { var mid = (good + bad) >> 1; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static long BinarySearch(long good, long bad, Func<long, long, long, bool> condition) { while (Abs(bad - good) > 1) { var mid = (good + bad) >> 1; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static double BinarySearch(double good, double bad, double precision, Predicate<double> condition) { while (Abs(bad - good) > precision) { var mid = (good + bad) / 2; if (condition(mid)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static double BinarySearch(double good, double bad, double precision, Func<double, double, double, bool> condition) { while (Abs(bad - good) > precision) { var mid = (good + bad) / 2; if (condition(good, mid, bad)) good = mid; else bad = mid; } return good; }
  [MI(256)] public static T PopLast<T>(this List<T> list) { T res = list[^1]; list.RemoveAt(list.Count - 1); return res; }
  [MI(256)] public static IEnumerable<T> PopRange<T>(this List<T> list, Range rng) { var (off, len) = rng.GetOffsetAndLength(list.Count); var res = list.GetRange(off, len); list.RemoveRange(off, len); return res; }
}

public class CIn {
  public CIn(Stream stream) { _stream = stream; }
  readonly Stream _stream; readonly byte[] _buffer = new byte[1024]; int _len, _ptr; bool _end; public bool End { [MI(256)] get => _end; }
  [MI(256)] byte R() { if (_end) throw new EndOfStreamException(); if (_ptr >= _len) { _ptr = 0; if ((_len = _stream.Read(_buffer, 0, 1024)) <= 0) { _end = true; return 0; } } return _buffer[_ptr++]; }
  public char c { [MI(256)] get { byte b; do b = R(); while (b < 33 || 126 < b); return (char)b; } }
  public string s { [MI(256)] get { var sb = new StringBuilder(); for (char b = c; 33 <= b && b <= 126; b = (char)R()) sb.Append(b); return sb.ToString(); } }
  public long l { [MI(256)] get { long res = 0; byte b; var negative = false; do b = R(); while (b != '-' && (b < '0' || '9' < b)); if (b == '-') { negative = true; b = R(); } for (; true; b = R()) { if (b < '0' || '9' < b) return negative ? -res : res; else res = res * 10 + b - '0'; } } }
  public ulong ul { [MI(256)] get { ulong res = 0; byte b; do b = R(); while (b < '0' || '9' < b); for (; true; b = R()) { if (b < '0' || '9' < b) return res; else res = res * 10 + b - '0'; } } }
  public int i { [MI(256)] get => (int)this.l; }
  public uint u { [MI(256)] get => (uint)this.ul; }
  public float f { [MI(256)] get => float.Parse(this.s, CultureInfo.InvariantCulture); }
  public double d { [MI(256)] get => double.Parse(this.s, CultureInfo.InvariantCulture); }
  public decimal m { [MI(256)] get => decimal.Parse(this.s, CultureInfo.InvariantCulture); }
  public bigint b { [MI(256)] get => bigint.Parse(this.s, CultureInfo.InvariantCulture); }
}

public class COut : StreamWriter {
  public override IFormatProvider FormatProvider { [MI(256)] get => CultureInfo.InvariantCulture; }
  public COut(Stream stream) : base(stream, new UTF8Encoding(false, true)) { }
  public COut(Stream stream, Encoding encoding) : base(stream, encoding) { }
  [MI(256)] public void Print2D(IEnumerable<IEnumerable<char>> seq) { foreach (var x in seq) WriteLine(string.Concat(x)); }
  [MI(256)] public void Print2D<T>(IEnumerable<IEnumerable<T>> seq) { foreach (var x in seq) WriteLine(string.Join(' ', x)); }
  [MI(256)] public void Print2D(char[,] array) { var span = MemoryMarshal.CreateReadOnlySpan(ref array[0, 0], array.Length); int h = array.GetLength(0), w = array.GetLength(1); for (int i = 0; i < h; i++) WriteLine(new string(span[(i * w)..((i + 1) * w)])); }
  [MI(256)] public void Print2D<T>(T[,] array) { int h = array.GetLength(0), w = array.GetLength(1); for (int i = 0; i < h; i++) { var sb = new StringBuilder(2 * w); for (int j = 0; j < w; j++) { sb.Append(array[i, j]); if (j < w - 1) sb.Append(' '); } WriteLine(sb); } }
}

public class LowerBound<T> : IComparer<T> where T : IComparable<T> { [MI(256)] public int Compare(T x, T y) => 0 <= x!.CompareTo(y) ? 1 : -1; }
public class UpperBound<T> : IComparer<T> where T : IComparable<T> { [MI(256)] public int Compare(T x, T y) => 0 < x!.CompareTo(y) ? 1 : -1; }

public record struct P(int X, int Y) : IEquatable<P> {
  [MI(256)] public static implicit operator P((int X, int Y) t) => new(t.X, t.Y);
  [MI(256)] public static P operator +(P a, P b) => new(a.X + b.X, a.Y + b.Y);
  [MI(256)] public static P operator -(P a, P b) => new(a.X - b.X, a.Y - b.Y);
  [MI(256)] public static P operator *(P a, int b) => new(a.X * b, a.Y * b);
  [MI(256)] public static P operator /(P a, int b) => new(a.X / b, a.Y / b);
  [MI(256)] public bool In_Closed(int min_x, int max_x, int min_y, int max_y) => min_x <= X && X <= max_x && min_y <= Y && Y <= max_y;
  [MI(256)] public bool In_Closed(P a, P b) => Min(a.X, b.X) <= X && X <= Max(a.X, b.X) && Min(a.Y, b.Y) <= Y && Y <= Max(a.Y, b.Y);
  [MI(256)] public readonly double DistE(P p) { double dx = (double)X - p.X, dy = (double)Y - p.Y; return Math.Sqrt(dx * dx + dy * dy); }
  [MI(256)] public readonly long DistE2(P p) { long dx = (long)X - p.X, dy = (long)Y - p.Y; return dx * dx + dy * dy; }
  [MI(256)] public readonly long DistM(P p) => Math.Abs((long)X - p.X) + Math.Abs((long)Y - p.Y);
  [MI(256)] public override readonly string ToString() => X.ToString() + " " + Y.ToString();
  [MI(256)] public override readonly int GetHashCode() => base.GetHashCode();
}

#endregion
