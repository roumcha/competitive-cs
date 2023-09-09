/// <summary>Numer, Denom &lt;= <c>int.MaxValue</c></summary>
public struct Frac : IComparable, IComparable<Frac>, IEquatable<Frac> {
  public long N, D;
  [MI(256)] public Frac(long numer, long denom) { N = numer; D = denom; Reduce(); }
  [MI(256)] public static implicit operator Frac(long numer) => new Frac() { N = numer, D = 1 };
  [MI(256)] public void Reduce() { long g = Gcd(N, D); N /= g; D /= g; if (D < 0) { N = -N; D = -D; } }
  [MI(256)] public Frac Reciprocal() => new Frac(D, N);
  [MI(256)] public static Frac operator -(Frac x) => new Frac(-x.N, x.D);
  [MI(256)] public static Frac operator +(Frac x, Frac y) => new Frac(x.N * y.D + y.N * x.D, x.D * y.D);
  [MI(256)] public static Frac operator -(Frac x, Frac y) => new Frac(x.N * y.D - y.N * x.D, x.D * y.D);
  [MI(256)] public static Frac operator *(Frac x, Frac y) => new Frac(x.N * y.N, x.D * y.D);
  [MI(256)] public static Frac operator /(Frac x, Frac y) => new Frac(x.N * y.D, x.D * y.N);
  [MI(256)] public static bool operator ==(Frac x, Frac y) => x.N == y.N && x.D == y.D;
  [MI(256)] public static bool operator !=(Frac x, Frac y) => x.N != y.N || x.D != y.D;
  [MI(256)] public static bool operator <(Frac x, Frac y) => x.N * y.D < y.N * x.D;
  [MI(256)] public static bool operator <=(Frac x, Frac y) => x.N * y.D <= y.N * x.D;
  [MI(256)] public static bool operator >(Frac x, Frac y) => x.N * y.D > y.N * x.D;
  [MI(256)] public static bool operator >=(Frac x, Frac y) => x.N * y.D >= y.N * x.D;
  [MI(256)] bool IEquatable<Frac>.Equals(Frac y) => N == y.N && D == y.D;
  [MI(256)] int IComparable<Frac>.CompareTo(Frac y) => (N * y.D).CompareTo(y.N * D);
  [MI(256)] public override bool Equals(object o) { var y = (Frac)o; return N == y.N && D == y.D; }
  [MI(256)] public int CompareTo(object o) { var y = (Frac)o; return (N * y.D).CompareTo(y.N * D); }
  [MI(256)] public override int GetHashCode() => HashCode.Combine(N, D);
  [MI(256)] public override string ToString() => $"frn[{N}/{D}]";
}
