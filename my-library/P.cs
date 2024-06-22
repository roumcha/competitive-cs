public readonly record struct P(int X, int Y) : IEquatable<P> {
  [MI(R256)] public static implicit operator P((int X, int Y) t) => new(t.X, t.Y);
  [MI(R256)] public void Deconstruct(out int X, out int Y) { X = this.X; Y = this.Y; }
  public static P Zero { [MI(R256)] get => (0, 0); }
  [MI(R256)] public static P operator +(P a, P b) => (a.X + b.X, a.Y + b.Y);
  [MI(R256)] public static P operator -(P a, P b) => (a.X - b.X, a.Y - b.Y);
  [MI(R256)] public static P operator *(P a, int b) => (a.X * b, a.Y * b);
  [MI(R256)] public static P operator /(P a, int b) => (a.X / b, a.Y / b);
  [MI(R256)] public bool InInterval(P ulIncl, P drExcl) => ulIncl.X <= this.X && this.X < drExcl.X && ulIncl.Y <= this.Y && this.Y < drExcl.Y;
  [MI(R256)] public bool InClosedInterval(P a, P b) => Min(a.X, b.X) <= this.X && this.X <= Max(a.X, b.X) && Min(a.Y, b.Y) <= this.Y && this.Y <= Max(a.Y, b.Y);
  [MI(R256)] public double DistE(P p) { double dx = (double)this.X - p.X, dy = (double)this.Y - p.Y; return Math.Sqrt(dx * dx + dy * dy); }
  [MI(R256)] public long DistE2(P p) { long dx = (long)this.X - p.X, dy = (long)this.Y - p.Y; return dx * dx + dy * dy; }
  [MI(R256)] public long DistM(P p) => Math.Abs((long)this.X - p.X) + Math.Abs((long)this.Y - p.Y);
  [MI(R256)] public override string ToString() => this.X.ToString() + " " + this.Y.ToString();
  [MI(R256)] public string ToString(string pre, string sep, string post) => pre + this.X.ToString() + sep + this.Y.ToString() + post;
  public static readonly ReadOnlyCollection<P> Dir4 = new P[] { (0, 1), (1, 0), (0, -1), (-1, 0), }.AsReadOnly();
  public static readonly ReadOnlyCollection<P> Dir8 = new P[] { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1), }.AsReadOnly();
  [MI(R256)] public static IEnumerable<P> Range(P ulIncl, P drExcl) { for (int i = ulIncl.X; i < drExcl.X; i++) { for (int j = ulIncl.Y; j < drExcl.Y; j++) yield return new(i, j); } }
  [MI(R256)] public static IEnumerable<P> Range(P drExcl) { for (int i = 0; i < drExcl.X; i++) { for (int j = 0; j < drExcl.Y; j++) yield return new(i, j); } }
}
