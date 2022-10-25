// using System.Runtime.CompilerServices;

public interface IStaticMod { uint Mod { get; } bool IsPrime { get; } }
public readonly struct Mod1000000007 : IStaticMod
{ public uint Mod => 1000000007; public bool IsPrime => true; }
public readonly struct Mod998244353 : IStaticMod
{ public uint Mod => 998244353; public bool IsPrime => true; }
public readonly struct StaticModInt<T>
: IEquatable<StaticModInt<T>>,
  IFormattable where T : struct, IStaticMod
{
  internal readonly uint _v;
  private static readonly T op = default;
  public int Value => (int)_v;
  public static int Mod => (int)op.Mod;
  public static StaticModInt<T> Zero => default;
  public static StaticModInt<T> One => new StaticModInt<T>(1u);
  [MethodImpl(256)]
  public static StaticModInt<T> Raw(int v)
  {
    var u = unchecked((uint)v);
    Contract.Assert(
      u < Mod, $"{nameof(u)} must be less than {nameof(Mod)}.");
    return new StaticModInt<T>(u);
  }
  [MethodImpl(256)] public StaticModInt(long v) : this(Round(v)) { }
  [MethodImpl(256)]
  public StaticModInt(ulong v) : this((uint)(v % op.Mod)) { }
  [MethodImpl(256)] private StaticModInt(uint v) => _v = v;
  [MethodImpl(256)]
  private static uint Round(long v)
  { var x = v % op.Mod; if (x < 0) x += op.Mod; return (uint)x; }
  [MethodImpl(256)]
  public static StaticModInt<T> operator ++(StaticModInt<T> v)
  {
    var x = v._v + 1;
    if (x == op.Mod) x = 0; return new StaticModInt<T>(x);
  }
  [MethodImpl(256)]
  public static StaticModInt<T> operator --(StaticModInt<T> v)
  {
    var x = v._v;
    if (x == 0) x = op.Mod; return new StaticModInt<T>(x - 1);
  }
  [MethodImpl(256)]
  public static StaticModInt<T> operator +(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  {
    var v = lhs._v + rhs._v;
    if (v >= op.Mod) v -= op.Mod; return new StaticModInt<T>(v);
  }
  [MethodImpl(256)]
  public static StaticModInt<T> operator -(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  {
    unchecked
    {
      var v = lhs._v - rhs._v;
      if (v >= op.Mod) v += op.Mod; return new StaticModInt<T>(v);
    }
  }
  [MethodImpl(256)]
  public static StaticModInt<T> operator *(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  => new StaticModInt<T>((uint)((ulong)lhs._v * rhs._v % op.Mod));
  /// <summary>
  /// 除算を行います。
  /// </summary>
  /// <remarks>
  /// <para>- 制約: <paramref name="rhs"/> に乗法の逆元が存在する。
  /// （gcd(<paramref name="rhs"/>, mod) = 1）</para>
  /// <para>- 計算量: O(log(mod))</para>
  /// </remarks>
  [MethodImpl(256)]
  public static StaticModInt<T> operator /(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  => lhs * rhs.Inv();
  [MethodImpl(256)]
  public static StaticModInt<T> operator +(StaticModInt<T> v) => v;
  [MethodImpl(256)]
  public static StaticModInt<T> operator -(StaticModInt<T> v)
  => new StaticModInt<T>(v._v == 0 ? 0 : op.Mod - v._v);
  [MethodImpl(256)]
  public static bool operator ==(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  => lhs._v == rhs._v;
  [MethodImpl(256)]
  public static bool operator !=(
    StaticModInt<T> lhs, StaticModInt<T> rhs)
  => lhs._v != rhs._v;
  [MethodImpl(256)]
  public static implicit operator StaticModInt<T>(int v)
  => new StaticModInt<T>(v);
  [MethodImpl(256)]
  public static implicit operator StaticModInt<T>(uint v)
  => new StaticModInt<T>((long)v);
  [MethodImpl(256)]
  public static implicit operator StaticModInt<T>(long v)
  => new StaticModInt<T>(v);
  [MethodImpl(256)]
  public static implicit operator StaticModInt<T>(ulong v)
  => new StaticModInt<T>(v);
  /// <summary>
  /// 自身を x として、x^<paramref name="n"/> を返します。
  /// </summary>
  /// <remarks>
  /// <para>制約: 0≤|<paramref name="n"/>|</para>
  /// <para>計算量: O(log(<paramref name="n"/>))</para>
  /// </remarks>
  [MethodImpl(256)]
  public StaticModInt<T> Pow(long n)
  {
    Contract.Assert(0 <= n, $"{nameof(n)} must be positive.");
    var x = this; var r = new StaticModInt<T>(1U);
    while (n > 0) { if ((n & 1) > 0) r *= x; x *= x; n >>= 1; }
    return r;
  }
  /// <summary>
  /// 自身を x として、 xy≡1 なる y を返します。
  /// </summary>
  /// <remarks>
  /// <para>制約: gcd(x, mod) = 1</para>
  /// </remarks>
  [MethodImpl(256)]
  public StaticModInt<T> Inv()
  {
    if (op.IsPrime)
    {
      Contract.Assert(
        _v > 0, reason: $"{nameof(Value)} must be positive.");
      return Pow(op.Mod - 2);
    }
    else
    {
      var (g, x) = InvGCD(_v, op.Mod);
      Contract.Assert(
        g == 1, reason: $"gcd({nameof(x)}, {nameof(Mod)}) must be 1.");
      return new StaticModInt<T>(x);
    }
  }
  [MethodImpl(256)]
  static (long, long) InvGCD(long a, long b)
  {
    a = SafeMod(a, b); if (a == 0) return (b, 0);
    long s = b, t = a, m0 = 0, m1 = 1, u;
    while (true)
    {
      if (t == 0) { if (m0 < 0) m0 += b / s; return (s, m0); }
      u = s / t; s -= t * u; m0 -= m1 * u;
      if (s == 0) { if (m1 < 0) m1 += b / t; return (t, m1); }
      u = t / s; t -= s * u; m1 -= m0 * u;
    }
  }
  [MethodImpl(256)]
  static long SafeMod(long x, long m)
  { x %= m; if (x < 0) x += m; return x; }
  public override string ToString() => _v.ToString();
  public string ToString(string format, IFormatProvider formatProvider)
  => _v.ToString(format, formatProvider);
  public override bool Equals(object obj)
  => obj is StaticModInt<T> m && Equals(m);
  [MethodImpl(256)]
  public bool Equals(StaticModInt<T> other) => _v == other._v;
  public override int GetHashCode() => _v.GetHashCode();

  static class Contract
  {
    [Conditional("ATCODER_CONTRACT")]
    public static void Assert(bool condition, string reason = null)
    { if (!condition) throw new ContractAssertException(reason); }

    [Conditional("ATCODER_CONTRACT")]
    public static void Assert(
      Func<bool> conditionFunc, string reason = null)
    {
      if (!conditionFunc()) throw new ContractAssertException(reason);
    }
  }
  class ContractAssertException : Exception
  {
    public ContractAssertException() : base() { }
    public ContractAssertException(string message) : base(message) { }
    public ContractAssertException(
      string message, Exception innerException)
    : base(message, innerException) { }
  }
}