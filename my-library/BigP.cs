public readonly record struct BigP(bigint X, bigint Y) : IEquatable<BigP> {
  [MI(R256)] public static implicit operator BigP((bigint X, bigint Y) t) => new(t.X, t.Y);
  [MI(R256)] public static BigP operator +(BigP a, BigP b) => new(a.X + b.X, a.Y + b.Y);
  [MI(R256)] public static BigP operator +(BigP a, P b) => new(a.X + b.X, a.Y + b.Y);
  [MI(R256)] public static BigP operator +(P a, BigP b) => new(a.X + b.X, a.Y + b.Y);
  [MI(R256)] public static BigP operator -(BigP a, BigP b) => new(a.X - b.X, a.Y - b.Y);
  [MI(R256)] public static BigP operator -(BigP a, P b) => new(a.X - b.X, a.Y - b.Y);
  [MI(R256)] public static BigP operator -(P a, BigP b) => new(a.X - b.X, a.Y - b.Y);
  [MI(R256)] public static BigP operator *(BigP a, bigint b) => new(a.X * b, a.Y * b);
  [MI(R256)] public static BigP operator /(BigP a, bigint b) => new(a.X / b, a.Y / b);
  [MI(R256)] public readonly bool InClosed(bigint min_x, bigint max_x, bigint min_y, bigint max_y) => min_x <= this.X && this.X <= max_x && min_y <= this.Y && this.Y <= max_y;
  [MI(R256)] public readonly bool InClosed(BigP a, BigP b) => bigint.Min(a.X, b.X) <= this.X && this.X <= bigint.Max(a.X, b.X) && bigint.Min(a.Y, b.Y) <= this.Y && this.Y <= bigint.Max(a.Y, b.Y);
  [MI(R256)] public readonly bool InClosed(P a, P b) => Math.Min(a.X, b.X) <= this.X && this.X <= Math.Max(a.X, b.X) && Math.Min(a.Y, b.Y) <= this.Y && this.Y <= Math.Max(a.Y, b.Y);
  [MI(R256)] public readonly double DistE(BigP p) { double dx = (double)this.X - (double)p.X, dy = (double)this.Y - (double)p.Y; return Math.Sqrt(dx * dx + dy * dy); }
  [MI(R256)] public readonly bigint DistE2(BigP p) { bigint dx = this.X - p.X, dy = this.Y - p.Y; return dx * dx + dy * dy; }
  [MI(R256)] public readonly bigint DistM(BigP p) => bigint.Abs(this.X - p.X) + bigint.Abs(this.Y - p.Y);
  [MI(R256)] public override readonly string ToString() => this.X.ToString() + " " + this.Y.ToString();
  [MI(R256)] public readonly string ToString(string pre, string sep, string post) => pre + this.X.ToString() + sep + this.Y.ToString() + post;
}
