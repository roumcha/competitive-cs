// using System.Runtime.CompilerServices;

struct P : IEquatable<P> {
  public int X, Y;
  [MethodImpl(256)] public P(int x, int y) { X = x; Y = y; }
  [MethodImpl(256)]
  public static implicit operator P((int X, int Y) t)
  => new P(t.X, t.Y);
  [MethodImpl(256)]
  public static P operator +(P a, P b) => new P(a.X + b.X, a.Y + b.Y);
  [MethodImpl(256)]
  public static P operator -(P a, P b) => new P(a.X - b.X, a.Y - b.Y);
  [MethodImpl(256)]
  public static P operator *(P a, int b) => new P(a.X * b, a.Y * b);
  [MethodImpl(256)]
  public static P operator /(P a, int b) => new P(a.X / b, a.Y / b);
  [MethodImpl(256)]
  public double DistE(P p) {
    double dx = (double)X - p.X, dy = (double)Y - p.Y;
    return Math.Sqrt(dx * dx + dy * dy);
  }
  [MethodImpl(256)]
  public long DistE2(P p) {
    long dx = (long)X - p.X, dy = (long)Y - p.Y;
    return dx * dx + dy * dy;
  }
  [MethodImpl(256)]
  public long DistM(P p)
  => Math.Abs((long)X - p.X) + Math.Abs((long)Y - p.Y);
  [MethodImpl(256)]
  public override string ToString()
  => "(" + X.ToString() + ", " + Y.ToString() + ")";
  [MethodImpl(256)]
  public bool Equals(P b) => this.X == b.X && this.Y == b.Y;
  [MethodImpl(256)]
  public static bool operator ==(P a, P b) => a.Equals(b);
  [MethodImpl(256)]
  public static bool operator !=(P a, P b) => !a.Equals(b);
}
