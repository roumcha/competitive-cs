/// <summary>Numer, Denom &lt;= <c>int.MaxValue</c></summary>
public struct Frac : IComparable, IComparable<Frac>, IEquatable<Frac> {
  public long N, D;
  [MI(R256)] public Frac(long numer, long denom) { N = numer; D = denom; this.Reduce(); }
  [MI(R256)] public static implicit operator Frac(long numer) => new() { N = numer, D = 1 };
  [MI(R256)] public void Reduce() { long g = Gcd(N, D); N /= g; D /= g; if (D < 0) { N = -N; D = -D; } }
  [MI(R256)] public readonly Frac Reciprocal() => new(D, N);
  [MI(R256)] public static Frac operator -(Frac x) => new(-x.N, x.D);
  [MI(R256)] public static Frac operator +(Frac x, Frac y) => new(x.N * y.D + y.N * x.D, x.D * y.D);
  [MI(R256)] public static Frac operator -(Frac x, Frac y) => new(x.N * y.D - y.N * x.D, x.D * y.D);
  [MI(R256)] public static Frac operator *(Frac x, Frac y) => new(x.N * y.N, x.D * y.D);
  [MI(R256)] public static Frac operator /(Frac x, Frac y) => new(x.N * y.D, x.D * y.N);
  [MI(R256)] public static bool operator ==(Frac x, Frac y) => x.N == y.N && x.D == y.D;
  [MI(R256)] public static bool operator !=(Frac x, Frac y) => x.N != y.N || x.D != y.D;
  [MI(R256)] public static bool operator <(Frac x, Frac y) => x.N * y.D < y.N * x.D;
  [MI(R256)] public static bool operator <=(Frac x, Frac y) => x.N * y.D <= y.N * x.D;
  [MI(R256)] public static bool operator >(Frac x, Frac y) => x.N * y.D > y.N * x.D;
  [MI(R256)] public static bool operator >=(Frac x, Frac y) => x.N * y.D >= y.N * x.D;
  [MI(R256)] readonly bool IEquatable<Frac>.Equals(Frac y) => N == y.N && D == y.D;
  [MI(R256)] readonly int IComparable<Frac>.CompareTo(Frac y) => (N * y.D).CompareTo(y.N * D);
  [MI(R256)] public override readonly bool Equals(object o) { var y = (Frac)o; return N == y.N && D == y.D; }
  [MI(R256)] public readonly int CompareTo(object o) { var y = (Frac)o; return (N * y.D).CompareTo(y.N * D); }
  [MI(R256)] public override readonly int GetHashCode() => HashCode.Combine(N, D);
  [MI(R256)] public override readonly string ToString() => $"frn[{N}/{D}]";
}
